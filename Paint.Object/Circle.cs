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
        private Point centre;
        private double radius;

        public Circle (Graphics graphics, Point centre, double radius, 
            int width, Color color, Color fillColor, LineType type)
            : base(graphics, width, color, fillColor, type)
        {
            this.centre = centre;
            this.radius = radius;
        }

        public override void Draw()
        {
            this.Select();

            //var left = new Point(this.graphics, this.centre.X - (int)radius, this.centre.Y - (int)radius);
            //var top = new Point(this.graphics, this.centre.X + (int)radius, this.centre.Y + (int)radius);

            //var xMax = left.X > top.X ? left.X : top.X;
            //var yMax = left.Y > top.Y ? left.Y : top.Y;
            //var xMin = left.X < top.X ? left.X : top.X;
            //var yMin = left.Y < top.Y ? left.Y : top.Y;

            //this.graphics.DrawEllipse(this.pen, xMin, yMin, (float)this.radius * 2, (float)this.radius * 2);
        }


        public override IShape Copy(Point newPosition)
        {
            //return new Circle(this.graphics, new Point(this.graphics, newPosition.X, newPosition.Y), this.radius, this.width, this.color, this.fillColor, this.type);
            var newCentre = new Point(this.graphics, newPosition.X + (int)this.radius, newPosition.Y + (int)this.radius);
            return new Circle(this.graphics, newCentre, this.radius, this.width, this.color, this.fillColor, this.type);
        }

        protected override Bounds GetBounds()
        {
            var left = new Point(this.graphics, this.centre.X - (int)radius, this.centre.Y - (int)radius);
            var top = new Point(this.graphics, this.centre.X + (int)radius, this.centre.Y + (int)radius);

            var xMax = left.X > top.X ? left.X : top.X;
            var yMax = left.Y > top.Y ? left.Y : top.Y;
            var xMin = left.X < top.X ? left.X : top.X;
            var yMin = left.Y < top.Y ? left.Y : top.Y;

            return new Bounds
            {
                Left = new Point(this.graphics, xMin, yMin),
                Top = new Point(this.graphics, xMax, yMax)
            };
        }
    }
}
