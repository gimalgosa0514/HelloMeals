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
        public UserMealPlanControl()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
