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

namespace mealplan.custom
{
    
    // 날짜를 누르면 해당 날짜에 먹은 음식들이 TodayPanel 처럼 나오는 패널.
    public partial class UserMealPlanControl : UserControl
    {
        IMealService mealService;
        DateTime selectedDate;
        public UserMealPlanControl()
        {
            InitializeComponent();
            mealService = new MealServiceImpl(new MealRepositoryImpl());
            selectedDate = DateTime.Now;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            selectedDate = e.Start;
            //해당 날짜 바꾸고 불러옴 
            dataView.DataSource = mealService.getMealByDate(Session.LoginedUser.LoginId, selectedDate.ToString("yy'/'MM'/'dd"));
            setNutritionalLabel();
        }

        private void breakfastBtn_Click(object sender, EventArgs e)
        {
            dataView.DataSource = mealService.getMealByDate(Session.LoginedUser.LoginId,"아침",selectedDate.ToString("yy'/'MM'/'dd"));
            setNutritionalLabel();
        }

        private void lunchBtn_Click(object sender, EventArgs e)
        {
            dataView.DataSource = mealService.getMealByDate(Session.LoginedUser.LoginId, "점심", selectedDate.ToString("yy'/'MM'/'dd"));
            setNutritionalLabel();
        }

        private void dinnerBtn_Click(object sender, EventArgs e)
        {
            dataView.DataSource = mealService.getMealByDate(Session.LoginedUser.LoginId, "저녁", selectedDate.ToString("yy'/'MM'/'dd"));
            setNutritionalLabel();
        }

        private void snackBtn_Click(object sender, EventArgs e)
        {
            dataView.DataSource = mealService.getMealByDate(Session.LoginedUser.LoginId, "간식", selectedDate.ToString("yy'/'MM'/'dd"));
            setNutritionalLabel();
        }

        private void allBtn_Click(object sender, EventArgs e)
        {
            dataView.DataSource = mealService.getMealByDate(Session.LoginedUser.LoginId, selectedDate.ToString("yy'/'MM'/'dd"));
            setNutritionalLabel();
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
    }
}
