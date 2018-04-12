using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GeneratePoints.Models;

namespace GeneratePoints
{
    public class BarnsleyFern : Shape
    {
        public BarnsleyFern()
        {
            ShapeName = "barnsley";

            Settings.DataPointRadius = 0.004;
            Settings.CameraZoom = 11;
            Settings.AnchorTransmit = 1.0;

            var anchors = new List<List<double>>();
            var anchor1 = new List<double> { -1, -1, 0 };
            var anchor2 = new List<double> { 1, -1, 0 };
            var anchor3 = new List<double> { 0, 1, 0 };

            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            AnchorPoints = MakeAnchorPoints(anchors);
        }



        public override string WriteDataPoints(string dirname,int currentFrame = 1)
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
            var outputfilename = ShapeName + "_c" + currentFrame + "_p" + Settings.MaxDataPoints + "-datapoints.txt";

            if (!Settings.Overwrite)
            {
                if (File.Exists(outputfilename))
                {
                    return outputfilename;
                }
            }
            File.Delete(outputfilename);
            double yShift = -5;
            var rnd = new Random(42);
            var output = "";

            var a = new List<double> { 0.0, 0.85, 0.2, -0.15 };
            var b = new List<double> { 0.0, 0.04, -0.26, 0.28 };
            var c = new List<double> { 0.0, -0.04, 0.23, 0.26 };
            var d = new List<double> { 0.16, 0.85, 0.22, 0.24 };
            var f = new List<double> { 0, 1.6, 1.6, 0.44 };

            var min = -0.04;
            var max = 0.08;
            var steps = (max - min) / (double)Settings.FrameCount;
            var bval = min + (steps * currentFrame);
            b[2] = bval;

            for (int i = 0; i < Settings.MaxDataPoints; i++)
            {
                var val = rnd.Next(0, 100);
                int n;
                if (val == 1)
                {
                    n = 0;
                }
                else if (val < 85)
                {
                    n = 1;
                }
                else if (val < 93)
                {
                    n = 2;
                }
                else
                {
                    n = 3;
                }
                xPoint = a[n] * xPoint + b[n] * yPoint;
                yPoint = c[n] * xPoint + d[n] * yPoint + f[n];



                var outputstr = "<" + xPoint + "," + (yPoint + yShift) + "," + zPoint + ">";
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

        public void StartRenderProgressive(string dirname)
        {
            var anchorsFilename = WriteAnchorsFile(dirname);
            var dataFiles = new List<string>();
            for (var i = 0; i < Settings.FrameCount; i++)
            {
                var datapointsFilename = WriteDataPoints(dirname,i);
               dataFiles.Add(datapointsFilename);
            }

            var povFilename = PreparePovRayFilesWithIni( dataFiles, anchorsFilename, dirname);
            Console.WriteLine(povFilename);


        }



    }
}
