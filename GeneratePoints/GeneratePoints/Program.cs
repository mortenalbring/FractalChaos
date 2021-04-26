using System;
using GeneratePoints.GameStyles;
using GeneratePoints.Models;
using GeneratePoints.Polyhedra;
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
        private const string RootDirectory = "D:\\Files\\Projects\\ChaosFractals";
        private static void Main(string[] args)
        {
            try
            {
                var p = new Polygon(3);
                //p.StartRender();

                // Posts.Polyhedra.CubePost2();

                //Posts.Polyhedra.TetraPostNoRepeat();
                //Posts.Polyhedra.TetraPostTest();
                //Posts.Polyhedra.DoDecaTest();
                Posts.Polyhedra.PentagonTest();
                //Posts.Polyhedra.OctoPost();

                
             
                //var dirName = "D:\\Files\\Dropbox\\Coding projects\\FractalChaos\\GeneratePoints\\GeneratePoints\\bin\\Debug\\dualTetraPost5\\zoominto";
                //var searchPattern = "*.png";
                //Utility.EstimateCalcTime(dirName, searchPattern);
                // Posts.Polyhedra.OctoPostNoRepeat();

                // Barns.BarnsleyFarn();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}