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
    public partial class Tray4Form : Form
    {
        DataGrid dataGrid = new DataGrid();
        TXT myTXT = new TXT();
        public int formNum = 0;
        public Tray4Form()
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
        private void Tray4Form_Load(object sender, EventArgs e)
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
            YieldMode1.Text = Variable.YieldMode[12];
            YieldMode2.Text = Variable.YieldMode[13];
            YieldMode3.Text = Variable.YieldMode[14];
            YieldMode4.Text = Variable.YieldMode[15];

            string[] strDown13 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\13\tray");
            if (strDown13.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test1DataGrid, strDown13);
            }

            string[] strDown14 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\14\tray");
            if (strDown14.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test2DataGrid, strDown14);
            }

            string[] strDown15 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\15\tray");
            if (strDown15.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test3DataGrid, strDown15);
            }

            string[] strDown16 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\16\tray");
            if (strDown16.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test4DataGrid, strDown16);
            }
        }
    }
}
