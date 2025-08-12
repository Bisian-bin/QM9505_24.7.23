namespace QM9505
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.主界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IO界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通信界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.调试界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SummaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.权限登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.MainTabPage = new System.Windows.Forms.TabPage();
            this.SubTabPage = new System.Windows.Forms.TabPage();
            this.SubPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.MainTabPage.SuspendLayout();
            this.SubTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.主界面ToolStripMenuItem,
            this.参数界面ToolStripMenuItem,
            this.IO界面ToolStripMenuItem,
            this.通信界面ToolStripMenuItem,
            this.调试界面ToolStripMenuItem,
            this.数据记录ToolStripMenuItem,
            this.SummaryToolStripMenuItem,
            this.权限登录ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1916, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 主界面ToolStripMenuItem
            // 
            this.主界面ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.主界面ToolStripMenuItem.Name = "主界面ToolStripMenuItem";
            this.主界面ToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.主界面ToolStripMenuItem.Text = "主界面";
            this.主界面ToolStripMenuItem.Click += new System.EventHandler(this.主界面ToolStripMenuItem_Click);
            // 
            // 参数界面ToolStripMenuItem
            // 
            this.参数界面ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.参数界面ToolStripMenuItem.Name = "参数界面ToolStripMenuItem";
            this.参数界面ToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.参数界面ToolStripMenuItem.Text = "参数界面";
            this.参数界面ToolStripMenuItem.Click += new System.EventHandler(this.参数界面ToolStripMenuItem_Click);
            // 
            // IO界面ToolStripMenuItem
            // 
            this.IO界面ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IO界面ToolStripMenuItem.Name = "IO界面ToolStripMenuItem";
            this.IO界面ToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.IO界面ToolStripMenuItem.Text = "IO界面";
            this.IO界面ToolStripMenuItem.Click += new System.EventHandler(this.IO界面ToolStripMenuItem1_Click);
            // 
            // 通信界面ToolStripMenuItem
            // 
            this.通信界面ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.通信界面ToolStripMenuItem.Name = "通信界面ToolStripMenuItem";
            this.通信界面ToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.通信界面ToolStripMenuItem.Text = "通信界面";
            this.通信界面ToolStripMenuItem.Click += new System.EventHandler(this.通信界面ToolStripMenuItem_Click);
            // 
            // 调试界面ToolStripMenuItem
            // 
            this.调试界面ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.调试界面ToolStripMenuItem.Name = "调试界面ToolStripMenuItem";
            this.调试界面ToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.调试界面ToolStripMenuItem.Text = "调试界面";
            this.调试界面ToolStripMenuItem.Click += new System.EventHandler(this.调试界面ToolStripMenuItem_Click);
            // 
            // 数据记录ToolStripMenuItem
            // 
            this.数据记录ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.数据记录ToolStripMenuItem.Name = "数据记录ToolStripMenuItem";
            this.数据记录ToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.数据记录ToolStripMenuItem.Text = "数据记录";
            this.数据记录ToolStripMenuItem.Click += new System.EventHandler(this.数据记录ToolStripMenuItem_Click);
            // 
            // SummaryToolStripMenuItem
            // 
            this.SummaryToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SummaryToolStripMenuItem.Name = "SummaryToolStripMenuItem";
            this.SummaryToolStripMenuItem.Size = new System.Drawing.Size(95, 24);
            this.SummaryToolStripMenuItem.Text = "Summary";
            this.SummaryToolStripMenuItem.Click += new System.EventHandler(this.SummaryToolStripMenuItem_Click);
            // 
            // 权限登录ToolStripMenuItem
            // 
            this.权限登录ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.权限登录ToolStripMenuItem.Name = "权限登录ToolStripMenuItem";
            this.权限登录ToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.权限登录ToolStripMenuItem.Text = "权限登录";
            this.权限登录ToolStripMenuItem.Click += new System.EventHandler(this.权限登录ToolStripMenuItem_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(3, 3);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1910, 997);
            this.MainPanel.TabIndex = 200;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.MainTabPage);
            this.TabControl.Controls.Add(this.SubTabPage);
            this.TabControl.Location = new System.Drawing.Point(0, 11);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(1924, 1029);
            this.TabControl.TabIndex = 0;
            // 
            // MainTabPage
            // 
            this.MainTabPage.Controls.Add(this.MainPanel);
            this.MainTabPage.Location = new System.Drawing.Point(4, 22);
            this.MainTabPage.Name = "MainTabPage";
            this.MainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.MainTabPage.Size = new System.Drawing.Size(1916, 1003);
            this.MainTabPage.TabIndex = 0;
            this.MainTabPage.Text = "tabPage1";
            this.MainTabPage.UseVisualStyleBackColor = true;
            // 
            // SubTabPage
            // 
            this.SubTabPage.Controls.Add(this.SubPanel);
            this.SubTabPage.Location = new System.Drawing.Point(4, 22);
            this.SubTabPage.Name = "SubTabPage";
            this.SubTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SubTabPage.Size = new System.Drawing.Size(1916, 1003);
            this.SubTabPage.TabIndex = 1;
            this.SubTabPage.Text = "tabPage2";
            this.SubTabPage.UseVisualStyleBackColor = true;
            // 
            // SubPanel
            // 
            this.SubPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubPanel.Location = new System.Drawing.Point(3, 3);
            this.SubPanel.Name = "SubPanel";
            this.SubPanel.Size = new System.Drawing.Size(1910, 997);
            this.SubPanel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1916, 1041);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.TabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "首页v20231113";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.MainTabPage.ResumeLayout(false);
            this.SubTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 主界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 调试界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通信界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SummaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IO界面ToolStripMenuItem;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage MainTabPage;
        private System.Windows.Forms.TabPage SubTabPage;
        private System.Windows.Forms.Panel SubPanel;
        private System.Windows.Forms.ToolStripMenuItem 权限登录ToolStripMenuItem;
    }
}

