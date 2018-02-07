using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GeneratePoints
{
    public class AnchorPoint
    {
        public double x;
        public double y;
        public double z;

        public double r;
        public double g;
        public double b;
    }
    class Program
    {
        static void Main(string[] args)
        {
            var shapeName = "cube";

            var tetrahedron = GenerateTetrahedron();

            var cube = GenerateCube();

            

            var shape = cube;
            var anchorPoints = shape.Count;
            var outputAnchors = shapeName + "-anchors.txt";


            var outputAnchorStr = "";            
          
            for (int i = 0; i < anchorPoints; i++)
            {                
                var x = shape[i].x;
                var y = shape[i].y;
                var z = shape[i].z;
                var outputstr = "<" + x + ", " + y + "," + z + ">";
                outputAnchorStr = outputAnchorStr + outputstr + ",";
                
                outputAnchorStr = outputAnchorStr + "<" + shape[i].r + ", " + shape[i].g + "," + shape[i].b + ">,";

            }

            File.Delete(outputAnchors);            
            File.AppendAllText(outputAnchors, outputAnchorStr);            

            var maxPoints = 100000;
            var rnd = new Random();
            var output = "";

            var xPoint = 0.0;
            var yPoint = 0.0;
            var zPoint = 0.0;

            var rPoint = 0.0;
            var gPoint = 0.0;
            var bPoint = 0.0;

            var outputfilename = shapeName + "-datapoints.txt";
            File.Delete(outputfilename);
            for (int i = 0; i < maxPoints; i++)
            {
                var val = rnd.Next(0, anchorPoints);

                xPoint = (xPoint + shape[val].x) / 2;
                yPoint = (yPoint + shape[val].y) / 2;
                zPoint = (zPoint + shape[val].z) / 2;

                rPoint = (rPoint + shape[val].r) / 2;
                gPoint = (gPoint + shape[val].g) / 2;
                bPoint = (bPoint + shape[val].b) / 2;



                var outputstr = "<" + xPoint + ", " + yPoint + "," + zPoint + ">";
                output = output + outputstr + ",";

                output = output + "<" + rPoint + ", " + gPoint + "," + bPoint + ">,";

                Console.WriteLine("Writing points\t" + i + "\t" + maxPoints);
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
            var output = new List<AnchorPoint>();
            foreach (var anchor in anchors)
            {
                var anch = new AnchorPoint();
                anch.x = anchor[0];
                anch.y = anchor[1];
                anch.z = anchor[2];

                anch.r = 1 - (double) anch.x /2;
                anch.g = 1 - (double)anch.y / 2;
                anch.b = 1 - (double)anch.z / 2;
                output.Add(anch);
            }
            return output;
        }

        private static List<AnchorPoint> GenerateCube()
        {
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
            return output;
        }


    }
}
