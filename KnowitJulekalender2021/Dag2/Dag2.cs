using System.Globalization;

namespace KnowitJulekalender2021.Dag2;

public class Dag2 : ISolution
{
    public void Execute()
    {
        var points = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag2\\cities.csv")
            .Skip(1)
            .Select(a => a.Split(',').Last());

        var pointsParsed = new List<Point>();

        foreach (var point in points)
        {
            pointsParsed.Add(new Point(double.Parse(point.Substring(6).Split(' ').First(), CultureInfo.InvariantCulture), double.Parse(point.Split(' ').Last().Replace(")", ""), CultureInfo.InvariantCulture)));
        }

        var currentPos = new Point(0, 90);
        var dict = new Dictionary<Point, double>();

        var shortestDistance = double.MaxValue;
        var shortestPoint = pointsParsed.First();
        var totalDistance = 0.0;

        while (pointsParsed.Count > 0)
        {
            foreach (var point in pointsParsed)
            {
                var distance = Distance(currentPos, point);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    shortestPoint = point;
                }
            }

            pointsParsed.Remove(shortestPoint);
            totalDistance += shortestDistance;
            currentPos = shortestPoint;
            shortestDistance = double.MaxValue;
        }

        totalDistance += Distance(currentPos, new Point(0, 90));

        Console.WriteLine(Math.Round(totalDistance));
    }

    private double ToRadians(double angle)
    {
        return angle * Math.PI / 180;
    }

    private double Distance(Point point1, Point point2)
    {
        var lon1 = ToRadians(point1.Longitude);
        var lon2 = ToRadians(point2.Longitude);
        var lat1 = ToRadians(point1.Latitude);
        var lat2 = ToRadians(point2.Latitude);

        var dlon = lon2 - lon1;
        var dlat = lat2 - lat1;
        var a = Math.Pow(Math.Sin(dlat / 2), 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Pow(Math.Sin(dlon / 2), 2);

        var c = 2 * Math.Asin(Math.Sqrt(a));

        var r = 6371;

        return c * r;
    }
}

public record Point (double Longitude, double Latitude);
