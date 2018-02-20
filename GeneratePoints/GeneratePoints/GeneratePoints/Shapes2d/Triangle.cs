using System.Collections.Generic;

namespace GeneratePoints
{
   
        public class Triangle : Shape
        {
            public Triangle()
            {
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
