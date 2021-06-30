using System;
using System.Collections.Generic;
using GeneratePoints.Models;

namespace GeneratePoints.Polyhedra
{
    public class Ico : Shape
    {
        public Ico()
        {
            ShapeName = "ico";
            var anchors = new List<List<double>>();
            //var phi = (1 + Math.Sqrt(5)) / 2;
            var phi = 1.618;
            
            anchors.Add(new List<double> {1,0,phi});
            anchors.Add(new List<double> {-1,0,phi});
            anchors.Add(new List<double> {1,0,-phi});
            anchors.Add(new List<double> {-1,0,-phi});
            
            anchors.Add(new List<double> {0,phi,1});
            anchors.Add(new List<double> {0,-phi,1});
            anchors.Add(new List<double> {0,phi,-1});
            anchors.Add(new List<double> {0,-phi,-1});
            
            anchors.Add(new List<double> {phi,1,0});
            anchors.Add(new List<double> {-phi,1,0});
            anchors.Add(new List<double> {phi,-1,0});
            anchors.Add(new List<double> {-phi,-1,0});
            
            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }
}