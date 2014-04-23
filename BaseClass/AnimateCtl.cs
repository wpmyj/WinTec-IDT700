using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

namespace BaseClass
{
	
	public enum FrameLayouts
	{
		Horisontal = 0,
		Vertical = 1
	}

	/// <summary>
	/// Summary description for AnimateCtl.
	/// </summary>
	public class AnimateCtl : System.Windows.Forms.Control
	{
		private Bitmap bitmap;
		private int frameCount;
		private int frameWidth;
		private  int frameHeight;
		private Graphics graphics;
		private int currentFrame = 0;
        private Color backColor;
        private int delayInterval;
        
        //private int sec = -1;

        System.Threading.Timer fTimer;
        //System.Threading.Timer fTimer1;

        public AnimateCtl(Color backColor, int frWidth, int delayInterval)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            System.IO.Stream stream = asm.GetManifestResourceStream(asm.GetName().Name + ".wait.gif");
            this.bitmap = new Bitmap(stream);

            graphics = this.CreateGraphics();
            frameWidth = frWidth;
            frameCount = bitmap.Width / frameWidth;
            frameHeight = bitmap.Height;

            this.Size = new Size(frameWidth + 25, frameHeight);
            this.BackColor = backColor;
            this.backColor = backColor;
            this.delayInterval = delayInterval;
        }

        public void StartAnimation()
        {
            currentFrame = 0;
            //sec = -1;
            //¶¯»­
            fTimer = new System.Threading.Timer(new System.Threading.TimerCallback(timer_Tick), null, 0, delayInterval);
            ////¼ÆÊ±
            //fTimer1 = new System.Threading.Timer(
            //    new System.Threading.TimerCallback(timer_Tick1), null, 200, 1000);
        }

        public void StopAnimation()
        {
            if (fTimer != null)
            {
                fTimer.Dispose();
                fTimer = null;
            }
            //if (fTimer1 != null)
            //{
            //    fTimer1.Dispose();
            //    fTimer1 = null;
            //}
        }

        private void timer_Tick(object obj)
		{
            if (currentFrame < frameCount - 1)
                currentFrame++;
            else
                currentFrame = 0;
            int XLocation = currentFrame * frameWidth;
            Rectangle rect = new Rectangle(XLocation, 0, frameWidth, frameHeight);
            graphics.DrawImage(bitmap, 0, 0, rect, GraphicsUnit.Pixel);
		}
        
        
        //private void timer_Tick1(object obj)
        //{
        //    sec += 1;
        //    graphics.FillRectangle(new SolidBrush(this.backColor), frameWidth, 0, 30, 20);
        //    graphics.DrawString(Convert.ToString(sec),
        //        new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
        //        new SolidBrush(Color.Red), frameWidth+2, 0);
        //}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }	
	}
}
