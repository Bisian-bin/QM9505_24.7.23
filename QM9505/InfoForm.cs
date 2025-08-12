using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QM9505
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
        }


        private void InfoForm_Load(object sender, EventArgs e)
        {
            Variable.info = true;
            lotText.Text = "";
            trayNumText.Text = "";

            this.StartPosition = FormStartPosition.CenterScreen;
            int width = System.Windows.Forms.SystemInformation.WorkingArea.Width;
            int hight = System.Windows.Forms.SystemInformation.WorkingArea.Height;
            this.Location = new Point(width / 2 - 200, hight / 2 - 200);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variable.BatchNum = lotText.Text.Trim();
            Variable.inTrayNumSet = trayNumText.Text.Trim();
            if (lotText.Text != ""&&trayNumText.Text != "")
            {
                Variable.inTrayNumRecord = 0;
                Variable.info = false;
                this.Close();
            }
        }
    }
}
