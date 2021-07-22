using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Petshop_Atlantico.Utils
{
    public static class Utils
    {
        public static string GetEnumDisplayAttribute(Enum item)
        {
            return item.GetType().GetMember(item.ToString()).First().GetCustomAttribute<DisplayAttribute>().Name.ToString();
        }

        public static string FilterPhoneNumber(string number)
        {
            try
            {
                return Regex.Replace(number, @"[^0-9]+", "");
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
