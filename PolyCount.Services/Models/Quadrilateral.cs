using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyCount.Services.Models
{
    public class Quadrilateral : Polygon
    {
        public Point P1 { get; }
        public Point P2 { get; }
        public Point P3 { get; }
        public Point P4 { get; }

        public string Display => $"({P1.X}, {P1.Y}), ({P2.X}, {P2.Y}), ({P3.X}, {P3.Y}), ({P4.X}, {P4.Y})";


        public bool IsRectangle
        {
            get
            {
                foreach (var point in Points)
                {
                    var others = Points.Where(o => o.Id != point.Id)
                        .ToList();

                    var triangle = new Triangle(others[0], others[1], others[2]);

                    if (!triangle.IsRightTriangle)
                        return false;
                }

                return true;
            }
        }

        public bool IsSquare
        {
            get
            {
                var lines = new[]
                {
                    Graph.Distance(P1, P2),
                    Graph.Distance(P2, P4),
                    Graph.Distance(P4, P3),
                    Graph.Distance(P3, P1),
                };

                return IsRectangle && Graph.IsEqual(lines);
            }
        }

        public Quadrilateral(Point p1, Point p2, Point p3, Point p4)
        {
            RawPoints = new HashSet<Point> { p1, p2, p3, p4 };

            if (RawPoints.Count != 4)
                throw  new ArgumentException( "There must be four unique points to create a Quadrilateral");

            Normalize();

            P1 = Points[0];
            P2 = Points[1];
            P3 = Points[2];
            P4 = Points[3];
        }

        public Quadrilateral(IReadOnlyList<Point> points)
        {
            if (points.Count != 4)
                throw  new ArgumentException( "There must be four unique points to create a Quadrilateral");

            RawPoints = new HashSet<Point> { points[0], points[1], points[2], points[3] };

            Normalize();

            P1 = Points[0];
            P2 = Points[1];
            P3 = Points[2];
            P4 = Points[3];
        }

        public override string ToString()
        {
            return Display;
        }

        public override int GetHashCode()
        {
            return Display.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var quadrilateral = (Quadrilateral) obj;
            return quadrilateral.Display == Display;
        }

        public static bool operator == (Quadrilateral lhs, Quadrilateral rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator != (Quadrilateral lhs, Quadrilateral rhs)
        {
            return !(lhs == rhs);
        }
    }
}