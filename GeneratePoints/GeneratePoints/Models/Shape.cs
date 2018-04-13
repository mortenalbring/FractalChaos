using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GeneratePoints.Models
{
    /// <summary>
    /// This is the general class for rendering the chaos game for any particular shape.    
    /// </summary>
    public class Shape
    {
        /// <summary>
        /// The name of the shape (triangle, octohedron, etc.). This is just used as part of the filenames
        /// </summary>
        public string ShapeName;

        /// <summary>
        /// The list of anchor points for the shape
        /// </summary>
        public List<AnchorPoint> AnchorPoints = new List<AnchorPoint>();

        /// <summary>
        /// The rendering and animation settings for this shape
        /// </summary>
        public Settings Settings = new Settings();


        /// <summary>
        /// Writes the file for the anchor points of the shape
        /// </summary>
        /// <returns>Path to the anchors</returns>
        public virtual string WriteAnchorsFile(string dirname)
        {
            var outputAnchors = ShapeName + "-anchors.txt";
            outputAnchors = dirname + "/" + outputAnchors;
            if (!Settings.Calculation.Overwrite)
            {
                if (File.Exists(outputAnchors))
                {
                    return outputAnchors;
                }
            }

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
            if (File.Exists(outputAnchors))
            {
                File.Delete(outputAnchors);
            }

            File.AppendAllText(outputAnchors, outputAnchorStr);
            return outputAnchors;
        }

        private string GetDatapointsFilename(string append = "")
        {
            var outputfilename = ShapeName + "_r" + Settings.Calculation.Ratio + "_p" + Settings.Calculation.MaxDataPoints + append + "-datapoints.txt";
            return outputfilename;
        }

        private string GetAnchorsFilename()
        {
            var outputAnchors = ShapeName + "-anchors.txt";
            return outputAnchors;
        }

        /// <summary>
        /// Plays the chaos game and writes the data points file for the co-ordinates and colours of the data point.
        /// The output file is a series of vectors of 'x,y,z' co-ordinates and then also 'r,g,b' values for each point.
        /// The format is chosen to be easily read by POV-Ray.
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

            var outputfilename = GetDatapointsFilename();
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

            for (int i = 0; i < Settings.Calculation.MaxDataPoints; i++)
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

                    double timePerElem = sw.Elapsed.TotalSeconds / (i + 1);
                    var elemsRemaining = Settings.Calculation.MaxDataPoints - i;
                    var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");

                    Console.WriteLine("Writing points\t" + i + "\t" + Settings.Calculation.MaxDataPoints + "\t" + minsRemaining + " mins remaining");
                }

            }
            File.AppendAllText(outputfilename, output);
            return outputfilename;
        }

        public virtual string WriteDataPointsNoRepeatAnchor(string dirname, int currentFrame = 1)
        {
            var rnd = new Random();
            var output = "";

            var xPoint = 0.0;
            var yPoint = 0.0;
            var zPoint = 0.0;

            var rPoint = 0.0;
            var gPoint = 0.0;
            var bPoint = 0.0;

            var outputfilename = GetDatapointsFilename();
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
            var previousVal = 0;

            for (int i = 0; i < Settings.Calculation.MaxDataPoints; i++)
            {
                var val = rnd.Next(0, AnchorPoints.Count);
                if (val == previousVal)
                {
                    while (val == previousVal)
                    {
                        val = rnd.Next(0, AnchorPoints.Count);
                    }
                }
                previousVal = val;

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

                    double timePerElem = sw.Elapsed.TotalSeconds / (i + 1);
                    var elemsRemaining = Settings.Calculation.MaxDataPoints - i;
                    var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");

                    Console.WriteLine("Writing points\t" + i + "\t" + Settings.Calculation.MaxDataPoints + "\t" + minsRemaining + " mins remaining");
                }

            }
            File.AppendAllText(outputfilename, output);
            return outputfilename;
        }

        /// <summary>
        /// Starts the normal render
        /// </summary>
        public virtual void StartRender(string dirname)
        {
            var dataPointsFilename = GetDatapointsFilename();
            var anchorsFilename = GetAnchorsFilename();
            
            Utility.CreateDirectory(dirname, Settings.Calculation.Overwrite);
            WriteAnchorsFile(dirname);
            WriteDataPoints(dirname);         
            var dataFiles = new List<string>();
            dataFiles.Add(dataPointsFilename);
            var povFile = PovRay.PreparePovRayFilesWithIni(Settings,dataFiles, anchorsFilename, dirname);
            Console.WriteLine("Written " + povFile);           
        }
        public virtual void StartRenderNoRepeat(string dirname)
        {
            var dataPointsFilename = GetDatapointsFilename();
            var anchorsFilename = GetAnchorsFilename();

            Utility.CreateDirectory(dirname, Settings.Calculation.Overwrite);
            WriteAnchorsFile(dirname);
            WriteDataPointsNoRepeatAnchor(dirname);
            var dataFiles = new List<string>();
            dataFiles.Add(dataPointsFilename);
            var povFile = PovRay.PreparePovRayFilesWithIni(Settings,dataFiles, anchorsFilename, dirname);
            Console.WriteLine("Written " + povFile);
        }

       


       

        public void RenderWithAngle(string dirname, double angle)
        {
            RenderWithAngle(dirname,angle,angle);
        }


        public void RenderWithAngle(string dirname, double minAngle, double maxAngle)
        {
            Utility.CreateDirectory(dirname, Settings.Calculation.Overwrite);
            var anchorsFilename = GetAnchorsFilename();
            WriteAnchorsFile(dirname);

            var rnd = new Random();
        

            var sw = new Stopwatch();
            sw.Start();         
            var cWriteCount = 0;        
            var xmax = 0.0;

            var angleSteps = (maxAngle - minAngle) / Settings.Calculation.FrameCount;
            var datapointFiles = new List<string>();
            for (int fIndex = 0; fIndex <= Settings.Calculation.FrameCount; fIndex++)
            {
                var angle = minAngle + (angleSteps * fIndex);
                var dataPointsFilename = GetDatapointsFilename("_a" + angle);
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

                for (int i = 0; i < Settings.Calculation.MaxDataPoints; i++)
                {

                    var val = rnd.Next(0, AnchorPoints.Count);
             
                    zPoint = (zPoint + AnchorPoints[val].Z) * Settings.Calculation.Ratio;

                    if (val == (AnchorPoints.Count - 1))
                    {


                        var cx = AnchorPoints[val].X;
                        var cy = AnchorPoints[val].Y;

                        var s = Math.Sin(angle);
                        var c = Math.Cos(angle);

                        xPoint = xPoint - cx;
                        yPoint = yPoint - cy;


                        var xnew = (xPoint * c) - (yPoint * s);
                        var ynew = (xPoint * s) + (yPoint * c);
                        xPoint = (cx + xnew);
                        yPoint = (cy + ynew);
                        xPoint = (xPoint + AnchorPoints[val].X) * Settings.Calculation.Ratio;
                        yPoint = (yPoint + AnchorPoints[val].Y) * Settings.Calculation.Ratio;

                    }
                    else
                    {
                        xPoint = (xPoint + AnchorPoints[val].X) * Settings.Calculation.Ratio;
                        yPoint = (yPoint + AnchorPoints[val].Y) * Settings.Calculation.Ratio;

                    }

                    if (xPoint > xmax)
                    {
                        xmax = xPoint;
                    }
                   
                    rPoint = (rPoint + AnchorPoints[val].R) * 0.5;
                    gPoint = (gPoint + AnchorPoints[val].G) * 0.5;
                    bPoint = (bPoint + AnchorPoints[val].B) * 0.5;

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

                double timePerElem = sw.Elapsed.TotalSeconds / (fIndex + 1);
                var elemsRemaining = Settings.Calculation.FrameCount - fIndex;
                var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");
                Console.WriteLine("Writing points\t" + fIndex + "\t" + Settings.Calculation.FrameCount + "\t" + minsRemaining +" mins remaining");
                datapointFiles.Add(dataPointsFilename);
                

                Debug.WriteLine(xmax);
                File.AppendAllText(dataPointsLocation, output);

            }

            PovRay.PreparePovRayFilesWithIni(Settings,datapointFiles, anchorsFilename, dirname);

        }



        public static List<AnchorPoint> MakeAnchorPoints(List<List<double>> anchors)
        {

            var colours = new List<List<double>>();

            colours.Add(new List<double> { 1, 0, 0 });
            colours.Add(new List<double> { 0, 1, 0 });
            colours.Add(new List<double> { 0, 0, 1 });
            colours.Add(new List<double> { 1, 1, 0 });
            colours.Add(new List<double> { 1, 0, 1 });
            colours.Add(new List<double> { 0, 1, 1 });
            colours.Add(new List<double> { 0.9, 0.9, 0.9 });
            colours.Add(new List<double> { 0.1, 0.1, 0.1 });


            var rscale = 1 / (double)anchors.Count;
            var gscale = 1 / (double)anchors.Count;
            var bscale = 1 / (double)anchors.Count;
            var rnd = new Random();
            for (int i = 7; i < anchors.Count; i++)
            {
                var val = rnd.Next(0, 7);
                var col = colours[val];

                rscale = (col[0] + col[1]) / 2;
                gscale = (col[1] + col[2]) / 2;
                bscale = (col[0] + col[2]) / 2;

                colours.Add(new List<double> { rscale, gscale, bscale });
            }


            var output = new List<AnchorPoint>();
            var p = 0;
            foreach (var anchor in anchors)
            {

                var anch = new AnchorPoint();

                anch.X = anchor[0];
                anch.Y = anchor[1];
                anch.Z = anchor[2];

                var color = colours[p];

                anch.R = color[0];
                anch.G = color[1];
                anch.B = color[2];
                output.Add(anch);
                p++;
            }
            return output;
        }
    }
}