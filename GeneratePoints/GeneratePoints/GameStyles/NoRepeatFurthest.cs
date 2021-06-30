﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GeneratePoints.Models;

namespace GeneratePoints.GameStyles
{
    public class NoRepeatFurthest
    {
               public static string WriteDataPointsNoRepeatFurthestAnchor(Settings settings, List<AnchorPoint> anchorPoints,
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

            
            var furthestAnchorDict = Shape.GetFurthestAnchors(anchorPoints);
            
            
            for (var i = 0; i < settings.Calculation.MaxDataPoints; i++)
            {
                var val = rnd.Next(0, anchorPoints.Count);

                var anchorPoint = anchorPoints[val];
                var prevAnchor = anchorPoints[previousVal];
                var disallowedAnchors = furthestAnchorDict[prevAnchor];

                if (disallowedAnchors.Contains(anchorPoint))
                {
                    while (disallowedAnchors.Contains(anchorPoint))
                    {
                        val = rnd.Next(0, anchorPoints.Count);
                        anchorPoint = anchorPoints[val];
                    }
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

                    Console.WriteLine("Writing points (norepeatfurthest) \t" + i + "\t" + settings.Calculation.MaxDataPoints + "\t" +
                                      minsRemaining + " mins remaining");
                }
            }

            File.AppendAllText(outputfilename, output);
            return outputfilename;
        }
    }
}