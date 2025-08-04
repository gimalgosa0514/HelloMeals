using mealplan.domain.foods.model;
using mealplan.domain.foods.repository;
using mealplan.domain.foods.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mealplan.form
{
    public partial class AddFoodForm : Form
    {
        IFoodService foodService;
        public AddFoodForm()
        {
            InitializeComponent();
            foodService = new FoodServiceImpl(new FoodRepositoryImpl());
        }

        
        // 음식 등록하는 부분 구현해야함 ㅎ
        private void button1_Click(object sender, EventArgs e)
        {
            string _foodName = foodName.Text;
            int _kcal = int.Parse(kcal.Text);
            int _carbo = int.Parse(carbo.Text);
            int _protein = int.Parse(protein.Text);
            int _fat = int.Parse(fat.Text);
            int _nrv = int.Parse(nrv.Text);
            string _nrvType = nrvType.Text;

            Food food = new Food(_foodName, _kcal, _carbo, _protein, _fat, _nrv, _nrvType);
            foodService.saveFood(food);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
