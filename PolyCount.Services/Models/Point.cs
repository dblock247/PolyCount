using System.Linq;
using System.Text.RegularExpressions;

namespace PolyCount.Services.Models
{
    public class Point
    {
        public int X { get; }
        public int Y { get; }

        public string Id => $"{X}{Y}";

        public string Display => $"({X}, {Y})";

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(string points)
        {
            var coordinates = Regex
                .Replace(points, "[^0-9,]", string.Empty)
                .Split(",")
                .Select(int.Parse)
                .ToList();

            X = coordinates.First();
            Y = coordinates.Last();
        }

        public override string ToString()
        {
            return Display;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var p = (Point) obj;
            return (X == p.X) && (Y == p.Y);
        }

        public static bool operator == (Point lhs, Point rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator != (Point lhs, Point rhs)
        {
            return !(lhs == rhs);
        }
    }
}