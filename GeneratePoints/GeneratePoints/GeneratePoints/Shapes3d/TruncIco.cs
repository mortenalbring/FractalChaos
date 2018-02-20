using System;
using System.Collections.Generic;

namespace GeneratePoints.Shapes3d
{
    public class TruncIco : Shape
    {
        public TruncIco()
        {
            ShapeName = "truncico";
            Settings.AnchorRadius = 0.15;
            Settings.DataPointRadius = 0.005;

            Settings.CameraOffset = 5;
            Settings.AnchorTransmit = 1;
            Settings.Ratio = 0.3;

            var phi = (1 + Math.Sqrt(5)) / 2;
            var anchors = new List<List<double>>
            {
                new List<double> {0, 1, 3 * phi},
                new List<double> {0, -1, 3 * phi},
                new List<double> {0, 1, -3 * phi},
                new List<double> {0, -1, -3 * phi},
                new List<double> {1, (2 + phi), 2 * phi},
                new List<double> {1, (2 + phi), -2 * phi},
                new List<double> {1, -1 * (2 + phi), 2 * phi},
                new List<double> {1, -1 * (2 + phi), -2 * phi},
                new List<double> {-1, (2 + phi), 2 * phi},
                new List<double> {-1, (2 + phi), -2 * phi},
                new List<double> {-1, -1 * (2 + phi), 2 * phi},
                new List<double> {-1, -1 * (2 + phi), -2 * phi},
                new List<double> {2, (1 + 2 * phi), phi},
                new List<double> {2, (1 + 2 * phi), -1 * phi},
                new List<double> {2, -1 * (1 + 2 * phi), phi},
                new List<double> {2, -1 * (1 + 2 * phi), -1 * phi},
                new List<double> {-2, (1 + 2 * phi), phi},
                new List<double> {-2, (1 + 2 * phi), -1 * phi},
                new List<double> {-2, -1 * (1 + 2 * phi), phi},
                new List<double> {-2, -1 * (1 + 2 * phi), -1 * phi}
            };





            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }
}
