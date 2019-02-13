using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    public class Ellipse : Shape, IShape
    {
        private readonly Graphics graphics;
        private Point centre;
        private double radiusA;
        private double radiusB;

        public Ellipse(Graphics graphics, Point centre, double radiusA, double radiusB, 
            int width, Color color, Color fillColor, LineType type)
            : base(width, color, fillColor, type)
        {
            this.graphics = graphics;
            this.centre = centre;
            this.radiusA = radiusA;
            this.radiusB = radiusB;
        }

        public IShape Copy(Point newPosition)
        {
            return null;
        }

        public void Draw()
        {
            this.graphics.DrawEllipse(this.pen, this.centre.X, this.centre.Y, (float)this.radiusA, (float)this.radiusB);
        }

        public bool IsInBounds(Point point)
        {
            var left = new Point(this.graphics, this.centre.X - (int)radiusA, this.centre.Y - (int)radiusB);
            var top = new Point(this.graphics, this.centre.X + (int)radiusA, this.centre.Y + (int)radiusB);

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

        public override void Select()
        {
            //TODO
        }
    }
}
