using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace voronoi
{

    public class DelaunayTriangulator
    {       
     
        public IEnumerable<Triangle> BowyerWatson(IEnumerable<DelaunayPoint> points, List<Triangle> border)
        {
            //var supraTriangle = GenerateSupraTriangle();
            var triangulation = new HashSet<Triangle>(border);

            foreach (var point in points)
            {
                var badTriangles = FindBadTriangles(point, triangulation);
                var polygon = FindHoleBoundaries(badTriangles);

                foreach (var triangle in badTriangles)
                {
                    foreach (var vertex in triangle.Vertices)
                    {
                        vertex.AdjacentTriangles.Remove(triangle);
                    }
                }
                triangulation.RemoveWhere(o => badTriangles.Contains(o));

                foreach (var edge in polygon)
                {
                    var triangle = new Triangle(point, edge.Point1, edge.Point2);
                    triangulation.Add(triangle);
                }
            }

            //triangulation.RemoveWhere(o => o.Vertices.Any(v => supraTriangle.Vertices.Contains(v)));
            return triangulation;
        }

        private List<Edge> FindHoleBoundaries(ISet<Triangle> badTriangles)
        {
            var edges = new List<Edge>();
            foreach (var triangle in badTriangles)
            {
                edges.Add(new Edge(triangle.Vertices[0], triangle.Vertices[1]));
                edges.Add(new Edge(triangle.Vertices[1], triangle.Vertices[2]));
                edges.Add(new Edge(triangle.Vertices[2], triangle.Vertices[0]));
            }
            var grouped = edges.GroupBy(o => o);
            var boundaryEdges = edges.GroupBy(o => o).Where(o => o.Count() == 1).Select(o => o.First());
            return boundaryEdges.ToList();
        }

        //private Triangle GenerateSupraTriangle()
        //{
        //    //   1  -> maxX
        //    //  / \
        //    // 2---3
        //    // |
        //    // v maxY
        //    var margin = 500;
        //    var point1 = new DelaunayPoint(0.5 * MaxX, -2 * MaxX - margin);
        //    var point2 = new DelaunayPoint(-2 * MaxY - margin, 2 * MaxY + margin);
        //    var point3 = new DelaunayPoint(2 * MaxX + MaxY + margin, 2 * MaxY + margin);
        //    return new Triangle(point1, point2, point3);
        //}

        private ISet<Triangle> FindBadTriangles(DelaunayPoint point, HashSet<Triangle> triangles)
        {
            var badTriangles = triangles.Where(o => o.IsPointInsideCircumcircle(point));
            return new HashSet<Triangle>(badTriangles);
        }
    }

    public class Edge
    {
        public DelaunayPoint Point1;
        public DelaunayPoint Point2;

        public Edge(DelaunayPoint point1, DelaunayPoint point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            var edge = obj as Edge;

            var samePoints = Point1 == edge.Point1 && Point2 == edge.Point2;
            var samePointsReversed = Point1 == edge.Point2 && Point2 == edge.Point1;
            return samePoints || samePointsReversed;
        }

        public override int GetHashCode()
        {
            int hCode = (int)Point1.X ^ (int)Point1.Y ^ (int)Point2.X ^ (int)Point2.Y;
            return hCode.GetHashCode();
        }
    }



    public class DelaunayPoint
    {
        public double X;
        public double Y;

        public HashSet<Triangle> FAdjacentTriangles = new HashSet<Triangle>();

        public HashSet<Triangle> AdjacentTriangles
        {
            get
            {
                return FAdjacentTriangles;
            }
        }

        public DelaunayPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public string Key()
        {
            return ((int)X).ToString() + '-' +
                      ((int)Y).ToString();
        }

        public Point GetPoint()
        {
            return new Point((int)X, (int)Y);
        }
    }

    public class Triangle
    {

        public DelaunayPoint Circumcenter { get; private set; }
        public double RadiusSquared;
        public DelaunayPoint[] Vertices = new DelaunayPoint[3];




        public IEnumerable<Triangle> TrianglesWithSharedEdge
        {
            get
            {
                var neighbors = new HashSet<Triangle>();
                foreach (var vertex in Vertices)
                {
                    var trianglesWithSharedEdge = vertex.AdjacentTriangles.Where(o =>
                    {
                        return o != this && SharesEdgeWith(o);
                    });
                    neighbors.UnionWith(trianglesWithSharedEdge);
                }

                return neighbors;
            }
        }

        public Triangle(DelaunayPoint point1, DelaunayPoint point2, DelaunayPoint point3)
        {
            if (!IsCounterClockwise(point1, point2, point3))
            {
                Vertices[0] = point1;
                Vertices[1] = point3;
                Vertices[2] = point2;
            }
            else
            {
                Vertices[0] = point1;
                Vertices[1] = point2;
                Vertices[2] = point3;
            }

            Vertices[0].AdjacentTriangles.Add(this);
            Vertices[1].AdjacentTriangles.Add(this);
            Vertices[2].AdjacentTriangles.Add(this);
            UpdateCircumcircle();
        }

        private void UpdateCircumcircle()
        {
            // https://codefound.wordpress.com/2013/02/21/how-to-compute-a-circumcircle/#more-58
            // https://en.wikipedia.org/wiki/Circumscribed_circle
            var p0 = Vertices[0];
            var p1 = Vertices[1];
            var p2 = Vertices[2];
            var dA = p0.X * p0.X + p0.Y * p0.Y;
            var dB = p1.X * p1.X + p1.Y * p1.Y;
            var dC = p2.X * p2.X + p2.Y * p2.Y;

            var aux1 = (dA * (p2.Y - p1.Y) + dB * (p0.Y - p2.Y) + dC * (p1.Y - p0.Y));
            var aux2 = -(dA * (p2.X - p1.X) + dB * (p0.X - p2.X) + dC * (p1.X - p0.X));
            var div = (2 * (p0.X * (p2.Y - p1.Y) + p1.X * (p0.Y - p2.Y) + p2.X * (p1.Y - p0.Y)));

            if (div == 0)
            {
                throw new System.Exception();
            }

            var center = new DelaunayPoint(aux1 / div, aux2 / div);
            Circumcenter = center;
            RadiusSquared = (center.X - p0.X) * (center.X - p0.X) + (center.Y - p0.Y) * (center.Y - p0.Y);
        }

        private bool IsCounterClockwise(DelaunayPoint point1, DelaunayPoint point2, DelaunayPoint point3)
        {
            var result = (point2.X - point1.X) * (point3.Y - point1.Y) -
                (point3.X - point1.X) * (point2.Y - point1.Y);
            return result > 0;
        }

        public bool SharesEdgeWith(Triangle triangle)
        {
            var sharedVertices = Vertices.Where(o => triangle.Vertices.Contains(o)).Count();
            return sharedVertices == 2;
        }

        public bool IsPointInsideCircumcircle(DelaunayPoint point)
        {
            var d_squared = (point.X - Circumcenter.X) * (point.X - Circumcenter.X) +
                (point.Y - Circumcenter.Y) * (point.Y - Circumcenter.Y);
            return d_squared < RadiusSquared;
        }
    }
}
