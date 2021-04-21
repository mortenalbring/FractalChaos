using System.Collections.Generic;

namespace GeneratePoints.Models
{
    /// <summary>
    ///     These are the scene settings for the created POV-Ray file.
    ///     These settings can be changed manually in the .pov file itself afterwards, without any need to re-run the
    ///     calculations
    /// </summary>
    public class RenderSettings
    {
        /// <summary>
        ///     The radius of the anchor points
        /// </summary>
        public double AnchorRadius = 0.016;

        /// <summary>
        ///     The transparency of the anchor points (0.0 is opaque, 1.0 is transparent)
        /// </summary>
        public double AnchorTransmit = 0.7;

        /// <summary>
        ///     How high up to position the camera away from the origin
        /// </summary>
        public double CameraYOffset = 0.1;

        /// <summary>
        ///     How far away from the origin to position the camera
        /// </summary>
        public double CameraZoom = 2.5;

        /// <summary>
        ///     The radius of the data points. You may want to scale this with the number of data points.
        /// </summary>
        public double DataPointRadius = 0.0005;

        /// <summary>
        ///     Where the camera should look at
        /// </summary>
        public List<double> LookAt = new List<double>(3) {0, 0, 0};

        /// <summary>
        ///     Whether to build up the data points over time, from 0 up to the max data point setting
        /// </summary>
        public bool RenderProgressively = true;

        /// <summary>
        ///     Whether to rotate the camera about the origin or not
        /// </summary>
        public bool RotateCamera = true;

        /// <summary>
        ///     Whether to include a white background or not in the scene
        /// </summary>
        public bool TransparentBackground = true;

        public AnchorStyle AnchorStyle = AnchorStyle.VertexPoint;
        
    }
    
    public enum AnchorStyle
    {
        VertexPoint,
        EdgePoints,
    }
}