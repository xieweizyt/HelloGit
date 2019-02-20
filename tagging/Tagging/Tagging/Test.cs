using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tagging
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        double scale = 0;
        private void Test_Load(object sender, EventArgs e)
        {
            Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);
            int winWidth = (int)Math.Round(ScreenArea.Width * 0.7); //屏幕宽度
            Image img = Image.FromFile(@"C:\Users\pssczyw\AppData\Roaming\Tagging\test1\3.jpg");
            picShowImg.SizeMode = PictureBoxSizeMode.Zoom;
            picShowImg.Width = winWidth;
            scale = Convert.ToDouble(winWidth) / Convert.ToDouble(img.Width);
            double height = scale * Convert.ToDouble(img.Height);
            picShowImg.Height = (int)Math.Round(height, 0);
            picShowImg.Image = img;
        }
        Point start; //画框的起始点
        Point end;//画框的结束点<br>
        bool blnDraw;//判断是否绘制<br>
        Rectangle rect;
        private void picShowImg_MouseUp(object sender, MouseEventArgs e)
        {
            end = e.Location;
            blnDraw = false; //结束绘制
        }

        private void picShowImg_MouseDown(object sender, MouseEventArgs e)
        {
            start = e.Location;
            Invalidate();
            blnDraw = true;
        }

        private void picShowImg_MouseMove(object sender, MouseEventArgs e)
        {
            if (blnDraw)
            {
                if (e.Button != MouseButtons.Left)//判断是否按下左键
                    return;
                Point tempEndPoint = e.Location; //记录框的位置和大小
                rect.Location = new Point(
                Math.Min(start.X, tempEndPoint.X),
                Math.Min(start.Y, tempEndPoint.Y));
                rect.Size = new Size(
                Math.Abs(start.X - tempEndPoint.X),
                Math.Abs(start.Y - tempEndPoint.Y));
                picShowImg.Invalidate();
            }
        }

        private void picShowImg_Paint(object sender, PaintEventArgs e)
        {
            if (blnDraw)
            {
                if (picShowImg.Image != null)
                {
                    if (rect != null && rect.Width > 0 && rect.Height > 0)
                    {
                        e.Graphics.DrawRectangle(new Pen(Color.Red, 1), rect);//重新绘制颜色为红色
                        
                    }
                }
            }
            else
            {
                rect.Location = new Point((int)(Math.Round(2474 * scale)), (int)(Math.Round(356 * scale)));
                rect.Size = new Size((int)(Math.Round(2885 * scale))- (int)(Math.Round(2474 * scale)), (int)(Math.Round(422 * scale))- (int)(Math.Round(356 * scale)));
                e.Graphics.DrawRectangle(new Pen(Color.Red, 1), rect);//重新绘制颜色为红色

                //rect.Location = new Point(10, 10);
                //rect.Size = new Size(600, 300);
                //e.Graphics.DrawRectangle(new Pen(Color.Red, 1), rect);//重新绘制颜色为红色
            }
        }
    }
}
