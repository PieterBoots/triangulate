using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;




public class Tri
{
  // ---------------------------------------------------------------
  // ---------------------------------------------------------------
  // ---------------------------------------------------------------
  //Points (Vertices)
  //public struct dVertex
  //{
  //  public int x;
  //  public int y;
  //  public int z;
  //}

  //Created Triangles, vv# are the vertex pointers
  public struct dTriangle
  {
    public int vv0;
    public int vv1;
    public int vv2;
    public Color col;
    public Boolean active;
    public Boolean externColor;
  }

  public struct dLine
  {
    public int vv0;
    public int vv1;  
  }

  public struct dvertexidx
  {
    public int idx;
    public Point p;
    public Boolean change;
  }

  public struct dpair
  {
    public int i1;
    public int i2;
    public int i3;
    public int i4;
    public int one;
    public int two;  
  }

  //Set these as applicable
  public List<Point> Vertex = new List<Point>();
  public List<Point> Vertex_org = new List<Point>();
  public List<dTriangle> Triangle = new List<dTriangle>();
  public List<dpair> pair = new List<dpair>();
  //public Point[] Vertex;
  //public dTriangle[] Triangle;
  

  public Tri()
  {
    ////Our points
   // recalc();
  }

  //public Boolean FindTwoConnectingTriangles(Point vrP1, Point vrP2, ref int tr1, ref int tr2)
  //{
  //  int tr3 = -1;
  //  int tr4=-1;
  //  Point vert1;
  //  Point vert2;
  //  Point vert3;
  //  int cnt = 0;
  //  tr1 = -1;
  //  tr2 = -1;

  //  // graph.DrawLine(p, new Point(vrP1.X, vrP1.Y), new Point(vrP2.X, vrP2.Y));
  //  vert1 = Vertex[0];
  //  vert2 = Vertex[0];
  //  vert3 = Vertex[0];

  //  if (vrP1.X != vrP2.X || vrP1.Y != vrP2.Y)
  //  {
  //    for (int i = 0; i < Vertex.Count; i++)
  //    {
  //      vert1 = Vertex[Triangle[i].vv0];
  //      vert2 = Vertex[Triangle[i].vv1];
  //      vert3 = Vertex[Triangle[i].vv2];
  //      cnt = 0;
  //      if ((vert1.X == vrP1.X) && (vert1.Y == vrP1.Y) || (vert1.X == vrP2.X) && (vert1.Y == vrP2.Y))
  //      {
  //        cnt = cnt + 1;
  //      }
  //      if ((vert2.X == vrP1.X) && (vert2.Y == vrP1.Y) || (vert2.X == vrP2.X) && (vert2.Y == vrP2.Y))
  //      {
  //        cnt = cnt + 1;
  //      }
  //      if ((vert3.X == vrP1.X) && (vert3.Y == vrP1.Y) || (vert3.X == vrP2.X) && (vert3.Y == vrP2.Y))
  //      {
  //        cnt = cnt + 1;
  //      }


  //      if (cnt >= 2)
  //      {
  //        if (tr1 == -1)
  //        {
  //          tr1 = i;
  //        }
  //        else
  //        {
  //          if (tr2 == -1 && i != tr1)
  //          {
  //            tr2 = i;
  //          }
  //          else
  //          {
  //            if (i != tr1 && i != tr2 && tr3==-1)
  //            {
  //              tr3 = i;
  //            }
  //            else
  //            {
  //              if (i != tr1 && i != tr2 && i!=tr3)
  //              {
  //                tr4 = i;
  //                return true;
  //              }
  //            }
  //          }
  //        }
  //      }
  //    }
  //  }
  //  return false;
  //}

  //public void SplitTwoTriangles(Tri.dLine line, int tr1, ref dTriangle triangle1, ref dTriangle triangle2)
  //{
  //  int i1 = 0;
  //  Point vrP1 = Vertex[line.vv0];
  //  Point vrP2 = Vertex[line.vv1];
   
  //    Point vert1;
  //    Point vert2;
  //    Point vert3;
   
  //    vert1 = Vertex[Triangle[tr1].vv0];
  //    vert2 = Vertex[Triangle[tr1].vv1];
  //    vert3 = Vertex[Triangle[tr1].vv2];

  //    if (((vert1.X == vrP1.X) && (vert1.Y == vrP1.Y) || (vert1.X == vrP2.X) && (vert1.Y == vrP2.Y))
  //      &&
  //      ((vert2.X == vrP1.X) && (vert2.Y == vrP1.Y) || (vert2.X == vrP2.X) && (vert2.Y == vrP2.Y)))
  //    {
  //      i1 = Triangle[tr1].vv2;
  //    }
  //    if (((vert2.X == vrP1.X) && (vert2.Y == vrP1.Y) || (vert2.X == vrP2.X) && (vert2.Y == vrP2.Y))
  //      &&
  //     ((vert3.X == vrP1.X) && (vert3.Y == vrP1.Y) || (vert3.X == vrP2.X) && (vert3.Y == vrP2.Y)))
  //    {
  //      i1 = Triangle[tr1].vv0;
  //    }
  //    if (((vert3.X == vrP1.X) && (vert3.Y == vrP1.Y) || (vert3.X == vrP2.X) && (vert3.Y == vrP2.Y))
  //      &&
  //     ((vert1.X == vrP1.X) && (vert1.Y == vrP1.Y) || (vert1.X == vrP2.X) && (vert1.Y == vrP2.Y)))
  //    {
  //      i1 = Triangle[tr1].vv1;
  //    }
  //    triangle1.vv0 = i1;
  //    triangle1.vv1 = line.vv0;
  //    triangle1.vv2 = -9999;

  //    triangle2.vv0 = i1;
  //    triangle2.vv1 = line.vv1;
  //    triangle2.vv2 = -9999;           
  //}
    


  //public dLine closestLine(int eX, int eY)
  //{
  //  dLine line = new dLine();
  //  PointF pOut;
  //  Point vert1;
  //  Point vert2;
  //  Point vert3;
  //  double minDist = 999999;
  //  for (int i = 0; i < Vertex.Count; i++)
  //  {
  //    vert1 = Vertex[Triangle[i].vv0];
  //    vert2 = Vertex[Triangle[i].vv1];
  //    vert3 = Vertex[Triangle[i].vv2];

  //    double dist1 = H.FindDistanceToSegment(new PointF(eX, eY), new PointF(vert1.X, vert1.Y), new PointF(vert2.X, vert2.Y), out pOut);
  //    if (dist1 < minDist)
  //    {
  //      minDist = dist1;
  //      line.vv0 = Triangle[i].vv0;
  //       line.vv1 = Triangle[i].vv1;
  //    }
  //    double dist2 = H.FindDistanceToSegment(new PointF(eX, eY), new PointF(vert2.X, vert2.Y), new PointF(vert3.X, vert3.Y), out pOut);
  //    if (dist2 < minDist)
  //    {
  //      minDist = dist2;
  //      line.vv0 = Triangle[i].vv1;
  //      line.vv1 = Triangle[i].vv2;
  //    }
  //    double dist3 = H.FindDistanceToSegment(new PointF(eX, eY), new PointF(vert3.X, vert3.Y), new PointF(vert1.X, vert1.Y), out pOut);
  //    if (dist3 < minDist)
  //    {
  //      minDist = dist3;
  //      line.vv0 = Triangle[i].vv2;
  //      line.vv1 = Triangle[i].vv0;
  //    }
  //  }
  //  return line;
  //}


  public int NearestPoint(int x, int y)
  {
    double min = 9999;
    int midlepick = 0;
    Point vr1 = Vertex[0];
    for (int i = 0; i < Vertex.Count; i++)
    {
      vr1 = Vertex[i];
      double dist = Math.Sqrt((x - vr1.X) * (x - vr1.X) + (y - vr1.Y) * (y - vr1.Y));
      if (dist < min)
      {
        min = dist;
        midlepick = i;
      }
    }
    return midlepick;
  }

  public void GetTrianglesWithPoint(int midlepick, ref System.Collections.ArrayList run)
  {
    for (int i = 0; i < Triangle.Count; i++)
    {
      if (Triangle[i].vv0 == midlepick || Triangle[i].vv1 == midlepick || Triangle[i].vv2 == midlepick)
      {
        if (!run.Contains(i))
        {
          run.Add(i);
        }
      }
    }
  }

  //public void GetLinesFromTriangles(ref   System.Collections.ArrayList lines,  System.Collections.ArrayList run)
  //{
  //  foreach (int idx in run)
  //  {
  //    Tri.dTriangle t = Triangle[idx];
  //    dLine l1;
  //    l1.vv0 = t.vv0;
  //    l1.vv1 = t.vv1;
  //    lines.Add(l1);
  //    dLine l2;
  //    l2.vv0 = t.vv1;
  //    l2.vv1 = t.vv2;
  //    lines.Add(l2);
  //    dLine l3;
  //    l3.vv0 = t.vv2;
  //    l3.vv1 = t.vv0;
  //    lines.Add(l3);
  //  }
  //}

  //public void dopairs()
  //{
  //  foreach (dpair apair in pair)
  //  {
  //    int i1 = apair.i1;
  //    int i2 = apair.i2;
  //    int i3 = apair.i3;
  //    int i4 = apair.i4;

  //    dTriangle d1 = new dTriangle();
  //    d1.vv0 = i2;
  //    d1.vv1 = i3;
  //    d1.vv2 = i1;
  //    d1.active = true;

  //    dTriangle d2 = new dTriangle();
  //    d2.vv0 = i2;
  //    d2.vv1 = i3;
  //    d2.vv2 = i4;
  //    d2.active = true;

  //    dTriangle d3 = new dTriangle();
  //    d3.vv0 = i1;
  //    d3.vv1 = i4;
  //    d3.vv2 = i2;
  //    d3.active = true;



  //    dTriangle d4 = new dTriangle();
  //    d4.vv0 = i1;
  //    d4.vv1 = i4;
  //    d4.vv2 = i3;
  //    d4.active = true;


  //    Color c = Color.White;

  //    int err1 = voronoi.Program.frm1.draw_triangle(ref c, Vertex[d1.vv0], Vertex[d1.vv1], Vertex[d1.vv2], false,-1);
  //    int err2 = voronoi.Program.frm1.draw_triangle(ref c, Vertex[d2.vv0], Vertex[d2.vv1], Vertex[d2.vv2], false,-1);

  //    int err3 = voronoi.Program.frm1.draw_triangle(ref c, Vertex[d3.vv0], Vertex[d3.vv1], Vertex[d3.vv2], false,-1);
  //    int err4 = voronoi.Program.frm1.draw_triangle(ref c, Vertex[d4.vv0], Vertex[d4.vv1], Vertex[d4.vv2], false,-1);


  //    if (err1 + err2 > err3 + err4)
  //    {
  //      dTriangle da = Triangle[apair.one];
  //      da.vv0 = d3.vv0;
  //      da.vv1 = d3.vv1;
  //      da.vv2 = d3.vv2;
  //      dTriangle db = Triangle[apair.two];
  //      db.vv0 = d4.vv0;
  //      db.vv1 = d4.vv1;
  //      db.vv2 = d4.vv2;     
  //    }
  //    else
  //    {
  //      dTriangle da = Triangle[apair.one];
  //      da.vv0 = d1.vv0;
  //      da.vv1 = d1.vv1;
  //      da.vv2 = d1.vv2;
  //      dTriangle db = Triangle[apair.two];
  //      db.vv0 = d2.vv0;
  //      db.vv1 = d2.vv1;
  //      db.vv2 = d2.vv2;    
  //    }
  //  }
  //}

  public void recalc()
  {

    int i = 0;
    int j = 0;

    int n = 16;
    int s = 1024 / n + 1;
    int xx = 0;
    int yy = 0;

    System.Text.StringBuilder bld = new StringBuilder();
    for (int y = 0; y <= 768 / n; y++)
    {
      for (int x = 0; x <= 1024 / n; x++)
      {
        if (!Vertex.Contains(new Point(x * n, y * n)))
        {
          yy = y * n;
          xx = x * n;
          if (yy == 768) { yy = 767; }
          if (xx == 1024) { xx = 1023; }
          Vertex.Add(new Point(xx, yy));
          Vertex_org.Add(new Point(xx, yy));
          bld.AppendLine(x.ToString() + "," + y.ToString());
        }
      }
    }
    System.IO.File.WriteAllText("oup.txt", bld.ToString());


    for (int y = 0; y < 768 / n; y++)
    {
      for (int x = 0; x < 1024 / n; x++)
      {
        int i1 = Vertex.IndexOf(new Point(x * n, y * n));

        xx = (x + 1) * n;
        if (xx == 1024) { xx = 1023; }
        int i2 = Vertex.IndexOf(new Point(xx, y * n));

        yy = (y + 1) * n;
        if (yy == 768) { yy = 767; }
        int i3 = Vertex.IndexOf(new Point(x * n, yy));

        yy = (y + 1) * n;
        xx = (x + 1) * n;
        if (yy == 768) { yy = 767; }
        if (xx == 1024) { xx = 1023; }
        int i4 = Vertex.IndexOf(new Point(xx, yy));

        dTriangle d1 = new dTriangle();
        d1.vv0 = i2;
        d1.vv1 = i3;
        d1.vv2 = i1;
        d1.active = true;

       

        dTriangle d2 = new dTriangle();
        d2.vv0 = i2;
        d2.vv1 = i3;
        d2.vv2 = i4;
        d2.active = true;

        dTriangle d3 = new dTriangle();
        d3.vv0 = i1;
        d3.vv1 = i4;
        d3.vv2 = i2;
        d3.active = true;



        dTriangle d4 = new dTriangle();
        d4.vv0 = i1;
        d4.vv1 = i4;
        d4.vv2 = i3;
        d4.active = true;


        Color c = Color.White;

        int err1 = voronoi.Program.frm1.draw_triangle(ref c, Vertex[d1.vv0], Vertex[d1.vv1], Vertex[d1.vv2], false,-1);
        int err2= voronoi.Program.frm1.draw_triangle(ref c, Vertex[d2.vv0], Vertex[d2.vv1], Vertex[d2.vv2], false,-1);

        int err3 = voronoi.Program.frm1.draw_triangle(ref c, Vertex[d3.vv0], Vertex[d3.vv1], Vertex[d3.vv2], false,-1);
        int err4 = voronoi.Program.frm1.draw_triangle(ref c, Vertex[d4.vv0], Vertex[d4.vv1], Vertex[d4.vv2], false,-1);


        if (err1 + err2 > err3 + err4)
        {
          Triangle.Add(d3);
          Triangle.Add(d4);
          dpair apair = new dpair();

          apair.one = Triangle.Count - 1;
          apair.two = Triangle.Count - 2;
          apair.i1 = i1;
          apair.i2 = i2;
          apair.i3 = i3;
          apair.i4 = i4;
          pair.Add(apair);
        }
        else
        {
          Triangle.Add(d1);
          Triangle.Add(d2);
          dpair apair = new dpair();
          apair.one = Triangle.Count - 1;
          apair.two = Triangle.Count - 2;
          apair.i1=i1;
          apair.i2 = i2;
          apair.i3 = i3;
          apair.i4 = i4;
          pair.Add(apair);
        }

        
        
      }
    }
  }


}
  

  