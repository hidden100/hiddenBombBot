using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp1
{
    public static class ImageHelper
    {
        public static Bitmap PrintScreen()
        {
            //Create a new bitmap.
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                           Screen.PrimaryScreen.Bounds.Height,
                                           PixelFormat.Format24bppRgb);

            // Create a graphics object from the bitmap.
            using (var gfxScreenshot = Graphics.FromImage(bmpScreenshot))
            {

                // Take the screenshot from the upper left corner to the right bottom corner.
                gfxScreenshot.CopyFromScreen(0,
                                            0,
                                            0,
                                            0,
                                            Screen.PrimaryScreen.Bounds.Size,
                                            CopyPixelOperation.SourceCopy);

                // Save the screenshot to the specified path that the user has chosen.
                bmpScreenshot.Save(@"D:\ImagemROBO\Screenshot.png", ImageFormat.Png);
            }
            return bmpScreenshot;
        }

        public static Point? FindImageOnPScreen(string path)
        {
            Bitmap printScreen = ImageHelper.PrintScreen();
            Bitmap toFind = new Bitmap(path);

            //Bitmap clone = new Bitmap(toFind.Width, toFind.Height,
            //    System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            //using (Graphics gr = Graphics.FromImage(clone))
            //{
            //    gr.DrawImage(toFind, new Rectangle(0, 0, clone.Width, clone.Height));
            //}
            Color initialPointToFind = toFind.GetPixel(0, 0);
            for( int i =0; i<printScreen.Width; i++)
            {
                for(int j = 0; j<printScreen.Height; j++)
                {
                    Color pixel = printScreen.GetPixel(i, j);
                    if (IsEqualColor(pixel, initialPointToFind))
                    {
                        Point? toClick = CompareImageFromPoint(printScreen, toFind, i, j);
                        if( toClick != null)
                        {
                            printScreen.Dispose();
                            toFind.Dispose();
                            return toClick.Value;
                        }
                    }
                }
            }
            printScreen.Dispose();
            toFind.Dispose();
            return null;
        }

        private static Point? CompareImageFromPoint(Bitmap printScreen, Bitmap toFind, int iStart, int jStart)
        {
            for(int i = 0; i< toFind.Width; i++)
            {
                for(int j = 0; j< toFind.Height; j++)
                {
                    Color imageToFindPixel = toFind.GetPixel(i, j);
                    if(iStart + i >= printScreen.Width || jStart + j >= printScreen.Height)
                    {
                        return null;
                    }
                    Color imageInPS = printScreen.GetPixel(iStart + i, jStart + j);
                    if(!IsEqualColor(imageInPS, imageToFindPixel))
                    {
                        return null;
                    }
                }
            }
            return new Point(iStart + toFind.Width/2,jStart + toFind.Height/2);
        }

        public static bool IsEqualColor(Color a, Color b)
        {
            if(a.A == b.A && a.R == b.R && a.G == b.G && a.B == b.B)
            {
                return true;
            }
            return false;
        }

    }
}
