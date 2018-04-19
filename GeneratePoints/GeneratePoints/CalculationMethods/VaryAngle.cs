using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GeneratePoints.Models;

namespace GeneratePoints.CalculationMethods
{
    public class VaryAngle
    {
        public static List<string> WriteDataPointsVaryAngle(string shapeName, Settings settings,
            List<AnchorPoint> anchorPoints, string dirname)
        {
            var minAngle = settings.Calculation.AngleMin;
            var maxAngle = settings.Calculation.AngleMax;

            if (minAngle == 0 && maxAngle == 0 && settings.Calculation.Angle != 0)
            {
                minAngle = settings.Calculation.Angle;
                maxAngle = settings.Calculation.Angle;
            }

            var rnd = new Random();


            var sw = new Stopwatch();
            sw.Start();
            var cWriteCount = 0;
            var xmax = 0.0;

            var angleSteps = (maxAngle - minAngle) / settings.Calculation.FrameCount;
            var datapointFiles = new List<string>();
            for (var fIndex = 0; fIndex <= settings.Calculation.FrameCount; fIndex++)
            {
                var angle = minAngle + angleSteps * fIndex;
                var dataPointsFilename = Utility.GetDatapointsFilename(shapeName, settings, "_a" + angle);
                var dataPointsLocation = dirname + "/" + dataPointsFilename;

                if (File.Exists(dataPointsLocation))
                {
                    datapointFiles.Add(dataPointsFilename);

                    continue;
                }

                var output = "";

                var xPoint = 0.0;
                var yPoint = 0.0;
                var zPoint = 0.0;

                var rPoint = 0.0;
                var gPoint = 0.0;
                var bPoint = 0.0;

                for (var i = 0; i < settings.Calculation.MaxDataPoints; i++)
                {
                    var val = rnd.Next(0, anchorPoints.Count);

                    zPoint = (zPoint + anchorPoints[val].Z) * settings.Calculation.Ratio;

                    if (val == anchorPoints.Count - 1)
                    {
                        var cx = anchorPoints[val].X;
                        var cy = anchorPoints[val].Y;

                        var s = Math.Sin(angle);
                        var c = Math.Cos(angle);

                        xPoint = xPoint - cx;
                        yPoint = yPoint - cy;


                        var xnew = xPoint * c - yPoint * s;
                        var ynew = xPoint * s + yPoint * c;
                        xPoint = cx + xnew;
                        yPoint = cy + ynew;
                        xPoint = (xPoint + anchorPoints[val].X) * settings.Calculation.Ratio;
                        yPoint = (yPoint + anchorPoints[val].Y) * settings.Calculation.Ratio;
                    }
                    else
                    {
                        xPoint = (xPoint + anchorPoints[val].X) * settings.Calculation.Ratio;
                        yPoint = (yPoint + anchorPoints[val].Y) * settings.Calculation.Ratio;
                    }

                    if (xPoint > xmax) xmax = xPoint;

                    rPoint = (rPoint + anchorPoints[val].R) * 0.5;
                    gPoint = (gPoint + anchorPoints[val].G) * 0.5;
                    bPoint = (bPoint + anchorPoints[val].B) * 0.5;

                    var outputstr = "<" + xPoint + "," + yPoint + "," + zPoint + ">";

                    output = output + outputstr + ",";

                    output = output + "<" + rPoint + "," + gPoint + "," + bPoint + ">,";
                    cWriteCount++;
                    if (cWriteCount == 1000)
                    {
                        File.AppendAllText(dataPointsLocation, output);
                        output = "";
                        cWriteCount = 0;
                    }
                }

                var timePerElem = sw.Elapsed.TotalSeconds / (fIndex + 1);
                var elemsRemaining = settings.Calculation.FrameCount - fIndex;
                var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");
                Console.WriteLine("Writing points\t" + fIndex + "\t" + settings.Calculation.FrameCount + "\t" +
                                  minsRemaining +
                                  " mins remaining");
                datapointFiles.Add(dataPointsFilename);


                Debug.WriteLine(xmax);
                File.AppendAllText(dataPointsLocation, output);
            }

            return datapointFiles;
        }
    }
}