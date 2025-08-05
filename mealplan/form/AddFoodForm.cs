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
            double _kcal = double.Parse(kcal.Text);
            double _carbo = double.Parse(carbo.Text);
            double _protein = double.Parse(protein.Text);
            double _fat = double.Parse(fat.Text);
            int _nrv = int.Parse(nrv.Text);
            string _nrvType = nrvType.Text;

            Food food = new Food(_foodName, _kcal, _carbo, _protein, _fat, _nrv, _nrvType);
            if(foodService.saveFood(food))
            {
                MessageBox.Show("등록되었습니다.");
                this.Close();
            } else
            {
                MessageBox.Show("등록 실패 -> 다시 입력해주세요.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
