using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace MVCMusicStore.Tools
{
    public class PictureToSave
    {

        public string PictureString { get; set; }
        public int CoordX1 { get; set; }
        public int CoordX2 { get; set; }
        public int CoordY1 { get; set; }
        public int CoordY2 { get; set; }

        public Image CroppedPicture
        {
            get
            {
                Image image;
                byte[] fileBytes = Convert.FromBase64String(PictureString.Substring(PictureString.IndexOf(',') + 1));
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(fileBytes, 0, fileBytes.Length);
                    image = Image.FromStream(ms);
                }
                return Methods.FotoCropByCoords(image, CoordX1, CoordX2, CoordY1, CoordY2, Convert.ToDouble(image.Height) / 400);
            }
        }

        public Image SmallPicture
        {
            get
            {
                return Methods.FotoResizeBySizes(CroppedPicture, 100, 75);
            }
        }
    }
}