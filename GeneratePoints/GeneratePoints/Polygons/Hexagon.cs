using GeneratePoints.Models;

namespace GeneratePoints.Polygons
{
    public class Hexagon : Polygon
    {
        public Hexagon()
        {
            Settings.Render.CameraZoom = 2.2;
            Settings.Render.AnchorRadius = 0.025;
            Settings.Calculation.FrameCount = 10;
            Settings.Render.RotateCamera = false;
            Settings.Render.AnchorTransmit = 0.7;
            Settings.Render.DataPointRadius = 0.005;
            ShapeName = "Hexagon";
            Vertices = 6;
            var anchors = CalculateVertices();

            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }
}