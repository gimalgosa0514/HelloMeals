namespace mealplan.custom
{
    partial class UserMealPlanControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.kcalLabel = new System.Windows.Forms.Label();
            this.hydroLabel = new System.Windows.Forms.Label();
            this.proteinLabel = new System.Windows.Forms.Label();
            this.fatLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.oracleCommand1 = new Oracle.ManagedDataAccess.Client.OracleCommand();
            this.dataView = new System.Windows.Forms.DataGridView();
            this.lunchBtn = new System.Windows.Forms.Button();
            this.breakfastBtn = new System.Windows.Forms.Button();
            this.dinnerBtn = new System.Windows.Forms.Button();
            this.snackBtn = new System.Windows.Forms.Button();
            this.allBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.CalendarDimensions = new System.Drawing.Size(1, 2);
            this.monthCalendar1.Location = new System.Drawing.Point(62, 77);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.monthCalendar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 457);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.allBtn);
            this.panel2.Controls.Add(this.snackBtn);
            this.panel2.Controls.Add(this.dinnerBtn);
            this.panel2.Controls.Add(this.breakfastBtn);
            this.panel2.Controls.Add(this.lunchBtn);
            this.panel2.Controls.Add(this.dataView);
            this.panel2.Controls.Add(this.kcalLabel);
            this.panel2.Controls.Add(this.hydroLabel);
            this.panel2.Controls.Add(this.proteinLabel);
            this.panel2.Controls.Add(this.fatLabel);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(354, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(620, 457);
            this.panel2.TabIndex = 2;
            // 
            // kcalLabel
            // 
            this.kcalLabel.AutoSize = true;
            this.kcalLabel.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.kcalLabel.Location = new System.Drawing.Point(537, 38);
            this.kcalLabel.Name = "kcalLabel";
            this.kcalLabel.Size = new System.Drawing.Size(15, 17);
            this.kcalLabel.TabIndex = 15;
            this.kcalLabel.Text = "0";
            // 
            // hydroLabel
            // 
            this.hydroLabel.AutoSize = true;
            this.hydroLabel.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.hydroLabel.Location = new System.Drawing.Point(450, 38);
            this.hydroLabel.Name = "hydroLabel";
            this.hydroLabel.Size = new System.Drawing.Size(15, 17);
            this.hydroLabel.TabIndex = 14;
            this.hydroLabel.Text = "0";
            // 
            // proteinLabel
            // 
            this.proteinLabel.AutoSize = true;
            this.proteinLabel.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.proteinLabel.Location = new System.Drawing.Point(378, 38);
            this.proteinLabel.Name = "proteinLabel";
            this.proteinLabel.Size = new System.Drawing.Size(15, 17);
            this.proteinLabel.TabIndex = 13;
            this.proteinLabel.Text = "0";
            // 
            // fatLabel
            // 
            this.fatLabel.AutoSize = true;
            this.fatLabel.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.fatLabel.Location = new System.Drawing.Point(321, 38);
            this.fatLabel.Name = "fatLabel";
            this.fatLabel.Size = new System.Drawing.Size(15, 17);
            this.fatLabel.TabIndex = 12;
            this.fatLabel.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(531, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 21);
            this.label8.TabIndex = 11;
            this.label8.Text = "칼로리";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(444, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 21);
            this.label7.TabIndex = 10;
            this.label7.Text = "탄수화물";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(372, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 21);
            this.label6.TabIndex = 9;
            this.label6.Text = "단백질";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(315, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "지방";
            // 
            // dataView
            // 
            this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataView.Location = new System.Drawing.Point(35, 135);
            this.dataView.Name = "dataView";
            this.dataView.RowTemplate.Height = 23;
            this.dataView.Size = new System.Drawing.Size(550, 249);
            this.dataView.TabIndex = 16;
            // 
            // lunchBtn
            // 
            this.lunchBtn.BackgroundImage = global::mealplan.Properties.Resources.contrast;
            this.lunchBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.lunchBtn.Location = new System.Drawing.Point(76, 96);
            this.lunchBtn.Name = "lunchBtn";
            this.lunchBtn.Size = new System.Drawing.Size(36, 33);
            this.lunchBtn.TabIndex = 17;
            this.lunchBtn.UseVisualStyleBackColor = true;
            this.lunchBtn.Click += new System.EventHandler(this.lunchBtn_Click);
            // 
            // breakfastBtn
            // 
            this.breakfastBtn.BackgroundImage = global::mealplan.Properties.Resources.sun;
            this.breakfastBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.breakfastBtn.Location = new System.Drawing.Point(34, 96);
            this.breakfastBtn.Name = "breakfastBtn";
            this.breakfastBtn.Size = new System.Drawing.Size(36, 33);
            this.breakfastBtn.TabIndex = 21;
            this.breakfastBtn.UseVisualStyleBackColor = true;
            this.breakfastBtn.Click += new System.EventHandler(this.breakfastBtn_Click);
            // 
            // dinnerBtn
            // 
            this.dinnerBtn.BackgroundImage = global::mealplan.Properties.Resources.sunset;
            this.dinnerBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.dinnerBtn.Location = new System.Drawing.Point(118, 96);
            this.dinnerBtn.Name = "dinnerBtn";
            this.dinnerBtn.Size = new System.Drawing.Size(36, 33);
            this.dinnerBtn.TabIndex = 22;
            this.dinnerBtn.UseVisualStyleBackColor = true;
            this.dinnerBtn.Click += new System.EventHandler(this.dinnerBtn_Click);
            // 
            // snackBtn
            // 
            this.snackBtn.BackgroundImage = global::mealplan.Properties.Resources.moon;
            this.snackBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.snackBtn.Location = new System.Drawing.Point(160, 96);
            this.snackBtn.Name = "snackBtn";
            this.snackBtn.Size = new System.Drawing.Size(36, 33);
            this.snackBtn.TabIndex = 23;
            this.snackBtn.UseVisualStyleBackColor = true;
            this.snackBtn.Click += new System.EventHandler(this.snackBtn_Click);
            // 
            // allBtn
            // 
            this.allBtn.Location = new System.Drawing.Point(510, 106);
            this.allBtn.Name = "allBtn";
            this.allBtn.Size = new System.Drawing.Size(75, 23);
            this.allBtn.TabIndex = 24;
            this.allBtn.Text = "전체보기";
            this.allBtn.UseVisualStyleBackColor = true;
            this.allBtn.Click += new System.EventHandler(this.allBtn_Click);
            // 
            // UserMealPlanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserMealPlanControl";
            this.Size = new System.Drawing.Size(974, 457);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label kcalLabel;
        private System.Windows.Forms.Label hydroLabel;
        private System.Windows.Forms.Label proteinLabel;
        private System.Windows.Forms.Label fatLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private Oracle.ManagedDataAccess.Client.OracleCommand oracleCommand1;
        private System.Windows.Forms.Button allBtn;
        private System.Windows.Forms.Button snackBtn;
        private System.Windows.Forms.Button dinnerBtn;
        private System.Windows.Forms.Button breakfastBtn;
        private System.Windows.Forms.Button lunchBtn;
        private System.Windows.Forms.DataGridView dataView;
    }
}
