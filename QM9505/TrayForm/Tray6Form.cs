using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QM9505.TrayForm
{
    public partial class Tray6Form : Form
    {
        DataGrid dataGrid = new DataGrid();
        TXT myTXT = new TXT();
        public int formNum = 0;
        public Tray6Form()
        {
            InitializeComponent();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            formNum += 1;
            if (formNum > 1)
            {
                //timer1.Enabled = false;
                //timer1.Stop();
            }

            base.OnVisibleChanged(e);
            if (!IsHandleCreated)
            {
                this.Close();
            }
        }
        private void Tray6Form_Load(object sender, EventArgs e)
        {
            DataGridINI();
            timer1.Enabled = true;
            timer1.Start();
        }

        public void DataGridINI()
        {
            dataGrid.IniLeftModelTrayW(Test1DataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftModelTrayW(Test2DataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftModelTrayW(Test3DataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftModelTrayW(Test4DataGrid, Variable.RowNum, Variable.ListNum);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            YieldMode1.Text = Variable.YieldMode[20];
            YieldMode2.Text = Variable.YieldMode[21];
            YieldMode3.Text = Variable.YieldMode[22];
            YieldMode4.Text = Variable.YieldMode[23];

            string[] strDown21 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\21\tray");
            if (strDown21.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test1DataGrid, strDown21);
            }

            string[] strDown22 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\22\tray");
            if (strDown22.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test2DataGrid, strDown22);
            }

            string[] strDown23 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\23\tray");
            if (strDown23.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test3DataGrid, strDown23);
            }

            string[] strDown24 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\24\tray");
            if (strDown24.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test4DataGrid, strDown24);
            }
        }
    }
}
