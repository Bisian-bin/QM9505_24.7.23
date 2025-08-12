using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QM9505
{
    public partial class RecordForm : Form
    {
        Access access = new Access();
        DataTable dt = new DataTable();
        ExcelHelper excelHelper = new ExcelHelper();
        DataGrid dataGrid = new DataGrid();
        public int row;
        public int col;
        public RecordForm()
        {
            InitializeComponent();
        }

        #region ******发送******

        #region 搜索
        private void BtnSendSearch_Click(object sender, EventArgs e)
        {
            if (textBoxSend.Text == "")
            {
                access.SearchSendData(SendListBox);//将结果显示在界面
            }
            else
            {
                try
                {
                    string CONN = Access.GetSqlConnectionString();
                    SendListBox.Items.Clear();
                    OleDbConnection conn = new OleDbConnection(CONN);//连接
                    conn.Open();//打开
                    OleDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select  * from BIBSendRecord where QR like '%" + textBoxSend.Text.Trim() + "%'";
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        SendListBox.Items.Add(reader["DataTime"].ToString() + "-" + reader["QR"].ToString());
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                }
            }
        }

        #endregion

        #region ListBox双击
        private void SendListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            access.SearchSendList(SendDataGrid, SendListBox.SelectedItem.ToString());

        }
        #endregion

        #region 按时间查询
        private void btnProductSendSearch_Click(object sender, EventArgs e)
        {
            btnProductSendSearch.Enabled = false;
            access.SearchSendData(SendDataGrid, dateTimeProductBeginSend, dateTimeProductOverSend);
            btnProductSendSearch.Enabled = true;
        }
        #endregion

        #region 数据保存
        private void btnProductSendSave_Click(object sender, EventArgs e)
        {
            btnProductSendSave.Enabled = false;
            excelHelper.SaveAs(SendDataGrid);
            btnProductSendSave.Enabled = true;
        }
        #endregion

        #endregion

        #region ******接受******

        #region 搜索
        private void BtnReciveSearch_Click(object sender, EventArgs e)
        {
            if (textBoxRecive.Text == "")
            {
                access.SearchReceiveData(ReciveListBox);//将结果显示在界面
            }
            else
            {
                try
                {
                    string CONN = Access.GetSqlConnectionString();
                    ReciveListBox.Items.Clear();
                    OleDbConnection conn = new OleDbConnection(CONN);//连接
                    conn.Open();//打开
                    OleDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select  * from BIBReceiveRecord where QR like '%" + textBoxRecive.Text.Trim() + "%'";
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ReciveListBox.Items.Add(reader["DataTime"].ToString() + "-" + reader["QR"].ToString());
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                }
            }
        }

        #endregion

        #region ListBox双击
        private void ReciveListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            access.SearchReceiveList(ReciveDataGrid, ReciveListBox.SelectedItem.ToString());
        }

        #endregion

        #region 按时间查询
        private void btnProductReciveSearch_Click(object sender, EventArgs e)
        {
            btnProductReciveSearch.Enabled = false;
            access.SearchReceiveData(ReciveDataGrid, dateTimeProductBeginRecive, dateTimeProductOverRecive);
            btnProductReciveSearch.Enabled = true;
        }

        #endregion

        #region 数据保存
        private void btnProductReciveSave_Click(object sender, EventArgs e)
        {
            btnProductReciveSave.Enabled = false;
            excelHelper.SaveAs(ReciveDataGrid);
            btnProductReciveSave.Enabled = true;
        }


























        #endregion

        #endregion


    }
}
