using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    class Circle : Shape, IShape
    {
        protected Point centre;
        protected double radius;

        public Circle (Point centre, double radius, 
            int width, Color color, Color fillColor, LineType type)
            : base(width, color, fillColor, type)
        {
            this.centre = centre;
            this.radius = radius;
        }

        public void Draw()
        {
        }
    }
}
