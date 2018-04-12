using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
                var parsedKey = String.Format("{0:0.0000}", file.Key).Replace('.', '-');
                var newfilename = "t3/triangle_" + i + ".png";
                var newpath = Path.Combine(dirpath, newfilename);
                File.Copy(file.Value, newpath);

                i++;
            }

        }
    }
}
