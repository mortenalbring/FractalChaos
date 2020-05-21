using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GeneratePoints.Models;

namespace GeneratePoints.GameStyles
{
    /// <summary>
    ///     This variant of the chaos game has the added constraint that the randomly chosen anchor point cannot be either of
    ///     the two anchor points closest to the previous. But it can be the same one.
    /// </summary>
    public class NoRepeatNearest
    {
        public static string WriteDataPointsNoRepeatAnchor(Settings settings, List<AnchorPoint> anchorPoints,
            string dirname, string dataPointsFileName)
        {
            var rnd = new Random();
            var output = "";

            var xPoint = 0.0;
            var yPoint = 0.0;
            var zPoint = 0.0;

            var rPoint = 0.0;
            var gPoint = 0.0;
            var bPoint = 0.0;

            var outputfilename = dataPointsFileName;
            outputfilename = dirname + "/" + outputfilename;

            if (!settings.Calculation.Overwrite)
            {
                if (File.Exists(outputfilename))
                {
                    return outputfilename;
                }
            }

            File.Delete(outputfilename);
            var sw = new Stopwatch();
            sw.Start();
            var cWriteCount = 0;
            var previousVal = 0;

            for (var i = 0; i < settings.Calculation.MaxDataPoints; i++)
            {
                var val = rnd.Next(0, anchorPoints.Count);
                var disallowed = GetDisallowed(previousVal, anchorPoints.Count);


                if (disallowed.Contains(val))
                {
                    while (disallowed.Contains(val))
                        val = rnd.Next(0, anchorPoints.Count);
                }

                previousVal = val;

                xPoint = (xPoint + anchorPoints[val].X) * settings.Calculation.Ratio;
                yPoint = (yPoint + anchorPoints[val].Y) * settings.Calculation.Ratio;
                zPoint = (zPoint + anchorPoints[val].Z) * settings.Calculation.Ratio;

                rPoint = (rPoint + anchorPoints[val].R) * settings.Calculation.Ratio;
                gPoint = (gPoint + anchorPoints[val].G) * settings.Calculation.Ratio;
                bPoint = (bPoint + anchorPoints[val].B) * settings.Calculation.Ratio;


                var outputstr = "<" + xPoint + "," + yPoint + "," + zPoint + ">";
                output = output + outputstr + ",";

                output = output + "<" + rPoint + "," + gPoint + "," + bPoint + ">,";


                cWriteCount++;
                if (cWriteCount == 1000)
                {
                    File.AppendAllText(outputfilename, output);
                    output = "";
                    cWriteCount = 0;

                    var timePerElem = sw.Elapsed.TotalSeconds / (i + 1);
                    var elemsRemaining = settings.Calculation.MaxDataPoints - i;
                    var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");

                    Console.WriteLine("Writing points\t" + i + "\t" + settings.Calculation.MaxDataPoints + "\t" +
                                      minsRemaining + " mins remaining");
                }
            }

            File.AppendAllText(outputfilename, output);
            return outputfilename;
        }

        private static List<int> GetDisallowed(int previous, int anchorPointCount)
        {
            var output = new List<int>();

            if (previous == 0)
            {
                output.Add(1);
                output.Add(anchorPointCount);
                return output;
            }

            if (previous == anchorPointCount)
            {
                output.Add(anchorPointCount - 1);
                output.Add(0);
                return output;
            }

            output.Add(previous + 1);
            output.Add(previous - 1);

            return output;
        }
    }
}