using mealplan.custom;
using mealplan.domain.users.repository;
using mealplan.domain.users.service;
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
    public partial class MainForm : Form
    {

        private IUserService userService;
        public MainForm()
        {
            InitializeComponent();
            userService = new UserServiceImpl(new UserRepositoryImpl());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            welcomLabel.Text = Session.LoginedUser.Name+"님 환영합니다!";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            userService.Logout(Session.LoginedUser.LoginId);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            LoadCustomControl(new TodayPanelControl());
        }


        // MainForm에서 중앙에 패널만 바꾸려고 만든거임.
        private void LoadCustomControl(UserControl uc)
        {
            // 일단 메인 패널에 올라와있는거 싹 지우고
            mainpPanel.Controls.Clear();

            // 그다음 유저 컨트롤러 파라미터로 받은거 메인 패널 크기에 맞춰준다음
            uc.Dock= DockStyle.Fill;
            // 추가.
            mainpPanel.Controls.Add(uc);
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadCustomControl(new UserMealPlanControl());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadCustomControl(new FoodSearchControl());
        }
    }
}
