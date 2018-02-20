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
            var anchorsFilename = shape.WriteAnchorsFile();

            var datapointsFilename = shape.WriteDataPoints();
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

      

    }
}
