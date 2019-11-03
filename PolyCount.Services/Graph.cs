using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PolyCount.Core.Extensions;
using PolyCount.Services.Interfaces;
using PolyCount.Services.Models;
using Serilog;

namespace PolyCount.Services
{
    public class Graph : IGraph
    {
        public HashSet<Point> Points { get; set; }

        public string RawDisplay => string
            .Join(", ", Points.ToList()
                .Select(o => o.Display));

        public string Display => $"Points: {RawDisplay}";

        public static Graph Load(string filepath)
        {
            Log.Information($"Loading data from file: {filepath}");
            var points = File.ReadAllLines(filepath)
                .Where(o => !o.IsNullOrWhiteSpace())
                .Select(o => new Point(o))
                .ToHashSet();

            var graph = new Graph { Points = points };
            Log.Information(($"Points Loaded: {graph.Display}"));

            return graph;
        }

        public void Solve()
        {
            var quadrilaterals = new List<Quadrilateral>();

            foreach (var point in Points)
            {
                var points = Points
                    .Where(o => o.Id != point.Id)
                    .ToList();

                quadrilaterals.AddRange(
                    BuildRectangles(new List<Point> {point}, points));
            }

            quadrilaterals = quadrilaterals
                .Distinct()
                .ToList();

            Console.WriteLine($"Results");
            Console.WriteLine("--------");

            Console.WriteLine($"Points Graphed: {Display}");
            Console.WriteLine($"Rectangles: {quadrilaterals.Count(o => o.IsRectangle)}");
            Console.WriteLine($"Squares: {quadrilaterals.Count(o => o.IsSquare)}");

            Console.WriteLine();
            Console.WriteLine("Rectangles:");
            Console.WriteLine("-------------------------------------");

            for (var i = 0; i < quadrilaterals.Count(); ++i)
                Console.WriteLine($"{i + 1}. {quadrilaterals[i].Display} {(quadrilaterals[i].IsSquare ? "*" : string.Empty)}");

            Console.WriteLine();
            Console.WriteLine($"Note: * indicates the rectangle is also a square");
        }

        private static IEnumerable<Quadrilateral> BuildRectangles(IList<Point> rect, IReadOnlyList<Point> points)
        {
            var rectangles = new List<Quadrilateral>();

            foreach (var point in points)
            {
                if (rect.Count == 1)
                {
                    rectangles.AddRange(BuildRectangles(new List<Point> { rect[0], point },
                        points.Where(o => o != point)
                            .ToList()));
                }

                if (rect.Count == 2)
                {
                    var triangle = new Triangle(rect[0], rect[1], point);

                    if (!triangle.IsRightTriangle)
                        continue;

                    var pts = points
                        .Where(o => o != point)
                        .ToList();

                    rectangles.AddRange(
                        pts
                            .Select(pt => new Quadrilateral(rect[0], rect[1], point, pt))
                            .Where(quadrilateral => quadrilateral.IsRectangle));
                }
            }

            return rectangles;
        }

        public static decimal Distance(Point first, Point second)
        {
            return (decimal)Math.Sqrt(Math.Pow(second.X - first.X, 2) + Math.Pow(second.Y -  first.Y, 2));
        }

        public static bool IsEqual(params decimal[] lines)
        {
            for (var i = 1; i < lines.Length; ++i)
                if (lines[0] != lines[i])
                    return false;

            return true;
        }
    }
}