using GeneratePoints.Models;

namespace GeneratePoints.Polygons
{
    public class Square : Polygon
    {
        public Square()
        {
            Settings.Render.CameraZoom = 3.5;
            Settings.Render.AnchorRadius = 0.015;
            Settings.Calculation.FrameCount = 10;
            ShapeName = "Square";
            Vertices = 4;
            var anchors = CalculateVertices();

            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }
}