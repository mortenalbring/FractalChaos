using System.Collections.Generic;

namespace GeneratePoints.Models
{
    public class Settings
    {
        public double CameraZoom = 2.5;
        public double CameraYOffset = 0.1;
        public List<double> LookAt = new List<double>(3){0,0,0};
        public int MaxDataPoints = 10000000;
        public double Ratio = 0.5;
        public double AnchorRadius = 0.016;
        public double DataPointRadius = 0.0005;

        public double AnchorTransmit = 0.7;

        public int FrameCount = 10;

        public bool Overwrite = false;
        public bool TransparentBackground = true;
        public bool RotateCamera = true;
        public bool RenderProgressively = true;
         
       

    }



}
