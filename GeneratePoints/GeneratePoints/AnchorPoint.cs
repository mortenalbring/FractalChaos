using System;
using System.Collections.Generic;

namespace GeneratePoints
{
    public class AnchorPoint
    {
        public AnchorPoint()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public double R;
        public double B;
        public double G;
        
        public double X;
        public double Y;
        public double Z;
        
        public List<AnchorPoint> NearestNeighbours = new List<AnchorPoint>();
    }
    
    
}