# Petshop_Atlantico

O DB utilizado no projeto é o sql server implementado usando entity framework.

Para configurar o DB insira a connection string de acesso ao sql server no arquivo appsettings.Json.

Template:

  "ConnectionStrings": {
    "PetshopConnection": "Server=Servidor;Initial Catalog=PetshopDb;User Id=usuario;Password=password"
  }

abra um terminal na pasta do projeto e cole ou digite "dotnet ef database update"


se você não possui o entity framework tools instalado você pode usar o comando abaixo: 

	dotnet tool install --global dotnet-ef

Uma vez o entity framework tools instalado, rode novamente o comando: dotnet ef database update

Uma vez com a base de dados criada, você deve inserir o número de alojamentos desejados ao seu petshop.

Como você não pode criar novos alojamentos por dentro do app você deve inseri-los diretamente na Base de Dados.

Template para a criação de 10 alojamentos: 


{
	INSERT INTO PetshopDb..Lodgings (OccupationStatus, OccupantId) VALUES (0, NULL)
	INSERT INTO PetshopDb..Lodgings (OccupationStatus, OccupantId) VALUES (0, NULL)
	INSERT INTO PetshopDb..Lodgings (OccupationStatus, OccupantId) VALUES (0, NULL)
	INSERT INTO PetshopDb..Lodgings (OccupationStatus, OccupantId) VALUES (0, NULL)
	INSERT INTO PetshopDb..Lodgings (OccupationStatus, OccupantId) VALUES (0, NULL)
	INSERT INTO PetshopDb..Lodgings (OccupationStatus, OccupantId) VALUES (0, NULL)
	INSERT INTO PetshopDb..Lodgings (OccupationStatus, OccupantId) VALUES (0, NULL)
	INSERT INTO PetshopDb..Lodgings (OccupationStatus, OccupantId) VALUES (0, NULL)
	INSERT INTO PetshopDb..Lodgings (OccupationStatus, OccupantId) VALUES (0, NULL)
	INSERT INTO PetshopDb..Lodgings (OccupationStatus, OccupantId) VALUES (0, NULL)
}

Todos estes passos feitos basta apenas executar a aplicação.
