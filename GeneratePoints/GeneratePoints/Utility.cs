using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GeneratePoints.Models;

namespace GeneratePoints
{
    public class Utility
    {
        public static void CreateDirectory(string dirName, bool overwrite)
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
                if (!overwrite)
                {
                    return;
                }

                var di = new DirectoryInfo(newDir);
                foreach (var file in di.GetFiles())
                    file.Delete();
                foreach (var dir in di.GetDirectories())
                    dir.Delete(true);
            }
        }

        public static void FindUnrun(string directoryName)
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

        public static void EstimateCalcTime(string dirName, string searchPattern)
        {
            var di = new DirectoryInfo(dirName);
            var files = di.GetFiles(searchPattern).OrderBy(e => e.CreationTime);

            var output = new Dictionary<string,double>(); 
            DateTime previousCreate = DateTime.MinValue;
            var i = 0;
            foreach (var f in files)
            {
                if (i == 0)
                {
                    previousCreate = f.CreationTime;
                }
                else
                {
                    var createTime = f.CreationTime;
                    TimeSpan tsSpan = createTime - previousCreate;

                    var substr = f.FullName.Substring(f.FullName.Length - 7).Replace(".png","");
                    output.Add(substr,tsSpan.TotalMilliseconds);
                    previousCreate = createTime;
                }

                i++;

            }

            foreach (var o in output)
            {
                Console.WriteLine(o.Key + "\t" + o.Value);
            }
            
            var xx = 42;


        }
        
        public static string GetDatapointsFilename(string shapeName, Settings settings, string append = "")
        {
            var outputfilename = $"{shapeName}{settings.Calculation.MaxDataPoints:e2}{append}-dp.txt";
            return outputfilename;
        }

        public static void RenameImages(string directoryName)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var rootDir = Path.GetDirectoryName(path);
            var dirpath = Path.Combine(rootDir, directoryName);
            var files = Directory.GetFiles(dirpath);
            var filesDict = new Dictionary<double, string>();
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
                    filesDict.Add(num, file);
                    var xx = 42;
                }
            }

            filesDict = filesDict.OrderBy(e => e.Key).ToDictionary(e => e.Key, e => e.Value);

            var i = 1;
            foreach (var file in filesDict)
            {
                var parsedKey = string.Format("{0:0.0000}", file.Key).Replace('.', '-');
                var newfilename = "t3/triangle_" + i + ".png";
                var newpath = Path.Combine(dirpath, newfilename);
                File.Copy(file.Value, newpath);

                i++;
            }
        }

        private static List<List<T>> ChunkList<T>(List<T> list, int nSize = 30)
        {
            var chunkedList = new List<List<T>>();

            for (var i = 0; i < list.Count; i += nSize)
                chunkedList.Add(list.GetRange(i, Math.Min(nSize, list.Count - i)));

            return chunkedList;
        }
    }
}