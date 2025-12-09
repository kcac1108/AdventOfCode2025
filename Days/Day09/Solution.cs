namespace Days.Day09;

public class Solution : BaseSolution
{
    private static readonly Input Input = new(File.ReadAllText("Input Files/09.txt"));
    
    public override object RunPart1()
    {
        var pontos = Input.ToCoordinateList();

        long maxArea = 0;

        for (var i = 0; i < pontos.Count; i++)
        {
            for (var j = i + 1; j < pontos.Count; j++)
            {
                var p1 = pontos[i];
                var p2 = pontos[j];

                long width = Math.Abs(p1.X - p2.X) + 1;
                long height = Math.Abs(p1.Y - p2.Y) + 1;
                
                var area = width * height;

                if (area > maxArea)
                {
                    maxArea = area;
                }
            }
        }
        return maxArea;
    }

    private record Segment(long X1, long Y1, long X2, long Y2)
    {
        public bool IsHorizontal => Y1 == Y2;
        public long MinX => Math.Min(X1, X2);
        public long MaxX => Math.Max(X1, X2);
        public long MinY => Math.Min(Y1, Y2);
        public long MaxY => Math.Max(Y1, Y2);
    }

    public override object RunPart2()
    {
        var points = Input.ToCoordinateList();

        var segments = new List<Segment>();
        for (var i = 0; i < points.Count; i++)
        {
            var p1 = points[i];
            var p2 = points[(i + 1) % points.Count];
            segments.Add(new Segment(p1.X, p1.Y, p2.X, p2.Y));
        }

        long maxArea = 0;

        for (var i = 0; i < points.Count; i++)
        {
            for (var j = i + 1; j < points.Count; j++)
            {
                var p1 = points[i];
                var p2 = points[j];

                long rx1 = Math.Min(p1.X, p2.X);
                long rx2 = Math.Max(p1.X, p2.X);
                long ry1 = Math.Min(p1.Y, p2.Y);
                long ry2 = Math.Max(p1.Y, p2.Y);

                var currentArea = (rx2 - rx1 + 1) * (ry2 - ry1 + 1);

                if (currentArea <= maxArea) continue;

                if (IsRectangleValid(rx1, rx2, ry1, ry2, segments))
                {
                    maxArea = currentArea;
                }
            }
        }

        return maxArea;
    }
    
    private static bool IsRectangleValid(long rx1, long rx2, long ry1, long ry2, List<Segment> segments)
    {
        foreach (var seg in segments)
        {
            if (seg.IsHorizontal)
            {
                if (seg.Y1 <= ry1 || seg.Y1 >= ry2) continue;
                var overlapStart = Math.Max(seg.MinX, rx1);
                var overlapEnd = Math.Min(seg.MaxX, rx2);
                if (overlapStart < overlapEnd) return false;
            }
            else
            {
                if (seg.X1 <= rx1 || seg.X1 >= rx2) continue;
                var overlapStart = Math.Max(seg.MinY, ry1);
                var overlapEnd = Math.Min(seg.MaxY, ry2);
                if (overlapStart < overlapEnd) return false;
            }
        }

        var centerX = (rx1 + rx2) / 2.0;
        var centerY = (ry1 + ry2) / 2.0;

        return IsPointInPolygon(centerX, centerY, segments);
    }
    
    private static bool IsPointInPolygon(double x, double y, List<Segment> segments)
    {
        var intersections = segments.Where(seg => !seg.IsHorizontal).Count(seg => seg.X1 > x && y >= seg.MinY && y <= seg.MaxY);

        return (intersections % 2) != 0;
    }
}