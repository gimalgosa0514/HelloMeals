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
            
            MessageBox.Show(nrvType.Text);
        }
    }
}
