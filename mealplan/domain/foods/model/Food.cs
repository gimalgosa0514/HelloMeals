using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.foods.model
{
    public class Food
    {

        public Food(int foodId, string name, int kcal, int carbohydrate, int protein, int fat, int nutrientReferenceValue, string nrvType)
        {
            FoodId = foodId;
            Name = name;
            Kcal = kcal;
            Carbohydrate = carbohydrate;
            Protein = protein;
            Fat = fat;
            NutrientReferenceValue = nutrientReferenceValue;
            NrvType = nrvType;
        }
        public Food(string name, int kcal, int carbohydrate, int protein, int fat, int nutrientReferenceValue, string nrvType)
        {
            Name = name;
            Kcal = kcal;
            Carbohydrate = carbohydrate;
            Protein = protein;
            Fat = fat;
            NutrientReferenceValue = nutrientReferenceValue;
            NrvType = nrvType;
        }


        public int FoodId { get; set; }//CodeName
        public string Name { get; set; }
        public int Kcal {  get; set; }
        public int Carbohydrate {  get; set; }
        public int Protein { get; set; }
        public int Fat {  get; set; }
        public int NutrientReferenceValue { get; set; }
        public string NrvType { get; set; }

    }
}
