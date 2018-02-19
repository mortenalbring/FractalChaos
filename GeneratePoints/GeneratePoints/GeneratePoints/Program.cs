using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace GeneratePoints
{
    public class Shape
    {
        public string ShapeName;
        public List<AnchorPoint> AnchorPoints = new List<AnchorPoint>();
    }
    public class AnchorPoint
    {
        public double X;
        public double Y;
        public double Z;

        public double R;
        public double G;
        public double B;
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            var tetrahedron = GenerateTetrahedron();

            var cube = GenerateCube();

            var octo = GenerateOctahedron();

            var ico = GenerateIco();


            var shape = octo;            
            var outputAnchors = shape.ShapeName + "-anchors.txt";


            var outputAnchorStr = "";            
          
            for (int i = 0; i < shape.AnchorPoints.Count; i++)
            {                
                var x = shape.AnchorPoints[i].X;
                var y = shape.AnchorPoints[i].Y;
                var z = shape.AnchorPoints[i].Z;
                var outputstr = "<" + x + ", " + y + "," + z + ">";
                outputAnchorStr = outputAnchorStr + outputstr + ",";
                
                outputAnchorStr = outputAnchorStr + "<" + shape.AnchorPoints[i].R + ", " + shape.AnchorPoints[i].G + "," + shape.AnchorPoints[i].B + ">,";

            }

            File.Delete(outputAnchors);            
            File.AppendAllText(outputAnchors, outputAnchorStr);            

            var maxPoints = 1000000;
            var ratio = 2;
            WriteDataPoints(shape, maxPoints,ratio);

            //Console.WriteLine("Done");
           
           
            var frames = 3;

            var inifiles = new List<string>();

            for (int i = 0; i < frames; i++)
            {
                PreparePovRayFiles(i,frames);

                var inifile = WritePovrayIniFile(i,frames);

                inifiles.Add(inifile);

              
            }

            foreach (var file in inifiles)
            {
                Console.WriteLine("Rendering " + file);
             //   Process.Start("C:\\Program Files\\POV-Ray\\v3.7\\bin\\pvengine64.exe", "/RENDER " + file);
           //     Thread.Sleep(300000);
            }

        }

        private static void PreparePovRayFiles(int currentFrame,int maxFrames)
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            var dirsplit = directory.Split('\\');
            var basedir = dirsplit[0] + "\\" + dirsplit[1];
            var nocamFile = "fc-nocam.pov";

            var compiledFilename = "fc-nocam_f" + currentFrame + ".pov";

            var nocamPath = Path.Combine(basedir, nocamFile);
            var compiledFile = Path.Combine(directory, compiledFilename);
            if (File.Exists(compiledFile))
            {
                File.Delete(compiledFile);
            }

            double clock = ((double) currentFrame / (double) maxFrames);

            File.Copy(nocamPath, compiledFile);

            var cameraString =
                "\n\n\ncamera {\t\r\n\tlocation <sin(2*pi*" + clock +")*2.5, 0.1, cos(2*pi* + " + clock +")*2.5>\t\t           \r\n\tlook_at <0,0,0>       \t\r\n\trotate <0,0,0>\r\n}  ";

            File.AppendAllText(compiledFile,cameraString);
        }

        private static string WritePovrayIniFile(int currentFrame, int maxFrames)
        {
            var iniFile = "fc-nocam_f" + currentFrame + ".ini";

            var povOutputFilename = "test_" + currentFrame.ToString("00000") + ".png";

            var lines = new List<string>();
            lines.Add("Input_File_Name=fc-nocam_f" + currentFrame + ".pov\n");
            lines.Add("Output_File_Name=" + povOutputFilename + "\r\n");                     
            File.WriteAllLines(iniFile,lines);

            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

                  
            var inifilepath = Path.Combine(directory, iniFile);

           
            return inifilepath;

        }

        private static void WriteDataPoints(Shape shape, int maxPoints, int ratio)
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


                var outputstr = "<" + xPoint + ", " + yPoint + "," + zPoint + ">";
                output = output + outputstr + ",";

                output = output + "<" + rPoint + ", " + gPoint + "," + bPoint + ">,";


                cWriteCount++;
                if (cWriteCount == 1000)
                {
                    File.AppendAllText(outputfilename, output);
                    output = "";
                    cWriteCount = 0;

                    double timePerElem = (double) sw.Elapsed.TotalSeconds / (double) (i + 1);
                    var elemsRemaining = (maxPoints - i);
                    var minsRemaining = ((elemsRemaining * timePerElem) / 60).ToString("N");

                    Console.WriteLine("Writing points\t" + i + "\t" + maxPoints + "\t" + minsRemaining + " mins remaining");
                }
            }

            File.AppendAllText(outputfilename, output);
        }

        private static List<AnchorPoint> GenerateTetrahedron()
        {
            var anchors = new List<List<double>>();
            var anchor1 = new List<double> { -1, 0, -1 / Math.Sqrt(2) };
            var anchor2 = new List<double> { 1, 0, -1 / Math.Sqrt(2) };
            var anchor3 = new List<double> { 0, 1, 1 / Math.Sqrt(2) };
            var anchor4 = new List<double> { 0, -1, 1 / Math.Sqrt(2) };

            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            anchors.Add(anchor4);
            var rndColor = new Random();

            var output = MakeAnchorPoints(anchors);
            return output;           
        }


        private static List<AnchorPoint> MakeAnchorPoints(List<List<double>> anchors)
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


            var rscale = 1 / anchors.Count;
            var gscale = 1 / anchors.Count;
            var bscale = 1 / anchors.Count;
            for (int i = 7; i < anchors.Count; i++)
            {
                rscale = rscale * i;
                gscale = gscale * i;
                bscale = bscale * 1;

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

        private static Shape GenerateCube()
        {
            var anch = new Shape();
            anch.ShapeName = "cube";

            var anchors = new List<List<double>>();
            var anchor1 = new List<double> { -1, -1, -1 };
            var anchor2 = new List<double> { 1, -1, -1 };
            var anchor3 = new List<double> { -1,  1, -1 };
            var anchor4 = new List<double> {  1,  1, -1 };
            var anchor5 = new List<double> { -1 , 1, 1 };
            var anchor6 = new List<double> { 1 , 1, 1 };

            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            anchors.Add(anchor4);
            anchors.Add(anchor5);
            anchors.Add(anchor6);

            var output = MakeAnchorPoints(anchors);
            anch.AnchorPoints = output;

            return anch;
        }

        private static Shape GenerateOctahedron()
        {
            var anch = new Shape();
            anch.ShapeName = "octahedron";

            var anchors = new List<List<double>>();
            var anchor1 = new List<double> { -1, 0, 0 };
            var anchor2 = new List<double> {  1, 0, 0 };
            var anchor3 = new List<double> { 0, -1, 0 };
            var anchor4 = new List<double> { 0, 1, 0 };
            var anchor5 = new List<double> { 0, 0, -1 };
            var anchor6 = new List<double> { 0, 0, 1 };

            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            anchors.Add(anchor4);
            anchors.Add(anchor5);
            anchors.Add(anchor6);

            var output = MakeAnchorPoints(anchors);
            anch.AnchorPoints = output;

            return anch;
        }
        private static Shape GenerateIco()
        {
            var anch = new Shape();
            anch.ShapeName = "ico";

            var anchors = new List<List<double>>();
            var phi = (1 + Math.Sqrt(5)) / 2;

            var anchor1 = new List<double> { 0, 0, phi };
            var anchor2 = new List<double> { 0, 0, -phi };

            var anchor3 = new List<double> { 0.5, phi/2, Math.Sqrt(phi)/2 };
            var anchor4 = new List<double> { -0.5, phi / 2, Math.Sqrt(phi) / 2 };
            var anchor5 = new List<double> { 0.5, -(phi / 2), Math.Sqrt(phi) / 2 };
            var anchor6 = new List<double> { -0.5, -(phi / 2), Math.Sqrt(phi) / 2 };

            var anchor7 = new List<double> { 0.5, phi / 2, -Math.Sqrt(phi) / 2 };
            var anchor8 = new List<double> { -0.5, phi / 2, -Math.Sqrt(phi) / 2 };
            var anchor9 = new List<double> { 0.5, -(phi / 2), -Math.Sqrt(phi) / 2 };
            var anchor10 = new List<double> { -0.5, -(phi / 2), -Math.Sqrt(phi) / 2 };

            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            anchors.Add(anchor4);
            anchors.Add(anchor5);
            anchors.Add(anchor6);
            anchors.Add(anchor7);
            anchors.Add(anchor8);
            anchors.Add(anchor9);
            anchors.Add(anchor10);

            var output = MakeAnchorPoints(anchors);
            anch.AnchorPoints = output;

            return anch;
        }

    }
}
