using System;
using System.Collections.Generic;
using GeneratePoints.Models;

namespace GeneratePoints.Polyhedra
{
    public class Dodecahedron : Shape
    {
        public Dodecahedron()
        {
            ShapeName = "dodeca";
            var phi = (1 + Math.Sqrt(5)) / 2;
            var nphi = -1 * (1 + Math.Sqrt(5)) / 2;

            var anchors = new List<List<double>>();
            anchors.Add(new List<double> {1,1,1});
            anchors.Add(new List<double> {1,1,-1});
            anchors.Add(new List<double> {1,-1,1});
            anchors.Add(new List<double> {1,-1,-1});
            anchors.Add(new List<double> {-1,1,1});
            anchors.Add(new List<double> {-1,1,-1});
            anchors.Add(new List<double> {-1,-1,1});
            anchors.Add(new List<double> {-1,-1,-1});
            
            
            anchors.Add(new List<double> {0,phi,1/phi});
            anchors.Add(new List<double> {0,nphi,1/phi});
            anchors.Add(new List<double> {0,phi,-1/phi});
            anchors.Add(new List<double> {0,nphi,-1/phi});
            
            anchors.Add(new List<double> {1/phi,0,phi});
            anchors.Add(new List<double> {1/phi,0,nphi});
            anchors.Add(new List<double> {-1/phi,0,phi});
            anchors.Add(new List<double> {-1/phi,0,nphi});

            anchors.Add(new List<double> {phi,1/phi,0});            
            anchors.Add(new List<double> {nphi,1/phi,0});
            anchors.Add(new List<double> {phi,-1/phi,0});            
            anchors.Add(new List<double> {nphi,-1/phi,0});
            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }
}