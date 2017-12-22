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


        private static Bitmap GetAreaImage(int x, int y, int w, int h)
        {
            Bitmap image = new Bitmap(w,h);
            Graphics imgGraphics = Graphics.FromImage(image);
            imgGraphics.CopyFromScreen(x, y, 0, 0, new Size(w,h));
            return image;
        }

        public static Color getDominantColor(int x, int y, int w, int h)
        {
            Bitmap bmp = GetAreaImage(x, y, w, h);
            //Used for tally
            int r = 0;
            int g = 0;
            int b = 0;

            int total = 0;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color clr = bmp.GetPixel(x, y);

                    r += clr.R;
                    g += clr.G;
                    b += clr.B;

                    total++;
                }
            }
            r /= total;
            g /= total;
            b /= total;

            return Color.FromArgb(r, g, b);
        }
    }
}
