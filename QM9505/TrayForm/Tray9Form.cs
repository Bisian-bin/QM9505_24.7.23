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
    public partial class Tray9Form : Form
    {
        DataGrid dataGrid = new DataGrid();
        TXT myTXT = new TXT();
        public int formNum = 0;
        public Tray9Form()
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
        private void Tray9Form_Load(object sender, EventArgs e)
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
            YieldMode1.Text = Variable.YieldMode[32];
            YieldMode2.Text = Variable.YieldMode[33];
            YieldMode3.Text = Variable.YieldMode[34];
            YieldMode4.Text = Variable.YieldMode[35];

            string[] strDown33 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\33\tray");
            if (strDown33.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test1DataGrid, strDown33);
            }

            string[] strDown34 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\34\tray");
            if (strDown34.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test2DataGrid, strDown34);
            }

            string[] strDown35 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\35\tray");
            if (strDown35.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test3DataGrid, strDown35);
            }

            string[] strDown36 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\36\tray");
            if (strDown36.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test4DataGrid, strDown36);
            }
        }
    }
}
