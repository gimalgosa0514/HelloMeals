using Oracle.OciServicesSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.users.model
{
    public class User
    {

        
        public User(string loginId, string password, string name, string gender, string birthdate, string height, string weight)
        {
            this.loginId = loginId;
            Password = password;
            Name = name;
            Gender = gender;
            Birthdate = birthdate;
            Height = height;
            Weight = weight;
        }

        public string loginId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Birthdate { get; set; } 
        public string Height { get; set; }
        public string Weight { get; set; }


        


    }
}
