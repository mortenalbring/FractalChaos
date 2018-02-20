using System;
using System.Collections.Generic;

namespace GeneratePoints.Shapes3d
{
  
        public class Ico : Shape
        {
            public Ico()
            {
                ShapeName = "ico";
                var anchors = new List<List<double>>();
                var phi = (1 + Math.Sqrt(5)) / 2;

                var anchor1 = new List<double> { 0, 0, phi };
                var anchor2 = new List<double> { 0, 0, -phi };

                var anchor3 = new List<double> { 0.5, phi / 2, Math.Sqrt(phi) / 2 };
                var anchor4 = new List<double> { -0.5, phi / 2, Math.Sqrt(phi) / 2 };
                var anchor5 = new List<double> { 0.5, -(phi / 2), Math.Sqrt(phi) / 2 };
                var anchor6 = new List<double> { -0.5, -(phi / 2), Math.Sqrt(phi) / 2 };

                var anchor7 = new List<double> { 0.5, phi / 2, -Math.Sqrt(phi) / 2 };
                var anchor8 = new List<double> { -0.5, phi / 2, -Math.Sqrt(phi) / 2 };
                var anchor9 = new List<double> { 0.5, -(phi / 2), -Math.Sqrt(phi) / 2 };
                var anchor10 = new List<double> { -0.5, -(phi / 2), -Math.Sqrt(phi) / 2 };

                anchors.Add(anchor1);
                anchors.Add(anchor2);
                anchors.Add(anchor3);
                anchors.Add(anchor4);
                anchors.Add(anchor5);
                anchors.Add(anchor6);
                anchors.Add(anchor7);
                anchors.Add(anchor8);
                anchors.Add(anchor9);
                anchors.Add(anchor10);

             
                AnchorPoints = MakeAnchorPoints(anchors);

            }
        }

    
}
