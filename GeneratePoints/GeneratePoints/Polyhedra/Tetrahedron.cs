using System;
using System.Collections.Generic;
using GeneratePoints.Models;

namespace GeneratePoints.Polyhedra
{
    public class Tetrahedron : Shape
    {
        public Tetrahedron()
        {
            ShapeName = "tetra";
            Settings.Render.CameraZoom = 3;
            var anchors = new List<List<double>>();
            var anchor1 = new List<double> {-1, 0, -1 / Math.Sqrt(2)};
            var anchor2 = new List<double> {1, 0, -1 / Math.Sqrt(2)};
            var anchor3 = new List<double> {0, -1, 1 / Math.Sqrt(2)};
            var anchor4 = new List<double> {0, 1, 1 / Math.Sqrt(2)};


            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            anchors.Add(anchor4);

            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }
}