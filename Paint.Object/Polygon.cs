using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    public class Polygon : Polyline
    {
        public Polygon(Graphics graphics, int width, Color color, Color fillColor, LineType type) 
            : base(graphics, width, color, fillColor, type)
        {
        }

        public Polygon(Graphics graphics, List<Point> points, int width, Color color, Color fillColor, LineType type)
            : base(graphics, points, width, color, fillColor, type)
        {
        }

        public override IShape Copy(Point newPosition)
        {
            var xMin = this.points.Min(p => p.X);
            var yMin = this.points.Min(p => p.Y);

            var deltaX = newPosition.X - xMin;
            var deltaY = newPosition.Y - yMin;

            var newPoints = this.points
                .Select(p => new Point(this.graphics, p.X + deltaX, p.Y + deltaY))
                .ToList();
            return new Polygon(this.graphics, newPoints, this.width, this.color, this.fillColor, this.type);
        }

        public override void Draw()
        {
            base.Draw();

            var lastPoint = this.points.Last();
            var firstPoint = this.points.First();

            this.graphics.DrawLine(this.pen, new System.Drawing.Point(lastPoint.X, lastPoint.Y), 
                new System.Drawing.Point(firstPoint.X, firstPoint.Y));
        }
    }
}
