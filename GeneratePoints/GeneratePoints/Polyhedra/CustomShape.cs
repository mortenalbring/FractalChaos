using System.Collections.Generic;
using GeneratePoints.Models;

namespace GeneratePoints.Polyhedra
{
    public class CustomShape : Shape
    {
       
        public CustomShape()
        {
            Settings.Render.DataPointRadius = 0.002;
            Settings.Calculation.MaxDataPoints = 100000;
            Settings.Calculation.FrameCount = 10;
            Settings.Render.CameraZoom = 3.2;
            Settings.Render.RenderProgressively = false;
            Settings.Calculation.Overwrite = true;
            Settings.Render.AnchorRadius = 0.005;
            ShapeName = "mmm";

            var anchors = new List<List<double>>
            {
            };

            
            var elems = 10;
            var start = -1.0;
            var end = 1.0;
            double inc = (start - end) / elems;
            for (int i = 0; i < elems; i++)
            {
                var yVal = i * inc;
                anchors.Add(new List<double> {-1, yVal,1});
                anchors.Add(new List<double> {1, yVal,1});
            }


            this.Settings.Render.AnchorStyle = AnchorStyle.VertexPoint; 
            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }
}