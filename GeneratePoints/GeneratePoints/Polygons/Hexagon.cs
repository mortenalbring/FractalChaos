using GeneratePoints.Models;

namespace GeneratePoints.Polygons
{

    public class Hexagon : Polygon
    {
        public Hexagon()
        {
            Settings.CameraOffset = 2.2;
            Settings.AnchorRadius = 0.025;
            Settings.FrameCount = 10;
            Settings.RotateCamera = false;
            Settings.AnchorTransmit = 0.7;
            Settings.DataPointRadius = 0.005;
            ShapeName = "Hexagon";
            Vertices = 6;
            var anchors = CalculateVertices();

            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }


}
