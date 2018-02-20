using System.Collections.Generic;

namespace GeneratePoints
{
    public class Shape
    {
        public string ShapeName;
        public List<AnchorPoint> AnchorPoints = new List<AnchorPoint>();

        public static List<AnchorPoint> MakeAnchorPoints(List<List<double>> anchors)
        {

            var colours = new List<List<double>>();

            colours.Add(new List<double> { 1, 0, 0 });
            colours.Add(new List<double> { 0, 1, 0 });
            colours.Add(new List<double> { 0, 0, 1 });
            colours.Add(new List<double> { 1, 1, 0 });
            colours.Add(new List<double> { 1, 0, 1 });
            colours.Add(new List<double> { 0, 1, 1 });
            colours.Add(new List<double> { 1, 1, 1 });
            colours.Add(new List<double> { 0, 0, 0 });


            var rscale = 1 / anchors.Count;
            var gscale = 1 / anchors.Count;
            var bscale = 1 / anchors.Count;
            for (int i = 7; i < anchors.Count; i++)
            {
                rscale = rscale * i;
                gscale = gscale * i;
                bscale = bscale * 1;

                colours.Add(new List<double> { rscale, gscale, bscale });
            }


            var output = new List<AnchorPoint>();
            var p = 0;
            foreach (var anchor in anchors)
            {

                var anch = new AnchorPoint();

                anch.X = anchor[0];
                anch.Y = anchor[1];
                anch.Z = anchor[2];

                var color = colours[p];

                anch.R = color[0];
                anch.G = color[1];
                anch.B = color[2];
                output.Add(anch);
                p++;
            }
            return output;
        }
    }
}