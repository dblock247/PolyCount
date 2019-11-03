using System.Collections.Generic;
using PolyCount.Services.Models;

namespace PolyCount.Services.Interfaces
{
    public interface IGraph
    {
        HashSet<Point> Points { get; set; }
        string RawDisplay { get; }
        string Display { get; }
        void Solve();
    }
}