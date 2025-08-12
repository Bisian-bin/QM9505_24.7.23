namespace QM9505
{
    partial class TCPForm
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
            this.components = new System.ComponentModel.Container();
            this.timerScan = new System.Windows.Forms.Timer(this.components);
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.photoDataClean = new System.Windows.Forms.Button();
            this.photoClientPort = new System.Windows.Forms.Label();
            this.photoClientIP = new System.Windows.Forms.Label();
            this.photostate = new System.Windows.Forms.Label();
            this.photoUnconnect = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.photoConnect = new System.Windows.Forms.Button();
            this.photoPort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.photoIP = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.photoMsgRec = new System.Windows.Forms.TextBox();
            this.btnPhotoMegSend = new System.Windows.Forms.Button();
            this.photoMegSend = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.robotRecord = new System.Windows.Forms.TextBox();
            this.btnTobotResst = new System.Windows.Forms.Button();
            this.robotDataClean = new System.Windows.Forms.Button();
            this.robotClientPort = new System.Windows.Forms.Label();
            this.robotClientIP = new System.Windows.Forms.Label();
            this.robotUnconnect = new System.Windows.Forms.Button();
            this.robotstate = new System.Windows.Forms.Label();
            this.robotConnect = new System.Windows.Forms.Button();
            this.robotPort = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.robotIP = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.robotMsgRec = new System.Windows.Forms.TextBox();
            this.btnRobotMegSend = new System.Windows.Forms.Button();
            this.robotMegSend = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.qrDataClean = new System.Windows.Forms.Button();
            this.qrClientPort = new System.Windows.Forms.Label();
            this.qrClientIP = new System.Windows.Forms.Label();
            this.qrstate = new System.Windows.Forms.Label();
            this.qrUnconnect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.qrConnect = new System.Windows.Forms.Button();
            this.qrPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.qrIP = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.qrMsgRec = new System.Windows.Forms.TextBox();
            this.btnQRMegSend = new System.Windows.Forms.Button();
            this.qrMegSend = new System.Windows.Forms.TextBox();
            this.TabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.tabPage1);
            this.TabControl1.Controls.Add(this.tabPage2);
            this.TabControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TabControl1.Location = new System.Drawing.Point(8, 9);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(1890, 976);
            this.TabControl1.TabIndex = 99;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.groupBox10);
            this.tabPage1.Controls.Add(this.groupBox9);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1882, 946);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "通信设置1";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.photoDataClean);
            this.groupBox10.Controls.Add(this.photoClientPort);
            this.groupBox10.Controls.Add(this.photoClientIP);
            this.groupBox10.Controls.Add(this.photostate);
            this.groupBox10.Controls.Add(this.photoUnconnect);
            this.groupBox10.Controls.Add(this.label13);
            this.groupBox10.Controls.Add(this.label14);
            this.groupBox10.Controls.Add(this.photoConnect);
            this.groupBox10.Controls.Add(this.photoPort);
            this.groupBox10.Controls.Add(this.label11);
            this.groupBox10.Controls.Add(this.photoIP);
            this.groupBox10.Controls.Add(this.label12);
            this.groupBox10.Controls.Add(this.photoMsgRec);
            this.groupBox10.Controls.Add(this.btnPhotoMegSend);
            this.groupBox10.Controls.Add(this.photoMegSend);
            this.groupBox10.Location = new System.Drawing.Point(3, 13);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(706, 715);
            this.groupBox10.TabIndex = 100;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Photo通信(服务端)";
            // 
            // photoDataClean
            // 
            this.photoDataClean.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.photoDataClean.Location = new System.Drawing.Point(584, 372);
            this.photoDataClean.Margin = new System.Windows.Forms.Padding(2);
            this.photoDataClean.Name = "photoDataClean";
            this.photoDataClean.Size = new System.Drawing.Size(112, 43);
            this.photoDataClean.TabIndex = 23;
            this.photoDataClean.Text = "清空";
            this.photoDataClean.UseVisualStyleBackColor = true;
            this.photoDataClean.Click += new System.EventHandler(this.photoDataClean_Click);
            // 
            // photoClientPort
            // 
            this.photoClientPort.AutoSize = true;
            this.photoClientPort.Location = new System.Drawing.Point(476, 108);
            this.photoClientPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.photoClientPort.Name = "photoClientPort";
            this.photoClientPort.Size = new System.Drawing.Size(104, 16);
            this.photoClientPort.TabIndex = 22;
            this.photoClientPort.Text = "客户端port:";
            // 
            // photoClientIP
            // 
            this.photoClientIP.AutoSize = true;
            this.photoClientIP.Location = new System.Drawing.Point(476, 39);
            this.photoClientIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.photoClientIP.Name = "photoClientIP";
            this.photoClientIP.Size = new System.Drawing.Size(86, 16);
            this.photoClientIP.TabIndex = 21;
            this.photoClientIP.Text = "客户端IP:";
            // 
            // photostate
            // 
            this.photostate.AutoSize = true;
            this.photostate.Location = new System.Drawing.Point(281, 114);
            this.photostate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.photostate.Name = "photostate";
            this.photostate.Size = new System.Drawing.Size(110, 16);
            this.photostate.TabIndex = 20;
            this.photostate.Text = "状态：未连接";
            // 
            // photoUnconnect
            // 
            this.photoUnconnect.Location = new System.Drawing.Point(160, 104);
            this.photoUnconnect.Margin = new System.Windows.Forms.Padding(2);
            this.photoUnconnect.Name = "photoUnconnect";
            this.photoUnconnect.Size = new System.Drawing.Size(91, 36);
            this.photoUnconnect.TabIndex = 19;
            this.photoUnconnect.Text = "停止监听";
            this.photoUnconnect.UseVisualStyleBackColor = true;
            this.photoUnconnect.Click += new System.EventHandler(this.photoUnconnect_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 167);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 16);
            this.label13.TabIndex = 18;
            this.label13.Text = "数据接受：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 444);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 16);
            this.label14.TabIndex = 17;
            this.label14.Text = "数据发送：";
            // 
            // photoConnect
            // 
            this.photoConnect.Location = new System.Drawing.Point(17, 104);
            this.photoConnect.Margin = new System.Windows.Forms.Padding(2);
            this.photoConnect.Name = "photoConnect";
            this.photoConnect.Size = new System.Drawing.Size(91, 36);
            this.photoConnect.TabIndex = 16;
            this.photoConnect.Text = "启动监听";
            this.photoConnect.UseVisualStyleBackColor = true;
            this.photoConnect.Click += new System.EventHandler(this.photoConnect_Click);
            // 
            // photoPort
            // 
            this.photoPort.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.photoPort.Location = new System.Drawing.Point(336, 34);
            this.photoPort.Margin = new System.Windows.Forms.Padding(2);
            this.photoPort.Multiline = true;
            this.photoPort.Name = "photoPort";
            this.photoPort.Size = new System.Drawing.Size(95, 36);
            this.photoPort.TabIndex = 14;
            this.photoPort.Text = "8000";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(281, 45);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "端口:";
            // 
            // photoIP
            // 
            this.photoIP.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.photoIP.Location = new System.Drawing.Point(87, 34);
            this.photoIP.Margin = new System.Windows.Forms.Padding(2);
            this.photoIP.Multiline = true;
            this.photoIP.Name = "photoIP";
            this.photoIP.Size = new System.Drawing.Size(164, 36);
            this.photoIP.TabIndex = 12;
            this.photoIP.Text = "192.168.3.10";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 45);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 16);
            this.label12.TabIndex = 11;
            this.label12.Text = "IP地址:";
            // 
            // photoMsgRec
            // 
            this.photoMsgRec.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.photoMsgRec.Location = new System.Drawing.Point(17, 201);
            this.photoMsgRec.Margin = new System.Windows.Forms.Padding(2);
            this.photoMsgRec.Multiline = true;
            this.photoMsgRec.Name = "photoMsgRec";
            this.photoMsgRec.ReadOnly = true;
            this.photoMsgRec.Size = new System.Drawing.Size(563, 214);
            this.photoMsgRec.TabIndex = 10;
            // 
            // btnPhotoMegSend
            // 
            this.btnPhotoMegSend.Location = new System.Drawing.Point(584, 655);
            this.btnPhotoMegSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnPhotoMegSend.Name = "btnPhotoMegSend";
            this.btnPhotoMegSend.Size = new System.Drawing.Size(112, 43);
            this.btnPhotoMegSend.TabIndex = 9;
            this.btnPhotoMegSend.Text = "发送";
            this.btnPhotoMegSend.UseVisualStyleBackColor = true;
            this.btnPhotoMegSend.Click += new System.EventHandler(this.btnPhotoMegSend_Click);
            // 
            // photoMegSend
            // 
            this.photoMegSend.Location = new System.Drawing.Point(17, 481);
            this.photoMegSend.Margin = new System.Windows.Forms.Padding(2);
            this.photoMegSend.Multiline = true;
            this.photoMegSend.Name = "photoMegSend";
            this.photoMegSend.Size = new System.Drawing.Size(563, 217);
            this.photoMegSend.TabIndex = 8;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.robotRecord);
            this.groupBox9.Controls.Add(this.btnTobotResst);
            this.groupBox9.Controls.Add(this.robotDataClean);
            this.groupBox9.Controls.Add(this.robotClientPort);
            this.groupBox9.Controls.Add(this.robotClientIP);
            this.groupBox9.Controls.Add(this.robotUnconnect);
            this.groupBox9.Controls.Add(this.robotstate);
            this.groupBox9.Controls.Add(this.robotConnect);
            this.groupBox9.Controls.Add(this.robotPort);
            this.groupBox9.Controls.Add(this.label10);
            this.groupBox9.Controls.Add(this.robotIP);
            this.groupBox9.Controls.Add(this.label9);
            this.groupBox9.Controls.Add(this.robotMsgRec);
            this.groupBox9.Controls.Add(this.btnRobotMegSend);
            this.groupBox9.Controls.Add(this.robotMegSend);
            this.groupBox9.Controls.Add(this.label6);
            this.groupBox9.Controls.Add(this.label5);
            this.groupBox9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox9.Location = new System.Drawing.Point(719, 13);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox9.Size = new System.Drawing.Size(687, 715);
            this.groupBox9.TabIndex = 99;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Robot通信(服务端)";
            // 
            // robotRecord
            // 
            this.robotRecord.Location = new System.Drawing.Point(272, 157);
            this.robotRecord.Multiline = true;
            this.robotRecord.Name = "robotRecord";
            this.robotRecord.Size = new System.Drawing.Size(291, 36);
            this.robotRecord.TabIndex = 27;
            // 
            // btnTobotResst
            // 
            this.btnTobotResst.Location = new System.Drawing.Point(155, 157);
            this.btnTobotResst.Margin = new System.Windows.Forms.Padding(2);
            this.btnTobotResst.Name = "btnTobotResst";
            this.btnTobotResst.Size = new System.Drawing.Size(110, 36);
            this.btnTobotResst.TabIndex = 26;
            this.btnTobotResst.Text = "机械手复位";
            this.btnTobotResst.UseVisualStyleBackColor = true;
            this.btnTobotResst.Click += new System.EventHandler(this.btnTobotResst_Click);
            // 
            // robotDataClean
            // 
            this.robotDataClean.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.robotDataClean.Location = new System.Drawing.Point(567, 372);
            this.robotDataClean.Margin = new System.Windows.Forms.Padding(2);
            this.robotDataClean.Name = "robotDataClean";
            this.robotDataClean.Size = new System.Drawing.Size(112, 43);
            this.robotDataClean.TabIndex = 15;
            this.robotDataClean.Text = "清空";
            this.robotDataClean.UseVisualStyleBackColor = true;
            this.robotDataClean.Click += new System.EventHandler(this.robotDataClean_Click);
            // 
            // robotClientPort
            // 
            this.robotClientPort.AutoSize = true;
            this.robotClientPort.Location = new System.Drawing.Point(459, 114);
            this.robotClientPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.robotClientPort.Name = "robotClientPort";
            this.robotClientPort.Size = new System.Drawing.Size(104, 16);
            this.robotClientPort.TabIndex = 14;
            this.robotClientPort.Text = "客户端port:";
            // 
            // robotClientIP
            // 
            this.robotClientIP.AutoSize = true;
            this.robotClientIP.Location = new System.Drawing.Point(459, 45);
            this.robotClientIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.robotClientIP.Name = "robotClientIP";
            this.robotClientIP.Size = new System.Drawing.Size(86, 16);
            this.robotClientIP.TabIndex = 13;
            this.robotClientIP.Text = "客户端IP:";
            // 
            // robotUnconnect
            // 
            this.robotUnconnect.Location = new System.Drawing.Point(155, 104);
            this.robotUnconnect.Margin = new System.Windows.Forms.Padding(2);
            this.robotUnconnect.Name = "robotUnconnect";
            this.robotUnconnect.Size = new System.Drawing.Size(91, 36);
            this.robotUnconnect.TabIndex = 12;
            this.robotUnconnect.Text = "停止监听";
            this.robotUnconnect.UseVisualStyleBackColor = true;
            this.robotUnconnect.Click += new System.EventHandler(this.robotUnconnect_Click);
            // 
            // robotstate
            // 
            this.robotstate.AutoSize = true;
            this.robotstate.Location = new System.Drawing.Point(269, 114);
            this.robotstate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.robotstate.Name = "robotstate";
            this.robotstate.Size = new System.Drawing.Size(110, 16);
            this.robotstate.TabIndex = 11;
            this.robotstate.Text = "状态：未连接";
            // 
            // robotConnect
            // 
            this.robotConnect.Location = new System.Drawing.Point(12, 104);
            this.robotConnect.Margin = new System.Windows.Forms.Padding(2);
            this.robotConnect.Name = "robotConnect";
            this.robotConnect.Size = new System.Drawing.Size(91, 36);
            this.robotConnect.TabIndex = 10;
            this.robotConnect.Text = "启动监听";
            this.robotConnect.UseVisualStyleBackColor = true;
            this.robotConnect.Click += new System.EventHandler(this.robotConnect_Click);
            // 
            // robotPort
            // 
            this.robotPort.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.robotPort.Location = new System.Drawing.Point(324, 34);
            this.robotPort.Margin = new System.Windows.Forms.Padding(2);
            this.robotPort.Multiline = true;
            this.robotPort.Name = "robotPort";
            this.robotPort.Size = new System.Drawing.Size(95, 36);
            this.robotPort.TabIndex = 8;
            this.robotPort.Text = "8001";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(269, 45);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 16);
            this.label10.TabIndex = 7;
            this.label10.Text = "端口:";
            // 
            // robotIP
            // 
            this.robotIP.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.robotIP.Location = new System.Drawing.Point(82, 34);
            this.robotIP.Margin = new System.Windows.Forms.Padding(2);
            this.robotIP.Multiline = true;
            this.robotIP.Name = "robotIP";
            this.robotIP.Size = new System.Drawing.Size(164, 36);
            this.robotIP.TabIndex = 6;
            this.robotIP.Text = "192.168.3.10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 44);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 16);
            this.label9.TabIndex = 5;
            this.label9.Text = "IP地址:";
            // 
            // robotMsgRec
            // 
            this.robotMsgRec.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.robotMsgRec.Location = new System.Drawing.Point(12, 201);
            this.robotMsgRec.Margin = new System.Windows.Forms.Padding(2);
            this.robotMsgRec.Multiline = true;
            this.robotMsgRec.Name = "robotMsgRec";
            this.robotMsgRec.ReadOnly = true;
            this.robotMsgRec.Size = new System.Drawing.Size(551, 214);
            this.robotMsgRec.TabIndex = 4;
            // 
            // btnRobotMegSend
            // 
            this.btnRobotMegSend.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRobotMegSend.Location = new System.Drawing.Point(567, 654);
            this.btnRobotMegSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnRobotMegSend.Name = "btnRobotMegSend";
            this.btnRobotMegSend.Size = new System.Drawing.Size(112, 43);
            this.btnRobotMegSend.TabIndex = 3;
            this.btnRobotMegSend.Text = "发送";
            this.btnRobotMegSend.UseVisualStyleBackColor = true;
            this.btnRobotMegSend.Click += new System.EventHandler(this.btnRobotMegSend_Click);
            // 
            // robotMegSend
            // 
            this.robotMegSend.Location = new System.Drawing.Point(12, 481);
            this.robotMegSend.Margin = new System.Windows.Forms.Padding(2);
            this.robotMegSend.Multiline = true;
            this.robotMegSend.Name = "robotMegSend";
            this.robotMegSend.Size = new System.Drawing.Size(551, 217);
            this.robotMegSend.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 444);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "数据发送：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 167);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "数据接受：";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1882, 946);
            this.tabPage2.TabIndex = 5;
            this.tabPage2.Text = "通信设置2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.qrDataClean);
            this.groupBox1.Controls.Add(this.qrClientPort);
            this.groupBox1.Controls.Add(this.qrClientIP);
            this.groupBox1.Controls.Add(this.qrstate);
            this.groupBox1.Controls.Add(this.qrUnconnect);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.qrConnect);
            this.groupBox1.Controls.Add(this.qrPort);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.qrIP);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.qrMsgRec);
            this.groupBox1.Controls.Add(this.btnQRMegSend);
            this.groupBox1.Controls.Add(this.qrMegSend);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(706, 715);
            this.groupBox1.TabIndex = 101;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "QR通信(客户端)";
            // 
            // qrDataClean
            // 
            this.qrDataClean.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qrDataClean.Location = new System.Drawing.Point(584, 372);
            this.qrDataClean.Margin = new System.Windows.Forms.Padding(2);
            this.qrDataClean.Name = "qrDataClean";
            this.qrDataClean.Size = new System.Drawing.Size(112, 43);
            this.qrDataClean.TabIndex = 23;
            this.qrDataClean.Text = "清空";
            this.qrDataClean.UseVisualStyleBackColor = true;
            this.qrDataClean.Click += new System.EventHandler(this.qrDataClean_Click);
            // 
            // qrClientPort
            // 
            this.qrClientPort.AutoSize = true;
            this.qrClientPort.Location = new System.Drawing.Point(476, 108);
            this.qrClientPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.qrClientPort.Name = "qrClientPort";
            this.qrClientPort.Size = new System.Drawing.Size(87, 16);
            this.qrClientPort.TabIndex = 22;
            this.qrClientPort.Text = "本地port:";
            // 
            // qrClientIP
            // 
            this.qrClientIP.AutoSize = true;
            this.qrClientIP.Location = new System.Drawing.Point(476, 39);
            this.qrClientIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.qrClientIP.Name = "qrClientIP";
            this.qrClientIP.Size = new System.Drawing.Size(69, 16);
            this.qrClientIP.TabIndex = 21;
            this.qrClientIP.Text = "本地IP:";
            // 
            // qrstate
            // 
            this.qrstate.AutoSize = true;
            this.qrstate.Location = new System.Drawing.Point(281, 114);
            this.qrstate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.qrstate.Name = "qrstate";
            this.qrstate.Size = new System.Drawing.Size(110, 16);
            this.qrstate.TabIndex = 20;
            this.qrstate.Text = "状态：未连接";
            // 
            // qrUnconnect
            // 
            this.qrUnconnect.Location = new System.Drawing.Point(160, 104);
            this.qrUnconnect.Margin = new System.Windows.Forms.Padding(2);
            this.qrUnconnect.Name = "qrUnconnect";
            this.qrUnconnect.Size = new System.Drawing.Size(91, 36);
            this.qrUnconnect.TabIndex = 19;
            this.qrUnconnect.Text = "断 开";
            this.qrUnconnect.UseVisualStyleBackColor = true;
            this.qrUnconnect.Click += new System.EventHandler(this.qrUnconnect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 167);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "数据接受：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 444);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "数据发送：";
            // 
            // qrConnect
            // 
            this.qrConnect.Location = new System.Drawing.Point(17, 104);
            this.qrConnect.Margin = new System.Windows.Forms.Padding(2);
            this.qrConnect.Name = "qrConnect";
            this.qrConnect.Size = new System.Drawing.Size(91, 36);
            this.qrConnect.TabIndex = 16;
            this.qrConnect.Text = "连 接";
            this.qrConnect.UseVisualStyleBackColor = true;
            this.qrConnect.Click += new System.EventHandler(this.qrConnect_Click);
            // 
            // qrPort
            // 
            this.qrPort.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qrPort.Location = new System.Drawing.Point(336, 34);
            this.qrPort.Margin = new System.Windows.Forms.Padding(2);
            this.qrPort.Multiline = true;
            this.qrPort.Name = "qrPort";
            this.qrPort.Size = new System.Drawing.Size(95, 36);
            this.qrPort.TabIndex = 14;
            this.qrPort.Text = "8010";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(281, 45);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 16);
            this.label8.TabIndex = 13;
            this.label8.Text = "端口:";
            // 
            // qrIP
            // 
            this.qrIP.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qrIP.Location = new System.Drawing.Point(87, 34);
            this.qrIP.Margin = new System.Windows.Forms.Padding(2);
            this.qrIP.Multiline = true;
            this.qrIP.Name = "qrIP";
            this.qrIP.Size = new System.Drawing.Size(164, 36);
            this.qrIP.TabIndex = 12;
            this.qrIP.Text = "192.168.3.100";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 45);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 16);
            this.label15.TabIndex = 11;
            this.label15.Text = "IP地址:";
            // 
            // qrMsgRec
            // 
            this.qrMsgRec.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qrMsgRec.Location = new System.Drawing.Point(17, 201);
            this.qrMsgRec.Margin = new System.Windows.Forms.Padding(2);
            this.qrMsgRec.Multiline = true;
            this.qrMsgRec.Name = "qrMsgRec";
            this.qrMsgRec.ReadOnly = true;
            this.qrMsgRec.Size = new System.Drawing.Size(563, 214);
            this.qrMsgRec.TabIndex = 10;
            // 
            // btnQRMegSend
            // 
            this.btnQRMegSend.Location = new System.Drawing.Point(584, 655);
            this.btnQRMegSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnQRMegSend.Name = "btnQRMegSend";
            this.btnQRMegSend.Size = new System.Drawing.Size(112, 43);
            this.btnQRMegSend.TabIndex = 9;
            this.btnQRMegSend.Text = "发送";
            this.btnQRMegSend.UseVisualStyleBackColor = true;
            this.btnQRMegSend.Click += new System.EventHandler(this.btnQRMegSend_Click);
            // 
            // qrMegSend
            // 
            this.qrMegSend.Location = new System.Drawing.Point(17, 481);
            this.qrMegSend.Margin = new System.Windows.Forms.Padding(2);
            this.qrMegSend.Multiline = true;
            this.qrMegSend.Name = "qrMegSend";
            this.qrMegSend.Size = new System.Drawing.Size(563, 217);
            this.qrMegSend.TabIndex = 8;
            // 
            // TCPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1910, 997);
            this.Controls.Add(this.TabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TCPForm";
            this.Text = "TCPForm";
            this.Load += new System.EventHandler(this.TCPForm_Load);
            this.TabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerScan;
        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button photoDataClean;
        private System.Windows.Forms.Label photoClientPort;
        private System.Windows.Forms.Label photoClientIP;
        private System.Windows.Forms.Label photostate;
        private System.Windows.Forms.Button photoUnconnect;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button photoConnect;
        private System.Windows.Forms.TextBox photoPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox photoIP;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox photoMsgRec;
        private System.Windows.Forms.Button btnPhotoMegSend;
        private System.Windows.Forms.TextBox photoMegSend;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button robotDataClean;
        private System.Windows.Forms.Label robotClientPort;
        private System.Windows.Forms.Label robotClientIP;
        private System.Windows.Forms.Button robotUnconnect;
        private System.Windows.Forms.Label robotstate;
        private System.Windows.Forms.Button robotConnect;
        private System.Windows.Forms.TextBox robotPort;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox robotIP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox robotMsgRec;
        private System.Windows.Forms.Button btnRobotMegSend;
        private System.Windows.Forms.TextBox robotMegSend;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTobotResst;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button qrDataClean;
        private System.Windows.Forms.Label qrClientPort;
        private System.Windows.Forms.Label qrClientIP;
        private System.Windows.Forms.Label qrstate;
        private System.Windows.Forms.Button qrUnconnect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button qrConnect;
        private System.Windows.Forms.TextBox qrPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox qrIP;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox qrMsgRec;
        private System.Windows.Forms.Button btnQRMegSend;
        private System.Windows.Forms.TextBox qrMegSend;
        private System.Windows.Forms.TextBox robotRecord;
    }
}