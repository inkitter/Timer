namespace Timer
{
    partial class frmMain
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.labTime = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.timRefresh = new System.Windows.Forms.Timer(this.components);
            this.MnuRBC = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtUserInput = new System.Windows.Forms.ToolStripTextBox();
            this.timeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labUserInput = new System.Windows.Forms.Label();
            this.transparentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuRBC.SuspendLayout();
            this.SuspendLayout();
            // 
            // labTime
            // 
            this.labTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labTime.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTime.Location = new System.Drawing.Point(1, 8);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(184, 38);
            this.labTime.TabIndex = 0;
            this.labTime.Text = "00:00:00.0";
            this.labTime.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.labTime_MouseDoubleClick);
            this.labTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ffrmmousedown);
            this.labTime.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ffrmmousemove);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Violet;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(182, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(50, 30);
            this.btnStart.TabIndex = 1;
            this.btnStart.TabStop = false;
            this.btnStart.Text = "Start";
            this.btnStart.UseMnemonic = false;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Violet;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(233, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(55, 30);
            this.btnReset.TabIndex = 2;
            this.btnReset.TabStop = false;
            this.btnReset.Text = "Reset";
            this.btnReset.UseMnemonic = false;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // timRefresh
            // 
            this.timRefresh.Tick += new System.EventHandler(this.timRefresh_Tick);
            // 
            // MnuRBC
            // 
            this.MnuRBC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.topToolStripMenuItem,
            this.txtUserInput,
            this.timeToolStripMenuItem,
            this.transparentToolStripMenuItem});
            this.MnuRBC.Name = "MnuRBC";
            this.MnuRBC.Size = new System.Drawing.Size(161, 139);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // topToolStripMenuItem
            // 
            this.topToolStripMenuItem.CheckOnClick = true;
            this.topToolStripMenuItem.Name = "topToolStripMenuItem";
            this.topToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.topToolStripMenuItem.Text = "Stay Top";
            this.topToolStripMenuItem.Click += new System.EventHandler(this.topToolStripMenuItem_Click);
            // 
            // txtUserInput
            // 
            this.txtUserInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserInput.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserInput.MaxLength = 20;
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size(100, 23);
            this.txtUserInput.Text = "备注";
            this.txtUserInput.ToolTipText = "回车确认，回车或双击解锁";
            this.txtUserInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserInput_KeyPress);
            this.txtUserInput.Click += new System.EventHandler(this.txtUserInput_Click);
            this.txtUserInput.DoubleClick += new System.EventHandler(this.txtUserInput_DoubleClick);
            // 
            // timeToolStripMenuItem
            // 
            this.timeToolStripMenuItem.Enabled = false;
            this.timeToolStripMenuItem.Name = "timeToolStripMenuItem";
            this.timeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.timeToolStripMenuItem.Text = "time";
            // 
            // labUserInput
            // 
            this.labUserInput.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labUserInput.Location = new System.Drawing.Point(12, 0);
            this.labUserInput.Name = "labUserInput";
            this.labUserInput.Size = new System.Drawing.Size(150, 10);
            this.labUserInput.TabIndex = 3;
            this.labUserInput.Text = "lab";
            this.labUserInput.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labUserInput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ffrmmousedown);
            this.labUserInput.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ffrmmousemove);
            // 
            // transparentToolStripMenuItem
            // 
            this.transparentToolStripMenuItem.Name = "transparentToolStripMenuItem";
            this.transparentToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.transparentToolStripMenuItem.Text = "Transparent";
            this.transparentToolStripMenuItem.Visible = false;
            this.transparentToolStripMenuItem.Click += new System.EventHandler(this.transparentToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(296, 54);
            this.ContextMenuStrip = this.MnuRBC;
            this.ControlBox = false;
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.labUserInput);
            this.Controls.Add(this.labTime);
            this.Controls.Add(this.btnReset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(100, 100);
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Timer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ffrmmousedown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ffrmmousemove);
            this.MnuRBC.ResumeLayout(false);
            this.MnuRBC.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labTime;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Timer timRefresh;
        private System.Windows.Forms.ContextMenuStrip MnuRBC;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timeToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox txtUserInput;
        private System.Windows.Forms.Label labUserInput;
        private System.Windows.Forms.ToolStripMenuItem transparentToolStripMenuItem;
    }
}

