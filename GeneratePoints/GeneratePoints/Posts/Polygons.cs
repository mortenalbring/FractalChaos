using System;
using GeneratePoints.GameStyles;
using GeneratePoints.Models;
using GeneratePoints.Polygons;

namespace GeneratePoints.Posts
{
    internal class Polygons
    {
        public static void CirclePost()
        {
            var p = new Polygon(10000);
            p.StartRender("circlePost1");
        }

        public static void HexagonPost()
        {
            var h = new Hexagon();
            h.Settings.Calculation.MaxDataPoints = 10000000;
            h.Settings.Calculation.Overwrite = true;
            h.Settings.Render.DataPointRadius = 0.001;
            h.Settings.Calculation.FrameCount = 1;
            h.Settings.Render.RotateCamera = false;
            h.Settings.Render.RenderProgressively = false;
            //  h.StartRender("hexagonPost");
            h.StartRender("hexagonNoRepeat", GameStyle.NoRepeat);
            //  h.RenderProgressively("Hexagon2");
        }


        public static void HexagonRotate()
        {
            var t = new Hexagon();
            t.Settings.Render.CameraZoom = 4;
            t.Settings.Calculation.MaxDataPoints = 100000;
            t.Settings.Render.DataPointRadius = 0.004;
            t.Settings.Calculation.FrameCount = 10;
            t.Settings.Render.RotateCamera = false;
            t.Settings.Calculation.Overwrite = true;
            t.Settings.Calculation.AngleMin = 0;
            t.Settings.Calculation.AngleMax = 2 * Math.PI;
            t.StartRender("triangleRotatePostSmall", GameStyle.WithAngle);
        }

        public static void HexagonVaryRatio()
        {
            var test = new Hexagon();
            test.Settings.Calculation.FrameCount = 1;
            test.Settings.Calculation.MaxDataPoints = 1000000;
            test.Settings.Render.DataPointRadius = 0.001;
            test.Settings.Render.RotateCamera = false;
            test.Settings.Render.RenderProgressively = false;
            test.Settings.Calculation.Overwrite = true;
            test.Settings.Calculation.FrameCount = 10;
            test.Settings.Calculation.RatioMin = 0;
            test.Settings.Calculation.RatioMax = 0.5;
            test.StartRender("pentagonVaryRatioG2", GameStyle.VaryRatio);
        }

        public static void NonagonPost()
        {
            var p = new Polygon(9);
            p.StartRender("nonagonPost1");
        }

        public static void OcatgonPost()
        {
            var p = new Polygon(8);
            p.Settings.Calculation.MaxDataPoints = 10000000;
            p.Settings.Calculation.Overwrite = true;
            p.Settings.Render.DataPointRadius = 0.0005;
            p.ShapeName = "Octagon";
            p.Settings.Calculation.FrameCount = 10;
            p.Settings.Render.DataPointRadius = 0.0005;
            p.StartRender("octagonpost1");
        }

        public static void PentagoneVaryRatio()
        {
            var test = new Pentagon();
            test.Settings.Calculation.FrameCount = 1;
            test.Settings.Calculation.MaxDataPoints = 1000000;
            test.Settings.Render.DataPointRadius = 0.001;
            test.Settings.Render.RotateCamera = false;
            test.Settings.Render.RenderProgressively = false;
            test.Settings.Calculation.Overwrite = true;
            test.Settings.Calculation.FrameCount = 10;
            test.Settings.Calculation.RatioMin = 0.5;
            test.Settings.Calculation.RatioMax = 2.5;
            test.StartRender("pentagonVaryRatioG2", GameStyle.VaryRatio);
        }

        public static void PentagonNoRepeat()
        {
            var s = new Pentagon();
            s.Settings.Render.RotateCamera = false;
            s.Settings.Calculation.MaxDataPoints = 10000000;
            s.Settings.Calculation.FrameCount = 100;
            s.Settings.Render.AnchorRadius = 0.04;
            s.Settings.Render.DataPointRadius = 0.003;
            s.Settings.Render.CameraZoom = 2.2;
            s.Settings.Calculation.Overwrite = true;
            s.StartRender("pentagonPostNoRepeat", GameStyle.NoRepeat);
        }

        public static void PentagonPost()
        {
            var p = new Pentagon();
            p.Settings.Calculation.MaxDataPoints = 10000000;
            p.Settings.Calculation.Overwrite = false;
            p.Settings.Render.DataPointRadius = 0.0005;
            p.Settings.Render.RenderProgressively = false;
            p.Settings.Calculation.FrameCount = 600;
            p.StartRender("pentagonPost");
        }

        public static void PentagonPostNearest()
        {
            var p = new Hexagon();
            p.Settings.Calculation.MaxDataPoints = 100000;
            p.Settings.Calculation.Overwrite = true;
            p.Settings.Render.DataPointRadius = 0.0005;
            p.Settings.Calculation.FrameCount = 10;
            p.StartRender("pentagonPostNearest", GameStyle.NoRepeatNearest);
        }

        public static void SeptagonPost()
        {
            var p = new Polygon(7);
            p.Settings.Calculation.MaxDataPoints = 10000000;
            p.Settings.Calculation.Overwrite = true;
            p.Settings.Render.DataPointRadius = 0.0005;
            p.ShapeName = "Septagon";
            p.Settings.Calculation.FrameCount = 10;
            p.StartRender("septagonPost1");
        }


        public static void SquareNoRepeat()
        {
            var s = new Square();
            s.Settings.Render.RotateCamera = false;
            s.Settings.Calculation.MaxDataPoints = 100000;
            s.Settings.Calculation.FrameCount = 100;
            s.Settings.Render.AnchorRadius = 0.04;
            s.Settings.Render.DataPointRadius = 0.003;
            s.Settings.Render.CameraZoom = 2.2;
            s.Settings.Calculation.Overwrite = true;
            s.StartRender("squarePostNoRepeat", GameStyle.NoRepeat);
        }

        public static void SquarePost()
        {
            var s = new Square();
            s.Settings.Render.RotateCamera = false;
            s.Settings.Calculation.MaxDataPoints = 10000000;
            s.Settings.Calculation.FrameCount = 100;
            s.Settings.Render.AnchorRadius = 0.04;
            s.Settings.Render.DataPointRadius = 0.001;
            s.Settings.Render.LookAt[1] = 0;
            s.Settings.Render.CameraZoom = 2;
            s.Settings.Calculation.Overwrite = true;
            s.Settings.Render.TransparentBackground = false;
            s.Settings.Render.RenderProgressively = false;
            //  s.StartRender("squarepost");
            s.StartRender("squarePostNoRepeat2", GameStyle.NoRepeat);
        }

        public static void TrianglePost()
        {
            var triangle = new Triangle();
            triangle.Settings.Render.RotateCamera = false;
            triangle.Settings.Calculation.MaxDataPoints = 6000;
            triangle.Settings.Calculation.FrameCount = 300;
            triangle.Settings.Render.AnchorRadius = 0.04;
            triangle.Settings.Render.DataPointRadius = 0.006;
            triangle.Settings.Render.CameraZoom = 2.2;
            triangle.StartRender("triangle5");
        }

        public static void TrianglePost2()
        {
            var triangle = new Triangle();
            triangle.Settings.Render.RotateCamera = false;
            triangle.Settings.Calculation.MaxDataPoints = 10;
            triangle.Settings.Calculation.FrameCount = 10;
            triangle.Settings.Render.AnchorRadius = 0.04;
            triangle.Settings.Render.DataPointRadius = triangle.Settings.Render.AnchorRadius / 2;
            triangle.Settings.Render.CameraZoom = 2.2;
            triangle.StartRender("triangePost2");
        }

        public static void TrianglePost3()
        {
            var triangle = new Triangle();
            triangle.Settings.Render.RotateCamera = false;
            triangle.Settings.Calculation.MaxDataPoints = 1000000;
            triangle.Settings.Calculation.FrameCount = 10;
            triangle.Settings.Render.AnchorRadius = 0.04;
            triangle.Settings.Render.DataPointRadius = 0.001;
            triangle.Settings.Render.CameraZoom = 2.2;
            triangle.StartRender("triangePost3");
        }

        public static void TriangleRotate()
        {
            var t = new Triangle();
            t.Settings.Render.CameraZoom = 3;
            t.Settings.Render.LookAt[1] = 0.4;
            t.Settings.Calculation.MaxDataPoints = 100000;
            t.Settings.Render.DataPointRadius = 0.002;
            t.Settings.Calculation.FrameCount = 500;
            t.Settings.Render.RotateCamera = false;
            t.Settings.Render.RenderProgressively = false;
            t.Settings.Render.TransparentBackground = false;
            t.Settings.Calculation.Overwrite = true;
            t.Settings.Calculation.AngleMin = 0;
            t.Settings.Calculation.AngleMax = 2 * Math.PI;
            t.StartRender("triangleRotatePostSmall", GameStyle.WithAngle);
        }

        public static void TriangleRotateSmall()
        {
            var t = new Triangle();
            t.Settings.Render.CameraZoom = 3;
            t.Settings.Render.LookAt[1] = 0.4;
            t.Settings.Calculation.MaxDataPoints = 10;
            t.Settings.Render.DataPointRadius = 0.02;
            t.Settings.Calculation.FrameCount = 10;
            t.Settings.Render.RotateCamera = false;
            t.Settings.Render.RenderProgressively = true;
            t.Settings.Render.TransparentBackground = false;
            t.Settings.Calculation.Overwrite = true;
            t.Settings.Render.AnchorTransmit = 0.1;
            t.Settings.Calculation.Angle = 2 * Math.PI;
            t.StartRender("triangleRotatePostSmall", GameStyle.WithAngle);

            //t.RenderWithAngle("triangleRotatePostSmall", (2 * Math.PI));
        }
    }
}