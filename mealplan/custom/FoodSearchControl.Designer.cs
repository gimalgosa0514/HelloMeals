namespace mealplan.custom
{
    partial class FoodSearchControl
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
            this.keyword = new System.Windows.Forms.TextBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.view = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.addMealButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.SuspendLayout();
            // 
            // keyword
            // 
            this.keyword.Location = new System.Drawing.Point(81, 30);
            this.keyword.Name = "keyword";
            this.keyword.Size = new System.Drawing.Size(147, 25);
            this.keyword.TabIndex = 0;
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(234, 30);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(46, 25);
            this.searchBtn.TabIndex = 1;
            this.searchBtn.Text = "검색";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "음식 이름 :";
            // 
            // view
            // 
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.Location = new System.Drawing.Point(36, 126);
            this.view.Name = "view";
            this.view.RowTemplate.Height = 23;
            this.view.Size = new System.Drawing.Size(892, 281);
            this.view.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(286, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "전체보기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addMealButton
            // 
            this.addMealButton.BackgroundImage = global::mealplan.Properties.Resources.Plus;
            this.addMealButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.addMealButton.Location = new System.Drawing.Point(934, 415);
            this.addMealButton.Name = "addMealButton";
            this.addMealButton.Size = new System.Drawing.Size(37, 39);
            this.addMealButton.TabIndex = 14;
            this.addMealButton.UseVisualStyleBackColor = true;
            this.addMealButton.Click += new System.EventHandler(this.addMealButton_Click);
            // 
            // FoodSearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addMealButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.view);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.keyword);
            this.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FoodSearchControl";
            this.Size = new System.Drawing.Size(974, 457);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox keyword;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView view;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button addMealButton;
    }
}
