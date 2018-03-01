using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GeneratePoints.Shapes3d
{

    public class Octahedron : Shape
    {
        public Octahedron()
        {
            Settings.DataPointRadius = 0.002;
            Settings.MaxDataPoints = 10000000;
            Settings.FrameCount = 4000;
            Settings.CameraOffset = 2.2;
            Settings.Overwrite = false;
            ShapeName = "octahedron";
            var anchors = new List<List<double>>();
            var anchor1 = new List<double> {-1, 0, 0};
            var anchor2 = new List<double> {1, 0, 0};
            var anchor3 = new List<double> {0, -1, 0};
            var anchor4 = new List<double> {0, 1, 0};
            var anchor5 = new List<double> {0, 0, -1};
            var anchor6 = new List<double> {0, 0, 1};

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


