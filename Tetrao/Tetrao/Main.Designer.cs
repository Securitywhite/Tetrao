namespace Tetrao
{
    partial class Main
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
            this.tbControl = new System.Windows.Forms.TabControl();
            this.tbFeatures = new System.Windows.Forms.TabPage();
            this.tbConFeatures = new System.Windows.Forms.TabControl();
            this.tbInfo = new System.Windows.Forms.TabPage();
            this.GrpBxGameInfo = new System.Windows.Forms.GroupBox();
            this.lblRoom = new System.Windows.Forms.Label();
            this.txtCurrRoomId = new System.Windows.Forms.TextBox();
            this.txtSwfbuild = new System.Windows.Forms.TextBox();
            this.txtGameHost = new System.Windows.Forms.TextBox();
            this.txtGamePort = new System.Windows.Forms.TextBox();
            this.txtGameIP = new System.Windows.Forms.TextBox();
            this.lblGameIP = new System.Windows.Forms.Label();
            this.lblSwfBuild = new System.Windows.Forms.Label();
            this.lblGamePort = new System.Windows.Forms.Label();
            this.lblGameHost = new System.Windows.Forms.Label();
            this.tbAutoSim = new System.Windows.Forms.TabPage();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.btnClick = new System.Windows.Forms.Button();
            this.chkShout = new System.Windows.Forms.CheckBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.lblRoomID = new System.Windows.Forms.Label();
            this.txtRoomID = new System.Windows.Forms.TextBox();
            this.btnLoadRoom = new System.Windows.Forms.Button();
            this.tbAccManager = new System.Windows.Forms.TabPage();
            this.CloneList = new System.Windows.Forms.ListView();
            this.HabboAccount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HabboID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoginAccount = new System.Windows.Forms.Button();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.Hotel = new System.Windows.Forms.ComboBox();
            this.lblHotel = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.MDI_client = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tlStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlStripActiveBots = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblProgUser = new System.Windows.Forms.Label();
            this.Coordinates = new System.Windows.Forms.Timer(this.components);
            this.tbExtras = new System.Windows.Forms.TabControl();
            this.tbSettings = new System.Windows.Forms.TabPage();
            this.chkProxyConnect = new System.Windows.Forms.CheckBox();
            this.chkEnableConsole = new System.Windows.Forms.CheckBox();
            this.chkAllAccounts = new System.Windows.Forms.CheckBox();
            this.tbConsole = new System.Windows.Forms.TabPage();
            this.rtbConsole = new System.Windows.Forms.RichTextBox();
            this.tbControl.SuspendLayout();
            this.tbFeatures.SuspendLayout();
            this.tbConFeatures.SuspendLayout();
            this.tbInfo.SuspendLayout();
            this.GrpBxGameInfo.SuspendLayout();
            this.tbAutoSim.SuspendLayout();
            this.tbAccManager.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tbExtras.SuspendLayout();
            this.tbSettings.SuspendLayout();
            this.tbConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbControl
            // 
            this.tbControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbControl.Controls.Add(this.tbFeatures);
            this.tbControl.Controls.Add(this.tbAccManager);
            this.tbControl.Location = new System.Drawing.Point(979, 24);
            this.tbControl.Name = "tbControl";
            this.tbControl.SelectedIndex = 0;
            this.tbControl.Size = new System.Drawing.Size(211, 512);
            this.tbControl.TabIndex = 1;
            // 
            // tbFeatures
            // 
            this.tbFeatures.Controls.Add(this.tbConFeatures);
            this.tbFeatures.Location = new System.Drawing.Point(4, 22);
            this.tbFeatures.Name = "tbFeatures";
            this.tbFeatures.Padding = new System.Windows.Forms.Padding(3);
            this.tbFeatures.Size = new System.Drawing.Size(203, 486);
            this.tbFeatures.TabIndex = 0;
            this.tbFeatures.Text = "Features";
            this.tbFeatures.UseVisualStyleBackColor = true;
            // 
            // tbConFeatures
            // 
            this.tbConFeatures.Controls.Add(this.tbInfo);
            this.tbConFeatures.Controls.Add(this.tbAutoSim);
            this.tbConFeatures.Location = new System.Drawing.Point(3, 6);
            this.tbConFeatures.Name = "tbConFeatures";
            this.tbConFeatures.SelectedIndex = 0;
            this.tbConFeatures.Size = new System.Drawing.Size(197, 477);
            this.tbConFeatures.TabIndex = 0;
            // 
            // tbInfo
            // 
            this.tbInfo.Controls.Add(this.GrpBxGameInfo);
            this.tbInfo.Location = new System.Drawing.Point(4, 22);
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tbInfo.Size = new System.Drawing.Size(189, 451);
            this.tbInfo.TabIndex = 0;
            this.tbInfo.Text = "Data";
            this.tbInfo.UseVisualStyleBackColor = true;
            // 
            // GrpBxGameInfo
            // 
            this.GrpBxGameInfo.Controls.Add(this.lblRoom);
            this.GrpBxGameInfo.Controls.Add(this.txtCurrRoomId);
            this.GrpBxGameInfo.Controls.Add(this.txtSwfbuild);
            this.GrpBxGameInfo.Controls.Add(this.txtGameHost);
            this.GrpBxGameInfo.Controls.Add(this.txtGamePort);
            this.GrpBxGameInfo.Controls.Add(this.txtGameIP);
            this.GrpBxGameInfo.Controls.Add(this.lblGameIP);
            this.GrpBxGameInfo.Controls.Add(this.lblSwfBuild);
            this.GrpBxGameInfo.Controls.Add(this.lblGamePort);
            this.GrpBxGameInfo.Controls.Add(this.lblGameHost);
            this.GrpBxGameInfo.Location = new System.Drawing.Point(6, 6);
            this.GrpBxGameInfo.Name = "GrpBxGameInfo";
            this.GrpBxGameInfo.Size = new System.Drawing.Size(172, 220);
            this.GrpBxGameInfo.TabIndex = 4;
            this.GrpBxGameInfo.TabStop = false;
            this.GrpBxGameInfo.Text = "Game Info";
            // 
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Location = new System.Drawing.Point(9, 176);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(89, 13);
            this.lblRoom.TabIndex = 9;
            this.lblRoom.Text = "Current Room ID:";
            // 
            // txtCurrRoomId
            // 
            this.txtCurrRoomId.Location = new System.Drawing.Point(9, 194);
            this.txtCurrRoomId.Name = "txtCurrRoomId";
            this.txtCurrRoomId.Size = new System.Drawing.Size(157, 20);
            this.txtCurrRoomId.TabIndex = 8;
            // 
            // txtSwfbuild
            // 
            this.txtSwfbuild.Location = new System.Drawing.Point(6, 149);
            this.txtSwfbuild.Name = "txtSwfbuild";
            this.txtSwfbuild.Size = new System.Drawing.Size(160, 20);
            this.txtSwfbuild.TabIndex = 7;
            // 
            // txtGameHost
            // 
            this.txtGameHost.Location = new System.Drawing.Point(6, 110);
            this.txtGameHost.Name = "txtGameHost";
            this.txtGameHost.Size = new System.Drawing.Size(160, 20);
            this.txtGameHost.TabIndex = 6;
            // 
            // txtGamePort
            // 
            this.txtGamePort.Location = new System.Drawing.Point(6, 71);
            this.txtGamePort.Name = "txtGamePort";
            this.txtGamePort.Size = new System.Drawing.Size(160, 20);
            this.txtGamePort.TabIndex = 5;
            // 
            // txtGameIP
            // 
            this.txtGameIP.Location = new System.Drawing.Point(6, 32);
            this.txtGameIP.Name = "txtGameIP";
            this.txtGameIP.Size = new System.Drawing.Size(160, 20);
            this.txtGameIP.TabIndex = 4;
            // 
            // lblGameIP
            // 
            this.lblGameIP.AutoSize = true;
            this.lblGameIP.Location = new System.Drawing.Point(6, 16);
            this.lblGameIP.Name = "lblGameIP";
            this.lblGameIP.Size = new System.Drawing.Size(51, 13);
            this.lblGameIP.TabIndex = 0;
            this.lblGameIP.Text = "Game IP:";
            // 
            // lblSwfBuild
            // 
            this.lblSwfBuild.AutoSize = true;
            this.lblSwfBuild.Location = new System.Drawing.Point(6, 133);
            this.lblSwfBuild.Name = "lblSwfBuild";
            this.lblSwfBuild.Size = new System.Drawing.Size(60, 13);
            this.lblSwfBuild.TabIndex = 3;
            this.lblSwfBuild.Text = "SWF Build:";
            // 
            // lblGamePort
            // 
            this.lblGamePort.AutoSize = true;
            this.lblGamePort.Location = new System.Drawing.Point(6, 55);
            this.lblGamePort.Name = "lblGamePort";
            this.lblGamePort.Size = new System.Drawing.Size(60, 13);
            this.lblGamePort.TabIndex = 1;
            this.lblGamePort.Text = "Game Port:";
            // 
            // lblGameHost
            // 
            this.lblGameHost.AutoSize = true;
            this.lblGameHost.Location = new System.Drawing.Point(3, 94);
            this.lblGameHost.Name = "lblGameHost";
            this.lblGameHost.Size = new System.Drawing.Size(63, 13);
            this.lblGameHost.TabIndex = 2;
            this.lblGameHost.Text = "Game Host:";
            // 
            // tbAutoSim
            // 
            this.tbAutoSim.Controls.Add(this.txtX);
            this.tbAutoSim.Controls.Add(this.txtY);
            this.tbAutoSim.Controls.Add(this.btnClick);
            this.tbAutoSim.Controls.Add(this.chkShout);
            this.tbAutoSim.Controls.Add(this.txtMessage);
            this.tbAutoSim.Controls.Add(this.btnSendMessage);
            this.tbAutoSim.Controls.Add(this.lblRoomID);
            this.tbAutoSim.Controls.Add(this.txtRoomID);
            this.tbAutoSim.Controls.Add(this.btnLoadRoom);
            this.tbAutoSim.Location = new System.Drawing.Point(4, 22);
            this.tbAutoSim.Name = "tbAutoSim";
            this.tbAutoSim.Padding = new System.Windows.Forms.Padding(3);
            this.tbAutoSim.Size = new System.Drawing.Size(189, 451);
            this.tbAutoSim.TabIndex = 1;
            this.tbAutoSim.Text = "Automation and Simulation";
            this.tbAutoSim.UseVisualStyleBackColor = true;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(36, 308);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(47, 20);
            this.txtX.TabIndex = 9;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(89, 308);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(47, 20);
            this.txtY.TabIndex = 8;
            // 
            // btnClick
            // 
            this.btnClick.Location = new System.Drawing.Point(50, 334);
            this.btnClick.Name = "btnClick";
            this.btnClick.Size = new System.Drawing.Size(72, 23);
            this.btnClick.TabIndex = 6;
            this.btnClick.Text = "Click";
            this.btnClick.UseVisualStyleBackColor = true;
            this.btnClick.Click += new System.EventHandler(this.btnClick_Click);
            // 
            // chkShout
            // 
            this.chkShout.AutoSize = true;
            this.chkShout.Location = new System.Drawing.Point(16, 194);
            this.chkShout.Name = "chkShout";
            this.chkShout.Size = new System.Drawing.Size(80, 17);
            this.chkShout.TabIndex = 5;
            this.chkShout.Text = "Shout/Bold";
            this.chkShout.UseVisualStyleBackColor = true;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(16, 168);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(143, 20);
            this.txtMessage.TabIndex = 4;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(41, 217);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(95, 23);
            this.btnSendMessage.TabIndex = 3;
            this.btnSendMessage.Text = "Send Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // lblRoomID
            // 
            this.lblRoomID.AutoSize = true;
            this.lblRoomID.Location = new System.Drawing.Point(13, 58);
            this.lblRoomID.Name = "lblRoomID";
            this.lblRoomID.Size = new System.Drawing.Size(52, 13);
            this.lblRoomID.TabIndex = 2;
            this.lblRoomID.Text = "Room ID:";
            // 
            // txtRoomID
            // 
            this.txtRoomID.Location = new System.Drawing.Point(16, 74);
            this.txtRoomID.Name = "txtRoomID";
            this.txtRoomID.Size = new System.Drawing.Size(143, 20);
            this.txtRoomID.TabIndex = 1;
            // 
            // btnLoadRoom
            // 
            this.btnLoadRoom.Location = new System.Drawing.Point(50, 102);
            this.btnLoadRoom.Name = "btnLoadRoom";
            this.btnLoadRoom.Size = new System.Drawing.Size(72, 23);
            this.btnLoadRoom.TabIndex = 0;
            this.btnLoadRoom.Text = "Load Room";
            this.btnLoadRoom.UseVisualStyleBackColor = true;
            this.btnLoadRoom.Click += new System.EventHandler(this.btnLoadRoom_Click);
            // 
            // tbAccManager
            // 
            this.tbAccManager.Controls.Add(this.CloneList);
            this.tbAccManager.Controls.Add(this.btnSave);
            this.tbAccManager.Controls.Add(this.groupBox1);
            this.tbAccManager.Location = new System.Drawing.Point(4, 22);
            this.tbAccManager.Name = "tbAccManager";
            this.tbAccManager.Padding = new System.Windows.Forms.Padding(3);
            this.tbAccManager.Size = new System.Drawing.Size(203, 486);
            this.tbAccManager.TabIndex = 1;
            this.tbAccManager.Text = "Account Manager";
            this.tbAccManager.UseVisualStyleBackColor = true;
            // 
            // CloneList
            // 
            this.CloneList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HabboAccount,
            this.HabboID});
            this.CloneList.FullRowSelect = true;
            this.CloneList.GridLines = true;
            this.CloneList.Location = new System.Drawing.Point(3, 191);
            this.CloneList.MultiSelect = false;
            this.CloneList.Name = "CloneList";
            this.CloneList.Size = new System.Drawing.Size(197, 259);
            this.CloneList.TabIndex = 9;
            this.CloneList.UseCompatibleStateImageBehavior = false;
            this.CloneList.View = System.Windows.Forms.View.Details;
            this.CloneList.SelectedIndexChanged += new System.EventHandler(this.CloneList_SelectedIndexChanged);
            // 
            // HabboAccount
            // 
            this.HabboAccount.Text = "Email";
            this.HabboAccount.Width = 117;
            // 
            // HabboID
            // 
            this.HabboID.Text = "Username";
            this.HabboID.Width = 77;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(44, 456);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save List";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoginAccount);
            this.groupBox1.Controls.Add(this.txtPass);
            this.groupBox1.Controls.Add(this.Hotel);
            this.groupBox1.Controls.Add(this.lblHotel);
            this.groupBox1.Controls.Add(this.lblUser);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.lblPass);
            this.groupBox1.Location = new System.Drawing.Point(6, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 167);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            // 
            // btnLoginAccount
            // 
            this.btnLoginAccount.Location = new System.Drawing.Point(38, 137);
            this.btnLoginAccount.Name = "btnLoginAccount";
            this.btnLoginAccount.Size = new System.Drawing.Size(99, 23);
            this.btnLoginAccount.TabIndex = 6;
            this.btnLoginAccount.Text = "Login";
            this.btnLoginAccount.UseVisualStyleBackColor = true;
            this.btnLoginAccount.Click += new System.EventHandler(this.btnLoginAccount_Click);
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(6, 71);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(165, 20);
            this.txtPass.TabIndex = 1;
            // 
            // Hotel
            // 
            this.Hotel.FormattingEnabled = true;
            this.Hotel.Items.AddRange(new object[] {
            ".com",
            ".com.br",
            ".com.tr",
            ".de",
            ".dk",
            ".es",
            ".fi",
            ".fr",
            ".it",
            ".nl",
            ".no",
            ".se"});
            this.Hotel.Location = new System.Drawing.Point(6, 110);
            this.Hotel.Name = "Hotel";
            this.Hotel.Size = new System.Drawing.Size(165, 21);
            this.Hotel.TabIndex = 4;
            // 
            // lblHotel
            // 
            this.lblHotel.AutoSize = true;
            this.lblHotel.Location = new System.Drawing.Point(3, 94);
            this.lblHotel.Name = "lblHotel";
            this.lblHotel.Size = new System.Drawing.Size(35, 13);
            this.lblHotel.TabIndex = 5;
            this.lblHotel.Text = "Hotel:";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(6, 16);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(35, 13);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "Email:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(9, 32);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(165, 20);
            this.txtUser.TabIndex = 0;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(6, 55);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(56, 13);
            this.lblPass.TabIndex = 3;
            this.lblPass.Text = "Password:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.MDI_client);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(965, 633);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Clients";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MDI_client
            // 
            this.MDI_client.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MDI_client.Location = new System.Drawing.Point(6, 6);
            this.MDI_client.Name = "MDI_client";
            this.MDI_client.Size = new System.Drawing.Size(953, 621);
            this.MDI_client.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(4, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(973, 659);
            this.tabControl1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStripStatus,
            this.tlStripActiveBots,
            this.toolStripCoords});
            this.statusStrip1.Location = new System.Drawing.Point(0, 664);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1190, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tlStripStatus
            // 
            this.tlStripStatus.Name = "tlStripStatus";
            this.tlStripStatus.Size = new System.Drawing.Size(45, 17);
            this.tlStripStatus.Text = "Ready |";
            // 
            // tlStripActiveBots
            // 
            this.tlStripActiveBots.Name = "tlStripActiveBots";
            this.tlStripActiveBots.Size = new System.Drawing.Size(84, 17);
            this.tlStripActiveBots.Text = "Active Bots: 0 |";
            // 
            // toolStripCoords
            // 
            this.toolStripCoords.Name = "toolStripCoords";
            this.toolStripCoords.Size = new System.Drawing.Size(64, 17);
            this.toolStripCoords.Text = "X: {0} Y: {1}";
            // 
            // lblProgUser
            // 
            this.lblProgUser.AutoSize = true;
            this.lblProgUser.Location = new System.Drawing.Point(987, 2);
            this.lblProgUser.Name = "lblProgUser";
            this.lblProgUser.Size = new System.Drawing.Size(35, 13);
            this.lblProgUser.TabIndex = 4;
            this.lblProgUser.Text = "label5";
            // 
            // Coordinates
            // 
            this.Coordinates.Interval = 10;
            this.Coordinates.Tick += new System.EventHandler(this.Coordinates_Tick);
            // 
            // tbExtras
            // 
            this.tbExtras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExtras.Controls.Add(this.tbSettings);
            this.tbExtras.Controls.Add(this.tbConsole);
            this.tbExtras.Location = new System.Drawing.Point(979, 538);
            this.tbExtras.Name = "tbExtras";
            this.tbExtras.SelectedIndex = 0;
            this.tbExtras.Size = new System.Drawing.Size(211, 123);
            this.tbExtras.TabIndex = 5;
            // 
            // tbSettings
            // 
            this.tbSettings.Controls.Add(this.chkProxyConnect);
            this.tbSettings.Controls.Add(this.chkEnableConsole);
            this.tbSettings.Controls.Add(this.chkAllAccounts);
            this.tbSettings.Location = new System.Drawing.Point(4, 22);
            this.tbSettings.Name = "tbSettings";
            this.tbSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tbSettings.Size = new System.Drawing.Size(203, 97);
            this.tbSettings.TabIndex = 0;
            this.tbSettings.Text = "Settings";
            this.tbSettings.UseVisualStyleBackColor = true;
            // 
            // chkProxyConnect
            // 
            this.chkProxyConnect.AutoSize = true;
            this.chkProxyConnect.Location = new System.Drawing.Point(6, 29);
            this.chkProxyConnect.Name = "chkProxyConnect";
            this.chkProxyConnect.Size = new System.Drawing.Size(95, 17);
            this.chkProxyConnect.TabIndex = 2;
            this.chkProxyConnect.Text = "Proxy Connect";
            this.chkProxyConnect.UseVisualStyleBackColor = true;
            this.chkProxyConnect.CheckedChanged += new System.EventHandler(this.chkProxyConnect_CheckedChanged);
            // 
            // chkEnableConsole
            // 
            this.chkEnableConsole.AutoSize = true;
            this.chkEnableConsole.Location = new System.Drawing.Point(7, 52);
            this.chkEnableConsole.Name = "chkEnableConsole";
            this.chkEnableConsole.Size = new System.Drawing.Size(100, 17);
            this.chkEnableConsole.TabIndex = 1;
            this.chkEnableConsole.Text = "Enable Console";
            this.chkEnableConsole.UseVisualStyleBackColor = true;
            this.chkEnableConsole.CheckedChanged += new System.EventHandler(this.chkEnableConsole_CheckedChanged);
            // 
            // chkAllAccounts
            // 
            this.chkAllAccounts.AutoSize = true;
            this.chkAllAccounts.Checked = true;
            this.chkAllAccounts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllAccounts.Location = new System.Drawing.Point(7, 6);
            this.chkAllAccounts.Name = "chkAllAccounts";
            this.chkAllAccounts.Size = new System.Drawing.Size(85, 17);
            this.chkAllAccounts.TabIndex = 0;
            this.chkAllAccounts.Text = "All Accounts";
            this.chkAllAccounts.UseVisualStyleBackColor = true;
            this.chkAllAccounts.CheckedChanged += new System.EventHandler(this.chkAllAccounts_CheckedChanged);
            // 
            // tbConsole
            // 
            this.tbConsole.Controls.Add(this.rtbConsole);
            this.tbConsole.Location = new System.Drawing.Point(4, 22);
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.Padding = new System.Windows.Forms.Padding(3);
            this.tbConsole.Size = new System.Drawing.Size(203, 97);
            this.tbConsole.TabIndex = 1;
            this.tbConsole.Text = "Console";
            this.tbConsole.UseVisualStyleBackColor = true;
            // 
            // rtbConsole
            // 
            this.rtbConsole.BackColor = System.Drawing.SystemColors.MenuText;
            this.rtbConsole.Location = new System.Drawing.Point(3, 3);
            this.rtbConsole.Name = "rtbConsole";
            this.rtbConsole.Size = new System.Drawing.Size(192, 90);
            this.rtbConsole.TabIndex = 0;
            this.rtbConsole.Text = "";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 686);
            this.Controls.Add(this.tbExtras);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tbControl);
            this.Controls.Add(this.lblProgUser);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.Name = "Main";
            this.Text = "Tetrao ~ Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.tbControl.ResumeLayout(false);
            this.tbFeatures.ResumeLayout(false);
            this.tbConFeatures.ResumeLayout(false);
            this.tbInfo.ResumeLayout(false);
            this.GrpBxGameInfo.ResumeLayout(false);
            this.GrpBxGameInfo.PerformLayout();
            this.tbAutoSim.ResumeLayout(false);
            this.tbAutoSim.PerformLayout();
            this.tbAccManager.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tbExtras.ResumeLayout(false);
            this.tbSettings.ResumeLayout(false);
            this.tbSettings.PerformLayout();
            this.tbConsole.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbControl;
        private System.Windows.Forms.TabPage tbFeatures;
        private System.Windows.Forms.TabPage tbAccManager;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoginAccount;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.ComboBox Hotel;
        private System.Windows.Forms.Label lblHotel;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tlStripStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripStatusLabel tlStripActiveBots;
        private System.Windows.Forms.TabControl tbConFeatures;
        private System.Windows.Forms.TabPage tbInfo;
        private System.Windows.Forms.TabPage tbAutoSim;
        private System.Windows.Forms.ToolStripStatusLabel toolStripCoords;
        private System.Windows.Forms.GroupBox GrpBxGameInfo;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.TextBox txtCurrRoomId;
        private System.Windows.Forms.TextBox txtSwfbuild;
        private System.Windows.Forms.TextBox txtGameHost;
        private System.Windows.Forms.TextBox txtGamePort;
        private System.Windows.Forms.TextBox txtGameIP;
        private System.Windows.Forms.Label lblGameIP;
        private System.Windows.Forms.Label lblSwfBuild;
        private System.Windows.Forms.Label lblGamePort;
        private System.Windows.Forms.Label lblGameHost;
        private System.Windows.Forms.Label lblProgUser;
        private System.Windows.Forms.ListView CloneList;
        private System.Windows.Forms.ColumnHeader HabboAccount;
        private System.Windows.Forms.ColumnHeader HabboID;
        private System.Windows.Forms.Panel MDI_client;
        private System.Windows.Forms.TextBox txtRoomID;
        private System.Windows.Forms.Button btnLoadRoom;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Label lblRoomID;
        private System.Windows.Forms.CheckBox chkShout;
        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.Timer Coordinates;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TabControl tbExtras;
        private System.Windows.Forms.TabPage tbSettings;
        private System.Windows.Forms.CheckBox chkProxyConnect;
        private System.Windows.Forms.CheckBox chkEnableConsole;
        private System.Windows.Forms.CheckBox chkAllAccounts;
        private System.Windows.Forms.TabPage tbConsole;
        private System.Windows.Forms.RichTextBox rtbConsole;
        private System.Windows.Forms.TextBox txtX;

    }
}

