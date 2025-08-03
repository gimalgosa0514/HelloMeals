using mealplan.domain.users.exception;
using mealplan.domain.users.repository;
using mealplan.domain.users.service;
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

        private IUserService userService;
        public LoginForm()
        {
            InitializeComponent();
            userService = new UserServiceImpl(new UserRepositoryImpl());
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
        }

        private void registBtn_Click(object sender, EventArgs e)
        {
            Form registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                userService.Login(idTextBox.Text, passwordTextBox.Text);
                MessageBox.Show(SystemMessage.WELCOME_MESSAGE);
                Form mainForm = new MainForm();
                
                // 임시방편 => 로그인 성공하면 숨기고, 메인 폼 보여줌. 메인폼 닫힌다? 그럼 그냥 폼 보여줌.
                this.Hide();
                mainForm.FormClosed += (s, args) => {
                    idTextBox.Text = idTextBox.Tag.ToString();
                    idTextBox.ForeColor = Color.Gray;
                    passwordTextBox.Text = passwordTextBox.Tag.ToString();
                    passwordTextBox.UseSystemPasswordChar = false;
                    passwordTextBox.ForeColor = Color.Gray;
                    this.Show();
                };
                mainForm.Show();
           
            } catch (UserNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
