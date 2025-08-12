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
    public partial class Tray5Form : Form
    {
        DataGrid dataGrid = new DataGrid();
        TXT myTXT = new TXT();
        public int formNum = 0;
        public Tray5Form()
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
        private void Tray5Form_Load(object sender, EventArgs e)
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
            YieldMode1.Text = Variable.YieldMode[16];
            YieldMode2.Text = Variable.YieldMode[17];
            YieldMode3.Text = Variable.YieldMode[18];
            YieldMode4.Text = Variable.YieldMode[19];

            string[] strDown17 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\17\tray");
            if (strDown17.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test1DataGrid, strDown17);
            }

            string[] strDown18 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\18\tray");
            if (strDown18.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test2DataGrid, strDown18);
            }

            string[] strDown19 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\19\tray");
            if (strDown19.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test3DataGrid, strDown19);
            }

            string[] strDown20 = myTXT.ReadTXT1(Application.StartupPath + @"\Map\20\tray");
            if (strDown20.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(Test4DataGrid, strDown20);
            }
        }
    }
}
