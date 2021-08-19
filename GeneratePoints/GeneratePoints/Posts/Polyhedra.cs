using System;
using System.Collections.Generic;
using GeneratePoints.GameStyles;
using GeneratePoints.Models;
using GeneratePoints.Polygons;
using GeneratePoints.Polyhedra;

namespace GeneratePoints.Posts
{
    internal class Polyhedra
    {
        private const string RootDirectory = "D:\\Files\\Projects\\ChaosFractals";

        public static void PentagonTest()
        {
            var t = new Pentagon();
            t.Settings.Calculation.MaxDataPoints = 10000000;
            t.Settings.Calculation.FrameCount = 10;
            t.Settings.Render.RenderProgressively = false;
            t.Settings.Render.RotateCamera = false;
            t.Settings.Calculation.Overwrite = true;
            t.Settings.Render.AnchorRadius = 0.0010;
            t.Settings.Render.AnchorStyle = AnchorStyle.EdgePoints;
            t.StartRender(RootDirectory, "penta3", GameStyle.NoRepeat);
        }
        public static void DoDecaTest()
        {
         var t = new Dodecahedron();
         t.Settings.Calculation.MaxDataPoints = 10000000;
         t.Settings.Calculation.FrameCount = 10;
         t.Settings.Render.RenderProgressively = false;
         t.Settings.Render.RotateCamera = false;
         t.Settings.Calculation.Overwrite = true;
         t.Settings.Render.AnchorRadius = 0.0010;
         t.Settings.Render.AnchorStyle = AnchorStyle.EdgePoints;
         t.StartRender(RootDirectory, "dodeca2021norepeatnearest", GameStyle.NoRepeatNearest);
        }
        public static void TetraPostTest()
        {
            var t = new Tetrahedron();
            //t.Settings.Calculation.MaxDataPoints = 10000;
            t.Settings.Calculation.MaxDataPoints = 10000000;
            t.Settings.Calculation.FrameCount = 100;
            t.Settings.Render.RenderProgressively = false;
            t.Settings.Render.RotateCamera = false;
            t.Settings.Calculation.Overwrite = true;
            t.Settings.Render.AnchorRadius = 0.0010;
            t.Settings.Render.AnchorStyle = AnchorStyle.EdgePoints;
            
            // t.Settings.Calculation.AngleMin = 0;
            // t.Settings.Calculation.AngleMax = Math.PI;
            t.StartRender(RootDirectory, "tetraPost18NoRepeat", GameStyle.NoRepeat);
        }
        
        public static void TetraPost()
        {
            var t = new Tetrahedron();
            //t.Settings.Calculation.MaxDataPoints = 1000000;
            t.Settings.Calculation.MaxDataPoints = 1000000;
            t.Settings.Calculation.FrameCount = 10;
            t.Settings.Render.RenderProgressively = false;
            t.Settings.Render.RotateCamera = true;
            t.Settings.Render.AnchorTransmit = 0;
            
            t.StartRender(RootDirectory,"tetraAnimPostNoRepeatNearest",GameStyle.NoRepeatNearest);
        }

        public static void CustomShape()
        {
            var s = new Shape();
            s.ShapeName = "M";
            s.AnchorPoints = new List<AnchorPoint>
            {
                new AnchorPoint
                {
                    X = -1,
                    Y = -1,
                    Z = 0,
                    R = 1,
                    G = 0,
                    B = 0
                },
                new AnchorPoint
                {
                    X = -1,
                    Y = 1,
                    Z = 0,
                    R = 0,
                    G = 1,
                    B = 0
                },
                new AnchorPoint
                {
                    X = 1,
                    Y = 1,
                    Z = 0,
                    R = 0,
                    G = 0,
                    B = 1
                },
                new AnchorPoint
                {
                    X = 1,
                    Y = -1,
                    Z = 0,
                    R = 0,
                    G = 1,
                    B = 1
                },
                new AnchorPoint
                {
                    X = 0,
                    Y = 0.5,
                    Z = 0,
                    R = 1,
                    G = 1,
                    B = 0
                },
            };

            


        }
        public static void OctoPost()
        {
            var t = new Octahedron();
            //t.Settings.Calculation.MaxDataPoints = 1000000;
            t.Settings.Calculation.MaxDataPoints = 20000000;
            t.Settings.Calculation.FrameCount = 10;
            t.Settings.Render.AnchorTransmit = 0.8;
            t.Settings.Render.AnchorRadius = 0.005;
            t.Settings.Render.DataPointRadius = 0.0005;
            t.Settings.Render.RotateCamera = true;
            t.Settings.Render.RenderProgressively = false;
            t.Settings.Calculation.Overwrite = true;
            t.Settings.Render.AnchorStyle = AnchorStyle.EdgePoints;
            t.StartRender(RootDirectory,"octoAnimPost2021NoRepeatNearest", GameStyle.NoRepeatNearest);
        }

        public static void TetraPostNoRepeatNearest()
        {
            var t = new Tetrahedron();
            //t.Settings.Calculation.MaxDataPoints = 1000000;
            t.Settings.Calculation.MaxDataPoints = 10000000;
            t.Settings.Calculation.FrameCount = 10;
            t.Settings.Render.TransparentBackground = false;
            t.Settings.Render.RenderProgressively = false;
            t.Settings.Render.RotateCamera = true;
            t.Settings.Calculation.Overwrite = true;
            t.StartRender(RootDirectory, "tetraAnimPostNoRepeatNearestv3", GameStyle.NoRepeatNearest);
        }
        
        public static void OctoPostNoRepeat()
        {
            var t = new Octahedron();
            //t.Settings.Calculation.MaxDataPoints = 1000000;
            t.Settings.Calculation.MaxDataPoints = 10000000;
            t.Settings.Calculation.FrameCount = 10;
            t.Settings.Render.AnchorTransmit = 0.0;
            t.Settings.Render.RenderProgressively = false;
            t.Settings.Render.TransparentBackground = true;
            t.Settings.Render.DataPointRadius = 0.0005;
            t.StartRender(RootDirectory,"ocanimPost3NoRepeat2021", GameStyle.NoRepeat);
            //t.StartRender("octoAnimPost3");
        }

        public static void PolyHedraNoRepeatNearest()
        {
            void SetProps(Shape shape)
            {
                shape.Settings.Calculation.MaxDataPoints = 10000000;
                shape.Settings.Render.TransparentBackground = true;
                shape.Settings.Render.RenderProgressively = false;
                shape.Settings.Render.RotateCamera = false;
                shape.Settings.Calculation.Overwrite = false;
                shape.Settings.Render.CameraZoom = 2.5;
                shape.Settings.Render.CameraYOffset = 2.5;

            }

            var subDir = "polyNoRepeatNearest";
            Shape t;
            
            t = new Cube();
            SetProps(t);
            t.StartRender(RootDirectory, subDir, GameStyle.NoRepeatNearest);
            
            t = new Octahedron();
            SetProps(t);
            t.StartRender(RootDirectory, subDir, GameStyle.NoRepeatNearest);

            t = new Dodecahedron();
            SetProps(t);
            t.StartRender(RootDirectory, subDir, GameStyle.NoRepeatNearest);

            t = new Ico();
            SetProps(t);
            t.StartRender(RootDirectory, subDir, GameStyle.NoRepeatNearest);
            
            subDir = "polyNoRepeatFurthest";
            t = new Cube();
            SetProps(t);
            t.StartRender(RootDirectory, subDir, GameStyle.NoRepeatFurthest);
            
            t = new Octahedron();
            SetProps(t);
            t.StartRender(RootDirectory, subDir, GameStyle.NoRepeatFurthest);

            t = new Dodecahedron();
            SetProps(t);
            t.StartRender(RootDirectory, subDir, GameStyle.NoRepeatFurthest);

            t = new Ico();
            SetProps(t);
            t.StartRender(RootDirectory, subDir, GameStyle.NoRepeatFurthest);

        }

        public static void RhombiCubePost()
        {
            var t = new Rhombicuboctahedron();
            t.ShapeName = "rhombicube";
            //t.Settings.Calculation.MaxDataPoints = 1000000;
            t.Settings.Calculation.MaxDataPoints = 10000000;
            t.Settings.Calculation.FrameCount = 10;
            t.Settings.Render.CameraZoom = 10.5;
            t.Settings.Render.TransparentBackground = true;
            t.Settings.Render.RenderProgressively = false;
            t.Settings.Render.RotateCamera = true;
            t.Settings.Calculation.Overwrite = true;
            t.Settings.Render.EdgePointRadius = 0.005;
            t.StartRender(RootDirectory, "rhombicube", GameStyle.NoRepeatNearest);
        }
        public static void CubePostNoRepeatNearest()
        {
            var t = new Cube();
            //t.Settings.Calculation.MaxDataPoints = 1000000;
            t.Settings.Calculation.MaxDataPoints = 10000000;
            t.Settings.Calculation.FrameCount = 10;
            t.Settings.Render.TransparentBackground = false;
            t.Settings.Render.RenderProgressively = false;
            t.Settings.Render.RotateCamera = true;
            t.Settings.Calculation.Overwrite = true;
            t.StartRender(RootDirectory, "cubeNoRepeatNearest", GameStyle.NoRepeatNearest);

        }

        public static void IcoPost()
        {

            var i = new Ico();
            i.Settings.Calculation.MaxDataPoints = 10000000;
            i.Settings.Calculation.FrameCount = 10;
            i.Settings.Calculation.Overwrite = true;
            i.Settings.Render.TransparentBackground = true;
            i.Settings.Render.RotateCamera = false;
            i.Settings.Render.RenderText = false;
            
            //i.StartRender(RootDirectory,"icoPost2021Normal", GameStyle.Normal);
            i.StartRender(RootDirectory,"icoPost2021NoRepeat", GameStyle.NoRepeat);
            i.StartRender(RootDirectory,"icoPost2021NoRepeatNearest", GameStyle.NoRepeatNearest);
        }
        public static void CubePost2()
        {
            var c = new Cube();
            c.Settings.Calculation.MaxDataPoints = 10000000;
            c.Settings.Calculation.FrameCount = 10;
            c.Settings.Render.TransparentBackground = true;
            c.Settings.Render.RotateCamera = true;
            c.Settings.Render.RenderText = false;

            c.StartRender(RootDirectory,"cubePost2021Normal", GameStyle.Normal);
            c.StartRender(RootDirectory,"cubePost2021NoRepeat", GameStyle.NoRepeat);
            c.StartRender(RootDirectory,"cubePost2021NoRepeatNearest", GameStyle.NoRepeatNearest);
        }


        public static void TetraRotate()
        {
            var t = new Tetrahedron();
            t.Settings.Render.CameraZoom = 4;
            t.Settings.Render.LookAt[1] = 0.3;
            t.Settings.Calculation.MaxDataPoints = 100000;
            t.Settings.Render.DataPointRadius = 0.002;
            t.Settings.Calculation.FrameCount = 4000;
            t.Settings.Render.RotateCamera = false;
            t.Settings.Render.RenderProgressively = false;
            t.Settings.Render.TransparentBackground = false;
            t.Settings.Calculation.Overwrite = false;
            t.Settings.Calculation.AngleMin = 0;
            t.Settings.Calculation.AngleMax = 2 * Math.PI;
            t.StartRender(RootDirectory,"tetraRotatePost", GameStyle.WithAngle);
        }
    }
}