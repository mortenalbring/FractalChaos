using GeneratePoints.Models;

namespace GeneratePoints.Polygons
{
    public class Septagon : Polygon
    {
        public Septagon()
        {
            Settings.Render.CameraZoom = 2.2;
            Settings.Render.AnchorRadius = 0.025;
            Settings.Calculation.FrameCount = 10;
            Settings.Render.RotateCamera = false;
            Settings.Render.AnchorTransmit = 0.7;
            Settings.Render.DataPointRadius = 0.001;
            ShapeName = "Septagon";
            Vertices = 7;
            var anchors = CalculateVertices();

            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }
}