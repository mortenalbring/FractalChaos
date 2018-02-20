using System.Collections.Generic;

namespace GeneratePoints.Shapes3d
{
    
        public class Cube : Shape
        {
            public Cube()
            {
                ShapeName = "cube";
                var anchors = new List<List<double>>();
                var anchor1 = new List<double> { -1, -1, -1 };
                var anchor2 = new List<double> { 1, -1, -1 };
                var anchor3 = new List<double> { -1, 1, -1 };
                var anchor4 = new List<double> { 1, 1, -1 };
                var anchor5 = new List<double> { -1, 1, 1 };
                var anchor6 = new List<double> { 1, 1, 1 };

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
