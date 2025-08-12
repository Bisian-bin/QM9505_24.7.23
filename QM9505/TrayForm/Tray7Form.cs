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
    public partial class Tray7Form : Form
    {
        DataGrid dataGrid = new DataGrid();
        TXT myTXT = new TXT();
        public int formNum = 0;
        public Tray7Form()
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
        private void Tray7Form_Load(object sender, EventArgs e)
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
            YieldMode1.Text = Variable.YieldMode[24];
            YieldMode2.Text = Variable.YieldMode[25];
            YieldMode3.Text = Variable.YieldMode[26];
            YieldMode4.Text = Variable.YieldMode[27];

            string[] strDown25 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\25\tray");
            if (strDown25.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test1DataGrid, strDown25);
            }

            string[] strDown26 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\26\tray");
            if (strDown26.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test2DataGrid, strDown26);
            }

            string[] strDown27 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\27\tray");
            if (strDown27.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test3DataGrid, strDown27);
            }

            string[] strDown28 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\28\tray");
            if (strDown28.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test4DataGrid, strDown28);
            }
        }
    }
}
