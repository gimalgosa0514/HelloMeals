using mealplan.domain.users.repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using mealplan.domain.users.model;
using mealplan.domain.users.service;
using mealplan.domain.users.exception;
using mealplan.util;
namespace mealplan.form
{
    public partial class RegisterForm : Form
    {
        
        IUserService userService;
        public RegisterForm()
        {
            InitializeComponent();
            userService = new UserServiceImpl(new UserRepositoryImpl());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 일단 인풋 다 받아줌.
            string _inputName = inputName.Text;
            string _inputId = inputId.Text;
            string _inputPassword = inputPassword.Text;
            string _gender = genderMale.Checked ? "남자" : "여자";
            string _birthDate = inputBirthdate.Text;
            string _height = inputHeight.Text;
            string _weight = inputWeight.Text;

            // 입력들이 모두 유효한지? 체크.
            bool isValid = true;
            foreach(TextBox tb in panel1.Controls.OfType<TextBox>())
            {
                isValid = inputValidation(tb);
                // 유효하지 않다? 그럼 끝냄.
                if (!isValid)
                {
                    return;
                }
            }



            try
            {
                if (userService.Regist(new User(_inputId, _inputPassword, _inputName, _gender, _birthDate, _height, _weight)))
                {
                    this.Close();
                }
            }
            catch (UserNotFoundException ex) {
                MessageBox.Show(ex.Message);
            }

            }

        
        private bool inputValidation(TextBox tb)
        {

            string inputType = "";
            switch (tb.Name)
            {
                case "inputName": inputType = "이름"; break;
                case "inputId": inputType = "아이디"; break;
                case "inputPassword": inputType = "비밀번호"; break;
                case "inputBirthdate": inputType = "생년월일"; break;
                case "inputHeight": inputType = "키"; break;
                case "inputWeight": inputType = "몸무게"; break;
            }
            if(tb.Text.Equals(tb.Tag.ToString())) {
                MessageBox.Show(inputType + SystemMessage.INPUT_CHECK_MESSAGE);
                return false;
            }
            return true;
            
        }
        

        
    }
}
