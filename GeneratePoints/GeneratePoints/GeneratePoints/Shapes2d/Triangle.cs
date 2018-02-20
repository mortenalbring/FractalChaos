using System.Collections.Generic;

namespace GeneratePoints
{
   
        public class Triangle : Shape
        {
            public Triangle()
            {
                Settings.CameraOffset = 2.5;
                Settings.AnchorRadius = 0.015;
                ShapeName = "triangle";
                var anchors = new List<List<double>>();
                var anchor1 = new List<double> { -1, -1, 0 };
                var anchor2 = new List<double> { 1, -1, 0 };
                var anchor3 = new List<double> { 0, 1, 0 };

                anchors.Add(anchor1);
                anchors.Add(anchor2);
                anchors.Add(anchor3);
                AnchorPoints = MakeAnchorPoints(anchors);
            }
        }

    
}
