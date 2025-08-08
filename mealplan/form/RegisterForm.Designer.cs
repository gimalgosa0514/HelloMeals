namespace mealplan.form
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.genderFemale = new System.Windows.Forms.RadioButton();
            this.genderMale = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.inputWeight = new mealplan.custom.PlaceholderTextBox();
            this.inputHeight = new mealplan.custom.PlaceholderTextBox();
            this.inputBirthdate = new mealplan.custom.PlaceholderTextBox();
            this.inputPassword = new mealplan.custom.PlaceholderTextBox();
            this.inputId = new mealplan.custom.PlaceholderTextBox();
            this.inputName = new mealplan.custom.PlaceholderTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SandyBrown;
            this.panel1.Controls.Add(this.inputWeight);
            this.panel1.Controls.Add(this.inputHeight);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.inputBirthdate);
            this.panel1.Controls.Add(this.inputPassword);
            this.panel1.Controls.Add(this.inputId);
            this.panel1.Controls.Add(this.inputName);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.genderFemale);
            this.panel1.Controls.Add(this.genderMale);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(12, 13);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 583);
            this.panel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Noto Sans KR", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(52, 514);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(205, 35);
            this.button2.TabIndex = 17;
            this.button2.Text = "취소하기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // genderFemale
            // 
            this.genderFemale.AutoSize = true;
            this.genderFemale.ForeColor = System.Drawing.Color.White;
            this.genderFemale.Location = new System.Drawing.Point(103, 330);
            this.genderFemale.Name = "genderFemale";
            this.genderFemale.Size = new System.Drawing.Size(48, 21);
            this.genderFemale.TabIndex = 16;
            this.genderFemale.Text = "여성";
            this.genderFemale.UseVisualStyleBackColor = true;
            // 
            // genderMale
            // 
            this.genderMale.AutoSize = true;
            this.genderMale.Checked = true;
            this.genderMale.ForeColor = System.Drawing.Color.White;
            this.genderMale.Location = new System.Drawing.Point(52, 330);
            this.genderMale.Name = "genderMale";
            this.genderMale.Size = new System.Drawing.Size(48, 21);
            this.genderMale.TabIndex = 15;
            this.genderMale.TabStop = true;
            this.genderMale.Text = "남성";
            this.genderMale.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Noto Sans KR", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(52, 473);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 35);
            this.button1.TabIndex = 14;
            this.button1.Text = "가입하기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Noto Sans KR", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(48, 307);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 19);
            this.label7.TabIndex = 12;
            this.label7.Text = "성별";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Noto Sans KR", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(48, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 19);
            this.label6.TabIndex = 9;
            this.label6.Text = "생년월일";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Noto Sans KR", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(48, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 19);
            this.label5.TabIndex = 7;
            this.label5.Text = "아이디";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Noto Sans KR", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(48, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "비밀번호";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Noto Sans KR", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(48, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "이름";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Noto Sans KR", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(84, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "회원가입";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Noto Sans KR", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(48, 411);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 19);
            this.label3.TabIndex = 24;
            this.label3.Text = "몸무게";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Noto Sans KR", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(48, 352);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 19);
            this.label8.TabIndex = 23;
            this.label8.Text = "키";
            // 
            // inputWeight
            // 
            this.inputWeight.ForeColor = System.Drawing.Color.Gray;
            this.inputWeight.Location = new System.Drawing.Point(52, 433);
            this.inputWeight.Name = "inputWeight";
            this.inputWeight.Size = new System.Drawing.Size(205, 25);
            this.inputWeight.TabIndex = 26;
            this.inputWeight.Tag = "예시)80";
            this.inputWeight.Text = "예시)80";
            // 
            // inputHeight
            // 
            this.inputHeight.ForeColor = System.Drawing.Color.Gray;
            this.inputHeight.Location = new System.Drawing.Point(52, 374);
            this.inputHeight.Name = "inputHeight";
            this.inputHeight.Size = new System.Drawing.Size(205, 25);
            this.inputHeight.TabIndex = 25;
            this.inputHeight.Tag = "예시)177.7";
            this.inputHeight.Text = "예시)177.7";
            // 
            // inputBirthdate
            // 
            this.inputBirthdate.ForeColor = System.Drawing.Color.Gray;
            this.inputBirthdate.Location = new System.Drawing.Point(52, 270);
            this.inputBirthdate.Name = "inputBirthdate";
            this.inputBirthdate.Size = new System.Drawing.Size(205, 25);
            this.inputBirthdate.TabIndex = 22;
            this.inputBirthdate.Tag = "예시)990514";
            this.inputBirthdate.Text = "예시)990514";
            // 
            // inputPassword
            // 
            this.inputPassword.ForeColor = System.Drawing.Color.Gray;
            this.inputPassword.Location = new System.Drawing.Point(52, 211);
            this.inputPassword.Name = "inputPassword";
            this.inputPassword.Size = new System.Drawing.Size(205, 25);
            this.inputPassword.TabIndex = 20;
            this.inputPassword.Tag = "비밀번호";
            this.inputPassword.Text = "비밀번호";
            // 
            // inputId
            // 
            this.inputId.ForeColor = System.Drawing.Color.Gray;
            this.inputId.Location = new System.Drawing.Point(52, 157);
            this.inputId.Name = "inputId";
            this.inputId.Size = new System.Drawing.Size(205, 25);
            this.inputId.TabIndex = 19;
            this.inputId.Tag = "예시)test1234";
            this.inputId.Text = "예시)test1234";
            // 
            // inputName
            // 
            this.inputName.ForeColor = System.Drawing.Color.Gray;
            this.inputName.Location = new System.Drawing.Point(52, 101);
            this.inputName.Name = "inputName";
            this.inputName.Size = new System.Drawing.Size(205, 25);
            this.inputName.TabIndex = 18;
            this.inputName.Tag = "예시)김희망";
            this.inputName.Text = "예시)김희망";
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 608);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RegisterForm";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton genderFemale;
        private System.Windows.Forms.RadioButton genderMale;
        private System.Windows.Forms.Button button1;
        private custom.PlaceholderTextBox inputBirthdate;
        private custom.PlaceholderTextBox inputPassword;
        private custom.PlaceholderTextBox inputId;
        private custom.PlaceholderTextBox inputName;
        private custom.PlaceholderTextBox inputWeight;
        private custom.PlaceholderTextBox inputHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
    }
}