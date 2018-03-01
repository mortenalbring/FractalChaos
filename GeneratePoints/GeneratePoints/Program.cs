using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using GeneratePoints.Shapes2d;
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

            tetrahedron.Settings.FrameCount = 10;
            tetrahedron.Settings.MaxDataPoints = 100000;
            //tetrahedron.RenderProgressively("tetra1");

            var octo = new Octahedron();
       
            FindUnrun("octoprogressive6");
            
       
         //   b.StartRender();
         //   octo.Settings.FrameCount = 50;
         
          //  octo.Settings.Overwrite = true;
            //octo.StartRender();
            //octo.RenderProgressively("octoprogressive6");


        }

        private static void FindUnrun(string directoryName)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var rootDir = Path.GetDirectoryName(path);
            var dirpath = Path.Combine(rootDir, directoryName);
            var files = Directory.GetFiles(dirpath);
            var unrundir = Path.Combine(rootDir, directoryName + "unrun");
            Directory.CreateDirectory(unrundir);
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


            var chunks = ChunkList(unrunfiles, 200);

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
