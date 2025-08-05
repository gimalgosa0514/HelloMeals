using mealplan.domain.meals.repository;
using mealplan.domain.meals.service;
using mealplan.form;
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

namespace mealplan.custom
{
    // 금일 먹은 음식에 대해 아침, 점심, 저녁, 간식에 따라 쿼리를 쏴 데이터 뷰어를 통해 보여주고,
    // 영양섭취량이랑 칼로리 계산해서 보여주는 패널
    // plus 버튼을 누르면 금일 식단에 먹은 음식을 추가할 수 있음.
    // TODO : 식단에 음식 추가하는 폼 만들고, 이제 비즈니스 로직 짜면 됨.
    public partial class TodayPanelControl : UserControl
    {
        IMealService mealService;
        string selectedMealFoodCodeName;
        string selectedMealType;
        public TodayPanelControl()
        {
            InitializeComponent();
            mealService = new MealServiceImpl(new MealRepositoryImpl());
            selectedMealFoodCodeName = null;
            selectedMealType = null;
        }

        private void TodayPanelControl_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void breakfast_Click(object sender, EventArgs e)
        {
            selectedMealType = "아침";
            dataView.DataSource = mealService.getTodayMeal(Session.LoginedUser.LoginId, "아침");
            setNutritionalLabel();
            // 왜 들어가지는데 출력이 안될까?? 일단 이거 해결해야함. -> 리스트에 들어가는 클래스 속성 중 private은 안나옴;;
        }

        private void lunch_Click(object sender, EventArgs e)
        {
            selectedMealType = "점심";
            dataView.DataSource = mealService.getTodayMeal(Session.LoginedUser.LoginId, "점심");
            setNutritionalLabel();
        }

        private void dinner_Click(object sender, EventArgs e)
        {
            selectedMealType = "저녁";
            dataView.DataSource = mealService.getTodayMeal(Session.LoginedUser.LoginId, "저녁");
            setNutritionalLabel();
        }

        private void snack_Click(object sender, EventArgs e)
        {
            selectedMealType = "간식";
            dataView.DataSource = mealService.getTodayMeal(Session.LoginedUser.LoginId, "간식");
            setNutritionalLabel();
        }

        private void addMealButton_Click(object sender, EventArgs e)
        {
            Form addMealFoodForm = new AddMealFoodForm(this);
            addMealFoodForm.ShowDialog();


        }

        public void reloadMeals(string mealType)
        {
            switch (mealType)
            {
                case "아침": breakfast_Click(null, null); break;
                case "점심": lunch_Click(null, null); break;
                case "저녁": dinner_Click(null, null); break;
                case "간식": snack_Click(null, null); break;
            }
        }

        public string sumNutritional(string nType)
        {
            double sum = 0;
            foreach (DataGridViewRow row in dataView.Rows)
            {

                // winform의 DataGridView에서 마지막에 한 행을 추가한다네, 그래서 이거 막아줘야 null이 안남.
                if (row.IsNewRow) continue;
                sum += double.Parse(row.Cells[nType].Value.ToString());
            }
            return sum.ToString();
        }

        public void setNutritionalLabel()
        {
            proteinLabel.Text = sumNutritional("Protein");
            kcalLabel.Text = sumNutritional("Kcal");
            hydroLabel.Text = sumNutritional("CarboHydrate");
            fatLabel.Text = sumNutritional("Fat");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(selectedMealFoodCodeName != null)
            {
                if (mealService.removeMealFood(int.Parse(selectedMealFoodCodeName)))
                {
                    MessageBox.Show("삭제되었습니다.");
                    reloadMeals(selectedMealType);
                }
            }
            else
            {
                MessageBox.Show("삭제할 음식을 선택하십시오.");
            }
                
            
        }

        private void dataView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataView.Rows[e.RowIndex];
                selectedMealFoodCodeName = row.Cells["MealFoodCodeName"].Value.ToString();
            }
        }

        private void selectAllBtn_Click(object sender, EventArgs e)
        {
            dataView.DataSource = mealService.getTodayMeal(Session.LoginedUser.LoginId);
            setNutritionalLabel();
        }
    }
}
