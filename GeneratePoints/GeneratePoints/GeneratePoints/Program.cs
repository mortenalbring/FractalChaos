using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using GeneratePoints.Shapes3d;

namespace GeneratePoints
{
    class Program
    {
        static void Main(string[] args)
        {
            var tetrahedron = new Tetrahedron();
            var cube = new Cube();
            var ico = new Ico();

           
            var octo = new Octahedron();

            var shape = new Triangle();
            var anchorsFilename = WriteAnchorsFile(shape);

            const int maxPoints = 100000;
            const double ratio = 2.5;
            var datapointsFilename = WriteDataPoints(shape, maxPoints, ratio, true);

            //Console.WriteLine("Done");


            const int frames = 1;

            var inifiles = new List<string>();

            for (var i = 0; i < frames; i++)
            {
                PreparePovRayFiles(i, frames, datapointsFilename, anchorsFilename);

                var inifile = WritePovrayIniFile(i, datapointsFilename);

                inifiles.Add(inifile);


            }

            foreach (var file in inifiles)
            {
                Console.WriteLine("Rendering " + file);
                Process.Start("C:\\Program Files\\POV-Ray\\v3.7\\bin\\pvengine64.exe", "/RENDER " + file);
              //  Thread.Sleep(300000);
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

                outputAnchorStr = outputAnchorStr + "<" + t.R + ", " + t.G + "," +
                                  t.B + ">,";
            }

            File.Delete(outputAnchors);
            File.AppendAllText(outputAnchors, outputAnchorStr);

            return outputAnchors;
        }

        private static void PreparePovRayFiles(int currentFrame, int maxFrames, string datapointsFilename, string anchorsFilename)
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

            double clock = currentFrame / (double)maxFrames;

            var noCamText = File.ReadAllText(nocamPath);



            var pointsFileVar = "#declare strDatapointsFile = \"" + datapointsFilename + "\"; \r\n";
            var anchorsFileVar = "#declare strAnchorsFile = \"" + anchorsFilename + "\"; \r\n";

            var cameraString =
                "\n\n\ncamera {\t\r\n\tlocation <sin(2*pi*" + clock + ")*2.1, 0.1, cos(2*pi*" + clock + ")*2.1>\t\t           \r\n\tlook_at <0,0,0>       \t\r\n\trotate <0,0,0>\r\n}\r\n";

            noCamText = pointsFileVar + anchorsFileVar + cameraString + noCamText;


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

        private static string WriteDataPoints(Shape shape, int maxPoints, double ratio, bool overwrite)
        {
            var rnd = new Random();
            var output = "";

            var xPoint = 0.0;
            var yPoint = 0.0;
            var zPoint = 0.0;

            var rPoint = 0.0;
            var gPoint = 0.0;
            var bPoint = 0.0;


            var outputfilename = shape.ShapeName + "_r" + ratio + "_p" + maxPoints + "-datapoints.txt";

            if (!overwrite)
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

            for (int i = 0; i < maxPoints; i++)
            {
                var val = rnd.Next(0, shape.AnchorPoints.Count);

                xPoint = (xPoint + shape.AnchorPoints[val].X) / ratio;
                yPoint = (yPoint + shape.AnchorPoints[val].Y) / ratio;
                zPoint = (zPoint + shape.AnchorPoints[val].Z) / ratio;

                rPoint = (rPoint + shape.AnchorPoints[val].R) / ratio;
                gPoint = (gPoint + shape.AnchorPoints[val].G) / ratio;
                bPoint = (bPoint + shape.AnchorPoints[val].B) / ratio;


                var outputstr = "<" + xPoint + "," + yPoint + "," + zPoint + ">";
                output = output + outputstr + ",";

                output = output + "<" + rPoint + "," +gPoint + "," + bPoint + ">,";


                cWriteCount++;
                if (cWriteCount == 1000)
                {
                    File.AppendAllText(outputfilename, output);
                    output = "";
                    cWriteCount = 0;

                    double timePerElem = sw.Elapsed.TotalSeconds / (i + 1);
                    var elemsRemaining = maxPoints - i;
                    var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");

                    Console.WriteLine("Writing points\t" + i + "\t" + maxPoints + "\t" + minsRemaining + " mins remaining");
                }
            }

            File.AppendAllText(outputfilename, output);

            return outputfilename;
        }
  
       
    }
}
