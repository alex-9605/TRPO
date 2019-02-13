using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    public class Polyline : Shape, IShape 
    {
        private readonly Graphics graphics;
        protected List<Point> points;

        public Polyline(Graphics graphics, List<Point> points, int width, Color color, Color fillColor, LineType type)
            : this(graphics, width, color, fillColor, type)
        {
            this.points = points;
        }

        public Polyline(Graphics graphics, int width, Color color, Color fillColor, LineType type)
            : base(width, color, fillColor, type)
        {
            this.points = new List<Point>();
            this.graphics = graphics;
        }

        public void AddPoint(Point point)
        {
            this.points.Add(point);
        }



        public virtual void Draw()
        {
            this.graphics.DrawLines(this.pen, this.points.Select(p => 
                new System.Drawing.Point(p.X, p.Y)).ToArray());
        }

        public bool IsInBounds(Point point)
        {
            var xMin = this.points.Min(p => p.X);
            var yMin = this.points.Min(p => p.Y);
            var xMax = this.points.Max(p => p.X);
            var yMax = this.points.Max(p => p.Y);

            if (point.X > xMin && point.X < xMax && point.Y > yMin && point.Y < yMax)
            {
                return true;
            }

            return false;
        }

        public IShape Copy(Point newPosition)
        {
            var xMin = this.points.Min(p => p.X);
            var yMin = this.points.Min(p => p.Y);

            var deltaX = newPosition.X - xMin;
            var deltaY = newPosition.Y - yMin;

            var newPoints = this.points
                .Select(p => new Point(this.graphics, p.X + deltaX, p.Y + deltaY))
                .ToList();
            return new Polyline(this.graphics, newPoints, this.width, this.color, this.fillColor, this.type);
        }

        public override void Select()
        {
            //TODO
        }
    }
}
