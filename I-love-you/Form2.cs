using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace I_love_you
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //获取屏幕的高宽
            Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);
            int width1 = ScreenArea.Width;
            int height1 = ScreenArea.Height;
            Random random = new Random();
            this.Location = new Point(random.Next(width1), random.Next(height1));
            Thread.Sleep(100);
        }
    }
}
