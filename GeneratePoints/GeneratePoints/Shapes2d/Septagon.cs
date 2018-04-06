using GeneratePoints.Models;

namespace GeneratePoints.Shapes2d
{
    public class Septagon : Polygon 
    {
        public Septagon()
        {
            Settings.CameraOffset = 2.2;
            Settings.AnchorRadius = 0.025;
            Settings.FrameCount = 10;
            Settings.RotateCamera = false;
            Settings.AnchorTransmit = 0.7;
            Settings.DataPointRadius = 0.005;
            ShapeName = "Septagon";            
            Vertices = 7;
            var anchors = CalculateVertices();

            AnchorPoints = MakeAnchorPoints(anchors);
        }
    }


}
