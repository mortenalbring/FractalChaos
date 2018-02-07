using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var tetrahedron = GenerateTetrahedron();
            var anchorsX = tetrahedron[0];
            var anchorsY = tetrahedron[1];
            var anchorsZ = tetrahedron[2];

            var anchorPoints = tetrahedron.Count;

            var outputAnchors = "anchors.txt";

            var anchorColours = "anchorColours.txt";

            var outputAnchorStr = "";
            var anchorColourStr = "";
            var rndColor = new Random();

            for (int i = 0; i < anchorPoints; i++)
            {                
                var x = tetrahedron[i].x;
                var y = tetrahedron[i].y;
                var z = tetrahedron[i].z;
                var outputstr = "<" + x + ", " + y + "," + z + ">";
                outputAnchorStr = outputAnchorStr + outputstr + ",";

                var r = (double)rndColor.Next(0, 100) / 100;
                var g = (double)rndColor.Next(0, 100) / 100;
                var b = (double)rndColor.Next(0, 100) / 100;

                outputAnchorStr = outputAnchorStr + "<" + r + ", " + g + "," + b + ">,";

            }

            File.Delete(outputAnchors);
            File.Delete(anchorColours);
            File.AppendAllText(outputAnchors, outputAnchorStr);
            File.AppendAllText(anchorColours, anchorColourStr);


            var maxPoints = 10000;
            var rnd = new Random();
            var output = "";

            var xPoint = 0.0;
            var yPoint = 0.0;
            var zPoint = 0.0;

            var rPoint = 0.0;
            var gPoint = 0.0;
            var bPoint = 0.0;

            var outputfilename = "test2.txt";
            File.Delete(outputfilename);
            for (int i = 0; i < maxPoints; i++)
            {
                var val = rnd.Next(0, anchorPoints);

                xPoint = (xPoint + tetrahedron[val].x) / 2;
                yPoint = (yPoint + tetrahedron[val].y) / 2;
                zPoint = (zPoint + tetrahedron[val].z) / 2;

                rPoint = (rPoint + tetrahedron[val].r) / 2;
                gPoint = (gPoint + tetrahedron[val].g) / 2;
                bPoint = (bPoint + tetrahedron[val].b) / 2;



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

            var output = new List<AnchorPoint>();
            foreach (var anchor in anchors)
            {
                var anch = new AnchorPoint();
                anch.x = anchor[0];
                anch.y = anchor[1];
                anch.z = anchor[2];
                anch.r = (double)rndColor.Next(0, 100) / 100;
                anch.g = (double)rndColor.Next(0, 100) / 100;
                anch.b = (double)rndColor.Next(0, 100) / 100;
                output.Add(anch);
            }
            return output;

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

           // return results;


        }
    }
}
