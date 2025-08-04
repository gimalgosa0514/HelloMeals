using mealplan.custom;
using mealplan.domain.foods.repository;
using mealplan.domain.foods.service;
using mealplan.domain.meals.model;
using mealplan.domain.meals.repository;
using mealplan.domain.meals.service;
using mealplan.util;
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
    public partial class AddMealFoodForm : Form
    {
        private IFoodService foodService;

        private IMealService mealService;

        private string selectedFoodId;

        UserControl parent;

        public AddMealFoodForm(UserControl parent)
        {
            InitializeComponent();
            foodService = new FoodServiceImpl(new FoodRepositoryImpl());
            mealService = new MealServiceImpl(new MealRepositoryImpl());
            selectedFoodId = null;
            this.parent = parent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = foodService.searchFoodByName(keywordTextBox.Text);
            dataGridView1.Columns["FoodId"].Visible = false;
            dataGridView1.Columns["NutrientReferenceValue"].Visible = false;
            dataGridView1.Columns["NrvType"].Visible = false;
            dataGridView1.Columns["Name"].HeaderText = "음식 이름";
            dataGridView1.Columns["Kcal"].HeaderText = "칼로리";
            dataGridView1.Columns["Carbohydrate"].HeaderText = "탄수화물";
            dataGridView1.Columns["Protein"].HeaderText = "단백질";
            dataGridView1.Columns["Fat"].HeaderText = "지방";
        }


        // 셀 클릭하면 넣을 음식 선택한거라고 간주, nrv_type 라벨 변경해주고, 바로 넣을 수 있게 아이디 저장해놓음.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedFoodId = row.Cells["FoodId"].Value.ToString();
                nrvTypeLabel.Text = row.Cells["NrvType"].Value.ToString();

            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int _amount = int.Parse(amount.Text);
            string _mealType = mealType.Text;
            MealFood mealFood = new MealFood(_amount, _mealType);

            if(selectedFoodId != null)
            {
                if(mealService.saveMeal(Session.LoginedUser.LoginId, int.Parse(selectedFoodId),mealFood))
                {
                    MessageBox.Show("등록 완료!");
                    if(parent is TodayPanelControl panel)
                    {
                        panel.reloadMeals(_mealType);
                    }
                    this.Close();
                } else
                {
                    MessageBox.Show("등록 실패! 다시 시도해주십시오.");
                }
                
            } else
            {
                MessageBox.Show("음식을 선택하세요");
            }
  
        }
    }
}
