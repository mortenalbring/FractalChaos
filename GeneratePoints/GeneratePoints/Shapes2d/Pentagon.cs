using System;
using System.Collections.Generic;
using GeneratePoints.Models;

namespace GeneratePoints.Shapes2d
{

    public class Pentagon : Shape
    {
        public Pentagon()
        {
            Settings.CameraOffset = 2.2;
            Settings.AnchorRadius = 0.025;
            Settings.FrameCount = 10;
            Settings.RotateCamera = false;       
            Settings.DataPointRadius = 0.0005;
            ShapeName = "Pentagon";
            var anchors = new List<List<double>>();
            var c1 = Math.Cos((2 * Math.PI) / 5);
            var c2 = Math.Cos(Math.PI/5);
            var s1 = Math.Sin((2 * Math.PI) / 5);

            var s2 = Math.Sin((4 * Math.PI) / 5);

            var anchor1 = new List<double> { 0, 1, 0 };
            var anchor2 = new List<double> { -s1, c1, 0 };
            var anchor3 = new List<double> { s1, c1, 0 };
            var anchor4 = new List<double> { -s2, -c2, 0 };
            var anchor5 = new List<double> { s2, -c2, 0 };

            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            anchors.Add(anchor4);
            anchors.Add(anchor5);
            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }


}
