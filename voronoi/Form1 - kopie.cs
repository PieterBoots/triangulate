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

    int[] minBuffer = new int[4000];
    int[] maxBuffer = new int[4000];

    byte[] binImg = new byte[1024 * 3 * 768];
    Int32[] idxdat = new Int32[1024 * 768];
    byte[] src = new byte[1024 * 3 * 768];
    Bitmap bmpclone = null;
    Bitmap bmp = null;
    Boolean bussy = false;
    Boolean Down = false;

    Tri Tri1 = new Tri();

    public Form1()
    {

      InitializeComponent();
      bmpclone = (Bitmap)pictureBox1.Image.Clone();
      bmp = (Bitmap)pictureBox1.Image.Clone();
      for (int y = 0; y < 768; y++)
      {
        for (int x = 0; x < 1024; x++)
        {
          Color c = bmpclone.GetPixel(x, y);
          src[x * 3 + y * 1024 * 3] = c.R;
          src[x * 3 + 1 + y * 1024 * 3] = c.G;
          src[x * 3 + 2 + y * 1024 * 3] = c.B;
        }
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {

      Tri1.recalc();

      refreshtriagles(false);

    }

    public void refreshtriagles(Boolean ex)
    {
      for (int x = 0; x < binImg.Length; x++)
      {
        binImg[x] = 0;
      }

      int j = 0;
      int idx = 0;
      for (int i=0;i<Tri1.Triangle.Count;i++)
      {
       Tri.dTriangle tri = Tri1.Triangle[i];
        if (tri.active == true)
        {


          int x1 = Tri1.Vertex[tri.vv0].X;
          int y1 = Tri1.Vertex[tri.vv0].Y;
          if (x1 > 1023) { x1 = 1023; }
          if (x1 < 0) { x1 = 0; }
          if (y1 > 767) { y1 = 767; };
          if (y1 < 0) { y1 = 0; }

          int x2 = Tri1.Vertex[tri.vv1].X;
          int y2 = Tri1.Vertex[tri.vv1].Y;
          if (x2 > 1023) { x2 = 1023; }
          if (x2 < 0) { x2 = 0; }
          if (y2 > 767) { y2 = 767; };
          if (y2 < 0) { y2 = 0; }

          int x3 = Tri1.Vertex[tri.vv2].X;
          int y3 = Tri1.Vertex[tri.vv2].Y;
          if (x3 > 1023) { x3 = 1023; }
          if (x3 < 0) { x3 = 0; }
          if (y3 > 767) { y3 = 767; };
          if (y3 < 0) { y3 = 0; }

          Color c = Color.White;

           
            draw_triangle(ref c, new Point(x1, y1), new Point(x2, y2), new Point(x3, y3), true,i);
     
          
          j = j + 1;
        }
      }      

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

    public void line_to_buffer(Point p1, Point p2)
    {
      int ymin = 0;
      int ymax = 0;
      int xmin = 0;
      int xmax = 0;
      if (p1.Y > p2.Y)
      {
        ymin = p2.Y;
        ymax = p1.Y;
        xmin = p2.X;
        xmax = p1.X;
      }
      else
      {
        ymin = p1.Y;
        ymax = p2.Y;
        xmin = p1.X;
        xmax = p2.X;
      }
      if (ymax - ymin > 0)
      {
        for (int i = 0; i <= ymax - ymin; i++)
        {
          if (ymin + i >= 0 && ymin + i <= 767)
          {
            int value = xmin + (xmax - xmin) * i / (ymax - ymin);
            if (value < minBuffer[ymin + i])
            {
              if (value < 0) { value = 0; }
              minBuffer[ymin + i] = value;
            }
            if (value > maxBuffer[ymin + i])
            {
              if (value > 1023)
              {
                value = 1023;
              }
              maxBuffer[ymin + i] = value;
            }
          }
        }
      }
    }

    public int max(int a, int b, int c)
  {
       int max = a;   
      if (b >max) { max = b; }
      if (c > max) { max = c; }
      return max;
  }

    public int min(int a, int b, int c)
    {
      int min = a;
      if (b < min) { min = b; }
      if (c < min) { min = c; }
      return min;
    }

    public int draw_triangle(ref Color col, Point p1, Point p2, Point p3, Boolean draw, int index)
    {
      int error = 0;
      double errf = 0;
      int ymax = max(p1.Y, p2.Y, p3.Y);
      int ymin = min(p1.Y, p2.Y, p3.Y);

      if (ymin < 0) { ymin = 0; };
      if (ymax > 767) { ymax = 767; };

      for (int i = ymin; i <= ymax; i++)
      {
        minBuffer[i] = 99999;
        maxBuffer[i] = 0;
      }

      line_to_buffer(p1, p2);
      line_to_buffer(p2, p3);
      line_to_buffer(p3, p1);


      int totr = 0;
      int totg = 0;
      int totb = 0;
      int cnt = 0;
      for (int i = ymin; i <= ymax; i++)
      {
        int tmp = i * 1024 * 3;
        for (int j = minBuffer[i]; j <= maxBuffer[i]; j++)
        {
          totr = totr + src[j * 3 + tmp];
          totg = totg + src[j * 3 + 1 + tmp];
          totb = totb + src[j * 3 + 2 + tmp];
          cnt = cnt + 1;
        }
      }

      errf = 0;
      byte r = 0;
      byte g = 0;
      byte b = 0;
      int srcr = 0;
      int srcg = 0;
      int srcb = 0;
      if (cnt != 0)
      {
        r = System.Convert.ToByte(totr / cnt);
        g = System.Convert.ToByte(totg / cnt);
        b = System.Convert.ToByte(totb / cnt);
      }

      if (draw)
      {
        for (int i = ymin; i <= ymax; i++)
        {
          int pos = minBuffer[i] * 3 + i * 1024 * 3;
          int ofs = i * 1024 + minBuffer[i];
          for (int j = minBuffer[i]; j <= maxBuffer[i]; j++)
          {
            idxdat[ofs ] = index;         
            binImg[pos] = b;
            binImg[pos + 1] = g;
            binImg[pos + 2] = r;                    
            pos = pos + 3;
            ofs++;            
          }
        }
      }
      else
      {
        for (int i = ymin; i <= ymax; i++)
        {
          int pos = minBuffer[i] * 3 + i * 1024 * 3;
          for (int j = minBuffer[i]; j <= maxBuffer[i]; j++)
          {
            idxdat[i * 1024 + j] = index;
            srcr = src[pos] - r;
            srcg = src[pos + 1] - g;
            srcb = src[pos + 2] - b;
            errf = errf + Math.Sqrt(srcr * srcr + srcg * srcg + srcb * srcb);
            pos = pos + 3;
          }
        }
      }
      return (int)errf;
    }



    public void improve(int x, int y)
    {  
      int cP = Tri1.NearestPoint(x, y);
      //find triangles;
      Point middle = Tri1.Vertex[cP];
      System.Collections.ArrayList run = new System.Collections.ArrayList();
      System.Collections.ArrayList run2 = new System.Collections.ArrayList();
      Tri1.GetTrianglesWithPoint(cP, ref run);

      foreach (int idx in run)
      {
        Tri1.GetTrianglesWithPoint(Tri1.Triangle[idx].vv0, ref run2);
        Tri1.GetTrianglesWithPoint(Tri1.Triangle[idx].vv1, ref run2);
        Tri1.GetTrianglesWithPoint(Tri1.Triangle[idx].vv2, ref run2);
      }


      int olderror = ErrorOfTriangles(run2);
    //  int oldx = Tri1.Vertex[cP].X;
    //  int oldy = Tri1.Vertex[cP].Y;
      Random rnd = new Random();

      System.Collections.ArrayList oldpoints = new System.Collections.ArrayList();

      foreach (int idx in run)
      {
        Tri.dvertexidx vr = new Tri.dvertexidx();
        vr.idx = Tri1.Triangle[idx].vv0;
        vr.p = Tri1.Vertex[Tri1.Triangle[idx].vv0];
        if (!oldpoints.Contains(vr))
        {
          oldpoints.Add(vr);
        }
        Tri.dvertexidx vr1 = new Tri.dvertexidx();
        vr1.idx = Tri1.Triangle[idx].vv1;
        vr1.p = Tri1.Vertex[Tri1.Triangle[idx].vv1];
        if (!oldpoints.Contains(vr1))
        {
          oldpoints.Add(vr1);
        }
        Tri.dvertexidx vr2 = new Tri.dvertexidx();
        vr2.idx = Tri1.Triangle[idx].vv2;
        vr2.p = Tri1.Vertex[Tri1.Triangle[idx].vv2];
        if (!oldpoints.Contains(vr2))
        {
          oldpoints.Add(vr2);
        }
      }

      System.Collections.ArrayList lines = new System.Collections.ArrayList();

     
      for (int i=0;i<oldpoints.Count;i++)        
      {
        Tri.dvertexidx vr = (Tri.dvertexidx)oldpoints[i];
        vr.change = false;
      }

      int mx = rnd.Next(6);
      for (int o = 0; o < 1; o++)
      {
        int idx = rnd.Next(oldpoints.Count);
        Tri.dvertexidx vr = (Tri.dvertexidx)oldpoints[idx];
        vr.change = true;
        oldpoints[idx] = vr;
      }
     
        for (int t = 0; t < 10; t++)
        {
          for (int i = 0; i < oldpoints.Count; i++)
          {
            Tri.dvertexidx vr = (Tri.dvertexidx)oldpoints[i];

            if (vr.change == true)
            {
              int xx = Tri1.Vertex_org[vr.idx].X + rnd.Next(32) - 16;
              int yy = Tri1.Vertex_org[vr.idx].Y + rnd.Next(32) - 16;
              if (xx < 0) { xx = 0; }
              if (xx > 1024) { xx = 1023; }
              if (yy < 0) { yy = 0; }
              if (yy > 767) { yy = 767; }

              Tri1.Vertex[vr.idx] = new Point(xx, yy);
            }
          }

          


         
         
          int newerror = ErrorOfTriangles(run2);
       
          if (newerror < olderror) 
          {
            olderror = newerror;
            oldpoints.Clear();
            foreach (int idx in run)
            {
              Tri.dvertexidx vr = new Tri.dvertexidx();
              vr.idx = Tri1.Triangle[idx].vv0;
              vr.p = Tri1.Vertex[Tri1.Triangle[idx].vv0];
              if (!oldpoints.Contains(vr))
              {
                oldpoints.Add(vr);
              }
              Tri.dvertexidx vr1 = new Tri.dvertexidx();
              vr1.idx = Tri1.Triangle[idx].vv1;
              vr1.p = Tri1.Vertex[Tri1.Triangle[idx].vv1];
              if (!oldpoints.Contains(vr1))
              {
                oldpoints.Add(vr1);
              }
              Tri.dvertexidx vr2 = new Tri.dvertexidx();
              vr2.idx = Tri1.Triangle[idx].vv2;
              vr2.p = Tri1.Vertex[Tri1.Triangle[idx].vv2];
              if (!oldpoints.Contains(vr2))
              {
                oldpoints.Add(vr2);
              }
            }
          }
          else
          {
            foreach (Tri.dvertexidx vr in oldpoints)
            {
              Tri1.Vertex[vr.idx]=vr.p;
            }
                    
          }
        }
      //}
     
    }

    public int ErrorOfTriangles(System.Collections.ArrayList lst)
    {
      int olderror = 0;
      Point vr1 = Tri1.Vertex[0];
      Point vr2 = Tri1.Vertex[0];
      Point vr3 = Tri1.Vertex[0];

      foreach (int idx in lst)
      {
        Tri.dTriangle t = Tri1.Triangle[idx];

        vr1 = Tri1.Vertex[t.vv0];
        vr2 = Tri1.Vertex[t.vv1];
        vr3 = Tri1.Vertex[t.vv2];

        Color c = Color.White;
        olderror = olderror + draw_triangle(ref c, new Point(vr1.X, vr1.Y), new Point(vr2.X, vr2.Y), new Point(vr3.X, vr3.Y), false, idx);

      }
      return olderror;
    }





    private void button2_Click(object sender, EventArgs e)
    {
      timer1.Enabled = true;
      Down = false;

      if (bussy == false)
      {
        bussy = true;
        Boolean skip = false;
        Random rnd = new Random();
        for (int x = 0; x < 400; x++)
        {
          int rx = rnd.Next(1023);
          int ry = rnd.Next(767);
          for (int i = 0; i < Tri1.Vertex.Count; i++)
          {
            if ((Tri1.Vertex[i].X == rx) && (Tri1.Vertex[i].Y == ry))
            {
              skip = true;
            }
          }
          if (skip == false)
          {
            improve(rx, ry);
          }

        }
        int o = 827;
        //Tri1.Triangulate(ref o);
        refreshtriagles(false);
        pictureBox1.Refresh();
        bussy = false;
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      timer1.Enabled = false;
      Down = false;
    }

 

    private void button4_Click(object sender, EventArgs e)
    {
      pictureBox1.Image.Save("outp.bmp");     
    }






  }
}
