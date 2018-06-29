using System;
using System.Collections.Generic;

namespace GeneratePoints.Models
{
    public class Polygon : Shape
    {
        public int Vertices;

        public Polygon()
        {
        }

        public Polygon(int vertices)
        {
            Vertices = vertices;
            ShapeName = vertices + "-gon";
            var anchors = CalculateVertices();
            AnchorPoints = MakeAnchorPoints(anchors);
        }

        public List<List<double>> CalculateVertices()
        {
            var anchors = new List<List<double>>();

            var angle = 2 * Math.PI / Vertices;

            for (var i = 0; i < Vertices; i++)
            {
                var x = Math.Sin(i * angle);
                var y = Math.Cos(i * angle);
                var anchor = new List<double> {x, y, 0};
                anchors.Add(anchor);
            }

            return anchors;
        }
    }
}