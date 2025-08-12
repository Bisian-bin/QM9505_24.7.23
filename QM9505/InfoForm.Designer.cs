namespace QM9505
{
    partial class InfoForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lotText = new System.Windows.Forms.TextBox();
            this.trayNumText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(70, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lot号:";
            // 
            // lotText
            // 
            this.lotText.Location = new System.Drawing.Point(132, 47);
            this.lotText.Multiline = true;
            this.lotText.Name = "lotText";
            this.lotText.Size = new System.Drawing.Size(150, 33);
            this.lotText.TabIndex = 1;
            // 
            // trayNumText
            // 
            this.trayNumText.Location = new System.Drawing.Point(132, 100);
            this.trayNumText.Multiline = true;
            this.trayNumText.Name = "trayNumText";
            this.trayNumText.Size = new System.Drawing.Size(150, 33);
            this.trayNumText.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(70, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "盘数:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(261, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 34);
            this.button1.TabIndex = 4;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(351, 214);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trayNumText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lotText);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InfoForm";
            this.Text = "InfoForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.InfoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox lotText;
        private System.Windows.Forms.TextBox trayNumText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}