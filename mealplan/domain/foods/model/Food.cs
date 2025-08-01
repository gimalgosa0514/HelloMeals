using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.foods.model
{
    public class Food
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public int Kcal {  get; set; }
        public int Carbohydrate {  get; set; }
        public int Protein { get; set; }
        public int Fat {  get; set; }
        public int Sugar { get; set; }
        public int NutrientReferenceValue { get; set; }
        public string NrvType { get; set; }

    }
}
