namespace GeneratePoints.GameStyles
{
    /// <summary>
    ///     This chooses the different game styles
    /// </summary>
    public enum GameStyle
    {
        /// <summary>
        ///     Plays the chaos game normally
        /// </summary>
        Normal,

        /// <summary>
        ///     Plays the chaos game but will never chose the same anchor point twice
        /// </summary>
        NoRepeat,

        /// <summary>
        ///     Plays the chaos game but will not choose either of the anchor points nearest to the previous one
        /// </summary>
        NoRepeatNearest,

        /// <summary>
        ///     Plays the chaos game but varies the distance towards the anchor point to go
        /// </summary>
        VaryRatio,

        /// <summary>
        ///     Plays the chaos game but adds in a rotation about some angle when one of the anchor points is rolled
        /// </summary>
        WithAngle
    }
}