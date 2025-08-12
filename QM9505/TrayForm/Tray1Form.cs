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
    public partial class Tray1Form : Form
    {
        DataGrid dataGrid = new DataGrid();
        TXT myTXT = new TXT();
        public int formNum = 0;
        public Tray1Form()
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
        private void Tray1Form_Load(object sender, EventArgs e)
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
            YieldMode1.Text = Variable.YieldMode[0];
            YieldMode2.Text = Variable.YieldMode[1];
            YieldMode3.Text = Variable.YieldMode[2];
            YieldMode4.Text = Variable.YieldMode[3];

            string[] strDown1 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\1\tray");
            if (strDown1.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test1DataGrid, strDown1);
            }

            string[] strDown2 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\2\tray");
            if (strDown2.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test2DataGrid, strDown2);
            }

            string[] strDown3 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\3\tray");
            if (strDown3.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test3DataGrid, strDown3);
            }

            string[] strDown4 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\4\tray");
            if (strDown4.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test4DataGrid, strDown4);
            }
        }
    }
}
