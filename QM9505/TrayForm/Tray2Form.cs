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
    public partial class Tray2Form : Form
    {
        DataGrid dataGrid = new DataGrid();
        TXT myTXT = new TXT();
        public int formNum = 0;

        public Tray2Form()
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

        private void Tray2Form_Load(object sender, EventArgs e)
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
            YieldMode1.Text = Variable.YieldMode[4];
            YieldMode2.Text = Variable.YieldMode[5];
            YieldMode3.Text = Variable.YieldMode[6];
            YieldMode4.Text = Variable.YieldMode[7];

            string[] strDown5 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\5\tray");
            if (strDown5.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test1DataGrid, strDown5);
            }

            string[] strDown6 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\6\tray");
            if (strDown6.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test2DataGrid, strDown6);
            }

            string[] strDown7 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\7\tray");
            if (strDown7.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test3DataGrid, strDown7);
            }

            string[] strDown8 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\8\tray");
            if (strDown8.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test4DataGrid, strDown8);
            }
        }
    }
}
