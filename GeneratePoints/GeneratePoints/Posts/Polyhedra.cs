using System;
using GeneratePoints.GameStyles;
using GeneratePoints.Polyhedra;

namespace GeneratePoints.Posts
{
    internal class Polyhedra
    {
        private const string RootDirectory = "D:\\Files\\Projects\\ChaosFractals";
        public static void TetraPostTest()
        {
            var t = new Tetrahedron();
            //t.Settings.Calculation.MaxDataPoints = 1000000;
            t.Settings.Calculation.MaxDataPoints = 1000000;
            t.Settings.Calculation.FrameCount = 100;
            t.Settings.Render.RenderProgressively = false;
            t.Settings.Render.RotateCamera = false;
            t.Settings.Calculation.Overwrite = true;
            t.Settings.Render.AnchorRadius = 0.0010;

            // t.Settings.Calculation.AngleMin = 0;
            // t.Settings.Calculation.AngleMax = Math.PI;
            t.StartRender(RootDirectory, "tetraPost9", GameStyle.Normal);
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

        public static void OctoPost()
        {
            var t = new Octahedron();
            //t.Settings.Calculation.MaxDataPoints = 1000000;
            t.Settings.Calculation.MaxDataPoints = 10000000;
            t.Settings.Calculation.FrameCount = 10;
            t.Settings.Render.AnchorTransmit = 0.8;
            t.Settings.Render.AnchorRadius = 0.0010;
            t.Settings.Render.DataPointRadius = 0.0005;
            t.Settings.Render.RotateCamera = true;
            t.Settings.Render.RenderProgressively = true;
            
            t.StartRender(RootDirectory,"octoAnimPost2021", GameStyle.Normal);
        }

        public static void TetraPostNoRepeat()
        {
            var t = new Tetrahedron();
            //t.Settings.Calculation.MaxDataPoints = 1000000;
            t.Settings.Calculation.MaxDataPoints = 10000000;
            t.Settings.Calculation.FrameCount = 10;
            t.Settings.Render.TransparentBackground = false;
            t.Settings.Render.RenderProgressively = false;
            t.Settings.Render.RotateCamera = true;
            t.StartRender(RootDirectory, "tetraAnimPostNoRepeat3", GameStyle.NoRepeat);
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

        public static void CubePost()
        {
            var c = new Cube();
            c.Settings.Calculation.MaxDataPoints = 10000000;
            c.Settings.Calculation.FrameCount = 10;
            c.Settings.Render.TransparentBackground = true;
            c.StartRender("cubePost1");
        }

        public static void CubePost2()
        {
            var c = new Cube();
            c.Settings.Calculation.MaxDataPoints = 10000000;
            c.Settings.Calculation.FrameCount = 10;
            c.Settings.Render.TransparentBackground = true;
            c.Settings.Render.RotateCamera = true;

            c.StartRender(RootDirectory,"cubePost2NoRepeat", GameStyle.NoRepeat);
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