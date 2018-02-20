using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePoints.Shapes3d
{
    public class TruncIco : Shape
    {
        public TruncIco()
        {
            ShapeName = "truncico";
            AnchorRadius = 0.15;

            var phi = (1 + Math.Sqrt(5)) / 2;
            var anchors = new List<List<double>>();

            anchors.Add(new List<double> { 0, 1, 3 * phi });
            anchors.Add(new List<double> { 0, -1, 3 * phi });
            anchors.Add(new List<double> { 0, 1, -3 * phi });
            anchors.Add(new List<double> { 0, -1, -3 * phi });


            anchors.Add(new List<double> { 1, (2 + phi), 2 * phi });
            anchors.Add(new List<double> { 1, (2 + phi), -2 * phi });
            anchors.Add(new List<double> { 1, -1 * (2 + phi), 2 * phi });
            anchors.Add(new List<double> { 1, -1 * (2 + phi), -2 * phi });
            anchors.Add(new List<double> { -1, (2 + phi), 2 * phi });
            anchors.Add(new List<double> { -1, (2 + phi), -2 * phi });
            anchors.Add(new List<double> { -1, -1 * (2 + phi), 2 * phi });
            anchors.Add(new List<double> { -1, -1 * (2 + phi), -2 * phi });

            anchors.Add(new List<double> { 2, (1 + 2 * phi), phi });
            anchors.Add(new List<double> { 2, (1 + 2 * phi), -1 * phi });
            anchors.Add(new List<double> { 2, -1*(1 + 2 * phi), phi });
            anchors.Add(new List<double> { 2, -1*(1 + 2 * phi), -1 * phi });
            anchors.Add(new List<double> { -2, (1 + 2 * phi), phi });
            anchors.Add(new List<double> { -2, (1 + 2 * phi), -1 * phi });
            anchors.Add(new List<double> { -2, -1*(1 + 2 * phi), phi });
            anchors.Add(new List<double> { -2, -1*(1 + 2 * phi), -1 * phi });

            AnchorPoints = MakeAnchorPoints(anchors);


        }
    }
}
