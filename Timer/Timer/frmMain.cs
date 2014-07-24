using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Timer
{
    public partial class frmMain : Form
    {
        static int isecond = 0, imsecond = 0, iminute = 0, ihour = 0;
        private Point offset;
        Thread tcount;
        static Boolean bReset=true, bShowBtn=true;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            timeToolStripMenuItem.Text = DateTime.Now.ToString();  //程序开启时间
            labUserInput.Text = "";
        }

        private void ffrmmousedown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left != e.Button) return;
            Point cur = this.PointToScreen(e.Location);
            offset = new Point(cur.X - this.Left, cur.Y - this.Top);
        }
        private void ffrmmousemove(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left != e.Button) return;
            Point cur = MousePosition;
            this.Location = new Point(cur.X - offset.X, cur.Y - offset.Y);
        }
        //点击任意位置移动窗体

        void tTimeCount()
        {
            while (1 == 1)
            {
                imsecond++;
                if (imsecond >= 10) { imsecond = 0; isecond++; }
                if (isecond >= 60) { isecond = 0; iminute++; }
                if (iminute >= 60) { iminute = 0; ihour++; }
                if (ihour > 99) { isecond = 0; imsecond = 0; iminute = 0; ihour = 0; }  //若超过99小时则重置
                if (isecond == 59) { System.GC.Collect(); }
                Thread.Sleep(100);
            }
        }
        //计时线程

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (timRefresh.Enabled == false) {
                tcount = new Thread(tTimeCount); 
                timRefresh.Enabled = true; 
                tcount.Start();
                btnStart.Text = "Stop";
                fSaveNowTime("Start: ");          
            }
            else { 
                timRefresh.Enabled = false; 
                tcount.Abort();
                btnStart.Text = "Start";
                System.GC.Collect();
            }
        }
        //开始按钮

        private void fSaveNowTime(string prestr)
        {
            if (bReset == true)
            {
                timeToolStripMenuItem.Text = prestr + DateTime.Now.ToString();
                bReset = false;
            }
        }
        //保存开始计时时间

        private void btnReset_Click(object sender, EventArgs e)
        {
            isecond = 0; imsecond = 0; iminute = 0; ihour = 0;
            bReset = true;
            System.GC.Collect();
            flabTimeChange(0);
            if (timRefresh.Enabled == true)
            {
                fSaveNowTime("Start: ");
            }
            else { fSaveNowTime("Reset: "); }
        }
        //重置按钮

        private void timRefresh_Tick(object sender, EventArgs e)
        {
            flabTimeChange(0);
        }
        //计时器控件刷新时间

        private void flabTimeChange(int ShowType)
        {
            string hh, mm, ss;
            if (ihour < 10) { hh = "0" + Convert.ToString(ihour); } else { hh = Convert.ToString(ihour); }
            if (iminute < 10) { mm = "0" + Convert.ToString(iminute); } else { mm = Convert.ToString(iminute); }
            if (isecond < 10) { ss = "0" + Convert.ToString(isecond); } else { ss = Convert.ToString(isecond); }
            //强制显示2位数字
            switch (ShowType)
            {
                case 1:
                    
                    break;
                default:
                    labTime.Text = hh + ":" + mm + ":" + ss + "." + imsecond;
                    if (this.TopMost == false)
                    {
                        if (txtUserInput.ReadOnly == true) { this.Text = hh + ":" + mm + ":" + ss + " - " + txtUserInput.Text; }
                        else { this.Text = hh + ":" + mm + ":" + ss + " - Timer"; }
                    }
                    break;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
                this.Close();
        }
        //下拉菜单-Exit

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                tcount.Abort();
            }
            catch
            {

            }
            //防止退出线程未关闭
        }

        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (topToolStripMenuItem.Checked == true) { this.TopMost = true; this.Opacity = 0.6; }
            else { this.TopMost = false; this.Opacity = 1; }
        }
        //置顶并改变透明度

        private void txtUserInput_DoubleClick(object sender, EventArgs e)
        {
            txtUserInput.ReadOnly = false;
        }

        private void txtUserInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                if (txtUserInput.ReadOnly == false)
                {
                    txtUserInput.ReadOnly = true; 
                    labUserInput.Text = txtUserInput.Text;
                    this.Text =  txtUserInput.Text+" - Timer";
                }
                else
                {
                    txtUserInput.ReadOnly = false; 
                    labUserInput.Text = "";
                    this.Text = "Timer";
                }
            }
        }

        private void txtUserInput_Click(object sender, EventArgs e)
        {
            txtUserInput.SelectAll();
        }

        private void labTime_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (bShowBtn == true) { bShowBtn = false; this.Width = 178; }
            else { bShowBtn = true; this.Width = 294; }
        }
    }
}
