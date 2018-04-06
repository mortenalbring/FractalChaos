using System.Collections.Generic;
using GeneratePoints.Models;

namespace GeneratePoints.Shapes2d
{

    public class Square : Shape
    {
        public Square()
        {
            Settings.CameraOffset = 3.5;
            Settings.AnchorRadius = 0.015;
            Settings.FrameCount = 10;
            ShapeName = "Square";
            var anchors = new List<List<double>>();
            var anchor1 = new List<double> { -1, -1, 0 };
            var anchor2 = new List<double> { -1, 1, 0 };
            var anchor3 = new List<double> { 1, -1, 0 };
            var anchor4 = new List<double> { 1, 1, 0 };

            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            anchors.Add(anchor4);
            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }


}
