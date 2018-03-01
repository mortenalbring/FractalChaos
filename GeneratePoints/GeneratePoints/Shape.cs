using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace GeneratePoints
{
    public class Shape
    {
        public string ShapeName;
        public List<AnchorPoint> AnchorPoints = new List<AnchorPoint>();

        public Settings Settings = new Settings();

        
        public virtual string WriteAnchorsFile()
        {
            var outputAnchors = ShapeName + "-anchors.txt";
            if (!Settings.Overwrite)
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

            

            File.Delete(outputAnchors);
            File.AppendAllText(outputAnchors, outputAnchorStr);
            return outputAnchors;
        }

        public virtual string WriteDataPoints(int currentFrame = 1)
        {
            var rnd = new Random();
            var output = "";

            var xPoint = 0.0;
            var yPoint = 0.0;
            var zPoint = 0.0;

            var rPoint = 0.0;
            var gPoint = 0.0;
            var bPoint = 0.0;


            var outputfilename = ShapeName + "_r" + Settings.Ratio + "_p" + Settings.MaxDataPoints + "-datapoints.txt";

            if (!Settings.Overwrite)
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

                    double timePerElem = sw.Elapsed.TotalSeconds / (i + 1);
                    var elemsRemaining = Settings.MaxDataPoints - i;
                    var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");

                    Console.WriteLine("Writing points\t" + i + "\t" + Settings.MaxDataPoints + "\t" + minsRemaining + " mins remaining");
                }
            }

            File.AppendAllText(outputfilename, output);

            return outputfilename;
        }

        public virtual void StartRender()
        {
            var anchorsFilename = WriteAnchorsFile();

            var datapointsFilename = WriteDataPoints();
            var inifiles = new List<string>();

            for (var i = 0; i < Settings.FrameCount; i++)
            {
                var dirname = datapointsFilename.Replace(".txt", "").Replace(".","");

                var povFile = PreparePovRayFiles(i, datapointsFilename, anchorsFilename, dirname);
                Console.WriteLine("Written " + povFile);
               // var inifile = WritePovrayIniFile(i, datapointsFilename,povFile);
               // inifiles.Add(inifile);
            }

            foreach (var file in inifiles)
            {
                Console.WriteLine("Rendering " + file);
               // Process.Start(Settings.PovRayPath, "/RENDER " + file);
                // Thread.Sleep(30000);
            }

        }

        public string WritePovrayIniFile(int currentFrame, string dataPointsFilename, string povFilename)
        {
            var iniFile = povFilename + ".ini";

            var povOutputFilename = dataPointsFilename + "_" + currentFrame.ToString("00000") + ".png";

            var lines = new List<string>();
            lines.Add("Input_File_Name=" + povFilename + "\n");
            lines.Add("Output_File_Name=" + povOutputFilename + "\r\n");
            File.WriteAllLines(iniFile, lines);

            var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);

            if (directory == null)
            {
                throw new NullReferenceException();
            }
            var inifilepath = Path.Combine(directory, iniFile);


            return inifilepath;

        }

        public static void CreateDirectory(string dirName)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            if (directory == null)
            {
                throw new Exception("No root directory found");
            }

            var newDir = Path.Combine(directory, dirName);

            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
            }
            else
            {
                var di = new DirectoryInfo(newDir);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

        }
        public string PreparePovRayFiles(int currentFrame,  string datapointsFilename, string anchorsFilename, string dirName)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            if (directory == null) return "";

            var newDir = Path.Combine(directory, dirName);

            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
            }                       

            
            var dirsplit = directory.Split('\\');
            var basedir = dirsplit[0] + "\\" + dirsplit[1];
            const string nocamFile = "fc-nocam.pov";

            var compiledFilename = "fc-" + ShapeName + "_f" + currentFrame.ToString("00000") + ".pov";

            var nocamPath = Path.Combine(basedir, nocamFile);
            var compiledFile = Path.Combine(newDir, compiledFilename);
            if (File.Exists(compiledFile))
            {
                File.Delete(compiledFile);
            }

            double clock = currentFrame / (double)Settings.FrameCount;

            var noCamText = File.ReadAllText(nocamPath);


            var pointsFileVar = "#declare strDatapointsFile = \"../" + datapointsFilename + "\"; \r\n";
            var anchorsFileVar = "#declare strAnchorsFile = \"../" + anchorsFilename + "\"; \r\n";
            var anchorRadiusVar = "#declare nAnchorRadius = " + Settings.AnchorRadius + "; \r\n";
            var datapointRadius = "#declare nDataPointRadius = " + Settings.DataPointRadius + "; \r\n";
            var pointStop = "#declare nPointStop = " + Settings.PointStop + "; \r\n";

            var anchorTransmit = "#declare nAnchorTransmit = " + Settings.AnchorTransmit + "; \r\n";

            var cameraString =
                "\n\n\ncamera {\t\r\n\tlocation <sin(2*pi*" + clock + ")*" + Settings.CameraOffset + ", 0.1, cos(2*pi*" + clock + ")*" + Settings.CameraOffset + ">\t\t           \r\n\tlook_at <0,0,0>       \t\r\n\trotate <0,0,0>\r\n}\r\n";

            noCamText = pointsFileVar + anchorsFileVar + anchorRadiusVar + datapointRadius + anchorTransmit + pointStop + cameraString + noCamText;


            File.WriteAllText(compiledFile, noCamText);

            return compiledFilename;
        }

        public void RenderProgressively(string dirname)
        {
            var anchorsFilename = WriteAnchorsFile();

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

        public static List<AnchorPoint> MakeAnchorPoints(List<List<double>> anchors)
        {

            var colours = new List<List<double>>();

            colours.Add(new List<double> { 1, 0, 0 });
            colours.Add(new List<double> { 0, 1, 0 });
            colours.Add(new List<double> { 0, 0, 1 });
            colours.Add(new List<double> { 1, 1, 0 });
            colours.Add(new List<double> { 1, 0, 1 });
            colours.Add(new List<double> { 0, 1, 1 });
            colours.Add(new List<double> { 1, 1, 1 });
            colours.Add(new List<double> { 0, 0, 0 });


            var rscale = 1 / (double)anchors.Count;
            var gscale = 1 / (double)anchors.Count;
            var bscale = 1 / (double)anchors.Count;
            var rnd = new Random();
            for (int i = 7; i < anchors.Count; i++)
            {
                var val = rnd.Next(0, 7);
                var col = colours[val];

                rscale = (col[0] + col[1])/2;
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