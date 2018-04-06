using System;
using System.Collections.Generic;
using GeneratePoints.Models;

namespace GeneratePoints.Shapes2d
{

    public class Hexagon : Shape
    {
        public Hexagon()
        {
            Settings.CameraOffset = 2.2;
            Settings.AnchorRadius = 0.025;
            Settings.FrameCount = 10;
            Settings.RotateCamera = false;
            Settings.AnchorTransmit = 0.7;
            Settings.DataPointRadius = 0.005;
            ShapeName = "Hexagon";
            var anchors = new List<List<double>>();

            double b = (1.0 / 2.0);
            double a = 1.73 / 2.0;
            

            var anchor1 = new List<double> { -b, a, 0 };
            var anchor2 = new List<double> { b, a, 0 };
            var anchor3 = new List<double> { -1, 0, 0 };
            var anchor4 = new List<double> { 1, 0, 0 };
            var anchor5 = new List<double> { -b, -a, 0 };
            var anchor6 = new List<double> { b, -a, 0 };

            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            anchors.Add(anchor4);
            anchors.Add(anchor5);
            anchors.Add(anchor6);
            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }


}
