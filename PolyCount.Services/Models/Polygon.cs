using System.Collections.Generic;
using System.Linq;
using PolyCount.Services.Interfaces;

namespace PolyCount.Services.Models
{
    public abstract class Polygon : IPolygon
    {
        public HashSet<Point> RawPoints { get; set; }
        public List<Point> Points => RawPoints.ToList();

        public Point Origin
        {
            get
            {
                var xOrigin = Points
                    .OrderBy(o => o.X)
                    .Select(o => o.X)
                    .First();

                var xPoints = Points
                    .Where(o => o.X == xOrigin);

                if (xPoints.Count() == 1)
                    return Points
                        .Single(o => o.X == xOrigin);

                return Points
                    .Where(o => o.X == xOrigin)
                    .OrderBy(o => o.Y)
                    .First();
            }
        }

        public HashSet<Point> Normalize()
        {
            var path = new List<Point> { Origin };

            var pts = Points
                .Where(o => o != Origin)
                .ToList();

            var count = pts.Count;
            for (var i = 0; i < count; ++i)
            {
                pts = pts
                    .Where(o => !path.Contains(o))
                    .ToList();

                var xOrigin = pts
                    .OrderBy(o => o.X)
                    .Select(o => o.X)
                    .First();

                var xPoints = pts
                    .Where(o => o.X == xOrigin)
                    .ToList();

                if (xPoints.Count() == 1)
                {
                    path.Add(pts
                        .Single(o => o.X == xOrigin));

                    continue;
                }

                path.Add(pts
                    .Where(o => o.X == xOrigin)
                    .OrderBy(o => o.Y)
                    .First());
            }

            return RawPoints = path.ToHashSet();
        }

        public HashSet<Point> Paremeter()
        {
            var path = new List<Point> { Origin };

            var pts = Points
                .Where(o => o != Origin)
                .ToList();

            while (pts.Any(o => !path.Contains(o)))
            {
                var xOrigin = pts
                    .OrderBy(o => o.X)
                    .Select(o => o.X)
                    .First();

                var xPts = pts
                    .Where(o => o.X == xOrigin)
                    .OrderBy(o => o.Y)
                    .ToList();
            }

            // TODO: Finish this method

            return path.ToHashSet();
        }
    }
}