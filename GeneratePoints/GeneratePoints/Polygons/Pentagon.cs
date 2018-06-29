using GeneratePoints.Models;

namespace GeneratePoints.Polygons
{
    public class Pentagon : Polygon
    {
        public Pentagon()
        {
            Settings.Render.CameraZoom = 2.2;
            Settings.Render.AnchorRadius = 0.025;
            Settings.Calculation.FrameCount = 10;
            Settings.Render.RotateCamera = false;
            Settings.Render.DataPointRadius = 0.0005;
            ShapeName = "Pentagon";
            Vertices = 5;
            var anchors = CalculateVertices();
            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }
}