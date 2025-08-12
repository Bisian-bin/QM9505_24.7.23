using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QM9505.TraySetForm
{
    public partial class Tray2SetForm : Form
    {
        DataGrid dataGrid = new DataGrid();
        TXT myTXT = new TXT();
        public Tray2SetForm()
        {
            InitializeComponent();
        }

        private void Tray2SetForm_Load(object sender, EventArgs e)
        {
            DataGridINI();

            //1#Down
            string[] strDown1 = myTXT.ReadTXT(Application.StartupPath + @"\Data\TCPModel\5\tray");
            myTXT.ReadTxtToDataGridMethod1(Test1DataGrid, strDown1);

            //2#Down
            string[] strDown2 = myTXT.ReadTXT(Application.StartupPath + @"\Data\TCPModel\6\tray");
            myTXT.ReadTxtToDataGridMethod1(Test2DataGrid, strDown2);

            //3#Down
            string[] strDown3 = myTXT.ReadTXT(Application.StartupPath + @"\Data\TCPModel\7\tray");
            myTXT.ReadTxtToDataGridMethod1(Test3DataGrid, strDown3);

            //4#Down
            string[] strDown4 = myTXT.ReadTXT(Application.StartupPath + @"\Data\TCPModel\8\tray");
            myTXT.ReadTxtToDataGridMethod1(Test4DataGrid, strDown4);
        }

        public void DataGridINI()
        {
            dataGrid.IniLeftModelTrayW(Test1DataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftModelTrayW(Test2DataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftModelTrayW(Test3DataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftModelTrayW(Test4DataGrid, Variable.RowNum, Variable.ListNum);
        }

        #region 循环遍历DataGridView获取所有数据
        public string[] GetDataGridValue(DataGridView dataGridView)
        {
            List<string> str = new List<string>();
            for (int i = 0; i < (int)Variable.RowNum; i++)
            {
                for (int j = 0; j < (int)Variable.ListNum; j++)
                {
                    if (dataGridView.Rows[i].Cells[j].Style.BackColor == Color.LightSkyBlue)
                    {
                        str.Add("00");
                    }
                    else
                    {
                        str.Add("01");
                    }
                }
            }
            return str.ToArray();
        }

        #endregion

        private void Test1DataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int MyCount = Test1DataGrid.GetCellCount(DataGridViewElementStates.Selected);
            if (MyCount > 0)
            {
                if (Test1DataGrid.AreAllCellsSelected(true))
                {
                    for (int j = 0; j < (int)Variable.RowNum; j++)
                    {
                        for (int i = 0; i < (int)Variable.ListNum; i++)
                        {
                            if (Test1DataGrid.Rows[j].Cells[i].Style.BackColor != Color.LightSkyBlue)
                            {
                                Test1DataGrid.Rows[j].Cells[i].Style.BackColor = Color.LightSkyBlue;
                            }
                            else
                            {
                                Test1DataGrid.Rows[j].Cells[i].Style.BackColor = Color.Pink;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < MyCount; i++)
                    {
                        if (Test1DataGrid.SelectedCells[i].Style.BackColor != Color.LightSkyBlue)
                        {
                            Test1DataGrid.SelectedCells[i].Style.BackColor = Color.LightSkyBlue;
                        }
                        else
                        {
                            Test1DataGrid.SelectedCells[i].Style.BackColor = Color.Pink;
                        }
                    }
                }
            }
            Test1DataGrid.CurrentCell = null;

            //1#
            string[] strDown1 = GetDataGridValue(Test1DataGrid);
            myTXT.WriteTxt(strDown1, Application.StartupPath + @"\Data\TCPModel\5\tray");
        }

        private void Test2DataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int MyCount = Test2DataGrid.GetCellCount(DataGridViewElementStates.Selected);
            if (MyCount > 0)
            {
                if (Test2DataGrid.AreAllCellsSelected(true))
                {
                    for (int j = 0; j < (int)Variable.RowNum; j++)
                    {
                        for (int i = 0; i < (int)Variable.ListNum; i++)
                        {
                            if (Test2DataGrid.Rows[j].Cells[i].Style.BackColor != Color.LightSkyBlue)
                            {
                                Test2DataGrid.Rows[j].Cells[i].Style.BackColor = Color.LightSkyBlue;
                            }
                            else
                            {
                                Test2DataGrid.Rows[j].Cells[i].Style.BackColor = Color.Pink;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < MyCount; i++)
                    {
                        if (Test2DataGrid.SelectedCells[i].Style.BackColor != Color.LightSkyBlue)
                        {
                            Test2DataGrid.SelectedCells[i].Style.BackColor = Color.LightSkyBlue;
                        }
                        else
                        {
                            Test2DataGrid.SelectedCells[i].Style.BackColor = Color.Pink;
                        }
                    }
                }
            }
            Test2DataGrid.CurrentCell = null;

            //2#
            string[] strDown1 = GetDataGridValue(Test2DataGrid);
            myTXT.WriteTxt(strDown1, Application.StartupPath + @"\Data\TCPModel\6\tray");
        }

        private void Test3DataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int MyCount = Test3DataGrid.GetCellCount(DataGridViewElementStates.Selected);
            if (MyCount > 0)
            {
                if (Test3DataGrid.AreAllCellsSelected(true))
                {
                    for (int j = 0; j < (int)Variable.RowNum; j++)
                    {
                        for (int i = 0; i < (int)Variable.ListNum; i++)
                        {
                            if (Test3DataGrid.Rows[j].Cells[i].Style.BackColor != Color.LightSkyBlue)
                            {
                                Test3DataGrid.Rows[j].Cells[i].Style.BackColor = Color.LightSkyBlue;
                            }
                            else
                            {
                                Test3DataGrid.Rows[j].Cells[i].Style.BackColor = Color.Pink;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < MyCount; i++)
                    {
                        if (Test3DataGrid.SelectedCells[i].Style.BackColor != Color.LightSkyBlue)
                        {
                            Test3DataGrid.SelectedCells[i].Style.BackColor = Color.LightSkyBlue;
                        }
                        else
                        {
                            Test3DataGrid.SelectedCells[i].Style.BackColor = Color.Pink;
                        }
                    }
                }
            }
            Test3DataGrid.CurrentCell = null;

            //3#
            string[] strDown1 = GetDataGridValue(Test3DataGrid);
            myTXT.WriteTxt(strDown1, Application.StartupPath + @"\Data\TCPModel\7\tray");
        }

        private void Test4DataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int MyCount = Test4DataGrid.GetCellCount(DataGridViewElementStates.Selected);
            if (MyCount > 0)
            {
                if (Test4DataGrid.AreAllCellsSelected(true))
                {
                    for (int j = 0; j < (int)Variable.RowNum; j++)
                    {
                        for (int i = 0; i < (int)Variable.ListNum; i++)
                        {
                            if (Test4DataGrid.Rows[j].Cells[i].Style.BackColor != Color.LightSkyBlue)
                            {
                                Test4DataGrid.Rows[j].Cells[i].Style.BackColor = Color.LightSkyBlue;
                            }
                            else
                            {
                                Test4DataGrid.Rows[j].Cells[i].Style.BackColor = Color.Pink;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < MyCount; i++)
                    {
                        if (Test4DataGrid.SelectedCells[i].Style.BackColor != Color.LightSkyBlue)
                        {
                            Test4DataGrid.SelectedCells[i].Style.BackColor = Color.LightSkyBlue;
                        }
                        else
                        {
                            Test4DataGrid.SelectedCells[i].Style.BackColor = Color.Pink;
                        }
                    }
                }
            }
            Test4DataGrid.CurrentCell = null;

            //4#
            string[] strDown1 = GetDataGridValue(Test4DataGrid);
            myTXT.WriteTxt(strDown1, Application.StartupPath + @"\Data\TCPModel\8\tray");
        }

    }
}
