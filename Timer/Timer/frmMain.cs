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
using System.Runtime.InteropServices;
using System.IO;

namespace Timer
{
    

    public partial class frmMain : Form
    {
        public class HotKeys
        {
            
            
            Dictionary<int, HotKeyCallBackHanlder> keymap = new Dictionary<int, HotKeyCallBackHanlder>();
            public delegate void HotKeyCallBackHanlder();
            public enum HotkeyModifiers
            {
                Alt = 1,
                Control = 2,
                Shift = 4,
                Win = 8
            }


            //public void Regist(IntPtr hWnd, int modifiers, Keys vk, HotKeyCallBackHanlder callBack)
            //{
            //    int id = keyid;
            //    if (!RegisterHotKey(hWnd, id, modifiers, vk))
            //        throw new Exception("注册失败！");
            //    keymap[id] = callBack;
            //}

            //public void UnRegist(IntPtr hWnd, HotKeyCallBackHanlder callBack)
            //{
            //    //foreach (KeyValuePair<int, HotKeyCallBackHanlder> var in keymap)
            //    //{
            //    //    if (var.Value == callBack)
            //    //        UnregisterHotKey(hWnd, var.Key);
            //    //}
            //    UnregisterHotKey(hWnd, keyid);
            //}

            public void ProcessHotKey(Message m)
            {
                if (m.Msg == 0x312)
                {
                    int id = m.WParam.ToInt32();
                    HotKeyCallBackHanlder callback;
                    if (keymap.TryGetValue(id, out callback))
                        callback();
                }
            }
        }

        //全局快捷键类

        public class IniFile
        {
            private string FFileName;
            [DllImport("kernel32")]
            private static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);
            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault,
            StringBuilder lpReturnedString, int nSize, string lpFileName);
            [DllImport("kernel32")]
            private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
            public IniFile(string filename)
            {
                FFileName = filename;
            }
            public int ReadInt(string section, string key, int def)
            {
                return GetPrivateProfileInt(section, key, def, FFileName);
            }
            public string ReadString(string section, string key, string def)
            {
                StringBuilder temp = new StringBuilder(1024);

                GetPrivateProfileString(section, key, def, temp, 1024, FFileName); return temp.ToString();
            }
            public void WriteInt(string section, string key, int iVal)
            {
                WritePrivateProfileString(section, key, iVal.ToString(), FFileName);
            }
            public void WriteString(string section, string key, string strVal)
            {
                WritePrivateProfileString(section, key, strVal, FFileName);
            }
            public void DelKey(string section, string key)
            {
                WritePrivateProfileString(section, key, null, FFileName);
            }
            public void DelSection(string section)
            {
                WritePrivateProfileString(section, null, null, FFileName);
            }
        }
        //ini文件类

        HotKeys h = new HotKeys();
        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;
        const int WM_CHAR = 0x0102;
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
        private static extern IntPtr WindowFromPoint(int px, int py);
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("user32.dll")]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, int modifiers, Keys vk);
        [DllImport("user32.dll")]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        //winAPI声明

        delegate void SetTextCallback(string text);

        IntPtr hwndkp;
        static int isecond = 0, imsecond = 0, iminute = 0, ihour = 0;
        private Point offset;
        Thread tcount;
        Thread tkp1, tkp2, tkp3, tkp4;
        static Boolean bShowBtn=true;
        static int kpt1=100,kpt2=100,kpt3=100,kpt4=100;
        static int kp1 = 49, kp2 = 50, kp3 = 51, kp4 = 52;
        IniFile finiset = new IniFile(".\\Timer.ini");
        KeyEventArgs e1, e2, e3, e4;
        static int keyid = 202;
        //变量声明

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            iniread();
            timeToolStripMenuItem.Text = DateTime.Now.ToString();  //程序开启时间
            labUserInput.Text = "";
            System.Windows.Forms.Keys key = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), txtHotKey.Text);
            fhotkeyChange();

        }

        private void valueRefresh()
        {
            kp1 = Convert.ToInt16(txtKeypress1.Text);
            kp2 = Convert.ToInt16(txtKeypress2.Text);
            kp3 = Convert.ToInt16(txtKeypress3.Text);
            kp4 = Convert.ToInt16(txtKeypress4.Text);
            kpt1 = Convert.ToInt16(txtKPTime1.Text);
            kpt2 = Convert.ToInt16(txtKPTime2.Text);
            kpt3 = Convert.ToInt16(txtKPTime3.Text);
            kpt4 = Convert.ToInt16(txtKPTime4.Text);
            label1.Text = ((Keys)kp1).ToString() + "\r\n" + ((Keys)kp2).ToString() + "\r\n" + ((Keys)kp3).ToString() + "\r\n" + ((Keys)kp4).ToString();
        }
        //刷新自动按键各变量

        private void iniread()
        {
            
            try
            {
                this.Left = finiset.ReadInt("Main", "X", 1);
                this.Top = finiset.ReadInt("Main", "Y", 1);
                txtKeypress1.Text = finiset.ReadString("KeyPress", "kp1", null);
                txtKeypress2.Text = finiset.ReadString("KeyPress", "kp2", null);
                txtKeypress3.Text = finiset.ReadString("KeyPress", "kp3", null);
                txtKeypress4.Text = finiset.ReadString("KeyPress", "kp4", null);
                txtKPTime1.Text = finiset.ReadString("KeyPress", "kpt1", null);
                txtKPTime2.Text = finiset.ReadString("KeyPress", "kpt2", null);
                txtKPTime3.Text = finiset.ReadString("KeyPress", "kpt3", null);
                txtKPTime4.Text = finiset.ReadString("KeyPress", "kpt4", null);
                txtHotKey.Text = finiset.ReadString("KeyPress", "HotKey", null);
                if (txtHotKey.Text == "") { txtHotKey.Text = "F10"; }
                valueRefresh();
            }
            catch
            {

            }
        }
        //读取ini文件

        private void iniwrite()
        {
            try
            {
                finiset.WriteString("KeyPress", "HotKey", txtHotKey.Text);
                finiset.WriteString("KeyPress", "kp1", txtKeypress1.Text);
                finiset.WriteString("KeyPress", "kp2", txtKeypress2.Text);
                finiset.WriteString("KeyPress", "kp3", txtKeypress3.Text);
                finiset.WriteString("KeyPress", "kp4", txtKeypress4.Text);
                finiset.WriteString("KeyPress", "kpt1", txtKPTime1.Text);
                finiset.WriteString("KeyPress", "kpt2", txtKPTime2.Text);
                finiset.WriteString("KeyPress", "kpt3", txtKPTime3.Text);
                finiset.WriteString("KeyPress", "kpt4", txtKPTime4.Text);
                finiset.WriteInt("Main", "X", this.Left);
                finiset.WriteInt("Main", "Y", this.Top);

            }
            catch
            {

            }
        }
        //保存ini文件

        public void OnHotkey()
        {
            btnKeyPressStart_Click(null,null);
            Setstat("Hotkey Get" + " \r\n");
        }
        //全局快捷键触发事件

        protected override void WndProc(ref Message m)//监视Windows消息
        {
            const int WM_HOTKEY = 0x0312;//按快捷键
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    btnKeyPressStart_Click(null,null);//调用主处理程序
                    break;
            }
            base.WndProc(ref m);
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

        private void ffrmmouseup(object sender, MouseEventArgs e)
        {

        }

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
        //计时用线程

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (timRefresh.Enabled == false) {
                tcount = new Thread(tTimeCount); 
                timRefresh.Enabled = true; 
                tcount.Start();
                btnStart.Text = "Pause";
                fSaveNowTime("Start");
            }
            else { 
                timRefresh.Enabled = false; 
                tcount.Abort();
                btnStart.Text = "Start";
                System.GC.Collect();
                fSaveNowTime("Pause");
            }
        }
        //开始计时按钮

        private void fSaveNowTime(string prestr)
        {
                timeToolStripMenuItem.Text = prestr+":" + DateTime.Now.ToLongTimeString();
                Setstatt(prestr + " \r\n");
        }
        //保存开始计时时间

        private void btnReset_Click(object sender, EventArgs e)
        {
            isecond = 0; imsecond = 0; iminute = 0; ihour = 0;
            System.GC.Collect();
            flabTimeChange(0);
            if (timRefresh.Enabled == true)
            {
                fSaveNowTime("Start ");
            }
            else { fSaveNowTime("Reset "); }
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
            try
            {
                UnregisterHotKey(Handle, keyid);
            }
            catch
            {

            }
            try
            {
                tkp1.Abort();
                Setlab("S", 1);
            }
            catch
            {

            }

            try
            {
                tkp2.Abort();
                Setlab("S", 2);
            }
            catch
            {

            }
            try
            {
                tkp3.Abort();
                Setlab("S", 3);
            }
            catch
            {

            }
            try
            {
                tkp4.Abort();
                Setlab("S", 4);
            }
            catch
            {

            }
            //防止退出线程未关闭
        }

        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (topToolStripMenuItem.Checked == true) { 
                this.TopMost = true; 
                this.Opacity = 0.6;
                transparentToolStripMenuItem.Visible = true;
                transparentToolStripMenuItem.Checked = true;
                Setstatt("Set Top " + " \r\n");
            }
            else { 
                this.TopMost = false; 
                this.Opacity = 1;
                transparentToolStripMenuItem.Visible = false;
                transparentToolStripMenuItem.Checked = false;
                Setstatt("Uncheck Set Top " + " \r\n");
            }
        }
        //置顶并改变透明度

        private void txtUserInput_DoubleClick(object sender, EventArgs e)
        {
            txtUserInput.ReadOnly = false;
            Setstatt("Locked ");
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
            if (bShowBtn == true) { bShowBtn = false; this.Width = 200; this.Height = 50; }
            else { bShowBtn = true; this.Width = 296; this.Height = 326; }
        }

        private void transparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem.Checked == true){
                transparentToolStripMenuItem.Checked = false;
            }
            else
            {
                transparentToolStripMenuItem.Checked = true;
            }
            if (transparentToolStripMenuItem.Checked==true){
                this.Opacity = 0.6;
            }
            else{
                this.Opacity=1;
            }
        }

        private void txtKeypress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtKeypress1_KeyDown(object sender, KeyEventArgs e)
        {
            e1 = e;
            TextBox sendertxtBox = (TextBox)sender;
            txtKeypress_KeyDown(sender, e, sendertxtBox);
            
        }

        private void txtKeypress2_KeyDown(object sender, KeyEventArgs e)
        {
            e2 = e;
            TextBox sendertxtBox = (TextBox)sender;
            txtKeypress_KeyDown(sender, e, sendertxtBox);
            
        }

        private void txtKeypress3_KeyDown(object sender, KeyEventArgs e)
        {
            e3 = e;
            TextBox sendertxtBox = (TextBox)sender;
            txtKeypress_KeyDown(sender, e, sendertxtBox);
            
        }

        private void txtKeypress4_KeyDown(object sender, KeyEventArgs e)
        {
            e4 = e;
            TextBox sendertxtBox = (TextBox)sender;
            txtKeypress_KeyDown(sender, e, sendertxtBox);
            
        }

        private void txtKeypress_KeyDown(object sender, KeyEventArgs e, TextBox sendertxtBox)
        {
            sendertxtBox.Text = e.KeyValue.ToString();
            switch (sendertxtBox.Name)
            {
                case "txtKeypress1":
                    {
                        break;
                    }
                case "txtKeypress2":
                    {
                        break;
                    }
                case "txtKeypress3":
                    {
                        break;
                    }
                case "txtKeypress4":
                    {
                        break;
                    }
            }
            valueRefresh();
        }

        private void txtKPTime1_TextChanged(object sender, EventArgs e)
        {
            TextBox sendertxtBox = (TextBox)sender;
            txt_TextChanged(sender, e, sendertxtBox);
            valueRefresh();
        }

        private void txtKPTime1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox sendertxtBox = (TextBox)sender;
            txtKPTime_Keypress(sender, e, sendertxtBox);
        }

        private void txtKPTime_Keypress(object sender, KeyPressEventArgs e, TextBox sendertxtBox)
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_TextChanged(object sender, EventArgs e, TextBox sendertxtBox)
        {
            try
            {
                if (sendertxtBox.Text == "")
                {
                    sendertxtBox.Text = "0";
                }
                if (Convert.ToInt64(sendertxtBox.Text)>30000){
                    sendertxtBox.Text = "";
                    sendertxtBox.Text = "30000";
                }
                valueRefresh();
            }
            catch { }
        }

        private void btnKeyPressStart_Click(object sender, EventArgs e)
        {
            //iniwrite();
            if (btnKeyPressStart.Text == "Start" && txtHotKey.Focused == false)
            {
                btnKeyPressStart.Text = "Stop";
                btnKeyPressStart.Enabled = true;
                txtStatus.ReadOnly = true;
                labTime.ForeColor = System.Drawing.Color.Red;
                hwndkp = WindowFromPoint(Cursor.Position.X, Cursor.Position.Y);
                Setstat("AutoKey Press Win ID:" + hwndkp.ToString() + " \r\n");
                if (kp1 >=32 && kp1<=225)
                {
                    tkp1 = new Thread(skp1);
                    tkp1.Start();
                }
                if (kp2 > 32 && kp2 < 225)
                {
                    tkp2 = new Thread(skp2);
                    tkp2.Start();
                }
                if (kp3 > 32 && kp3 < 225)
                {
                    tkp3 = new Thread(skp3);
                    tkp3.Start();
                }
                if (kp4 > 32 && kp4 < 225)
                {
                    tkp4 = new Thread(skp4);
                    tkp4.Start();
                }
            }
            else
            {
                btnKeyPressStart.Text = "Start";
                btnKeyPressStart.Enabled = false;
                txtStatus.ReadOnly = false;
                labTime.ForeColor = System.Drawing.Color.Black;
                Setstat("- ");
                try 
                {
                    tkp1.Abort();
                    Setlab("S",1);
                }
                catch 
                {

                }
                
                try
                {
                    tkp2.Abort();
                    Setlab("S",2);
                }
                catch
                {

                }
                try
                {
                    tkp3.Abort();
                    Setlab("S",3);
                }
                catch
                {

                }
                try
                {
                    tkp4.Abort();
                    Setlab("S",4);
                }
                catch
                {

                }
            }
        }
        private void Setstat(string text)
        {
            if (timRefresh.Enabled == true)
            {
                string stime = "";
                stime = labTime.Text.Substring(3,5);
                if (this.txtStatus.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(Setstat);
                    this.Invoke(d, new object[] { stime + " " + text });
                }
                else
                {
                    this.txtStatus.Text = stime + " " + text + this.txtStatus.Text;
                }
            }
            else
            {
                Setstatt(text);
            }
        }
        //keypress log 记录

        private void Setstatt(string text)
        {
            if (this.txtStatus.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Setstat);
                this.Invoke(d, new object[] { DateTime.Now.ToLongTimeString() + " " + text });
            }
            else
            {
                this.txtStatus.Text = DateTime.Now.ToLongTimeString() + " " + text + this.txtStatus.Text;
            }
        }
        //timer log 记录

        private void SetlabT1(string text)
        {
            if (this.labKP1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetlabT1);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labKP1.Text = text;
            }
        }
        private void SetlabT2(string text)
        {
            if (this.labKP2.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetlabT2);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labKP2.Text = text;
            }
        }
        private void SetlabT3(string text)
        {
            if (this.labKP3.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetlabT3);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labKP3.Text = text;
            }
        }
        private void SetlabT4(string text)
        {
            if (this.labKP4.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetlabT4);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labKP4.Text = text;
            }
        }
        private void Setlab(string text,int labID)
        {
            switch (labID)
            {
                case 1:
                    {
                        SetlabT1(text);
                        break;
                    }
                case 2:
                    {
                        SetlabT2(text);
                        break;
                    }
                case 3:
                    {
                        SetlabT3(text);
                        break;
                    }
                case 4:
                    {
                        SetlabT4(text);
                        break;
                    }
            }
        }

        void skp1()
        {
            Setlab("R",1);
            while (hwndkp != IntPtr.Zero)
            {
                //valueRefresh();
                PostMessage(hwndkp, WM_KEYDOWN, kp1, 0);
                Thread.Sleep(5);
                PostMessage(hwndkp, WM_KEYUP, kp1, 0);
                Thread.Sleep(kpt1);
            }
        }
        void skp2()
        {
            Setlab("R",2);
            while (hwndkp != IntPtr.Zero)
            {
                PostMessage(hwndkp, WM_KEYDOWN, kp2, 0);
                Thread.Sleep(5);
                PostMessage(hwndkp, WM_KEYUP, kp2, 0);
                Thread.Sleep(kpt2);
            }
            Thread.Sleep(kpt2);
        }
        void skp3()
        {
            Setlab("R",3);
            while (hwndkp != IntPtr.Zero)
            {
                PostMessage(hwndkp, WM_KEYDOWN, kp3, 0);
                Thread.Sleep(5);
                PostMessage(hwndkp, WM_KEYUP, kp3, 0);
                Thread.Sleep(kpt3);
            }
            Thread.Sleep(kpt3);
        }
        void skp4()
        {
            Setlab("R",4);
            while (hwndkp != IntPtr.Zero)
            {
                PostMessage(hwndkp, WM_KEYDOWN, kp4, 0);
                Thread.Sleep(5);
                PostMessage(hwndkp, WM_KEYUP, kp4, 0);
                Thread.Sleep(kpt4);
            }
            Thread.Sleep(kpt4);
        }

        private void txtHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            //TextBox sendertxtBox = (TextBox)sender;
            //txtKeypress_KeyDown(sender, e, sendertxtBox);
            txtHotKey.Text = e.KeyData.ToString();
            fhotkeyChange();
        }

        private void fhotkeyChange()
        {
            System.Windows.Forms.Keys key = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), txtHotKey.Text);
            try
            {
                UnregisterHotKey(Handle, keyid);
                //Setstat("Hotkey UnRegistered");
            }
            catch
            {

            }
            for (int i = 202; i < 213; i++)
            {
                try
                {
                    if (txtHotKey.Text != "Escape")
                    {
                        RegisterHotKey(Handle, i, 0, key);
                        Setstat("Hotkey Registered: ("+i+") "+  txtHotKey.Text+ " \r\n");
                    }
                    else
                    {
                        Setstat("Hotkey not set" + " \r\n");
                    }
                }
                catch
                {
                    Setstat("Hotkey Reg Error id=" +i+ " \r\n");
                }
                finally
                {
                    keyid=i;
                    i = 221;
                }
            }
        }

        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            iniwrite();
        }

        private void timValueRefresh_Tick(object sender, EventArgs e)
        {
            valueRefresh();
        }

        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string swpath = ".\\" + System.DateTime.Now.ToString("yyMMdd") + "_" + System.DateTime.Now.ToString("HHmmss") + "_Timerlog.txt";
                StreamWriter sw = new StreamWriter(swpath);
                sw.Write(txtStatus.Text);
                sw.Close();
                Setstatt("Log Saved: "+swpath+"\r\n");
            }
            catch
            {
                Setstatt("Save Log Fail \r\n");
            }

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtStatus.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtStatus.Copy();
        }

        private void clearToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                txtStatus.Text = "";
            }
            else
            {
                Setstatt("Right Click to Clear\r\n");
            }
        }
    }
}
