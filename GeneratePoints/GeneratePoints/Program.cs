using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GeneratePoints.Shapes2d;
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
            var tetrahedron = new Tetrahedron();
            var cube = new Cube();
            var ico = new Ico();

            tetrahedron.Settings.FrameCount = 10;
            tetrahedron.Settings.MaxDataPoints = 100000;
            //tetrahedron.RenderProgressively("tetra1");

            // TrianglePost();

            //      FindUnrun("octoprogressive6");

            var c = new Cube();
            c.Settings.FrameCount = 10;
            c.Settings.Ratio = 0.33;
            c.Settings.MaxDataPoints = 1000000;
            //c.RenderProgressively("cube6");

            var th = new Tetrahedron();
            th.Settings.FrameCount = 300;
            th.Settings.MaxDataPoints = 100000;
            th.Settings.DataPointRadius = 0.002;
            th.Settings.RotateCamera = true;
            th.Settings.Overwrite = true;
            th.RenderProgressively("tetra8");
            
            var octo = new Octahedron();
            //  octo.Settings.FrameCount = 2000;
            // octo.Settings.MaxDataPoints = 4000;

            //   octo.Settings.Overwrite = true;
            //octo.StartRender();
            // octo.RenderProgressively("octoprogressive7");
            //   FindUnrun("octoprogressive7");

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
            triangle.RenderProgressively("triangle4");


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
