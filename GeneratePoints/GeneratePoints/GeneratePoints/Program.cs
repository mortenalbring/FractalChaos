using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using GeneratePoints.Shapes3d;

namespace GeneratePoints
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var tetrahedron = new Tetrahedron();
            var cube = new Cube();
            var ico = new Ico();


            var octo = new Octahedron();

            var shape = new Triangle();
            shape.Settings.MaxDataPoints = 10000;
            StartRender(shape);
        }

        private static void StartRender(Shape shape)
        {
            var anchorsFilename = WriteAnchorsFile(shape);

            var datapointsFilename = WriteDataPoints(shape);
            var inifiles = new List<string>();

            for (var i = 0; i < shape.Settings.FrameCount; i++)
            {
                PreparePovRayFiles(i, datapointsFilename, anchorsFilename, shape);
                var inifile = WritePovrayIniFile(i, datapointsFilename);
                inifiles.Add(inifile);
            }

            foreach (var file in inifiles)
            {
                Console.WriteLine("Rendering " + file);
                Process.Start(shape.Settings.PovRayPath, "/RENDER " + file);
                // Thread.Sleep(30000);
            }

        }

        private static string WriteAnchorsFile(Shape shape)
        {
            var outputAnchors = shape.ShapeName + "-anchors.txt";
            var outputAnchorStr = "";
       
            foreach (var t in shape.AnchorPoints)
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

        private static void PreparePovRayFiles(int currentFrame, string datapointsFilename, string anchorsFilename, Shape shape)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);

            if (directory == null) return;
            var dirsplit = directory.Split('\\');
            var basedir = dirsplit[0] + "\\" + dirsplit[1];
            const string nocamFile = "fc-nocam.pov";

            var compiledFilename = "fc-nocam_f" + currentFrame + ".pov";

            var nocamPath = Path.Combine(basedir, nocamFile);
            var compiledFile = Path.Combine(directory, compiledFilename);
            if (File.Exists(compiledFile))
            {
                File.Delete(compiledFile);
            }

            double clock = currentFrame / (double)shape.Settings.FrameCount;

            var noCamText = File.ReadAllText(nocamPath);

            
            var pointsFileVar = "#declare strDatapointsFile = \"" + datapointsFilename + "\"; \r\n";
            var anchorsFileVar = "#declare strAnchorsFile = \"" + anchorsFilename + "\"; \r\n";
            var anchorRadiusVar = "#declare nAnchorRadius = " + shape.Settings.AnchorRadius + "; \r\n";
            var datapointRadius = "#declare nDataPointRadius = " + shape.Settings.DataPointRadius + "; \r\n";

            var anchorTransmit = "#declare nAnchorTransmit = " + shape.Settings.AnchorTransmit + "; \r\n";

            var cameraString =
                "\n\n\ncamera {\t\r\n\tlocation <sin(2*pi*" + clock + ")*" + shape.Settings.CameraOffset + ", 0.1, cos(2*pi*" + clock + ")*" + shape.Settings.CameraOffset + ">\t\t           \r\n\tlook_at <0,0,0>       \t\r\n\trotate <0,0,0>\r\n}\r\n";

            noCamText = pointsFileVar + anchorsFileVar + anchorRadiusVar + datapointRadius + anchorTransmit + cameraString + noCamText;


            File.WriteAllText(compiledFile, noCamText);
        }

        private static string WritePovrayIniFile(int currentFrame, string dataPointsFilename)
        {
            var iniFile = "fc-nocam_f" + currentFrame + ".ini";

            var povOutputFilename = dataPointsFilename + "_" + currentFrame.ToString("00000") + ".png";

            var lines = new List<string>();
            lines.Add("Input_File_Name=fc-nocam_f" + currentFrame + ".pov\n");
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

        private static string WriteDataPoints(Shape shape)
        {
            var rnd = new Random();
            var output = "";

            var xPoint = 0.0;
            var yPoint = 0.0;
            var zPoint = 0.0;

            var rPoint = 0.0;
            var gPoint = 0.0;
            var bPoint = 0.0;


            var outputfilename = shape.ShapeName + "_r" + shape.Settings.Ratio + "_p" + shape.Settings.MaxDataPoints + "-datapoints.txt";

            if (!shape.Settings.Overwrite)
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

            for (int i = 0; i < shape.Settings.MaxDataPoints; i++)
            {
                var val = rnd.Next(0, shape.AnchorPoints.Count);

                xPoint = (xPoint + shape.AnchorPoints[val].X) * shape.Settings.Ratio;
                yPoint = (yPoint + shape.AnchorPoints[val].Y) * shape.Settings.Ratio;
                zPoint = (zPoint + shape.AnchorPoints[val].Z) * shape.Settings.Ratio;

                rPoint = (rPoint + shape.AnchorPoints[val].R) * shape.Settings.Ratio;
                gPoint = (gPoint + shape.AnchorPoints[val].G) * shape.Settings.Ratio;
                bPoint = (bPoint + shape.AnchorPoints[val].B) * shape.Settings.Ratio;


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
                    var elemsRemaining = shape.Settings.MaxDataPoints - i;
                    var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");

                    Console.WriteLine("Writing points\t" + i + "\t" + shape.Settings.MaxDataPoints + "\t" + minsRemaining + " mins remaining");
                }
            }

            File.AppendAllText(outputfilename, output);

            return outputfilename;
        }


    }
}
