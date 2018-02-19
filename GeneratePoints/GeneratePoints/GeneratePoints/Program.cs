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


            var shapeName = "octo";

            var tetrahedron = GenerateTetrahedron();

            var cube = GenerateCube();

            var octo = GenerateOctahedron();

            var ico = GenerateIco();


            var shape = octo;
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

            var maxPoints = 10000000;
            var rnd = new Random();
            var output = "";

            var xPoint = 0.0;
            var yPoint = 0.0;
            var zPoint = 0.0;

            var rPoint = 0.0;
            var gPoint = 0.0;
            var bPoint = 0.0;

            var outputfilename = shapeName + maxPoints + "-datapoints.txt";
            File.Delete(outputfilename);
            var sw = new Stopwatch();
            sw.Start();
            var cWriteCount = 0;

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

               

                cWriteCount++;
                if (cWriteCount == 1000)
                {
                    File.AppendAllText(outputfilename, output);
                    output = "";
                    cWriteCount = 0;

                    double timePerElem = (double)sw.Elapsed.TotalSeconds / (double)(i + 1);
                    var elemsRemaining = (maxPoints - i);
                    var minsRemaining = ((elemsRemaining * timePerElem) / 60).ToString("N");

                    Console.WriteLine("Writing points\t" + i + "\t" + maxPoints + "\t" + minsRemaining + " mins remaining");
                }
            }

            File.AppendAllText(outputfilename, output);

            Console.ReadLine();

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

                anch.x = anchor[0];
                anch.y = anchor[1];
                anch.z = anchor[2];

                var color = colours[p];

                anch.r = color[0];
                anch.g = color[1];
                anch.b = color[2];
                output.Add(anch);
                p++;
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

        private static List<AnchorPoint> GenerateOctahedron()
        {
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
            return output;
        }
        private static List<AnchorPoint> GenerateIco()
        {
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
            return output;
        }

    }
}
