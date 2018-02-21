using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using GeneratePoints.Shapes3d;

namespace GeneratePoints
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var tetrahedron = new Tetrahedron();
            var cube = new Cube();
            var ico = new Ico();


            var octo = new Octahedron();


            var shape = new Triangle();
            
            var b = new BarnsleyFern();
            b.Settings.Overwrite = true;
            b.Settings.MaxDataPoints = 1000000;
            b.Settings.FrameCount = 10;
         
         //   b.StartRender();
            octo.Settings.FrameCount = 50;
         
          //  octo.Settings.Overwrite = true;
            //octo.StartRender();
            octo.RenderProgressively();
        }

     

   

        



      

    }
}
