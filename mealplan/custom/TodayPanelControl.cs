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
    // 금일 먹은 음식에 대해 아침, 점심, 저녁, 간식에 따라 쿼리를 쏴 데이터 뷰어를 통해 보여주고,
    // 영양섭취량이랑 칼로리 계산해서 보여주는 패널
    // plus 버튼을 누르면 금일 식단에 먹은 음식을 추가할 수 있음.
    // TODO : 식단에 음식 추가하는 폼 만들고, 이제 비즈니스 로직 짜면 됨.
    public partial class TodayPanelControl : UserControl
    {
        public TodayPanelControl()
        {
            InitializeComponent();
        }

        private void TodayPanelControl_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
