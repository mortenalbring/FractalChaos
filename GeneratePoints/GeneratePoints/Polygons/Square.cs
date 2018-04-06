using GeneratePoints.Models;

namespace GeneratePoints.Polygons
{
    public class Square : Polygon
    {
        public Square()
        {
            Settings.CameraOffset = 3.5;
            Settings.AnchorRadius = 0.015;
            Settings.FrameCount = 10;
            ShapeName = "Square";
            Vertices = 4;
            var anchors = CalculateVertices();

            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }


}
