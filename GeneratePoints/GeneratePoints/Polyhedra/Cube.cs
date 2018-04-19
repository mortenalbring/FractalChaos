using System.Collections.Generic;
using GeneratePoints.Models;

namespace GeneratePoints.Polyhedra
{
    public class Cube : Shape
    {
        public Cube()
        {
            Settings.Render.CameraZoom = 5;
            ShapeName = "cube";
            var anchors = new List<List<double>>();
            var anchor1 = new List<double> {-1, -1, -1};
            var anchor2 = new List<double> {1, -1, -1};
            var anchor3 = new List<double> {-1, 1, -1};
            var anchor4 = new List<double> {1, 1, -1};
            var anchor5 = new List<double> {-1, 1, 1};
            var anchor6 = new List<double> {1, 1, 1};
            var anchor7 = new List<double> {1, -1, 1};
            var anchor8 = new List<double> {-1, -1, 1};


            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            anchors.Add(anchor4);
            anchors.Add(anchor5);
            anchors.Add(anchor6);
            anchors.Add(anchor7);
            anchors.Add(anchor8);
            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }
}