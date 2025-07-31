using mealplan.form;
using mealplan.util;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mealplan
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        // 로그인 폼 진입 시 
        private void loginForm_Enter(object sender, EventArgs e)
        {
            TextBox loginForm = sender as TextBox;

            // 로그인 폼에 아이디 또는 패스워드라고 적혀있다? 그럼 지워야지
            if(loginForm.Text.Equals(loginForm.Tag.ToString()))
            {
                loginForm.Text = "";
                loginForm.ForeColor = Color.Black;
                    
                //만약 비밀번호 텍스트 박스라면 마스킹 처리
                if(loginForm.Tag.ToString().Equals("비밀번호"))
                {
                    loginForm.UseSystemPasswordChar = true;
                }



            }
        }

        private void loginForm_Leave(object sender, EventArgs e)
        {
            TextBox loginForm = sender as TextBox;

            if(loginForm.Text.Equals(""))
            {
                loginForm.Text = loginForm.Tag.ToString();
                loginForm.ForeColor = Color.Gray;

                // 비밀번호 텍박이면 마스킹 해제 
                if (loginForm.Tag.ToString().Equals("비밀번호"))
                {
                    loginForm.UseSystemPasswordChar = false;
                }
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            OracleUtil.ConnectDB();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void registBtn_Click(object sender, EventArgs e)
        {
            Form registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }
    }
}
