using mealplan.domain.foods.repository;
using mealplan.domain.foods.service;
using mealplan.form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mealplan.custom
{


    // 음식을 검색하면 음식에 정보를 테이블 형태로 보여주는 패널.
    public partial class FoodSearchControl : UserControl
    {
        private IFoodService foodService;
        public FoodSearchControl()
        {
            InitializeComponent();
            foodService = new FoodServiceImpl(new FoodRepositoryImpl());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            view.DataSource = foodService.getAllFood();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            view.DataSource = foodService.searchFoodByName(keyword.Text.ToString());
        }

        private void addMealButton_Click(object sender, EventArgs e)
        {
            Form addFoodForm = new AddFoodForm();

            addFoodForm.ShowDialog();
        }
    }
}
