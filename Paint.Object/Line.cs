using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    /// <summary>
    /// Класс линия
    /// </summary>
    public class Line : Shape, IShape
    {
        private Point start;
        private Point end;

        public Line(Graphics graphics, Point start, Point end, int width, Color color, Color fillColor, LineType type)
            : base(graphics, width, color, fillColor, type)
        {
            this.start = start;
            this.end = end;
        }
            
        public override void Draw()
        {
            this.graphics.DrawLine(this.pen, new System.Drawing.Point(this.start.X, this.start.Y), new System.Drawing.Point(this.end.X, this.end.Y));
        }

        public override IShape Copy(Point newPosition)
        {
            Point another;
            var left = this.start.X < this.end.X ? this.start : this.end;
            if (left.X == this.start.X && left.Y == this.start.Y)
            {
                another = this.end;
            }
            else
            {
                another = this.start;
            }

            var newStart = new Point(this.graphics, newPosition.X, Math.Abs(this.start.Y - this.end.Y));
            var newEnd = new Point(this.graphics, Math.Abs(this.start.X - this.end.X), newPosition.Y);

            return new Line(this.graphics, newStart, newEnd, this.width, this.color, this.fillColor, this.type);
        }

        protected override Bounds GetBounds()
        {
            var xMax = this.start.X > this.end.X ? this.start.X : this.end.X;
            var yMax = this.start.Y > this.end.Y ? this.start.Y : this.end.Y;
            var xMin = this.start.X < this.end.X ? this.start.X : this.end.X;
            var yMin = this.start.Y < this.end.Y ? this.start.Y : this.end.Y;

            return new Bounds
            {
                Left = new Point(this.graphics, xMin, yMin),
                Top = new Point(this.graphics, xMax, yMax)
            };
        }
    }
}
