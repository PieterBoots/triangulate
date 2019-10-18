using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

public class Polys
{
    // --------------------------------------------------------------- 

    public Int32 Width = 0;
    public Int32 Height = 0;
    public byte[] src =null; 
    public List<Point> PointsLst_cpy = new List<Point>();
    public int[] minBuffer = new int[4000];
    public int[] maxBuffer = new int[4000];
    //------------------------------------------------------
    public List<Point> PointsLst = new List<Point>();
    public List<Poly> Items = new List<Poly>();      

    //-----------------------

    public void init(ref Bitmap bmp)
    {
        Width = bmp.Width;
        Height = bmp.Height;
        src = new byte[Width * 3 * Height];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Color c = bmp.GetPixel(x, y);
                src[x * 3 + y * Width * 3] = c.R;
                src[x * 3 + 1 + y * Width * 3] = c.G;
                src[x * 3 + 2 + y * Width * 3] = c.B;
            }
        }
    }

    //-----------------------

    public int NearestPoint(int x, int y)
    {
      
        double min = 9999;
        int midlepick = 0;
        for (int i = 0; i < PointsLst.Count; i++)
        {
            Point pnt1 = PointsLst[i];
            double dist = Math.Sqrt((x - pnt1.X) * (x - pnt1.X) + (y - pnt1.Y) * (y - pnt1.Y));
            if (dist < min)
            {
                min = dist;
                midlepick = i;
            }
        }      
        return midlepick;
    }

    //-----------------------


    public void improve(int x, int y, ref byte[] img,Int32 value,Int32 margin)
    {
        Random rnd = new Random();

        int cP = NearestPoint(x, y);
        //find polys;
        Point middle = PointsLst[cP];
        System.Collections.ArrayList run = new System.Collections.ArrayList();
        GetpolysWithPoint(cP, ref run);
        int olderror = ErrorOfpolys(run, ref  img);
        int oldx = PointsLst[cP].X;
        int oldy = PointsLst[cP].Y;              
       
        System.Collections.ArrayList lines = new System.Collections.ArrayList();
        if (oldx != 0 && oldy != 0 && oldx != Width-1 && oldy != Height-1)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            for (int t = 0; t < 5; t++)
            {
                if (cP > 4)
                {
                    PointsLst[cP] = new Point(PointsLst[cP].X + rnd.Next(value * 2) - value, PointsLst[cP].Y + rnd.Next(value * 2) - value);
                }
                int newerror = ErrorOfpolys(run, ref img);


                foreach (int index in run)
                {
                    Poly pol = Items[index];
                    if (pol.IsCounterClockwise)
                    {
                        PointsLst[cP] = new Point(oldx, oldy);
                    }       
                }

                if 
                   (newerror < olderror*(1+margin/1000.0)) 
                        // (newerror < olderror) 
                {
                    olderror = newerror;
                    oldx = PointsLst[cP].X;
                    oldy = PointsLst[cP].Y;
                }
                else
                {
                    PointsLst[cP] = new Point(oldx, oldy);
                }
            }
            sw.Stop();
            long microseconds = sw.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
      //      MessageBox.Show("Operation completed in: " + microseconds + " (us)");
        }
    }

    //----------------------- 


    public void improve_slow(int x, int y, ref byte[] img, Int32 value, Int32 margin)
    {
        Random rnd = new Random();
        //---- stage  1
        int cP = NearestPoint(x, y);        
        Point middle = PointsLst[cP];
        System.Collections.ArrayList pre = new System.Collections.ArrayList();
        GetpolysWithPoint(cP, ref pre);
        //---- stage  2
        System.Collections.ArrayList pnts = new System.Collections.ArrayList();
        GetPointsFromPolys(pre, ref pnts);
        System.Collections.ArrayList run = new System.Collections.ArrayList();
        int[] oldx = new int[pnts.Count];// PointsLst[cP].X;
        int[] oldy = new int[pnts.Count];// PointsLst[cP].Y;     
        for (int j = 0; j < pnts.Count; j++)
        {
            GetpolysWithPoint((int)pnts[j], ref run);
            oldx[j] = PointsLst[(int)pnts[j]].X;
            oldy[j] = PointsLst[(int)pnts[j]].Y;
        }



        int olderror = ErrorOfpolys(run, ref  img);


        System.Collections.ArrayList lines = new System.Collections.ArrayList();
        // if (oldx != 0 && oldy != 0 && oldx != Width - 1 && oldy != Height - 1)
        // {
        Stopwatch sw = new Stopwatch();

        sw.Start();
        int pc = 0;
        int set = 0;
        
        for (int t = 0; t < 5; t++)
        {
            for (int ll = 0; ll < rnd.Next(4);ll++ )
            {
                pc = rnd.Next(pnts.Count - 1);
                set = (int)pnts[pc];
               
                int nx = PointsLst[set].X + rnd.Next(value * 2) - value;
                int ny = PointsLst[set].Y + rnd.Next(value * 2) - value;
                if (nx < 0)
                {
                    nx= 0;
                }
                if (ny < 0)
                {
                    ny = 0;
                }
                if (nx >1024)
                {
                    nx = 1024;
                }
                if (ny >768)
                {
                    ny = 768;
                }
                 PointsLst[set] = new Point(nx,ny );

            }
           

            int newerror = ErrorOfpolys(run, ref img);

            Boolean repair = false;
            foreach (int index in run)
            {
                Poly pol = Items[index];
                if (pol.IsCounterClockwise)
                {
                    repair = true;
                }
            }
     

            if
               ((newerror < olderror * (1 + margin / 1000.0)) && (repair == false))         
            {
                olderror = newerror;
                for (int j = 0; j < pnts.Count; j++)
                {
                    oldx[j] = PointsLst[(int)pnts[j]].X;
                    oldy[j] = PointsLst[(int)pnts[j]].Y;
                }
            }
            else
            {
                for (int j = 0; j < pnts.Count; j++)
                {
                    PointsLst[(int)pnts[j]] = new Point(oldx[j], oldy[j]);
                }
            }
        }
        sw.Stop();
        long microseconds = sw.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
        //      MessageBox.Show("Operation completed in: " + microseconds + " (us)");
        // }
    }

    //-----------------------

    public void GetPointsFromPolys(System.Collections.ArrayList apolys, ref System.Collections.ArrayList run)
    {
        foreach (int idx in apolys)
        {
            Poly pol = Items[idx];
            if (!(run.Contains(pol.p1))){
                run.Add(pol.p1);
            }
            if (!(run.Contains(pol.p2)))
            {
                run.Add(pol.p2);
            }
            if (!(run.Contains(pol.p3)))
            {
                run.Add(pol.p3);
            }
        }
    }

    //-----------------------

    public void GetpolysWithPoint(int midlepick, ref System.Collections.ArrayList run)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].p1 == midlepick || Items[i].p2 == midlepick || Items[i].p3 == midlepick)
            {
                if (!run.Contains(i))
                {
                    Items[i].active = true;
                    run.Add(i);
                }
            }
        }
    }   

    //-----------------------

    //public bool PointExists(int rx, int ry)
    //{
    //    for (int i = 0; i < PointsLst.Count; i++)
    //    {
    //        if ((PointsLst[i].X == rx) && (PointsLst[i].Y == ry))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    //-----------------------

    public void Draw(ref byte[] img)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Poly poly1 = Items[i];
            if (poly1.active == true)
            {
                poly1.Dopoly(this, PointsLst, true, img);             
            }
            poly1.active = false;
        }       
    }
    
    //-----------------------

    public int ErrorOfpolys(System.Collections.ArrayList lst, ref byte[] img)
    {
        int error = 0;       
        foreach (int idx in lst)
        {
            Poly t = Items[idx];          
            error = error + t.Dopoly(this, PointsLst, false);
        }
        return error;
    }

    //-----------------------

    public void recalc()
    {
        int n = 16;
        int s = Width / n + 1;
        int xx = 0;
        int yy = 0;
     

        System.Text.StringBuilder bld = new StringBuilder();
        for (int y = 0; y <= 768 / n; y++)
        {
            for (int x = 0; x <= Width / n; x++)
            {
                if (!PointsLst.Contains(new Point(x * n, y * n)))
                {
                    yy = y * n;
                    xx = x * n;
                    if (yy == Height) { yy = Height-1; }
                    if (xx == Width) { xx = Width-1; }
                    PointsLst.Add(new Point(xx, yy));
                    PointsLst_cpy.Add(new Point(xx, yy));

                }
            }
        }
        for (int y = 0; y < Height / n; y++)
        {
            for (int x = 0; x < Width / n; x++)
            {
                int i1 = PointsLst.IndexOf(new Point(x * n, y * n));
                xx = (x + 1) * n;
                if (xx == Width) { xx = Width-1; }
                int i2 = PointsLst.IndexOf(new Point(xx, y * n));
                yy = (y + 1) * n;
                if (yy == Height) { yy = Height-1; }
                int i3 = PointsLst.IndexOf(new Point(x * n, yy));
                yy = (y + 1) * n;
                xx = (x + 1) * n;
                if (yy == Height) { yy = Height-1; }
                if (xx == Width) { xx = Width-1; }
                int i4 = PointsLst.IndexOf(new Point(xx, yy));
                Poly d1 = new Poly(i2, i3, i1, PointsLst);
                Poly d2 = new Poly(i2, i3, i4, PointsLst);
                Poly d3 = new Poly(i1, i4, i2, PointsLst);
                Poly d4 = new Poly(i1, i4, i3, PointsLst);

                // Todo Parallel
                int err1 = d1.Dopoly(this, PointsLst, false);
                int err2 = d2.Dopoly(this, PointsLst, false);
                int err3 = d3.Dopoly(this, PointsLst, false);
                int err4 = d4.Dopoly(this, PointsLst, false);


                if (err1 + err2 > err3 + err4)
                {
                    Items.Add(d3);
                    Items.Add(d4);
                }
                else
                {
                    Items.Add(d1);
                    Items.Add(d2);
                }
            }
        }
    }

    //-----------------------

}
