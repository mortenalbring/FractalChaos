using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratePoints.Models;
using GeneratePoints.Polygons;
using GeneratePoints.Shapes3d;

namespace GeneratePoints
{
    public class Posts
    {
        public static void SquareNoRepeat()
        {
            var s = new Square();
            s.Settings.RotateCamera = false;
            s.Settings.MaxDataPoints = 100000;
            s.Settings.FrameCount = 100;
            s.Settings.AnchorRadius = 0.04;
            s.Settings.DataPointRadius = 0.003;
            s.Settings.CameraZoom = 2.2;
            s.Settings.Overwrite = true;
            s.StartRenderNoRepeat("squarePostNoRepeat");
        }
        public static void PentagonNoRepeat()
        {
            var s = new Pentagon();
            s.Settings.RotateCamera = false;
            s.Settings.MaxDataPoints = 10000000;
            s.Settings.FrameCount = 100;
            s.Settings.AnchorRadius = 0.04;
            s.Settings.DataPointRadius = 0.003;
            s.Settings.CameraZoom = 2.2;
            s.Settings.Overwrite = true;
            s.StartRenderNoRepeat("pentagonPostNoRepeat");
        }
        public static void TetraRotate()
        {
            var t = new Tetrahedron();
            t.Settings.CameraZoom = 4;
            t.Settings.LookAt[1] = 0.3;
            t.Settings.MaxDataPoints = 100000;
            t.Settings.DataPointRadius = 0.002;
            t.Settings.FrameCount = 4000;
            t.Settings.RotateCamera = false;
            t.Settings.RenderProgressively = false;
            t.Settings.TransparentBackground = false;
            t.Settings.Overwrite = false;
            t.RenderWithAngle("tetraRotatePost", 0, (2 * Math.PI));
        }

        public static void TriangleRotateSmall()
        {
            var t = new Triangle();
            t.Settings.CameraZoom = 3;
            t.Settings.LookAt[1] = 0.4;
            t.Settings.MaxDataPoints = 10;
            t.Settings.DataPointRadius = 0.02;
            t.Settings.FrameCount = 10;
            t.Settings.RotateCamera = false;
            t.Settings.RenderProgressively = true;
            t.Settings.TransparentBackground = false;
            t.Settings.Overwrite = true;
            t.Settings.AnchorTransmit = 0.1;
            t.RenderWithAngle("triangleRotatePostSmall", (2 * Math.PI));
        }

        public static void TriangleRotate()
        {
            var t = new Triangle();
            t.Settings.CameraZoom = 3;
            t.Settings.LookAt[1] = 0.4;
            t.Settings.MaxDataPoints = 100000;
            t.Settings.DataPointRadius = 0.002;
            t.Settings.FrameCount = 500;
            t.Settings.RotateCamera = false;
            t.Settings.RenderProgressively = false;
            t.Settings.TransparentBackground = false;
            t.Settings.Overwrite = true;
            t.RenderWithAngle("triangleRotatePost", 0, (2 * Math.PI));
        }
        public static void TriangleRotateSingle()
        {
            var t = new Triangle();
            t.Settings.CameraZoom = 3;
            t.Settings.LookAt[1] = 0.4;
            t.Settings.MaxDataPoints = 1000000;
            t.Settings.DataPointRadius = 0.001;
            t.Settings.FrameCount = 1;
            t.Settings.RotateCamera = false;
            t.Settings.RenderProgressively = false;
            t.Settings.TransparentBackground = false;
            t.Settings.Overwrite = true;
            t.RenderWithAngle("triangleRotateSingle2", (Math.PI / 2));
            //t.RenderRotate("triangleRotate4", 0, (2 * Math.PI));
        }

        public static void HexagonRotate()
        {
            var t = new Hexagon();
            t.Settings.CameraZoom = 4;
            t.Settings.MaxDataPoints = 100000;
            t.Settings.DataPointRadius = 0.004;
            t.Settings.FrameCount = 10;
            t.Settings.RotateCamera = false;
            t.Settings.Overwrite = true;
            t.RenderWithAngle("hexagonRotate", 0, (2 * Math.PI));
        }
        public static void TriangleVaryRatio()
        {
            var test = new Triangle();
            test.Settings.FrameCount = 1;
            test.Settings.MaxDataPoints = 100000;
            test.Settings.DataPointRadius = 0.001;
            test.Settings.Ratio = 0.3;
            test.Settings.Overwrite = false;
            var max = 300;
            var minR = 0.01;
            var maxR = 0.8;
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < max; i++)
            {
                var step = (i / (double)max);

                var r = (maxR * step) + minR;

                test.Settings.Ratio = r;
                test.StartRender("testRatioTest");
                double timePerElem = sw.Elapsed.TotalSeconds / (i + 1);
                var elemsRemaining = max - i;
                var minsRemaining = (elemsRemaining * timePerElem / 60).ToString("N");
                Console.WriteLine("Writing ratios\t" + i + "\t" + minsRemaining + " mins remaining");
            }


        }
        public static void CirclePost()
        {
            var p = new Polygon(10000);
            p.StartRender("circlePost1");
        }

        public static void NonagonPost()
        {
            var p = new Polygon(9);
            p.StartRender("nonagonPost1");
        }
        public static void SeptagonPost()
        {

            var p = new Polygon(7);
            p.Settings.MaxDataPoints = 10000000;
            p.Settings.Overwrite = true;
            p.Settings.DataPointRadius = 0.0005;
            p.ShapeName = "Septagon";
            p.Settings.FrameCount = 10;
            p.StartRender("septagonPost1");

        }
        public static void OcatgonPost()
        {
            var p = new Polygon(8);
            p.Settings.MaxDataPoints = 10000000;
            p.Settings.Overwrite = true;
            p.Settings.DataPointRadius = 0.0005;
            p.ShapeName = "Octagon";
            p.Settings.FrameCount = 10;
            p.Settings.DataPointRadius = 0.0005;
            p.StartRender("octagonpost1");

        }

        public static void HexagonPost()
        {
            var h = new Hexagon();
            h.Settings.MaxDataPoints = 10000000;
            h.Settings.Overwrite = true;
            h.Settings.DataPointRadius = 0.001;
            h.Settings.FrameCount = 1;
            h.Settings.RotateCamera = false;
            h.Settings.RenderProgressively = false;
          //  h.StartRender("hexagonPost");
            h.StartRenderNoRepeat("hexagonNoRepeat");
            //  h.RenderProgressively("Hexagon2");

        }

        public static void PentagonPost()
        {
            var p = new Pentagon();
            p.Settings.MaxDataPoints = 10000000;
            p.Settings.Overwrite = false;
            p.Settings.DataPointRadius = 0.0005;
            p.Settings.FrameCount = 600;
            p.StartRender("pentagonPost");
            p.StartRender("Petangon2");

        }

        public static void TrianglePost()
        {
            var triangle = new Triangle();
            triangle.Settings.RotateCamera = false;
            triangle.Settings.MaxDataPoints = 6000;
            triangle.Settings.FrameCount = 300;
            triangle.Settings.AnchorRadius = 0.04;
            triangle.Settings.DataPointRadius = 0.006;
            triangle.Settings.CameraZoom = 2.2;
            triangle.StartRender("triangle5");
        }

        public static void TrianglePost2()
        {
            var triangle = new Triangle();
            triangle.Settings.RotateCamera = false;
            triangle.Settings.MaxDataPoints = 10;
            triangle.Settings.FrameCount = 10;
            triangle.Settings.AnchorRadius = 0.04;
            triangle.Settings.DataPointRadius = triangle.Settings.AnchorRadius / 2;
            triangle.Settings.CameraZoom = 2.2;
            triangle.StartRender("triangePost2");
        }
        public static void TrianglePost3()
        {
            var triangle = new Triangle();
            triangle.Settings.RotateCamera = false;
            triangle.Settings.MaxDataPoints = 1000000;
            triangle.Settings.FrameCount = 10;
            triangle.Settings.AnchorRadius = 0.04;
            triangle.Settings.DataPointRadius = 0.001;
            triangle.Settings.CameraZoom = 2.2;
            triangle.StartRender("triangePost3");
        }

        public static void SquarePost()
        {
            var s = new Square();
            s.Settings.RotateCamera = false;
            s.Settings.MaxDataPoints = 1000000;
            s.Settings.FrameCount = 100;
            s.Settings.AnchorRadius = 0.04;
            s.Settings.DataPointRadius = 0.001;
            s.Settings.LookAt[1] = 0;
            s.Settings.CameraZoom = 2;
            s.Settings.Overwrite = true;
            s.Settings.TransparentBackground = false;
            s.Settings.RenderProgressively = false;
          //  s.StartRender("squarepost");
            s.StartRenderNoRepeat("squarePostNoRepeat");


        }

        public static void TetraPost()
        {
            var t = new Tetrahedron();
            t.Settings.MaxDataPoints = 1000000;
            t.Settings.FrameCount = 10;
            t.StartRender("tetraAnimPost");
        }
    }
}
