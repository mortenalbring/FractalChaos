﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using GeneratePoints.GameStyles;

namespace GeneratePoints.Models
{
    /// <summary>
    ///     This is the general class for rendering the chaos game for any particular shape.
    /// </summary>
    public class Shape
    {
        /// <summary>
        ///     The list of anchor points for the shape
        /// </summary>
        public List<AnchorPoint> AnchorPoints = new List<AnchorPoint>();

        /// <summary>
        ///     The rendering and animation settings for this shape
        /// </summary>
        public Settings Settings = new Settings();

        /// <summary>
        ///     The name of the shape (triangle, octohedron, etc.). This is just used as part of the filenames
        /// </summary>
        public string ShapeName;


        public static List<AnchorPoint> MakeAnchorPoints(List<List<double>> anchors)
        {
            var colours = new List<List<double>>
            {
                new List<double> {1, 0, 0},
                new List<double> {0, 1, 0},
                new List<double> {0, 0, 1},
                new List<double> {1, 1, 0},
                new List<double> {1, 0, 1},
                new List<double> {0, 1, 1},
                new List<double> {0.9, 0.9, 0.9},
                new List<double> {0.1, 0.1, 0.1}
            };

            var rnd = new Random();
            for (var i = 7; i < anchors.Count; i++)
            {
                var val = rnd.Next(0, 7);
                var col = colours[val];

                var rscale = (col[0] + col[1]) / 2;
                var gscale = (col[1] + col[2]) / 2;
                var bscale = (col[0] + col[2]) / 2;

                colours.Add(new List<double> {rscale, gscale, bscale});
            }


            var output = new List<AnchorPoint>();
            var p = 0;
            foreach (var anchor in anchors)
            {
                var color = colours[p];
                var anch = new AnchorPoint
                {
                    X = anchor[0],
                    Y = anchor[1],
                    Z = anchor[2],
                    R = color[0],
                    G = color[1],
                    B = color[2]
                };

                output.Add(anch);
                p++;
            }

            return output;
        }

        public virtual string StartRender(string rootDir, string dirname)
        {
            return StartRender(rootDir,dirname, GameStyle.Normal);
        }

        public virtual string StartRender(string rootDir)
        {
            return StartRender(rootDir,ShapeName, GameStyle.Normal);
        }

        public virtual string StartRender(string rootDir,GameStyle gameStyle)
        {
            return StartRender(rootDir,ShapeName, gameStyle);
        }

        private void WriteSettingsFile(string fullDir)
        {
            var settingsFilename = this.ShapeName + ".settings";
            var settingsStr = this.Settings.Calculation.ToString();
            var settingsPath = Path.Combine(fullDir, settingsFilename);
            if (File.Exists(settingsPath))
            {
                File.Delete(settingsPath);
            }
            File.WriteAllText(settingsPath, settingsStr);
        }
        public virtual string StartRender(string rootDir, string subDirectoryName, GameStyle gameStyle)
        {
            var fullDir = Path.Combine(rootDir, subDirectoryName);
           
            switch (gameStyle)
            {
                case GameStyle.Normal:
                    return StartRenderNormal(fullDir);
                case GameStyle.NoRepeat:
                    return StartRenderNoRepeat(fullDir);
                case GameStyle.NoRepeatNearest:
                    return StartRenderNoRepeatNearest(fullDir);
                case GameStyle.VaryRatio:
                    return StartRenderVaryRatio(fullDir);
                case GameStyle.WithAngle:
                    return StartRenderWithAngle(fullDir);
            }

            return StartRenderNormal(fullDir);
        }


        public string MakeAnchorEdgePoints()
        {
            var outputAnchorStr = "";
            
            foreach (var a in AnchorPoints)
            {
                foreach (var o in AnchorPoints)
                {
                    if (a.X == o.X && a.Y == o.Y && a.Z == o.Z)
                    {
                        continue;
                    }


                    var startX = a.X;
                    var endX = o.X;

                    var ancCount = 200;
                    
                    var diffX = (o.X - a.X) / ancCount;
                    var diffY = (o.Y - a.Y) / ancCount;
                    var diffZ = (o.Z - a.Z) / ancCount;
                    
                    var diffR = (o.R - a.R) / ancCount;
                    var diffG = (o.G - a.G) / ancCount;
                    var diffB = (o.B - a.B) / ancCount;

                    for (int i = 0; i < ancCount; i++)
                    {
                        var xVal = a.X + diffX * i;
                        var yVal = a.Y + diffY * i;
                        var zVal = a.Z + diffZ * i;

                        var rVal = a.R + diffR * i;
                        var gVal = a.G + diffG * i;
                        var bVal = a.B + diffB * i;

                        
                        outputAnchorStr += $"<{xVal},{yVal},{zVal}>,<{rVal},{gVal},{bVal}>,";

                    }
                    
                }
            }
       
            return outputAnchorStr;
        }


        private string MakeAnchorVertexPoints()
        {
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

            return outputAnchorStr;
        }
        
        /// <summary>
        ///     Writes the file for the anchor points of the shape
        /// </summary>
        /// <returns>Path to the anchors</returns>
        protected string WriteAnchorsFile(string dirname)
        {
            var anchorOutputFile = ShapeName + "-anchors.txt";
            anchorOutputFile = Path.Combine(dirname, anchorOutputFile);
            if (!Settings.Calculation.Overwrite)
            {
                if (File.Exists(anchorOutputFile))
                {
                    return anchorOutputFile;
                }
            }
            
            var outputAnchorStr = "";
            
            switch (this.Settings.Render.AnchorStyle)
            {
                case AnchorStyle.EdgePoints:
                    outputAnchorStr = MakeAnchorEdgePoints();
                    break;
                case AnchorStyle.VertexPoint:
                    outputAnchorStr = MakeAnchorVertexPoints();
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
            
            if (File.Exists(anchorOutputFile))
            {
                File.Delete(anchorOutputFile);
            }

            File.AppendAllText(anchorOutputFile, outputAnchorStr);
            return anchorOutputFile;
        }

        /// <summary>
        ///     Plays the chaos game and writes the data points file for the co-ordinates and colours of the data point.
        ///     The output file is a series of vectors of 'x,y,z' co-ordinates and then also 'r,g,b' values for each point.
        ///     The format is chosen to be easily read by POV-Ray.
        /// </summary>
        /// <param name="currentFrame"></param>
        /// <returns>Path to data point file</returns>
        public virtual string WriteDataPoints(string dirname, int currentFrame = 1)
        {
            var rnd = new Random();
            var output = "";

            var xPoint = 0.0;
            var yPoint = 0.0;
            var zPoint = 0.0;

            var rPoint = 0.0;
            var gPoint = 0.0;
            var bPoint = 0.0;

            var outputfilename = Utility.GetDatapointsFilename(ShapeName, Settings);
            outputfilename = dirname + "/" + outputfilename;

            if (!Settings.Calculation.Overwrite)
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

            for (var i = 0; i < Settings.Calculation.MaxDataPoints; i++)
            {
                var val = rnd.Next(0, AnchorPoints.Count);

                xPoint = (xPoint + AnchorPoints[val].X) * Settings.Calculation.Ratio;
                yPoint = (yPoint + AnchorPoints[val].Y) * Settings.Calculation.Ratio;
                zPoint = (zPoint + AnchorPoints[val].Z) * Settings.Calculation.Ratio;

                rPoint = (rPoint + AnchorPoints[val].R) * Settings.Calculation.Ratio;
                gPoint = (gPoint + AnchorPoints[val].G) * Settings.Calculation.Ratio;
                bPoint = (bPoint + AnchorPoints[val].B) * Settings.Calculation.Ratio;


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
                    var elemsRemaining = Settings.Calculation.MaxDataPoints - i;
                    var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");

                    Console.WriteLine("Writing points\t" + i + "\t" + Settings.Calculation.MaxDataPoints + "\t" +
                                      minsRemaining + " mins remaining");
                }
            }

            File.AppendAllText(outputfilename, output);
            return outputfilename;
        }


        private string GetAnchorsFilename()
        {
            var outputAnchors = ShapeName + "-anchors.txt";
            return outputAnchors;
        }


        /// <summary>
        ///     Renders but adds constraint that the previous anchor will never be chosen as the next anchor
        /// </summary>
        /// <param name="dirname"></param>
        private string StartRenderNoRepeat(string dirname)
        {
            var dataPointsFilename = Utility.GetDatapointsFilename(ShapeName, Settings);
            var anchorsFilename = GetAnchorsFilename();

            Utility.CreateDirectory(dirname, Settings.Calculation.Overwrite);
            WriteAnchorsFile(dirname);
            NoRepeat.WriteDataPointsNoRepeatAnchor(Settings, AnchorPoints, dirname, dataPointsFilename);
            var dataFiles = new List<string> {dataPointsFilename};
            var povFile = PovRay.PreparePovRayFilesWithIni(Settings, dataFiles, anchorsFilename, dirname);
            Console.WriteLine("Written " + povFile);
            return dataPointsFilename;
        }

        private string StartRenderNoRepeatNearest(string dirname)
        {
            var dataPointsFilename = Utility.GetDatapointsFilename(ShapeName, Settings);
            var anchorsFilename = GetAnchorsFilename();

            Utility.CreateDirectory(dirname, Settings.Calculation.Overwrite);
            WriteAnchorsFile(dirname);
            NoRepeatNearest.WriteDataPointsNoRepeatAnchor(Settings, AnchorPoints, dirname, dataPointsFilename);
            var dataFiles = new List<string> {dataPointsFilename};
            var povFile = PovRay.PreparePovRayFilesWithIni(Settings, dataFiles, anchorsFilename, dirname);
            Console.WriteLine("Written " + povFile);
            return dataPointsFilename;
        }
        
        /// <summary>
        ///     Starts the normal render
        /// </summary>
        private string StartRenderNormal(string dirname)
        {
            var dataPointsFilename = Utility.GetDatapointsFilename(ShapeName, Settings);
            var anchorsFilename = GetAnchorsFilename();

            Utility.CreateDirectory(dirname, Settings.Calculation.Overwrite);
            WriteAnchorsFile(dirname);
            WriteDataPoints(dirname);
            var dataFiles = new List<string> {dataPointsFilename};
            var povFile = PovRay.PreparePovRayFilesWithIniNew(this,Settings, dataFiles, anchorsFilename, dirname);
            Console.WriteLine("Written " + povFile);

            WriteSettingsFile(dirname);
            return dataPointsFilename;
        }

        private string StartRenderVaryRatio(string dirname)
        {
            var anchorsFilename = GetAnchorsFilename();

            Utility.CreateDirectory(dirname, Settings.Calculation.Overwrite);
            WriteAnchorsFile(dirname);
            var dataFiles = new List<string>();

            var minR = Settings.Calculation.RatioMin;
            var maxR = Settings.Calculation.RatioMax;

            for (var i = 0; i < Settings.Calculation.FrameCount; i++)
            {
                var step = i / (double) Settings.Calculation.FrameCount;

                var r = maxR * step + minR;
                Settings.Calculation.Ratio = r;
                //var file = StartRender(dirname);
                var file = WriteDataPoints(dirname);
                dataFiles.Add(file);
            }

            var povFile = PovRay.PreparePovRayFilesWithIni(Settings, dataFiles, anchorsFilename, dirname);
            Console.WriteLine("Written " + povFile);
            return povFile;
        }


        private string StartRenderWithAngle(string dirname)
        {
            Utility.CreateDirectory(dirname, Settings.Calculation.Overwrite);
            var anchorsFilename = GetAnchorsFilename();
            WriteAnchorsFile(dirname);

            var datapointFiles = VaryAngle.WriteDataPointsVaryAngle(ShapeName, Settings, AnchorPoints, dirname);

            var povFile = PovRay.PreparePovRayFilesWithIni(Settings, datapointFiles, anchorsFilename, dirname);

            return povFile;
        }
    }
}