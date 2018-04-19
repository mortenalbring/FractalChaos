namespace GeneratePoints.Models
{
    /// <summary>
    ///     These are the settings for the calculation. Changing these settings will require the calculation to be run again
    /// </summary>
    public class CalculationSettings
    {
        /// <summary>
        ///     Angle to rotate about, if calculation method is set to Rotate
        /// </summary>
        public double Angle = 0;

        public double AngleMax = 0;

        public double AngleMin = 0;

        /// <summary>
        ///     Number of frames to calculate.
        /// </summary>
        public int FrameCount = 10;

        /// <summary>
        ///     The maximum number of data points for the calculation
        /// </summary>
        public int MaxDataPoints = 10000000;

        /// <summary>
        ///     Whether to overwrite existing files or not.
        /// </summary>
        public bool Overwrite = false;

        /// <summary>
        ///     How far to move along the line towards the anchor
        /// </summary>
        public double Ratio = 0.5;

        /// <summary>
        ///     Maximum value for ratio if rendering with variable ratio
        /// </summary>
        public double RatioMax = 0.5;

        /// <summary>
        ///     Minimum value for ratio if rendering with variable ratio
        /// </summary>
        public double RatioMin = 0.5;
    }
}