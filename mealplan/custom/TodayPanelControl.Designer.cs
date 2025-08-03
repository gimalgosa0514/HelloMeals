namespace mealplan.custom
{
    partial class TodayPanelControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.addMealButton = new System.Windows.Forms.Button();
            this.snack = new System.Windows.Forms.Panel();
            this.dinner = new System.Windows.Forms.Panel();
            this.lunch = new System.Windows.Forms.Panel();
            this.breakfast = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.kcalLabel = new System.Windows.Forms.Label();
            this.hydroLabel = new System.Windows.Forms.Label();
            this.proteinLabel = new System.Windows.Forms.Label();
            this.fatLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Noto Sans KR", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(72, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "아침 식사";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Noto Sans KR", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(324, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "점심 식사";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Noto Sans KR", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(583, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "저녁 식사";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Noto Sans KR", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(832, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 24);
            this.label4.TabIndex = 11;
            this.label4.Text = "간식";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(170, 222);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(643, 219);
            this.dataGridView1.TabIndex = 12;
            // 
            // addMealButton
            // 
            this.addMealButton.BackgroundImage = global::mealplan.Properties.Resources.Plus;
            this.addMealButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.addMealButton.Location = new System.Drawing.Point(934, 415);
            this.addMealButton.Name = "addMealButton";
            this.addMealButton.Size = new System.Drawing.Size(37, 39);
            this.addMealButton.TabIndex = 13;
            this.addMealButton.UseVisualStyleBackColor = true;
            // 
            // snack
            // 
            this.snack.BackgroundImage = global::mealplan.Properties.Resources.night;
            this.snack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.snack.Location = new System.Drawing.Point(827, 13);
            this.snack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.snack.Name = "snack";
            this.snack.Size = new System.Drawing.Size(48, 58);
            this.snack.TabIndex = 7;
            // 
            // dinner
            // 
            this.dinner.BackgroundImage = global::mealplan.Properties.Resources.sunset;
            this.dinner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.dinner.Location = new System.Drawing.Point(596, 13);
            this.dinner.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dinner.Name = "dinner";
            this.dinner.Size = new System.Drawing.Size(48, 58);
            this.dinner.TabIndex = 6;
            // 
            // lunch
            // 
            this.lunch.BackgroundImage = global::mealplan.Properties.Resources.contrast;
            this.lunch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.lunch.Location = new System.Drawing.Point(337, 13);
            this.lunch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lunch.Name = "lunch";
            this.lunch.Size = new System.Drawing.Size(48, 58);
            this.lunch.TabIndex = 5;
            // 
            // breakfast
            // 
            this.breakfast.BackgroundImage = global::mealplan.Properties.Resources.sun;
            this.breakfast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.breakfast.Location = new System.Drawing.Point(85, 13);
            this.breakfast.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.breakfast.Name = "breakfast";
            this.breakfast.Size = new System.Drawing.Size(48, 58);
            this.breakfast.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.kcalLabel);
            this.panel1.Controls.Add(this.hydroLabel);
            this.panel1.Controls.Add(this.proteinLabel);
            this.panel1.Controls.Add(this.fatLabel);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(170, 162);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 59);
            this.panel1.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(356, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "지방";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(413, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "단백질";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(485, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 21);
            this.label7.TabIndex = 2;
            this.label7.Text = "탄수화물";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(572, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 21);
            this.label8.TabIndex = 3;
            this.label8.Text = "칼로리";
            // 
            // kcalLabel
            // 
            this.kcalLabel.AutoSize = true;
            this.kcalLabel.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.kcalLabel.Location = new System.Drawing.Point(578, 33);
            this.kcalLabel.Name = "kcalLabel";
            this.kcalLabel.Size = new System.Drawing.Size(15, 17);
            this.kcalLabel.TabIndex = 7;
            this.kcalLabel.Text = "0";
            // 
            // hydroLabel
            // 
            this.hydroLabel.AutoSize = true;
            this.hydroLabel.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.hydroLabel.Location = new System.Drawing.Point(491, 33);
            this.hydroLabel.Name = "hydroLabel";
            this.hydroLabel.Size = new System.Drawing.Size(15, 17);
            this.hydroLabel.TabIndex = 6;
            this.hydroLabel.Text = "0";
            // 
            // proteinLabel
            // 
            this.proteinLabel.AutoSize = true;
            this.proteinLabel.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.proteinLabel.Location = new System.Drawing.Point(419, 33);
            this.proteinLabel.Name = "proteinLabel";
            this.proteinLabel.Size = new System.Drawing.Size(15, 17);
            this.proteinLabel.TabIndex = 5;
            this.proteinLabel.Text = "0";
            // 
            // fatLabel
            // 
            this.fatLabel.AutoSize = true;
            this.fatLabel.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.fatLabel.Location = new System.Drawing.Point(362, 33);
            this.fatLabel.Name = "fatLabel";
            this.fatLabel.Size = new System.Drawing.Size(15, 17);
            this.fatLabel.TabIndex = 4;
            this.fatLabel.Text = "0";
            // 
            // TodayPanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.addMealButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.snack);
            this.Controls.Add(this.dinner);
            this.Controls.Add(this.lunch);
            this.Controls.Add(this.breakfast);
            this.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TodayPanelControl";
            this.Size = new System.Drawing.Size(974, 457);
            this.Load += new System.EventHandler(this.TodayPanelControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel snack;
        private System.Windows.Forms.Panel dinner;
        private System.Windows.Forms.Panel lunch;
        private System.Windows.Forms.Panel breakfast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button addMealButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label kcalLabel;
        private System.Windows.Forms.Label hydroLabel;
        private System.Windows.Forms.Label proteinLabel;
        private System.Windows.Forms.Label fatLabel;
    }
}
