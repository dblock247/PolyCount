using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyCount.Services.Models
{
    public class Triangle : Polygon
    {
        public Point P1 { get; }
        public Point P2 { get; }
        public Point P3 { get; }

        public string Display => $"({P1.X}, {P1.Y}), ({P2.X}, {P2.Y}), ({P3.X}, {P3.Y})";

        public Triangle(Point p1, Point p2, Point p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;

            RawPoints = new HashSet<Point> { P1, P2, P3 };

            if (RawPoints.Count != 3)
                throw  new ArgumentException("There must be three unique points to create a triangle");
        }

        public override string ToString()
        {
            return Display;
        }

        public bool IsRightTriangle
        {
            get
            {
                var lines = new List<decimal>
                {
                    Graph.Distance(P1, P2),
                    Graph.Distance(P2, P3),
                    Graph.Distance(P1, P3)
                }.OrderBy(o => o).ToList();

                return Math.Round((decimal) Math.Pow((double) lines[0], 2), 4) +
                       Math.Round((decimal) Math.Pow((double) lines[1], 2), 4) ==
                       Math.Round((decimal) Math.Pow((double) lines[2], 2), 4);
            }
        }
    }
}