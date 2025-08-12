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
    public partial class Tray8Form : Form
    {
        DataGrid dataGrid = new DataGrid();
        TXT myTXT = new TXT();
        public int formNum = 0;
        public Tray8Form()
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
        private void Tray8Form_Load(object sender, EventArgs e)
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
            YieldMode1.Text = Variable.YieldMode[28];
            YieldMode2.Text = Variable.YieldMode[29];
            YieldMode3.Text = Variable.YieldMode[30];
            YieldMode4.Text = Variable.YieldMode[31];

            string[] strDown29 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\29\tray");
            if (strDown29.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test1DataGrid, strDown29);
            }

            string[] strDown30 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\30\tray");
            if (strDown30.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test2DataGrid, strDown30);
            }

            string[] strDown31 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\31\tray");
            if (strDown31.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test3DataGrid, strDown31);
            }

            string[] strDown32 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\32\tray");
            if (strDown32.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test4DataGrid, strDown32);
            }
        }
    }
}
