using mealplan.domain.users.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.meals.model
{
    internal class Meal
    {

        public long MealId { get; set; }
        public string LoginId { get; set; }
        public string MenuId { get; set; }
        public string MealType { get; set; }
        public DateTime CreatedAt{ get; set; }
       
        //public string Image {  get; set; } 얘는 상황보고..
    }
}
