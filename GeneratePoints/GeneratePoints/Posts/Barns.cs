namespace GeneratePoints.Posts
{
    public class Barns
    {
        public static void BarnsleyFarn()
        {
            var b = new BarnsleyFern();
            b.Settings.Calculation.MaxDataPoints = 10000000;
            b.Settings.Calculation.Overwrite = true;
            b.Settings.Calculation.FrameCount = 1;
            b.StartRenderProgressive("barnspost");
        }
    }
}