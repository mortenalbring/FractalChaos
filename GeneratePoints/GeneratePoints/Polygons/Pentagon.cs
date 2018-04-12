using GeneratePoints.Models;

namespace GeneratePoints.Polygons
{

    public class Pentagon : Polygon
    {
        public Pentagon()
        {
            Settings.CameraZoom = 2.2;
            Settings.AnchorRadius = 0.025;
            Settings.FrameCount = 10;
            Settings.RotateCamera = false;       
            Settings.DataPointRadius = 0.0005;
            ShapeName = "Pentagon";
            Vertices = 5;
            var anchors = CalculateVertices();
            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }


}
