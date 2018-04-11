using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using GeneratePoints.Models;
using GeneratePoints.Polygons;
using GeneratePoints.Shapes3d;

namespace GeneratePoints
{
    /// <summary>
    /// This program generates the data points and POV-Ray files for rendering the chaos game for various different shapes
    /// It generates :
    ///  a file for the co-ordinates of the initial anchor points and their colours
    ///  a file for all the data points for the chaos game
    ///  a series of POV-Ray files for the render    
    /// Each Shape has various settings that control the render options and animation settings
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
        
            //PentagonPost();
      //HexagonPost();
            //TrianglePost2();
          //  TrianglePost3();
         //SquarePost();

           // var s = new Septagon();
            //s.RenderProgressively("sept1");
            //SeptagonPost();

        // OcatgonPost();
        //NonagonPost();
       // CirclePost();
       //HexagonPost2();
     //  TetraPost();
     //TestTriangle();

   //  RenameImages("testRatioTestPngs");
   TriangleRotate();
          //  HexagonRotate();
        }

        private static void RenameImages(string directoryName)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var rootDir = Path.GetDirectoryName(path);
            var dirpath = Path.Combine(rootDir, directoryName);
            var files = Directory.GetFiles(dirpath);
            var filesDict = new Dictionary<double,string>();
            foreach (var file in files)
            {
                var filesplit = file.Split('/');
                var filename = filesplit[filesplit.Length - 1];
                var filepart = filename.Split('_');
                var ratiopart = filepart[1];
                var numpart = ratiopart.Replace("r", "");
                double num;
                var s = double.TryParse(numpart, out num);
                if (s)
                {
                    filesDict.Add(num,file);
                    var xx = 42;
                }
                
            }

            filesDict = filesDict.OrderBy(e => e.Key).ToDictionary(e => e.Key, e => e.Value);

            var i = 1;
            foreach (var file in filesDict)
            {
                var parsedKey = String.Format("{0:0.0000}", file.Key).Replace('.', '-');
                var newfilename = "t3/triangle_" + i + ".png";
                var newpath = Path.Combine(dirpath, newfilename);
                File.Copy(file.Value,newpath);

                i++;
            }

        }

        private static void TriangleRotate()
        {
            var t = new Triangle();
            t.Settings.CameraOffset = 4;
            t.Settings.MaxDataPoints = 100000;
            t.Settings.DataPointRadius = 0.005;
            t.Settings.FrameCount = 10;
            t.Settings.RotateCamera = false;
            t.Settings.Overwrite = true;
            t.RenderRotate("triangleRotate");
        }

        private static void HexagonRotate()
        {
            var t = new Hexagon();
            t.Settings.CameraOffset = 4;
            t.Settings.MaxDataPoints = 100000;
            t.Settings.DataPointRadius = 0.005;
            t.Settings.FrameCount = 10;
            t.Settings.RotateCamera = false;
            t.RenderRotate("hexagonRotate");
        }
        private static void TriangleVaryRatio()
        {
            var test = new Triangle();
            test.Settings.FrameCount = 1;
            test.Settings.MaxDataPoints = 100000;
            test.Settings.DataPointRadius = 0.001;
            test.Settings.PointStop = 10000;
            test.Settings.Ratio = 0.3;
            test.Settings.Overwrite = false;
            var max = 300;
            var minR = 0.01;
            var maxR = 0.8;
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < max; i++)
            {
                var step = (i / (double) max);

                var r = (maxR * step) + minR;

                test.Settings.Ratio = r;
                test.RenderProgressively("testRatioTest");
                double timePerElem = sw.Elapsed.TotalSeconds / (i + 1);
                var elemsRemaining = max - i;
                var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");
                Console.WriteLine("Writing ratios\t" + i + "\t" + minsRemaining + " mins remaining");
            }
            

        }
        private static void CirclePost()
        {
            var p = new Polygon(10000);        
            p.RenderProgressively("circlePost1");
        }

        private static void NonagonPost()
        {
            var p = new Polygon(9);
            p.RenderProgressively("nonagonPost1");
        }
        private static void SeptagonPost()
        {

            var p = new Polygon(7);
            p.Settings.MaxDataPoints = 10000000;
            p.Settings.Overwrite = true;
            p.Settings.DataPointRadius = 0.0005;
            p.ShapeName = "Septagon";
            p.Settings.FrameCount = 10;
            p.RenderProgressively("septagonPost1");

        }
        private static void OcatgonPost()
        {
            var p = new Polygon(8);
            p.Settings.MaxDataPoints = 10000000;
            p.Settings.Overwrite = true;
            p.Settings.DataPointRadius = 0.0005;
            p.ShapeName = "Octagon";
            p.Settings.FrameCount = 10;
            p.Settings.DataPointRadius = 0.0005;
            p.RenderProgressively("octagonpost1");

        }

        private static void HexagonPost()
        {
            var h = new Hexagon();
            h.Settings.MaxDataPoints = 10000000;
            h.Settings.Overwrite = true;
            h.Settings.DataPointRadius = 0.001;
            h.Settings.FrameCount = 10;
            h.Settings.RotateCamera = false;
            h.StartRender();
          //  h.RenderProgressively("Hexagon2");

        }
       
        private static void PentagonPost()
        {
            var p = new Pentagon();
            p.Settings.MaxDataPoints = 10000000;
            p.Settings.Overwrite = false;
            p.Settings.DataPointRadius = 0.0005;
            p.Settings.FrameCount = 600;
            p.StartRender();
            p.RenderProgressively("Petangon2");

        }

        private static void TrianglePost()
        {
            var triangle = new Triangle();
            triangle.Settings.RotateCamera = false;
            triangle.Settings.MaxDataPoints = 6000;
            triangle.Settings.FrameCount = 300;
            triangle.Settings.AnchorRadius = 0.04;
            triangle.Settings.DataPointRadius = 0.006;
            triangle.Settings.CameraOffset = 2.2;
            triangle.RenderProgressively("triangle5");
        }

        private static void TrianglePost2()
        {
            var triangle = new Triangle();
            triangle.Settings.RotateCamera = false;
            triangle.Settings.MaxDataPoints = 10;
            triangle.Settings.FrameCount = 10;
            triangle.Settings.AnchorRadius = 0.04;
            triangle.Settings.DataPointRadius = triangle.Settings.AnchorRadius / 2;
            triangle.Settings.CameraOffset = 2.2;
            triangle.RenderProgressively("triangePost2");
        }
        private static void TrianglePost3()
        {
            var triangle = new Triangle();
            triangle.Settings.RotateCamera = false;
            triangle.Settings.MaxDataPoints = 1000000;
            triangle.Settings.FrameCount = 10;
            triangle.Settings.AnchorRadius = 0.04;
            triangle.Settings.DataPointRadius = 0.001;
            triangle.Settings.CameraOffset = 2.2;
            triangle.RenderProgressively("triangePost3");
        }

        private static void SquarePost()
        {
            var s = new Square();
            s.Settings.RotateCamera = false;
            s.Settings.MaxDataPoints = 100000;
            s.Settings.FrameCount = 100;
            s.Settings.AnchorRadius = 0.04;
            s.Settings.DataPointRadius = 0.003;
            s.Settings.CameraOffset = 2.2;
            s.RenderProgressively("squarepost");


        }

        private static void TetraPost()
        {
            var t = new Tetrahedron();
            t.Settings.MaxDataPoints = 1000000;
            t.Settings.FrameCount = 10;
            t.RenderProgressively("tetraAnimPost");
        }

        private static void FindUnrun(string directoryName)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var rootDir = Path.GetDirectoryName(path);
            var dirpath = Path.Combine(rootDir, directoryName);
            var files = Directory.GetFiles(dirpath);

            var povfiles = files.Where(e => e.EndsWith(".pov")).ToList();
            var unrunfiles = new List<string>();
            foreach (var povfile in povfiles)
            {
                var png = povfile.Replace(".pov", ".png");
                if (!File.Exists(png))
                {
                    unrunfiles.Add(povfile);
                }

            }


            var chunks = ChunkList(unrunfiles, 300);

            var c = 0;
            foreach (var chunk in chunks)
            {
                c++;
                var dirName = Path.Combine(rootDir, directoryName + "-unrun-" + c);
                Directory.CreateDirectory(dirName);
                foreach (var povfile in chunk)
                {
                    var povFileName = Path.GetFileName(povfile);
                    var unrunPath = Path.Combine(dirName, povFileName);
                    File.Copy(povfile, unrunPath);

                }

            }


        }

        private static List<List<T>> ChunkList<T>(List<T> list, int nSize = 30)
        {
            var chunkedList = new List<List<T>>();

            for (var i = 0; i < list.Count; i += nSize)
            {
                chunkedList.Add(list.GetRange(i, Math.Min(nSize, list.Count - i)));
            }

            return chunkedList;
        }











    }
}
