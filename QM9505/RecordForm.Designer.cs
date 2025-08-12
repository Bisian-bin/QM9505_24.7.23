
namespace QM9505
{
    partial class RecordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl6 = new System.Windows.Forms.TabControl();
            this.tabPage22 = new System.Windows.Forms.TabPage();
            this.groupBox46 = new System.Windows.Forms.GroupBox();
            this.BtnSendSearch = new System.Windows.Forms.Button();
            this.btnProductSendSave = new System.Windows.Forms.Button();
            this.btnProductSendSearch = new System.Windows.Forms.Button();
            this.dateTimeProductOverSend = new System.Windows.Forms.DateTimePicker();
            this.label897 = new System.Windows.Forms.Label();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.dateTimeProductBeginSend = new System.Windows.Forms.DateTimePicker();
            this.SendListBox = new System.Windows.Forms.ListBox();
            this.label898 = new System.Windows.Forms.Label();
            this.groupBox44 = new System.Windows.Forms.GroupBox();
            this.SendDataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage24 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnReciveSearch = new System.Windows.Forms.Button();
            this.btnProductReciveSave = new System.Windows.Forms.Button();
            this.btnProductReciveSearch = new System.Windows.Forms.Button();
            this.dateTimeProductOverRecive = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRecive = new System.Windows.Forms.TextBox();
            this.dateTimeProductBeginRecive = new System.Windows.Forms.DateTimePicker();
            this.ReciveListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ReciveDataGrid = new System.Windows.Forms.DataGridView();
            this.button75 = new System.Windows.Forms.Button();
            this.tabControl6.SuspendLayout();
            this.tabPage22.SuspendLayout();
            this.groupBox46.SuspendLayout();
            this.groupBox44.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SendDataGrid)).BeginInit();
            this.tabPage24.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReciveDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl6
            // 
            this.tabControl6.Controls.Add(this.tabPage22);
            this.tabControl6.Controls.Add(this.tabPage24);
            this.tabControl6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl6.Location = new System.Drawing.Point(12, 8);
            this.tabControl6.Name = "tabControl6";
            this.tabControl6.SelectedIndex = 0;
            this.tabControl6.Size = new System.Drawing.Size(1900, 983);
            this.tabControl6.TabIndex = 4;
            // 
            // tabPage22
            // 
            this.tabPage22.Controls.Add(this.groupBox46);
            this.tabPage22.Controls.Add(this.groupBox44);
            this.tabPage22.Location = new System.Drawing.Point(4, 26);
            this.tabPage22.Name = "tabPage22";
            this.tabPage22.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage22.Size = new System.Drawing.Size(1892, 953);
            this.tabPage22.TabIndex = 1;
            this.tabPage22.Text = "发送数据记录";
            this.tabPage22.UseVisualStyleBackColor = true;
            // 
            // groupBox46
            // 
            this.groupBox46.Controls.Add(this.BtnSendSearch);
            this.groupBox46.Controls.Add(this.btnProductSendSave);
            this.groupBox46.Controls.Add(this.btnProductSendSearch);
            this.groupBox46.Controls.Add(this.dateTimeProductOverSend);
            this.groupBox46.Controls.Add(this.label897);
            this.groupBox46.Controls.Add(this.textBoxSend);
            this.groupBox46.Controls.Add(this.dateTimeProductBeginSend);
            this.groupBox46.Controls.Add(this.SendListBox);
            this.groupBox46.Controls.Add(this.label898);
            this.groupBox46.Location = new System.Drawing.Point(6, -2);
            this.groupBox46.Name = "groupBox46";
            this.groupBox46.Size = new System.Drawing.Size(322, 972);
            this.groupBox46.TabIndex = 5;
            this.groupBox46.TabStop = false;
            // 
            // BtnSendSearch
            // 
            this.BtnSendSearch.BackColor = System.Drawing.SystemColors.Control;
            this.BtnSendSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnSendSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSendSearch.Location = new System.Drawing.Point(209, 30);
            this.BtnSendSearch.Name = "BtnSendSearch";
            this.BtnSendSearch.Size = new System.Drawing.Size(107, 37);
            this.BtnSendSearch.TabIndex = 1679;
            this.BtnSendSearch.Text = "搜索";
            this.BtnSendSearch.UseVisualStyleBackColor = false;
            this.BtnSendSearch.Click += new System.EventHandler(this.BtnSendSearch_Click);
            // 
            // btnProductSendSave
            // 
            this.btnProductSendSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnProductSendSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnProductSendSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductSendSave.Location = new System.Drawing.Point(180, 874);
            this.btnProductSendSave.Name = "btnProductSendSave";
            this.btnProductSendSave.Size = new System.Drawing.Size(136, 50);
            this.btnProductSendSave.TabIndex = 94;
            this.btnProductSendSave.Text = "导出";
            this.btnProductSendSave.UseVisualStyleBackColor = false;
            this.btnProductSendSave.Click += new System.EventHandler(this.btnProductSendSave_Click);
            // 
            // btnProductSendSearch
            // 
            this.btnProductSendSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnProductSendSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnProductSendSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductSendSearch.Location = new System.Drawing.Point(12, 874);
            this.btnProductSendSearch.Name = "btnProductSendSearch";
            this.btnProductSendSearch.Size = new System.Drawing.Size(136, 50);
            this.btnProductSendSearch.TabIndex = 95;
            this.btnProductSendSearch.Text = "查找";
            this.btnProductSendSearch.UseVisualStyleBackColor = false;
            this.btnProductSendSearch.Click += new System.EventHandler(this.btnProductSendSearch_Click);
            // 
            // dateTimeProductOverSend
            // 
            this.dateTimeProductOverSend.CalendarFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeProductOverSend.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeProductOverSend.Location = new System.Drawing.Point(106, 801);
            this.dateTimeProductOverSend.Name = "dateTimeProductOverSend";
            this.dateTimeProductOverSend.Size = new System.Drawing.Size(210, 31);
            this.dateTimeProductOverSend.TabIndex = 93;
            // 
            // label897
            // 
            this.label897.AutoSize = true;
            this.label897.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label897.Location = new System.Drawing.Point(9, 804);
            this.label897.Name = "label897";
            this.label897.Size = new System.Drawing.Size(91, 24);
            this.label897.TabIndex = 91;
            this.label897.Text = "结束时间:";
            // 
            // textBoxSend
            // 
            this.textBoxSend.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSend.Location = new System.Drawing.Point(12, 32);
            this.textBoxSend.Multiline = true;
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(191, 33);
            this.textBoxSend.TabIndex = 1678;
            // 
            // dateTimeProductBeginSend
            // 
            this.dateTimeProductBeginSend.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeProductBeginSend.Location = new System.Drawing.Point(106, 754);
            this.dateTimeProductBeginSend.Name = "dateTimeProductBeginSend";
            this.dateTimeProductBeginSend.Size = new System.Drawing.Size(210, 31);
            this.dateTimeProductBeginSend.TabIndex = 92;
            // 
            // SendListBox
            // 
            this.SendListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SendListBox.FormattingEnabled = true;
            this.SendListBox.ItemHeight = 20;
            this.SendListBox.Location = new System.Drawing.Point(12, 73);
            this.SendListBox.Name = "SendListBox";
            this.SendListBox.ScrollAlwaysVisible = true;
            this.SendListBox.Size = new System.Drawing.Size(304, 664);
            this.SendListBox.TabIndex = 1676;
            this.SendListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SendListBox_MouseDoubleClick);
            // 
            // label898
            // 
            this.label898.AutoSize = true;
            this.label898.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label898.Location = new System.Drawing.Point(9, 757);
            this.label898.Name = "label898";
            this.label898.Size = new System.Drawing.Size(91, 24);
            this.label898.TabIndex = 90;
            this.label898.Text = "开始时间:";
            // 
            // groupBox44
            // 
            this.groupBox44.Controls.Add(this.SendDataGrid);
            this.groupBox44.Location = new System.Drawing.Point(334, -2);
            this.groupBox44.Name = "groupBox44";
            this.groupBox44.Size = new System.Drawing.Size(1552, 972);
            this.groupBox44.TabIndex = 4;
            this.groupBox44.TabStop = false;
            // 
            // SendDataGrid
            // 
            this.SendDataGrid.AllowUserToAddRows = false;
            this.SendDataGrid.AllowUserToDeleteRows = false;
            this.SendDataGrid.AllowUserToResizeColumns = false;
            this.SendDataGrid.AllowUserToResizeRows = false;
            this.SendDataGrid.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.SendDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SendDataGrid.Location = new System.Drawing.Point(6, 17);
            this.SendDataGrid.Name = "SendDataGrid";
            this.SendDataGrid.ReadOnly = true;
            this.SendDataGrid.RowHeadersWidth = 51;
            this.SendDataGrid.RowTemplate.Height = 23;
            this.SendDataGrid.Size = new System.Drawing.Size(1537, 932);
            this.SendDataGrid.TabIndex = 96;
            // 
            // tabPage24
            // 
            this.tabPage24.Controls.Add(this.groupBox1);
            this.tabPage24.Controls.Add(this.groupBox2);
            this.tabPage24.Controls.Add(this.button75);
            this.tabPage24.Location = new System.Drawing.Point(4, 26);
            this.tabPage24.Name = "tabPage24";
            this.tabPage24.Size = new System.Drawing.Size(1892, 953);
            this.tabPage24.TabIndex = 3;
            this.tabPage24.Text = "接受数据记录";
            this.tabPage24.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnReciveSearch);
            this.groupBox1.Controls.Add(this.btnProductReciveSave);
            this.groupBox1.Controls.Add(this.btnProductReciveSearch);
            this.groupBox1.Controls.Add(this.dateTimeProductOverRecive);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxRecive);
            this.groupBox1.Controls.Add(this.dateTimeProductBeginRecive);
            this.groupBox1.Controls.Add(this.ReciveListBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, -10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 960);
            this.groupBox1.TabIndex = 96;
            this.groupBox1.TabStop = false;
            // 
            // BtnReciveSearch
            // 
            this.BtnReciveSearch.BackColor = System.Drawing.SystemColors.Control;
            this.BtnReciveSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnReciveSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnReciveSearch.Location = new System.Drawing.Point(209, 30);
            this.BtnReciveSearch.Name = "BtnReciveSearch";
            this.BtnReciveSearch.Size = new System.Drawing.Size(107, 37);
            this.BtnReciveSearch.TabIndex = 1679;
            this.BtnReciveSearch.Text = "搜索";
            this.BtnReciveSearch.UseVisualStyleBackColor = false;
            this.BtnReciveSearch.Click += new System.EventHandler(this.BtnReciveSearch_Click);
            // 
            // btnProductReciveSave
            // 
            this.btnProductReciveSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnProductReciveSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnProductReciveSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductReciveSave.Location = new System.Drawing.Point(180, 874);
            this.btnProductReciveSave.Name = "btnProductReciveSave";
            this.btnProductReciveSave.Size = new System.Drawing.Size(136, 50);
            this.btnProductReciveSave.TabIndex = 94;
            this.btnProductReciveSave.Text = "导出";
            this.btnProductReciveSave.UseVisualStyleBackColor = false;
            this.btnProductReciveSave.Click += new System.EventHandler(this.btnProductReciveSave_Click);
            // 
            // btnProductReciveSearch
            // 
            this.btnProductReciveSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnProductReciveSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnProductReciveSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductReciveSearch.Location = new System.Drawing.Point(12, 874);
            this.btnProductReciveSearch.Name = "btnProductReciveSearch";
            this.btnProductReciveSearch.Size = new System.Drawing.Size(136, 50);
            this.btnProductReciveSearch.TabIndex = 95;
            this.btnProductReciveSearch.Text = "查找";
            this.btnProductReciveSearch.UseVisualStyleBackColor = false;
            this.btnProductReciveSearch.Click += new System.EventHandler(this.btnProductReciveSearch_Click);
            // 
            // dateTimeProductOverRecive
            // 
            this.dateTimeProductOverRecive.CalendarFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeProductOverRecive.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeProductOverRecive.Location = new System.Drawing.Point(106, 801);
            this.dateTimeProductOverRecive.Name = "dateTimeProductOverRecive";
            this.dateTimeProductOverRecive.Size = new System.Drawing.Size(210, 31);
            this.dateTimeProductOverRecive.TabIndex = 93;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 804);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 24);
            this.label1.TabIndex = 91;
            this.label1.Text = "结束时间:";
            // 
            // textBoxRecive
            // 
            this.textBoxRecive.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxRecive.Location = new System.Drawing.Point(12, 32);
            this.textBoxRecive.Multiline = true;
            this.textBoxRecive.Name = "textBoxRecive";
            this.textBoxRecive.Size = new System.Drawing.Size(191, 33);
            this.textBoxRecive.TabIndex = 1678;
            // 
            // dateTimeProductBeginRecive
            // 
            this.dateTimeProductBeginRecive.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeProductBeginRecive.Location = new System.Drawing.Point(106, 754);
            this.dateTimeProductBeginRecive.Name = "dateTimeProductBeginRecive";
            this.dateTimeProductBeginRecive.Size = new System.Drawing.Size(210, 31);
            this.dateTimeProductBeginRecive.TabIndex = 92;
            // 
            // ReciveListBox
            // 
            this.ReciveListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ReciveListBox.FormattingEnabled = true;
            this.ReciveListBox.ItemHeight = 20;
            this.ReciveListBox.Location = new System.Drawing.Point(12, 73);
            this.ReciveListBox.Name = "ReciveListBox";
            this.ReciveListBox.ScrollAlwaysVisible = true;
            this.ReciveListBox.Size = new System.Drawing.Size(304, 664);
            this.ReciveListBox.TabIndex = 1676;
            this.ReciveListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ReciveListBox_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 757);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 24);
            this.label2.TabIndex = 90;
            this.label2.Text = "开始时间:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ReciveDataGrid);
            this.groupBox2.Location = new System.Drawing.Point(334, -10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1552, 972);
            this.groupBox2.TabIndex = 95;
            this.groupBox2.TabStop = false;
            // 
            // ReciveDataGrid
            // 
            this.ReciveDataGrid.AllowUserToAddRows = false;
            this.ReciveDataGrid.AllowUserToDeleteRows = false;
            this.ReciveDataGrid.AllowUserToResizeColumns = false;
            this.ReciveDataGrid.AllowUserToResizeRows = false;
            this.ReciveDataGrid.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.ReciveDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReciveDataGrid.Location = new System.Drawing.Point(6, 17);
            this.ReciveDataGrid.Name = "ReciveDataGrid";
            this.ReciveDataGrid.ReadOnly = true;
            this.ReciveDataGrid.RowHeadersWidth = 51;
            this.ReciveDataGrid.RowTemplate.Height = 23;
            this.ReciveDataGrid.Size = new System.Drawing.Size(1537, 943);
            this.ReciveDataGrid.TabIndex = 96;
            // 
            // button75
            // 
            this.button75.BackColor = System.Drawing.SystemColors.Control;
            this.button75.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button75.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button75.Location = new System.Drawing.Point(1896, 710);
            this.button75.Name = "button75";
            this.button75.Size = new System.Drawing.Size(85, 44);
            this.button75.TabIndex = 94;
            this.button75.Text = "导出";
            this.button75.UseVisualStyleBackColor = false;
            // 
            // RecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 997);
            this.Controls.Add(this.tabControl6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RecordForm";
            this.Text = "SearchForm";
            this.tabControl6.ResumeLayout(false);
            this.tabPage22.ResumeLayout(false);
            this.groupBox46.ResumeLayout(false);
            this.groupBox46.PerformLayout();
            this.groupBox44.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SendDataGrid)).EndInit();
            this.tabPage24.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReciveDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl6;
        private System.Windows.Forms.TabPage tabPage22;
        private System.Windows.Forms.GroupBox groupBox46;
        private System.Windows.Forms.Button BtnSendSearch;
        private System.Windows.Forms.TextBox textBoxSend;
        public System.Windows.Forms.ListBox SendListBox;
        private System.Windows.Forms.GroupBox groupBox44;
        private System.Windows.Forms.DataGridView SendDataGrid;
        private System.Windows.Forms.Button btnProductSendSearch;
        private System.Windows.Forms.Button btnProductSendSave;
        private System.Windows.Forms.DateTimePicker dateTimeProductOverSend;
        private System.Windows.Forms.DateTimePicker dateTimeProductBeginSend;
        private System.Windows.Forms.Label label897;
        private System.Windows.Forms.Label label898;
        private System.Windows.Forms.TabPage tabPage24;
        private System.Windows.Forms.Button button75;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnReciveSearch;
        private System.Windows.Forms.Button btnProductReciveSave;
        private System.Windows.Forms.Button btnProductReciveSearch;
        private System.Windows.Forms.DateTimePicker dateTimeProductOverRecive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRecive;
        private System.Windows.Forms.DateTimePicker dateTimeProductBeginRecive;
        public System.Windows.Forms.ListBox ReciveListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView ReciveDataGrid;
    }
}