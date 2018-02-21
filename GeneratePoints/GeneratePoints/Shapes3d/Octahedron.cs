using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GeneratePoints.Shapes3d
{

    public class Octahedron : Shape
    {
        public Octahedron()
        {
            Settings.DataPointRadius = 0.002;
            Settings.MaxDataPoints = 10000000;
            Settings.FrameCount = 4000;
            Settings.CameraOffset = 2.2;
            Settings.Overwrite = false;
            ShapeName = "octahedron";
            var anchors = new List<List<double>>();
            var anchor1 = new List<double> {-1, 0, 0};
            var anchor2 = new List<double> {1, 0, 0};
            var anchor3 = new List<double> {0, -1, 0};
            var anchor4 = new List<double> {0, 1, 0};
            var anchor5 = new List<double> {0, 0, -1};
            var anchor6 = new List<double> {0, 0, 1};

            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            anchors.Add(anchor4);
            anchors.Add(anchor5);
            anchors.Add(anchor6);

            AnchorPoints = MakeAnchorPoints(anchors);
        }

        public void RenderProgressively()
        {
            var anchorPoints = WriteAnchorsFile();
            var maxDataPoints = Settings.MaxDataPoints;
            var dirname = "octoprogressive5";

            WriteDataPointsProgressively(dirname, anchorPoints);          

        }

       

        public void WriteDataPointsProgressively(string dirname, string anchorsFilename)
        {
            Shape.CreateDirectory(dirname);
            var rnd = new Random();
            var output = "";

            var xPoint = 0.0;
            var yPoint = 0.0;
            var zPoint = 0.0;

            var rPoint = 0.0;
            var gPoint = 0.0;
            var bPoint = 0.0;



            var sw = new Stopwatch();
            sw.Start();

            var dataPointCount = 0;
            var frameProgress = 0;
            var cWriteCount = 0;
            var frameCountSteps = (int) (Settings.MaxDataPoints / (double) Settings.FrameCount);
            var outputfilename = dirname + "/" + ShapeName + "_r" + Settings.Ratio + "_p" + Settings.MaxDataPoints +
                                 "-datapoints.txt";
            for (int i = 0; i < Settings.MaxDataPoints; i++)
            {

                var val = rnd.Next(0, AnchorPoints.Count);

                xPoint = (xPoint + AnchorPoints[val].X) * Settings.Ratio;
                yPoint = (yPoint + AnchorPoints[val].Y) * Settings.Ratio;
                zPoint = (zPoint + AnchorPoints[val].Z) * Settings.Ratio;

                rPoint = (rPoint + AnchorPoints[val].R) * Settings.Ratio;
                gPoint = (gPoint + AnchorPoints[val].G) * Settings.Ratio;
                bPoint = (bPoint + AnchorPoints[val].B) * Settings.Ratio;


                var outputstr = "<" + xPoint + "," + yPoint + "," + zPoint + ">";
                output = output + outputstr + ",";

                output = output + "<" + rPoint + "," + gPoint + "," + bPoint + ">,";
                cWriteCount++;
                if (cWriteCount == 1000)
                {
                    File.AppendAllText(outputfilename, output);
                    output = "";
                    cWriteCount = 0;
                }

                if (i == dataPointCount)
                {
                    this.Settings.PointStop = dataPointCount;
                    frameProgress++;
                    dataPointCount = dataPointCount + frameCountSteps;


                    double timePerElem = sw.Elapsed.TotalSeconds / (i + 1);
                    var elemsRemaining = Settings.MaxDataPoints - i;
                    var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");
                    Console.WriteLine("Writing points\t" + i + "\t" + Settings.MaxDataPoints + "\t" + minsRemaining +
                                      " mins remaining");

                    PreparePovRayFiles(frameProgress, outputfilename, anchorsFilename, dirname);


                }
            }


        }


    }
}


