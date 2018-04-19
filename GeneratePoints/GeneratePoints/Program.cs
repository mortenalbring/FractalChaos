using GeneratePoints.Models;
using GeneratePoints.Posts;

namespace GeneratePoints
{
    /// <summary>
    ///     This program generates the data points and POV-Ray files for rendering the chaos game for various different shapes
    ///     It generates :
    ///     a file for the co-ordinates of the initial anchor points and their colours
    ///     a file for all the data points for the chaos game
    ///     a series of POV-Ray files for the render
    ///     Each Shape has various settings that control the render options and animation settings
    /// </summary>
    public class Program
    {
        private static void Main(string[] args)
        {
            // Posts.SquarePost();           
            // Posts.TriangleNoRepeat();
            //Posts.HexagonPost();
            // Posts.HexagonVaryRatio();
          
            var p = new Polygon(3);
            p.Settings.Calculation.MaxDataPoints = 10;
            p.Settings.Calculation.Overwrite = true;
            p.StartRender();
        }
    }
}