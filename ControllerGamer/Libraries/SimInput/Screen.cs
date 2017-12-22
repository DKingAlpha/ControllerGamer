using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ControllerGamer.Libraries.SimInput
{
    public static class Screen
    {
        public static int X => System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        public static int Y => System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

        public static Color getDominantColor(int x, int y, int w, int h)
        {
            Bitmap bmp = new Bitmap(w, h);
            Graphics imgGraphics = Graphics.FromImage(bmp);
            imgGraphics.CopyFromScreen(x, y, 0, 0, new Size(w, h));

            int r = 0;
            int g = 0;
            int b = 0;

            int total = 0;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color clr = bmp.GetPixel(i, j);

                    r += clr.R;
                    g += clr.G;
                    b += clr.B;

                    total++;
                }
            }
            r /= total;
            g /= total;
            b /= total;

            bmp.Dispose();
            imgGraphics.Dispose();

            return Color.FromArgb(r, g, b);
        }

        public static Bitmap ZoomScreen(int source_x, int source_y, int source_w, int source_h,int target_w, int target_h)
        {
            Bitmap bmp = new Bitmap(source_w, source_h);
            Graphics imgGraphics = Graphics.FromImage(bmp);
            imgGraphics.CopyFromScreen(source_x, source_y, 0, 0, new Size(source_w, source_h));

            Bitmap lpdimg = new Bitmap(target_w, target_h);
            Graphics lpdgrapg = Graphics.FromImage(lpdimg);
            lpdgrapg.DrawImage(bmp, new Rectangle(0, 0, target_w, target_h));


            bmp.Dispose();
            imgGraphics.Dispose();
            lpdgrapg.Dispose();

            return lpdimg;
        }
    }
}
