using System;
using System.Collections.Generic;
using GeneratePoints.Models;

namespace GeneratePoints.Polyhedra
{
    public class Rhombicuboctahedron : Shape
    {
        public Rhombicuboctahedron()
        {
            ShapeName = "rhombicube";
            var sqrt = (1 + Math.Sqrt(2));
            

            var anchors = new List<List<double>>();
            anchors.Add(new List<double> {1,1,sqrt});
            anchors.Add(new List<double> {1,1,-sqrt});
            anchors.Add(new List<double> {1,-1,sqrt});
            anchors.Add(new List<double> {1,-1,-sqrt});
            anchors.Add(new List<double> {-1,1,sqrt});
            anchors.Add(new List<double> {-1,1,-sqrt});
            anchors.Add(new List<double> {-1,-1,sqrt});
            anchors.Add(new List<double> {-1,-1,-sqrt});

            anchors.Add(new List<double> {1,sqrt,1});
            anchors.Add(new List<double> {1,-sqrt,1});
            anchors.Add(new List<double> {-1,sqrt,1});
            anchors.Add(new List<double> {-1,-sqrt,1});
            anchors.Add(new List<double> {1,sqrt,-1});
            anchors.Add(new List<double> {1,-sqrt,-1});
            anchors.Add(new List<double> {-1,sqrt,-1});
            anchors.Add(new List<double> {-1,-sqrt,-1});
            
            anchors.Add(new List<double> {sqrt,1,1});
            anchors.Add(new List<double> {-sqrt,1,1});
            anchors.Add(new List<double> {sqrt,1,-1});
            anchors.Add(new List<double> {-sqrt,1,-1});
            anchors.Add(new List<double> {sqrt,-1,1});
            anchors.Add(new List<double> {-sqrt,-1,1});
            anchors.Add(new List<double> {sqrt,-1,-1});
            anchors.Add(new List<double> {-sqrt,-1,-1});
            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }
}