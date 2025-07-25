namespace Server
{
    partial class frmServer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServer));
            groupBox1 = new GroupBox();
            label7 = new Label();
            btnClose = new Button();
            toolStrip1 = new ToolStrip();
            btnBegin = new ToolStripButton();
            btnPrevious = new ToolStripButton();
            toolStripLabel1 = new ToolStripLabel();
            txtFind = new ToolStripTextBox();
            toolStripLabel2 = new ToolStripLabel();
            btnFind = new ToolStripButton();
            toolStripLabel4 = new ToolStripLabel();
            btnNext = new ToolStripButton();
            btnEnd = new ToolStripButton();
            toolStripLabel6 = new ToolStripLabel();
            lblMessage = new ToolStripLabel();
            btnImport = new ToolStripButton();
            btnExport = new ToolStripButton();
            btnClear = new ToolStripButton();
            btnCancel = new Button();
            txtServerStatus = new TextBox();
            btnSave = new Button();
            txtServerConnection = new TextBox();
            btnDelete = new Button();
            txtServerID = new TextBox();
            btnEdit = new Button();
            txtServerName = new TextBox();
            btnStartServer = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.SkyBlue;
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(btnClose);
            groupBox1.Controls.Add(toolStrip1);
            groupBox1.Controls.Add(btnCancel);
            groupBox1.Controls.Add(txtServerStatus);
            groupBox1.Controls.Add(btnSave);
            groupBox1.Controls.Add(txtServerConnection);
            groupBox1.Controls.Add(btnDelete);
            groupBox1.Controls.Add(txtServerID);
            groupBox1.Controls.Add(btnEdit);
            groupBox1.Controls.Add(txtServerName);
            groupBox1.Controls.Add(btnStartServer);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(684, 251);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Server Mainboard";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.LightCyan;
            label7.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.MidnightBlue;
            label7.Location = new Point(196, 21);
            label7.Name = "label7";
            label7.Size = new Size(257, 37);
            label7.TabIndex = 23;
            label7.Text = "Server Mainboard";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top;
            btnClose.BackColor = Color.Turquoise;
            btnClose.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnClose.Location = new Point(465, 162);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 29);
            btnClose.TabIndex = 10;
            btnClose.Text = "...";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.MidnightBlue;
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnBegin, btnPrevious, toolStripLabel1, txtFind, toolStripLabel2, btnFind, toolStripLabel4, btnNext, btnEnd, toolStripLabel6, lblMessage, btnImport, btnExport, btnClear });
            toolStrip1.Location = new Point(3, 204);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(678, 44);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnBegin
            // 
            btnBegin.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnBegin.Image = (Image)resources.GetObject("btnBegin.Image");
            btnBegin.ImageTransparentColor = Color.Magenta;
            btnBegin.Name = "btnBegin";
            btnBegin.Size = new Size(23, 41);
            btnBegin.Text = "toolStripButton1";
            // 
            // btnPrevious
            // 
            btnPrevious.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnPrevious.Image = (Image)resources.GetObject("btnPrevious.Image");
            btnPrevious.ImageTransparentColor = Color.Magenta;
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(23, 41);
            btnPrevious.Text = "toolStripButton2";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripLabel1.ForeColor = Color.WhiteSmoke;
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(47, 41);
            toolStripLabel1.Text = "Find:";
            // 
            // txtFind
            // 
            txtFind.Name = "txtFind";
            txtFind.Size = new Size(100, 44);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.AutoSize = false;
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(15, 30);
            toolStripLabel2.Text = "          ";
            // 
            // btnFind
            // 
            btnFind.AutoSize = false;
            btnFind.BackColor = Color.CornflowerBlue;
            btnFind.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFind.ForeColor = Color.White;
            btnFind.Image = (Image)resources.GetObject("btnFind.Image");
            btnFind.ImageAlign = ContentAlignment.MiddleRight;
            btnFind.ImageTransparentColor = Color.Magenta;
            btnFind.Margin = new Padding(7);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(60, 30);
            btnFind.Text = "Find";
            btnFind.TextAlign = ContentAlignment.MiddleLeft;
            btnFind.TextImageRelation = TextImageRelation.TextBeforeImage;
            // 
            // toolStripLabel4
            // 
            toolStripLabel4.AutoSize = false;
            toolStripLabel4.Name = "toolStripLabel4";
            toolStripLabel4.Size = new Size(15, 30);
            toolStripLabel4.Text = "          ";
            // 
            // btnNext
            // 
            btnNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnNext.Image = (Image)resources.GetObject("btnNext.Image");
            btnNext.ImageTransparentColor = Color.Magenta;
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(23, 41);
            btnNext.Text = "toolStripButton3";
            // 
            // btnEnd
            // 
            btnEnd.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEnd.Image = (Image)resources.GetObject("btnEnd.Image");
            btnEnd.ImageTransparentColor = Color.Magenta;
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(23, 41);
            btnEnd.Text = "toolStripButton4";
            // 
            // toolStripLabel6
            // 
            toolStripLabel6.AutoSize = false;
            toolStripLabel6.Name = "toolStripLabel6";
            toolStripLabel6.Size = new Size(15, 30);
            toolStripLabel6.Text = "          ";
            // 
            // lblMessage
            // 
            lblMessage.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblMessage.ForeColor = Color.WhiteSmoke;
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(0, 41);
            // 
            // btnImport
            // 
            btnImport.AutoSize = false;
            btnImport.BackColor = Color.DodgerBlue;
            btnImport.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnImport.ForeColor = Color.White;
            btnImport.Image = (Image)resources.GetObject("btnImport.Image");
            btnImport.ImageAlign = ContentAlignment.MiddleRight;
            btnImport.ImageTransparentColor = Color.Magenta;
            btnImport.Margin = new Padding(7);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(80, 30);
            btnImport.Text = "Import";
            btnImport.TextAlign = ContentAlignment.MiddleLeft;
            btnImport.TextImageRelation = TextImageRelation.TextBeforeImage;
            // 
            // btnExport
            // 
            btnExport.AutoSize = false;
            btnExport.BackColor = Color.CornflowerBlue;
            btnExport.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.ForeColor = Color.White;
            btnExport.Image = (Image)resources.GetObject("btnExport.Image");
            btnExport.ImageAlign = ContentAlignment.MiddleRight;
            btnExport.ImageTransparentColor = Color.Magenta;
            btnExport.Margin = new Padding(7);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(80, 30);
            btnExport.Text = "Export";
            btnExport.TextAlign = ContentAlignment.MiddleLeft;
            btnExport.TextImageRelation = TextImageRelation.TextBeforeImage;
            // 
            // btnClear
            // 
            btnClear.AutoSize = false;
            btnClear.BackColor = Color.LightSlateGray;
            btnClear.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClear.ForeColor = Color.White;
            btnClear.Image = (Image)resources.GetObject("btnClear.Image");
            btnClear.ImageTransparentColor = Color.Magenta;
            btnClear.Margin = new Padding(7);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(80, 30);
            btnClear.Text = "Clear";
            btnClear.TextAlign = ContentAlignment.MiddleLeft;
            btnClear.TextImageRelation = TextImageRelation.TextBeforeImage;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top;
            btnCancel.BackColor = Color.DarkTurquoise;
            btnCancel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnCancel.Location = new Point(376, 162);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 29);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "...";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // txtServerStatus
            // 
            txtServerStatus.Anchor = AnchorStyles.Top;
            txtServerStatus.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtServerStatus.Location = new Point(428, 103);
            txtServerStatus.Name = "txtServerStatus";
            txtServerStatus.Size = new Size(112, 25);
            txtServerStatus.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top;
            btnSave.BackColor = Color.DeepSkyBlue;
            btnSave.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSave.Location = new Point(287, 162);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 29);
            btnSave.TabIndex = 8;
            btnSave.Text = "...";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // txtServerConnection
            // 
            txtServerConnection.Anchor = AnchorStyles.Top;
            txtServerConnection.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtServerConnection.Location = new Point(172, 133);
            txtServerConnection.Name = "txtServerConnection";
            txtServerConnection.Size = new Size(368, 25);
            txtServerConnection.TabIndex = 4;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top;
            btnDelete.BackColor = Color.SteelBlue;
            btnDelete.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnDelete.ForeColor = Color.AliceBlue;
            btnDelete.Location = new Point(198, 162);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 29);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "...";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // txtServerID
            // 
            txtServerID.Anchor = AnchorStyles.Top;
            txtServerID.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtServerID.Location = new Point(172, 103);
            txtServerID.Name = "txtServerID";
            txtServerID.Size = new Size(145, 25);
            txtServerID.TabIndex = 2;
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Top;
            btnEdit.BackColor = Color.RoyalBlue;
            btnEdit.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnEdit.ForeColor = Color.AliceBlue;
            btnEdit.Location = new Point(109, 162);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 29);
            btnEdit.TabIndex = 6;
            btnEdit.Text = "...";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // txtServerName
            // 
            txtServerName.Anchor = AnchorStyles.Top;
            txtServerName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtServerName.Location = new Point(172, 73);
            txtServerName.Name = "txtServerName";
            txtServerName.Size = new Size(145, 25);
            txtServerName.TabIndex = 1;
            // 
            // btnStartServer
            // 
            btnStartServer.Anchor = AnchorStyles.Top;
            btnStartServer.BackColor = Color.Navy;
            btnStartServer.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnStartServer.ForeColor = Color.AliceBlue;
            btnStartServer.Location = new Point(554, 21);
            btnStartServer.Name = "btnStartServer";
            btnStartServer.Size = new Size(100, 50);
            btnStartServer.TabIndex = 5;
            btnStartServer.Text = "Start Server";
            btnStartServer.UseVisualStyleBackColor = false;
            btnStartServer.Click += btnStartServer_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label4.Location = new Point(329, 106);
            label4.Name = "label4";
            label4.Size = new Size(93, 17);
            label4.TabIndex = 0;
            label4.Text = "Server Status:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label3.Location = new Point(30, 136);
            label3.Name = "label3";
            label3.Size = new Size(125, 17);
            label3.TabIndex = 0;
            label3.Text = "Server Connection:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label2.Location = new Point(86, 106);
            label2.Name = "label2";
            label2.Size = new Size(69, 17);
            label2.TabIndex = 0;
            label2.Text = "Server ID:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label1.Location = new Point(46, 76);
            label1.Name = "label1";
            label1.Size = new Size(109, 17);
            label1.TabIndex = 0;
            label1.Text = "Server name (*):";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.LightBlue;
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 251);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(684, 210);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            // 
            // frmServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmServer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Server Mainboard";
            Load += frmServer_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label7;
        private Button btnClose;
        private ToolStrip toolStrip1;
        private ToolStripButton btnBegin;
        private ToolStripButton btnPrevious;
        private ToolStripLabel toolStripLabel1;
        private ToolStripTextBox txtFind;
        private ToolStripLabel toolStripLabel2;
        private ToolStripButton btnFind;
        private ToolStripLabel toolStripLabel4;
        private ToolStripButton btnNext;
        private ToolStripButton btnEnd;
        private ToolStripLabel toolStripLabel6;
        private ToolStripLabel lblMessage;
        private ToolStripButton btnImport;
        private ToolStripButton btnExport;
        private ToolStripButton btnClear;
        private Button btnCancel;
        private TextBox txtServerStatus;
        private Button btnSave;
        private TextBox txtServerConnection;
        private Button btnDelete;
        private TextBox txtServerID;
        private Button btnEdit;
        private TextBox txtServerName;
        private Button btnStartServer;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
    }
}
