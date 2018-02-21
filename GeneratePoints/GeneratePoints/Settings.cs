using System.Runtime.CompilerServices;

namespace GeneratePoints
{
    public class Settings
    {
        public double CameraOffset = 2.5;
        public int MaxDataPoints = 100000;
        public double Ratio = 0.5;
        public double AnchorRadius = 0.016;
        public double DataPointRadius = 0.002;

        public double AnchorTransmit = 0.7;

        public int FrameCount = 1;

        public bool Overwrite = false;
        public int PointStop;
        public string PovRayPath = "C:\\Program Files\\POV-Ray\\v3.7\\bin\\pvengine64.exe";
        public Settings()
        {
            PointStop = MaxDataPoints;
        }

    }



}
