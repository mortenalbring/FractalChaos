using System.Collections.Generic;
using GeneratePoints.Models;

namespace GeneratePoints.Polygons
{

    public class Triangle : Shape
    {
        public Triangle()
        {
            Settings.Render.CameraZoom = 2.5;
            Settings.Render.AnchorRadius = 0.015;
            Settings.Calculation.FrameCount = 10;
            ShapeName = "triangle";
            var anchors = new List<List<double>>();
            var anchor1 = new List<double> { -1, -1, 0 };
            var anchor2 = new List<double> { 1, -1, 0 };
            var anchor3 = new List<double> { 0, 1, 0 };

            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            AnchorPoints = MakeAnchorPoints(anchors);
        }        
    }


}
