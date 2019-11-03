using System.Collections.Generic;
using PolyCount.Services.Models;

namespace PolyCount.Services.Interfaces
{
    public interface IPolygon
    {
        HashSet<Point> RawPoints { get; set; }
        List<Point> Points { get; }
        Point Origin { get; }
        HashSet<Point> Normalize();
    }
}