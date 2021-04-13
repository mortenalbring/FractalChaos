using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GeneratePoints.Models;

namespace GeneratePoints
{
    public class PovRay
    {

        public static string PreparePovRayFilesWithIniAndAnchorOutline(Settings settings, List<string> datapointsFilenames,
            string anchorsFilename, List<AnchorPoint> anchorPoints,
            string dirName)
        {
                        var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            if (directory == null)
            {
                return "";
            }

            var newDir = Path.Combine(directory, dirName);

            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
            }

            const string nocamFile = "fc-scene-layout.pov";
            var nocamPath = Path.Combine(directory, nocamFile);
            var withoutPath = datapointsFilenames.Select(file => Path.GetFileName(file)).ToList();
            anchorsFilename = Path.GetFileName(anchorsFilename);

            datapointsFilenames = withoutPath;
            var compiledFilename = Path.GetFileName(datapointsFilenames.First()) + ".pov";

            var compiledFile = Path.Combine(newDir, compiledFilename);
            if (File.Exists(compiledFile))
            {
                File.Delete(compiledFile);
            }


            var noCamText = File.ReadAllText(nocamPath);


            var fileNameStr = "{";
            for (var index = 0; index < datapointsFilenames.Count; index++)
            {
                var file = datapointsFilenames[index];
                fileNameStr = fileNameStr + "\"" + file + "\"";
                if (index < datapointsFilenames.Count - 1)
                {
                    fileNameStr = fileNameStr + ",";
                }
            }

            fileNameStr = fileNameStr + "}";

            var myClockVar = "#declare Start = 0;\r\n#declare End = " + (settings.Calculation.FrameCount - 1) +
                             ";\r\n#declare MyClock = Start+(End-Start)*clock;\r\n";


            var filenameVar = "#declare FileNames = array[" + datapointsFilenames.Count + "] " + fileNameStr + ";\r\n";

            var pointsFileVar = "#declare strDatapointsFile = FileNames[MyClock]; \r\n";
            if (datapointsFilenames.Count == 1)
            {
                pointsFileVar = "#declare strDatapointsFile = FileNames[0]; \r\n";
            }

            var anchorsFileVar = "#declare strAnchorsFile = \"" + anchorsFilename + "\"; \r\n";
            var anchorRadiusVar = "#declare nAnchorRadius = " + settings.Render.AnchorRadius + "; \r\n";
            var datapointRadius = "#declare nDataPointRadius = " + settings.Render.DataPointRadius + "; \r\n";
            var pointStop = "#declare nPointStop = " + settings.Calculation.MaxDataPoints + "; \r\n";
            if (settings.Render.RenderProgressively)
            {
                pointStop = "#declare nPointStop = " + settings.Calculation.MaxDataPoints + "*clock; \r\n";
            }

            var anchorTransmit = "#declare nAnchorTransmit = " + settings.Render.AnchorTransmit + "; \r\n";

            var background = " background { color rgb <1, 1, 1> }";
            var clock = 0;
            var cameraString =
                "\n\n\ncamera {\t\r\n\tlocation <sin(2*pi*" + clock + ")*" + settings.Render.CameraZoom + "," +
                settings.Render.CameraYOffset + ", cos(2*pi*" + clock + ")*" + settings.Render.CameraZoom +
                ">\t\t \r\n\tlook_at <" +
                settings.Render.LookAt[0] + "," + settings.Render.LookAt[1] + "," + settings.Render.LookAt[2] +
                ">       \t\r\n\trotate <0,0,0>\r\n}\r\n";

            if (settings.Render.RotateCamera)
            {
                cameraString =
                    "\n\n\ncamera {\t\r\n\tlocation <sin(2*pi*clock)*" + settings.Render.CameraZoom + "," +
                    settings.Render.CameraYOffset + ", cos(2*pi*clock)*" + settings.Render.CameraZoom +
                    ">\t\t  \r\n\tlook_at <" +
                    settings.Render.LookAt[0] + "," + settings.Render.LookAt[1] + "," + settings.Render.LookAt[2] +
                    ">       \t\r\n\trotate <0,0,0>\r\n}\r\n";
            }

            foreach (var a in anchorPoints)
            {
                foreach (var o in anchorPoints)
                {
                    if (a.X == o.X && a.Y == o.Y && a.Z == o.Z)
                    {
                        continue;
                    }
                    var cylinderStr = $@"cylinder {{
 <{a.X},{a.Y},{a.Z}>,
 <{o.X},{o.Y},{o.Z}>,
 0.0005
open
 pigment {{ rgb <{a.R},{a.G},{a.B}> transmit 0.9 }}
}}                                  
";

                    cameraString += cylinderStr;
                }
            }

            var variableStrings = new List<string>
            {
                myClockVar,
                filenameVar,
                pointsFileVar,
                anchorsFileVar,
                anchorRadiusVar,
                datapointRadius,
                pointStop,
                anchorTransmit
            };
            if (!settings.Render.TransparentBackground)
            {
                variableStrings.Add(background);
            }

            variableStrings.Add(cameraString);

            var vString = string.Join("\r\n", variableStrings.ToArray());

            noCamText = vString + noCamText;


            File.WriteAllText(compiledFile, noCamText);

            WritePovrayIniFile(settings, dirName, compiledFilename);
            return compiledFilename;
            
            
        }
        public static string PreparePovRayFilesWithIni(Settings settings, List<string> datapointsFilenames,
            string anchorsFilename,
            string dirName)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            if (directory == null)
            {
                return "";
            }

            var newDir = Path.Combine(directory, dirName);

            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
            }

            const string nocamFile = "fc-scene-layout.pov";
            var nocamPath = Path.Combine(directory, nocamFile);
            var withoutPath = datapointsFilenames.Select(file => Path.GetFileName(file)).ToList();
            anchorsFilename = Path.GetFileName(anchorsFilename);

            datapointsFilenames = withoutPath;
            var compiledFilename = Path.GetFileName(datapointsFilenames.First()) + ".pov";

            var compiledFile = Path.Combine(newDir, compiledFilename);
            if (File.Exists(compiledFile))
            {
                File.Delete(compiledFile);
            }


            var noCamText = File.ReadAllText(nocamPath);


            var fileNameStr = "{";
            for (var index = 0; index < datapointsFilenames.Count; index++)
            {
                var file = datapointsFilenames[index];
                fileNameStr = fileNameStr + "\"" + file + "\"";
                if (index < datapointsFilenames.Count - 1)
                {
                    fileNameStr = fileNameStr + ",";
                }
            }

            fileNameStr = fileNameStr + "}";

            var myClockVar = "#declare Start = 0;\r\n#declare End = " + (settings.Calculation.FrameCount - 1) +
                             ";\r\n#declare MyClock = Start+(End-Start)*clock;\r\n";


            var filenameVar = "#declare FileNames = array[" + datapointsFilenames.Count + "] " + fileNameStr + ";\r\n";

            var pointsFileVar = "#declare strDatapointsFile = FileNames[MyClock]; \r\n";
            if (datapointsFilenames.Count == 1)
            {
                pointsFileVar = "#declare strDatapointsFile = FileNames[0]; \r\n";
            }

            var anchorsFileVar = "#declare strAnchorsFile = \"" + anchorsFilename + "\"; \r\n";
            var anchorRadiusVar = "#declare nAnchorRadius = " + settings.Render.AnchorRadius + "; \r\n";
            var datapointRadius = "#declare nDataPointRadius = " + settings.Render.DataPointRadius + "; \r\n";
            var pointStop = "#declare nPointStop = " + settings.Calculation.MaxDataPoints + "; \r\n";
            if (settings.Render.RenderProgressively)
            {
                pointStop = "#declare nPointStop = " + settings.Calculation.MaxDataPoints + "*clock; \r\n";
            }

            var anchorTransmit = "#declare nAnchorTransmit = " + settings.Render.AnchorTransmit + "; \r\n";

            var background = " background { color rgb <1, 1, 1> }";
            var clock = 0;
            var cameraString =
                "\n\n\ncamera {\t\r\n\tlocation <sin(2*pi*" + clock + ")*" + settings.Render.CameraZoom + "," +
                settings.Render.CameraYOffset + ", cos(2*pi*" + clock + ")*" + settings.Render.CameraZoom +
                ">\t\t \r\n\tlook_at <" +
                settings.Render.LookAt[0] + "," + settings.Render.LookAt[1] + "," + settings.Render.LookAt[2] +
                ">       \t\r\n\trotate <0,0,0>\r\n}\r\n";

            if (settings.Render.RotateCamera)
            {
                cameraString =
                    "\n\n\ncamera {\t\r\n\tlocation <sin(2*pi*clock)*" + settings.Render.CameraZoom + "," +
                    settings.Render.CameraYOffset + ", cos(2*pi*clock)*" + settings.Render.CameraZoom +
                    ">\t\t  \r\n\tlook_at <" +
                    settings.Render.LookAt[0] + "," + settings.Render.LookAt[1] + "," + settings.Render.LookAt[2] +
                    ">       \t\r\n\trotate <0,0,0>\r\n}\r\n";
            }

            var variableStrings = new List<string>
            {
                myClockVar,
                filenameVar,
                pointsFileVar,
                anchorsFileVar,
                anchorRadiusVar,
                datapointRadius,
                pointStop,
                anchorTransmit
            };
            if (!settings.Render.TransparentBackground)
            {
                variableStrings.Add(background);
            }

            variableStrings.Add(cameraString);

            var vString = string.Join("\r\n", variableStrings.ToArray());

            noCamText = vString + noCamText;


            File.WriteAllText(compiledFile, noCamText);

            WritePovrayIniFile(settings, dirName, compiledFilename);
            return compiledFilename;
        }

        public static void WritePovrayIniFile(Settings settings, string dirname, string povFilename)
        {
            var iniFile = povFilename + ".ini";

            var lines = new List<string>
            {
                "Input_File_Name=" + povFilename + "\n",
                "Output_File_Name=" + povFilename + "\r\n",
                "Initial_Frame=1",
                "Final_Frame=" + settings.Calculation.FrameCount
            };
            var iniPath = dirname + "/" + iniFile;
            File.WriteAllLines(iniPath, lines);
        }
    }
}