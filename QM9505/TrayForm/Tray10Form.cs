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
    public partial class Tray10Form : Form
    {
        DataGrid dataGrid = new DataGrid();
        TXT myTXT = new TXT();
        public int formNum = 0;
        public Tray10Form()
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
        private void Tray10Form_Load(object sender, EventArgs e)
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
            YieldMode1.Text = Variable.YieldMode[36];
            YieldMode2.Text = Variable.YieldMode[37];
            YieldMode3.Text = Variable.YieldMode[38];
            YieldMode4.Text = Variable.YieldMode[39];

            string[] strDown37 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\37\tray");
            if (strDown37.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test1DataGrid, strDown37);
            }

            string[] strDown38 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\38\tray");
            if (strDown38.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test2DataGrid, strDown38);
            }

            string[] strDown39 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\39\tray");
            if (strDown39.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test3DataGrid, strDown39);
            }

            string[] strDown40 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\40\tray");
            if (strDown40.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test4DataGrid, strDown40);
            }
        }
    }
}
