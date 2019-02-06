using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    public class Circle : Shape, IShape
    {
        private readonly Graphics graphics;
        private Point centre;
        private double radius;

        public Circle (Graphics graphics, Point centre, double radius, 
            int width, Color color, Color fillColor, LineType type)
            : base(width, color, fillColor, type)
        {
            this.graphics = graphics;
            this.centre = centre;
            this.radius = radius;
        }

        public void Draw()
        {
            this.graphics.DrawEllipse(this.pen, this.centre.X, this.centre.Y, (float)this.radius, (float)this.radius);
        }
    }
}
