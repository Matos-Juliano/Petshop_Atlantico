using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Petshop_Atlantico.Enums;
using Petshop_Atlantico.Models;
using Petshop_Atlantico.Models.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Data
{
    public class AnimalRepository : IAnimalRepo
    {
        private readonly PetshopContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILodgingRepo _lodgingRepo;

        public AnimalRepository(PetshopContext petshopContext, IWebHostEnvironment hostEnvironment, ILodgingRepo lodgingRepo)
        {
            _context = petshopContext;
            _hostEnvironment = hostEnvironment;
            _lodgingRepo = lodgingRepo;
        }
       
        public AnimalViewModel GetAnimalById(int id)
        {
            try
            {

                var query = from animals in _context.Animals
                            join owner in _context.Owners on animals.Owner.Id equals owner.Id into o
                            from owner in o.DefaultIfEmpty()
                            join lodging in _context.Lodgings on animals.Id equals lodging.Occupant.Id into l
                            from lodging in l.DefaultIfEmpty()
                            select new AnimalViewModel
                            {
                                Id = animals.Id,
                                Name = animals.Name,
                                HospitalizationReason = animals.HospitalizationReason,
                                HealthStatus = animals.HealthStatus,
                                Picture = animals.Picture,
                                Owner = new Owner
                                {
                                    Id = owner.Id,
                                    Name = owner.Name,
                                    PhoneNumber = owner.PhoneNumber
                                },
                                Lodge = lodging
                            };

                return query.Single(p => p.Id == id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<AnimalListModel> GetList(string name, string ownerName, HealthStatus? healthStatus, int pageSize, int pageIndex)
        {
            try
            {
                if (pageIndex == 0)
                    pageIndex = 1;

                if (pageSize == 0)
                    pageSize = 10;

                int skip = (pageIndex - 1) * pageSize;

                var query = from animals in _context.Animals
                       join owner in _context.Owners on animals.Owner.Id equals owner.Id into o
                       from owner in o.DefaultIfEmpty()
                       join lodging in _context.Lodgings on animals.Id equals lodging.Occupant.Id into l
                       from lodging in l.DefaultIfEmpty()
                       select new AnimalListModel
                       {
                           Id = animals.Id,
                           Name = animals.Name,
                           HealthStatus = animals.HealthStatus,
                           OwnerName = owner.Name,
                           Lodging = lodging.Id
                       };

                if (!String.IsNullOrEmpty(name))
                    query = query.Where(p => p.Name == name);

                if (!String.IsNullOrEmpty(ownerName))
                    query = query.Where(p => p.OwnerName == ownerName);

                if (healthStatus != null)
                    query = query.Where(p => p.HealthStatus == healthStatus);

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(AnimalDTO animal)
        {
            var transaction = _context.Database.BeginTransaction();
            string profilePic = null;
            try
            {
                Owner owner = new Owner
                {
                    Name = animal.Owner.Name,
                    PhoneNumber = animal.Owner.PhoneNumber
                }; 

                _context.Owners.Add(owner);
                _context.SaveChanges();

                if (animal.PictureFile != null)
                    profilePic = UploadFile(animal.PictureFile);

                Animal newAnimal = new Animal()
                {
                    Name = animal.Name,
                    Owner = owner,
                    HospitalizationReason = animal.HospitalizationReason,
                    HealthStatus = (HealthStatus)animal.HealthStatus,
                    Picture = profilePic
                };

                _context.Animals.Add(newAnimal);
                _context.SaveChanges();

                Lodging lodging = _lodgingRepo.GetLodgingById(animal.Lodge);

                if (lodging == null)
                    throw new Exception("Local não encontrado.");

                if (lodging.OccupationStatus != OccupationStatus.Free)
                    throw new Exception("Não é possivel colocar o Pet em um local já ocupado.");

                lodging.OccupationStatus = OccupationStatus.Occupied;
                lodging.Occupant = newAnimal;

                _context.Lodgings.Update(lodging);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch(Exception ex)
            {
                DeleteFile(profilePic);
                transaction.Rollback();
                throw ex;
            }
        }

        public void Update(AnimalDTO animal)
        {
            var transaction = _context.Database.BeginTransaction();
            string profilePic = null;
            try
            {
                Owner owner = new Owner
                {
                    Id = (int)animal.Owner.Id,
                    Name = animal.Owner.Name,
                    PhoneNumber = animal.Owner.PhoneNumber
                };
                _context.Owners.Update(owner);
                _context.SaveChanges();

                Animal updatedAnimal = new Animal()
                {
                    Id = (int)animal.Id,
                    Name = animal.Name,
                    HospitalizationReason = animal.HospitalizationReason,
                    HealthStatus = (HealthStatus)animal.HealthStatus
                };

                if (animal.PictureFile != null)
                {
                    profilePic = UploadFile(animal.PictureFile);
                    updatedAnimal.Picture = profilePic;
                }

                _context.Animals.Update(updatedAnimal);
                _context.SaveChanges();

                Lodging lodging = _lodgingRepo.GetLodgingByOccupant(updatedAnimal.Id);

                if (lodging == null)
                    lodging = new Lodging
                    {
                        Id = animal.Lodge,
                        OccupationStatus = OccupationStatus.Occupied,
                        Occupant = updatedAnimal
                    };

                if (lodging.Id != animal.Lodge)
                {
                    lodging.Occupant = null;
                    lodging.OccupationStatus = OccupationStatus.Free;

                    _context.Lodgings.Update(lodging);
                    _context.SaveChanges();

                    lodging = _lodgingRepo.GetLodgingById(animal.Lodge);

                    if (lodging == null)
                        throw new Exception("Local não encontrado.");

                    if (lodging.OccupationStatus != OccupationStatus.Free)
                        throw new Exception("Não é possivel colocar o Pet em um local já ocupado.");

                    lodging.Occupant = updatedAnimal;
                    lodging.OccupationStatus = OccupationStatus.Occupied;
                }

                if(updatedAnimal.HealthStatus == HealthStatus.Recovered)
                    lodging.OccupationStatus = OccupationStatus.WaitingForOwner;

                _context.Lodgings.Update(lodging);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                DeleteFile(profilePic);
                throw ex;
            }
        }

        private string UploadFile(IFormFile file)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        private void DeleteFile(string file)
        {
            if (file == null)
                return;

            try
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, file);
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
