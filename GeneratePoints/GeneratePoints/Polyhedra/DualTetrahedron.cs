using System;
using System.Collections.Generic;
using GeneratePoints.Models;

namespace GeneratePoints.Polyhedra
{
    public class DualTetrahedron : Shape
    {
        public DualTetrahedron()
        {
            ShapeName = "dualtetra";
            Settings.Render.CameraZoom = 3;
            var anchors = new List<List<double>>();
            var anchor1 = new List<double> {-1, -1, -1};
            var anchor2 = new List<double> {-1,1,1};
            var anchor3 = new List<double> {1,-1,1};
            var anchor4 = new List<double> {1,1,-1};


            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            anchors.Add(anchor4);

            AnchorPoints = MakeAnchorPoints(anchors);
        }

    }
}