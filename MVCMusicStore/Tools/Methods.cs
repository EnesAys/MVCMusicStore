using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMusicStore.Tools
{
    public class Methods
    {
        public static Bitmap FotoCropByCoords(Image img, int x1, int x2, int y1, int y2, double ratio)
        {
            Rectangle cropRect = new Rectangle(Convert.ToInt32(x1 * ratio), Convert.ToInt32(y1 * ratio), Convert.ToInt32((x2 - x1) * ratio), Convert.ToInt32((y2 - y1) * ratio));
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);
            Graphics g = Graphics.FromImage(target);
            g.DrawImage(img, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
            g.Dispose();
            return target;
        }

        public static Bitmap FotoResizeBySizes(Image img, int width, int height)
        {
            Bitmap bmp = new Bitmap(img, new Size(width, height));
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(img, 0, 0, width, height);
            g.Dispose();
            return bmp;
        }
    }
}