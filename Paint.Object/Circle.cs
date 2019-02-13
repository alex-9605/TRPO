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

        public bool IsInBounds(Point point)
        {
            var left = new Point(this.graphics, this.centre.X - (int)radius, this.centre.Y - (int)radius);
            var top = new Point(this.graphics, this.centre.X + (int)radius, this.centre.Y + (int)radius);

            var xMax = left.X > top.X ? left.X : top.X;
            var yMax = left.Y > top.Y ? left.Y : top.Y;
            var xMin = left.X < top.X ? left.X : top.X;
            var yMin = left.Y < top.Y ? left.Y : top.Y;

            if (point.X > xMin && point.X < xMax && point.Y > yMin && point.Y < yMax)
            {
                return true;
            }

            return false;
        }

        public IShape Copy(Point newPosition)
        {
            //return new Circle(this.graphics, new Point(this.graphics, newPosition.X, newPosition.Y), this.radius, this.width, this.color, this.fillColor, this.type);
            var newCentre = new Point(this.graphics, newPosition.X + (int)this.radius, newPosition.Y + (int)this.radius);
            return new Circle(this.graphics, newCentre, this.radius, this.width, this.color, this.fillColor, this.type);
        }

        public override void Select()
        {
            //TODO
        }
    }
}
