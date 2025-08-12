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
    public partial class Tray3Form : Form
    {
        DataGrid dataGrid = new DataGrid();
        TXT myTXT = new TXT();
        public int formNum = 0;
        public Tray3Form()
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
        private void Tray3Form_Load(object sender, EventArgs e)
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
            YieldMode1.Text = Variable.YieldMode[8];
            YieldMode2.Text = Variable.YieldMode[9];
            YieldMode3.Text = Variable.YieldMode[10];
            YieldMode4.Text = Variable.YieldMode[11];

            string[] strDown9 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\9\tray");
            if (strDown9.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test1DataGrid, strDown9);
            }

            string[] strDown10 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\10\tray");
            if (strDown10.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test2DataGrid, strDown10);
            }

            string[] strDown11 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\11\tray");
            if (strDown11.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test3DataGrid, strDown11);
            }

            string[] strDown12 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\12\tray");
            if (strDown12.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test4DataGrid, strDown12);
            }
        }
    }
}
