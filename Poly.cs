using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


public class Poly
{
    public List<Point> RefPointsLst = null;
    public int p1;
    public int p2;
    public int p3;
    public Color col;
    public Boolean active;
    public Color rgb = Color.Black;

    public bool IsCounterClockwise
    {
         get { 
        var result = (RefPointsLst[p2].X - RefPointsLst[p1].X) * (RefPointsLst[p3].Y - RefPointsLst[p1].Y) -
            (RefPointsLst[p3].X - RefPointsLst[p1].X) * (RefPointsLst[p2].Y - RefPointsLst[p1].Y);
        return result > 0;
         }
    }

    //-----------------------

    public Poly(int a, int b, int c, List<Point>  aRefPointsLst)
    {
        p1 = a;
        p2 = b;
        p3 = c;
        RefPointsLst = aRefPointsLst;
        active = true;
    }

    //-----------------------

    public void line_to_buffer(ref Polys apolys, Point p1, Point p2)
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
                if (ymin + i >= 0 && ymin + i <= apolys.Height-1)
                {
                    int value = xmin + (xmax - xmin) * i / (ymax - ymin);
                    if (value < apolys.minBuffer[ymin + i])
                    {
                        if (value < 0) { value = 0; }
                        apolys.minBuffer[ymin + i] = value;
                    }
                    if (value > apolys.maxBuffer[ymin + i])
                    {
                        if (value > apolys.Width-1)
                        {
                            value = apolys.Width-1;
                        }
                        apolys.maxBuffer[ymin + i] = value;
                    }
                }
            }
        }
    }

    //-----------------------

    public int max(int a, int b, int c)
    {
        int max = a;
        if (b > max) { max = b; }
        if (c > max) { max = c; }
        return max;
    }

    //-----------------------

    public int min(int a, int b, int c)
    {
        int min = a;
        if (b < min) { min = b; }
        if (c < min) { min = c; }
        return min;
    }

    //-----------------------

    public int Dopoly(Polys apolys, List<Point> aPointsLst, Boolean draw, byte[] img=null)
    {
        double errf = 0;
        int ymax = 0;
        int ymin = 3999;
        Point pnt1 = aPointsLst[p1];
        Point pnt2 = aPointsLst[p2];
        Point pnt3 = aPointsLst[p3];

        ymax = max(pnt1.Y, pnt2.Y, pnt3.Y);
        ymin = min(pnt1.Y, pnt2.Y, pnt3.Y);
        if (ymin < 0) { ymin = 0; };
        if (ymax > apolys.Height - 1) { ymax = apolys.Height-1; };
        for (int i = ymin; i <= ymax; i++)
        {
            apolys.minBuffer[i] = 99999;
            apolys.maxBuffer[i] = 0;
        }
        line_to_buffer(ref apolys, pnt1, pnt2);
        line_to_buffer(ref apolys, pnt2, pnt3);
        line_to_buffer(ref apolys, pnt3, pnt1);
        int totr = 0;
        int totg = 0;
        int totb = 0;
        int cnt = 0;
        for (int i = ymin; i < ymax; i++)
        {
            int tmp = i * apolys.Width * 3;
            for (int j = apolys.minBuffer[i]; j < apolys.maxBuffer[i]; j++)
            {
                totr = totr + apolys.src[j * 3 + tmp];
                totg = totg + apolys.src[j * 3 + 1 + tmp];
                totb = totb + apolys.src[j * 3 + 2 + tmp];
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

        rgb = Color.FromArgb(r, g, b);
        for (int i = ymin; i <= ymax; i++)
        {
            int pos = apolys.minBuffer[i] * 3 + i * apolys.Width * 3;
            for (int j = apolys.minBuffer[i]; j <= apolys.maxBuffer[i]; j++)
            {
                if (draw == true)
                {
                    img[pos] = b;
                    img[pos + 1] = g;
                    img[pos + 2] = r;
                }
                else
                {
                    srcr = apolys.src[pos] - r;
                    srcg = apolys.src[pos + 1] - g;
                    srcb = apolys.src[pos + 2] - b;             
                    errf = errf + (srcr * srcr + srcg * srcg + srcb * srcb);
                }
                pos = pos + 3;
            }
        }
        return (int)errf;
    }

    //-----------------------   
}

