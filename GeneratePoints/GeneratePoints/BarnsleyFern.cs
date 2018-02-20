using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePoints
{
    public class BarnsleyFern : Shape
    {
        public BarnsleyFern()
        {
            ShapeName = "barnsley";
            var anchors = new List<List<double>>();
            var anchor1 = new List<double> { -1, -1, 0 };
            var anchor2 = new List<double> { 1, -1, 0 };
            var anchor3 = new List<double> { 0, 1, 0 };

            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            AnchorPoints = MakeAnchorPoints(anchors);
        }

        public override string WriteAnchorsFile()
        {
            var outputAnchors = ShapeName + "-anchors.txt";
            var outputAnchorStr = "";

            foreach (var t in AnchorPoints)
            {
                var x = t.X;
                var y = t.Y;
                var z = t.Z;
                var outputstr = "<" + x + ", " + y + "," + z + ">";
                outputAnchorStr = outputAnchorStr + outputstr + ",";

                outputAnchorStr = outputAnchorStr + "<" + t.R + "," + t.G + "," + t.B + ">,";
            }

            File.Delete(outputAnchors);
            File.AppendAllText(outputAnchors, outputAnchorStr);
            return outputAnchors;
        }

        public override string WriteDataPoints()
        {
            var xPoint = 0.0;
            var yPoint = 0.0;
            var zPoint = 0.0;

            var rPoint = 0.0;
            var gPoint = 0.8;
            var bPoint = 0.2;

            var sw = new Stopwatch();
            sw.Start();
            
            var cWriteCount = 0;
            var outputfilename = ShapeName + "_p" + Settings.MaxDataPoints + "-datapoints.txt";

            if (!Settings.Overwrite)
            {
                if (File.Exists(outputfilename))
                {
                    return outputfilename;
                }
            }
            var rnd = new Random();
            var output = "";
            for (int i = 0; i < Settings.MaxDataPoints; i++)
            {
                var val = rnd.Next(0, 100);
                if (val == 1)
                {
                    xPoint = 0;
                    yPoint = yPoint * 0.16;
                    continue;
                }

                if (val < 85)
                {
                    xPoint = 0.85 * xPoint + 0.04 * yPoint;
                    yPoint = -0.04 * xPoint + 0.85 * yPoint + 1.6;
                    continue;
                }
                if (val < 92)
                {
                    xPoint = 0.2* xPoint - 0.26 * yPoint;
                    yPoint = 0.23 * xPoint + 0.22 * yPoint + 1.6;
                    continue;
                }
                xPoint = -0.15 * xPoint + 0.28 * yPoint;
                yPoint = 0.26 * xPoint + 0.24 * yPoint + 0.44;

                var outputstr = "<" + xPoint + "," + yPoint + "," + zPoint + ">";
                output = output + outputstr + ",";

                output = output + "<" + rPoint + "," + gPoint + "," + bPoint + ">,";

                cWriteCount++;
                if (cWriteCount == 1000)
                {
                    File.AppendAllText(outputfilename, output);
                    output = "";
                    cWriteCount = 0;

                    double timePerElem = sw.Elapsed.TotalSeconds / (i + 1);
                    var elemsRemaining = Settings.MaxDataPoints - i;
                    var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");

                    Console.WriteLine("Writing points\t" + i + "\t" + Settings.MaxDataPoints + "\t" + minsRemaining + " mins remaining");
                }
            }

            File.AppendAllText(outputfilename, output);

            return outputfilename;
        }
    }
}
