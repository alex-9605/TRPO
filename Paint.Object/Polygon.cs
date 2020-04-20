using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    public class Polygon : Polyline
    {
        public override string Name => "Полигон";

        public Polygon(Guid? id, Graphics graphics, int width, Color color, Color fillColor, LineType type) 
            : base(id, graphics, width, color, fillColor, type)
        {
        }

        public Polygon(Guid? id, Graphics graphics, List<Point> points, int width, Color color, Color fillColor, LineType type)
            : base(id, graphics, points, width, color, fillColor, type)
        {
        }

        public override IShape Copy(Point newPosition)
        {
            var xMin = this.points.Min(p => p.X);
            var yMin = this.points.Min(p => p.Y);

            var deltaX = newPosition.X - xMin;
            var deltaY = newPosition.Y - yMin;

            var newPoints = this.points
                .Select(p => new Point(null, this.graphics, p.X + deltaX, p.Y + deltaY))
                .ToList();
            return new Polygon(null, this.graphics, newPoints, this.width, this.color, this.fillColor, this.type);
        }

        public override void Draw(Pen pen)
        {
            base.Draw(pen);

            var lastPoint = this.points.Last();
            var firstPoint = this.points.First();

            this.graphics.DrawLine(this.pen, new System.Drawing.Point(lastPoint.X, lastPoint.Y), 
                new System.Drawing.Point(firstPoint.X, firstPoint.Y));
            using (var brush = new SolidBrush(this.FillColor))
            {
                this.graphics.FillPolygon(brush, this.Points.Select(p => new PointF(p.X, p.Y)).ToArray(), FillMode.Alternate);
            }
        }
    }
}
