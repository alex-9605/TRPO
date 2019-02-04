using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    class Ellipse : Shape, IShape
    {
        protected Point centre;
        protected double radiusA;
        protected double radiusB;

        public Ellipse(Point centre, double radiusA, double radiusB, 
            int width, Color color, Color fillColor, LineType type)
            : base(width, color, fillColor, type)
        {
            this.centre = centre;
            this.radiusA = radiusA;
            this.radiusB = radiusB;
        }
        public void Draw()
        {
            
        }
    }
}
