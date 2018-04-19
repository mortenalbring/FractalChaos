namespace GeneratePoints.Models
{
    /// <summary>
    ///     The settings for the calculation and render of this shape.
    /// </summary>
    public class Settings
    {
        /// <summary>
        ///     Settings specific to the calculation. Changing these settings will require the calculation to be re-run.
        /// </summary>
        public CalculationSettings Calculation = new CalculationSettings();

        /// <summary>
        ///     Settings specific to the rendering. Changing these settings won't require recalculation, and can be modified
        ///     directly in the .pov file if needed.
        /// </summary>
        public RenderSettings Render = new RenderSettings();
    }
}