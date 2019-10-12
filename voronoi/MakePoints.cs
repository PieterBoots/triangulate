using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace voronoi
{
    class MakePoints
    {
        public Int32 Width = 0;
        public Int32 Height = 0;
        public Color[,] src = null;
        public Byte[,] points = null;

        public List<Point> PointsLst = new List<Point>();       

        //-----------------------

        public  MakePoints(ref Bitmap bmp)
        {
            Width = bmp.Width;
            Height = bmp.Height;
            src = new Color[Width , Height];
            points = new Byte[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    src[x, y] = bmp.GetPixel(x, y);
                }
            }

       
        }

        //-----------------------

        public void doit()
        {
            int n = 64;
            for (int y = 0; y < Height / n; y++)
            {
                for (int x = 0; x < Width / n; x++)
                {
                    dobox(x*n,y*n,n,0);      
                }
            }

             n = 32;
            for (int y = 0; y < Height / n; y++)
            {
                for (int x = 0; x < Width / n; x++)
                {
                    dobox(x * n, y * n, n, 50);
                }
            }

            n = 16;
            for (int y = 0; y < Height / n; y++)
            {
                for (int x = 0; x < Width / n; x++)
                {
                    dobox(x * n, y * n, n, 50);
                }
            }

            //n = 8;
            //for (int y = 0; y < Height / n; y++)
            //{
            //    for (int x = 0; x < Width / n; x++)
            //    {
            //        dobox(x * n, y * n, n, 80);
            //    }
            //}

        //    n = 4;
         //   for (int y = 0; y < Height / n; y++)
           // {
             //   for (int x = 0; x < Width / n; x++)
               // {
                 //   dobox(x * n, y * n, n, 60);
              //  }
           // }
                    
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (points[x, y] == 1)
                    {
                        PointsLst.Add(new Point(x, y));
                    }
                }
            }
        }

        //-----------------------

        public void dobox(int x, int y,int n,int amax)
        {
            double tot = 0;
            int cnt = 0;
            for (int yy = 0; yy < n; yy++)
            {
                for (int xx = 0; xx < n; xx++)
                {
                    double gray = src[(x + xx), (y + yy)].GetBrightness() * 256;
                    tot = tot + gray;
                    cnt = cnt + 1;
                }
            }
            tot = tot / cnt;

            double max = 0;
            for (int yy = 0; yy < n; yy++)
            {
                for (int xx = 0; xx < n; xx++)
                {
                    double gray = src[(x + xx), (y + yy)].GetBrightness()*256;
                    double delta = Math.Abs(gray  - tot);
                    if (delta>max)
                    {
                        max = delta;
                    }
                }
            }


            if (max > amax)
            {
                points[x, y] = 1;
                int xt=x + n;
                if (xt>Width - 2){xt=Width - 2;}
                int yt = y + n;
                if (yt > Height - 2) { yt = Height - 2; }

              
                  points[x, yt] = 1;
                
              
                   points[xt, y] = 1;
               
              
                   points[xt, yt] = 1;
                
            }                      
        }
    }
}
