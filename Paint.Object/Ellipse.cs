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
        private Point centre;
        private double radiusA;
        private double radiusB;

        public Ellipse(Graphics graphics, Point centre, double radiusA, double radiusB, 
            int width, Color color, Color fillColor, LineType type)
            : base(graphics, width, color, fillColor, type)
        {
            this.centre = centre;
            this.radiusA = radiusA;
            this.radiusB = radiusB;
        }

        public override void Draw()
        {
            var left = new Point(this.graphics, this.centre.X - (int)radiusA, this.centre.Y - (int)radiusB);
            var top = new Point(this.graphics, this.centre.X + (int)radiusA, this.centre.Y + (int)radiusB);
            var xMin = left.X < top.X ? left.X : top.X;
            var yMin = left.Y < top.Y ? left.Y : top.Y;

            this.graphics.DrawEllipse(this.pen, xMin, yMin, (float)this.radiusA * 2, (float)this.radiusB * 2);
        }

        public override IShape Copy(Point newPosition)
        {
            var newCentre = new Point(this.graphics, newPosition.X + (int)this.radiusA, newPosition.Y + (int)this.radiusB);
            return new Ellipse(this.graphics, newCentre, this.radiusA,this.radiusB, this.width, this.color, this.fillColor, this.type);
        }

        protected override Bounds GetBounds()
        {
            var left = new Point(this.graphics, this.centre.X - (int)radiusA, this.centre.Y - (int)radiusB);
            var top = new Point(this.graphics, this.centre.X + (int)radiusA, this.centre.Y + (int)radiusB);

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
