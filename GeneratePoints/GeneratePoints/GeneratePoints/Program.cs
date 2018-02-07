using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePoints
{
    class Program
    {
        static void Main(string[] args)
        {


            //To get the location the assembly normally resides on disk or the install directory
            string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            //once you have the path you get the directory with:
            var directory = System.IO.Path.GetDirectoryName(path);

            var anchorPoints = 11;
            var anchorsX = new List<double> {0, 0, 0, 0, 1, 1, -1, -1, 1.6, -1.6, 1.6, -1.6 };
            var anchorsY = new List<double> {1, -1, 1, 1, 1.6, -1.6, 1.6, -1.6, 0, 0, 0, 0};
            var anchorsZ = new List<double> {1.6, 1.6, -1.6, -1.6, 0, 0, 0, 0, 1, 1, -1, -1};

            var tetrahedron = GenerateTetrahedron();
            anchorsX = tetrahedron[0];
            anchorsY = tetrahedron[1];
            anchorsZ = tetrahedron[2];

            anchorPoints = anchorsX.Count;

            var outputAnchors = "anchors.txt";

            var outputAnchorStr = "";
            for (int i = 0; i < anchorPoints; i++)
            {
                var x = anchorsX[i];
                var y = anchorsY[i];
                var z = anchorsZ[i];
                var outputstr = "<" + x + ", " + y + "," + z + ">";
                outputAnchorStr = outputAnchorStr + outputstr + ",";
            }
            File.Delete(outputAnchors);
            File.AppendAllText(outputAnchors,outputAnchorStr);



            var maxPoints = 10000;
            var rnd = new Random();
            var output = "";

            var xPoint = 0.0;
            var yPoint = 0.0;
            var zPoint = 0.0;


            var outputfilename = "test2.txt";
            File.Delete(outputfilename);
            for (int i = 0; i < maxPoints; i++)
            {               
                var val = rnd.Next(0, anchorPoints);

                xPoint = (xPoint + anchorsX[val])/2;
                yPoint = (yPoint + anchorsY[val])/2;
                zPoint = (zPoint + anchorsZ[val])/2;

                var outputstr = "<" + xPoint + ", " + yPoint + "," + zPoint + ">";
                output = output + outputstr + ",";
                Console.WriteLine("Writing points\t"+i+"\t"+maxPoints);
            }

            File.AppendAllText(outputfilename,output);

        }

        private static List<List<double>> GenerateTetrahedron()
        {

            var anchors = new List<List<double>>();

            var anchor1 = new List<double> {-1, 0, -1 / Math.Sqrt(2)};
            var anchor2 = new List<double> { 1, 0, -1 / Math.Sqrt(2) };

            var anchor3 = new List<double> { 0, 1, 1 / Math.Sqrt(2) };
            var anchor4 = new List<double> { 0, -1, 1 / Math.Sqrt(2) };

            anchors.Add(anchor1);
            anchors.Add(anchor2);
            anchors.Add(anchor3);
            anchors.Add(anchor4);

            var xPoints = new List<double>();
            foreach (var anchor in anchors)
            {
                xPoints.Add(anchor[0]);
            }
            var yPoints = new List<double>();
            foreach (var anchor in anchors)
            {
                yPoints.Add(anchor[1]);
            }
            var zPoints = new List<double>();
            foreach (var anchor in anchors)
            {
                zPoints.Add(anchor[2]);
            }

            var results = new List<List<double>>();
            results.Add(xPoints);
            results.Add(yPoints);
            results.Add(zPoints);

            return results;


        }
    }
}
