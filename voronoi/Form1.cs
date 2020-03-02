using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace voronoi
{
    public partial class Form1 : Form
    {
        Int32 pos = 10;
        Int32 margin = 0;
        Int32 width = 0;
        Int32 height = 0;
        byte[] binImg = null;
        Bitmap bmp = null;
        Boolean bussy = false;
        Polys Polys1 = new Polys();

        public Form1()
        {
            InitializeComponent();     
            BuStart.Enabled=false;
            BuEnd.Visible=false;
        }

        //-----------------------

        public void refreshtriagles(Boolean ex)
        {
            //for (int x = 0; x < binImg.Length; x++)
            //{
            //    binImg[x] = 0;
            //}          
            Polys1.Draw(ref binImg);
            bmp = (Bitmap)pictureBox1.Image;
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;
            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(binImg, 0, ptr, bytes);
            // Unlock the bits.
            bmp.UnlockBits(bmpData);
            // Draw the modified image.
            pictureBox1.Image = bmp;
        }            

        //-----------------------

        private void BuStart_Click(object sender, EventArgs e)
        {
            BuStart.Visible=false;
            BuEnd.Visible = true;
            timer1.Enabled = true;
   
            if (bussy == false)
            {
                bussy = true;
                Random rnd = new Random();
                for (int x = 0; x < 400; x++)
                {
                    int rx = rnd.Next(width - 1);
                    int ry = rnd.Next(height - 1);                                                      
    
                    Polys1.improve(rx, ry, ref binImg,pos,margin);
                    
                }
                refreshtriagles(false);
                pictureBox1.Refresh();
                bussy = false;
            }
        }

        //-----------------------

        private void BuEnd_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            BuStart.Visible = true;
            BuEnd.Visible = false;
        }

        //-----------------------

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                bmp = (Bitmap)pictureBox1.Image.Clone();
                width = bmp.Width;
                height = bmp.Height;

                Polys1.init(ref bmp);

                MakePoints Makepoints1 = new MakePoints(ref bmp);
                Makepoints1.doit();

                var point0 = new DelaunayPoint(0, 0);
                var point1 = new DelaunayPoint(0, height - 1);
                var point2 = new DelaunayPoint(width - 1, height - 1);
                var point3 = new DelaunayPoint(width - 1, 0);
                var points = new List<DelaunayPoint>() { point0, point1, point2, point3 };
                var tri1 = new Triangle(point0, point1, point2);
                var tri2 = new Triangle(point0, point2, point3);
                var border = new List<Triangle>() { tri1, tri2 };

                DelaunayTriangulator delaunay = new DelaunayTriangulator();                               

                foreach (Point pnt in Makepoints1.PointsLst)
                {
                    bmp.SetPixel(pnt.X, pnt.Y, Color.Black);
                    points.Add(new DelaunayPoint(pnt.X+0.2, pnt.Y + 0.1));
                }

                IEnumerable<Triangle> tmp = delaunay.BowyerWatson(points, border);

                Graphics grp = pictureBox1.CreateGraphics();

                List<Point> pntlist = new List<Point>();
                List<string> strlist = new List<string>();

                foreach (var triangle in tmp)
                {

                    string key = triangle.Vertices[0].Key();                  
                    if (!strlist.Contains(key))
                    {
                        strlist.Add(key);
                        pntlist.Add(triangle.Vertices[0].GetPoint());
                    }
                    key = triangle.Vertices[1].Key();                 
                     if (!strlist.Contains(key))
                    {
                        strlist.Add(key);
                        pntlist.Add(triangle.Vertices[1].GetPoint());
                    }
                     key = triangle.Vertices[2].Key();                
                    if (!strlist.Contains(key))
                    {
                        strlist.Add(key);
                        pntlist.Add(triangle.Vertices[2].GetPoint());
                    }               
                }
                Polys1.PointsLst.Clear();
                Polys1.PointsLst_cpy.Clear();
                Polys1.Items.Clear();
                foreach(Point pnt in pntlist)
                {                  
                    Polys1.PointsLst.Add(pnt);
                    Polys1.PointsLst_cpy.Add(pnt);
                }

                foreach (var triangle in tmp)
                {
                    string key1 = triangle.Vertices[0].Key();
                    string key2 = triangle.Vertices[1].Key();
                    string key3 = triangle.Vertices[2].Key();                                  

                    strlist.IndexOf(key1);
                    Poly pol = new Poly(strlist.IndexOf(key1),
                        strlist.IndexOf(key2),
                        strlist.IndexOf(key3), Polys1.PointsLst);

                    if (pol.IsCounterClockwise)
                    {
                        int kk = pol.p2;
                        pol.p2 = pol.p1;
                        pol.p1 = kk;                       
                    }                 
                    Polys1.Items.Add(pol);                  
                }
                binImg = new byte[width * 3 * height];            
                refreshtriagles(false);
                BuStart.Enabled = true;
                BuEnd.Enabled = true;
            }
        }

        //-----------------------

        public double DivZero(double value, double replace)
        {
            if (value == 0.0)
            {
                return replace;
            }
            else
            {
                return value;
            }
        }


        private void mniSave_Click(object sender, EventArgs e)
        {

            pictureBox1.Image.Save("outp.bmp");
            System.Text.StringBuilder bld = new StringBuilder();            
            int cnt = 0;
          
            foreach (Poly poly1 in Polys1.Items)
            {
                string line = "tri({0:0.0},{1:0.0},{2:0.0},{3:0.0},{4:0.0},{5:0.0},{6},{7},{8});//{9}";
                double x1 = Polys1.PointsLst[poly1.p1].X;
                double y1 = Polys1.PointsLst[poly1.p1].Y;
                double x2 = Polys1.PointsLst[poly1.p2].X;
                double y2 = Polys1.PointsLst[poly1.p2].Y;
                double x3 = Polys1.PointsLst[poly1.p3].X;
                double y3 = Polys1.PointsLst[poly1.p3].Y;
                double avgx = (x1 + x2 + x3) / 3.0;
                double avgy = (y1 + y2 + y3) / 3.0;
                double d = Math.Sqrt((x1 - avgx) * (x1 - avgx) + (y1 - avgy) * (y1 - avgy));
                double x1d = 0; // (x1 - avgx) * 0.5 / DivZero(d, 99.0);
                double y1d = 0;// (y1 - avgy) * 0.5 / DivZero(d, 99.0);
                 d = Math.Sqrt((x2 - avgx) * (x2 - avgx) + (y2 - avgy) * (y2 - avgy));
                 double x2d = 0;// (x2 - avgx) * 0.5 / DivZero(d, 99.0);
                double y2d = 0;// (y2 - avgy) * 0.5 / DivZero(d, 99.0);
                 d = Math.Sqrt((x3 - avgx) * (x3 - avgx) + (y3 - avgy) * (y3 - avgy));
                 double x3d = 0;// (x3 - avgx) * 0.5 / DivZero(d, 99.0);
                 double y3d = 0;// (y3 - avgy) * 0.5 / DivZero(d, 99.0);

                string ln = string.Format(new System.Globalization.CultureInfo("en-GB") ,line, x1 + x1d, y1+y1d ,
                                         x2 + x2d, y2+y2d ,
                                         x3 + x3d, y3+y3d ,
                                                                      poly1.rgb.R, poly1.rgb.G, poly1.rgb.B, cnt).
                                                                      Replace("'", Convert.ToChar(34).ToString());
                bld.AppendLine(ln);
                cnt = cnt + 1;
            }             

             System.IO.File.WriteAllText("out.svg", bld.ToString());
        }

        //-----------------------

        private void buPlus_Click(object sender, EventArgs e)
        {
            pos = pos + 5;
            if (pos > 100) { pos = 100; }
            toolStripLabel3.Text = pos.ToString();
        }

        //-----------------------

        private void buMinus_Click(object sender, EventArgs e)
        {
            pos = pos - 5;
            if (pos < 2) { pos = 2; }
            toolStripLabel3.Text = pos.ToString();
        }

        //-----------------------

      
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (BuStart.Enabled == true)
            {
                Random rnd = new Random();
                for (int x = 0; x < 400; x++)
                {
                    int rx = e.X - 100 + rnd.Next(200);

                    int ry = e.Y - 100 + rnd.Next(200);
                    int xCoordinate = e.X;
                    int yCoordinate = e.Y;
                    if (Math.Sqrt((xCoordinate - rx) * (xCoordinate - rx) + (yCoordinate - ry) * (yCoordinate - ry)) < 30)
                        Polys1.improve(rx, ry, ref binImg, pos, margin);
                    
                }
                refreshtriagles(false);
                pictureBox1.Refresh();
            }
        }

      


        //-----------------------
    }
}
