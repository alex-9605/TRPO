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

        public override string Name => "Линия";

        public Point Start => this.start;

        public Point End => this.end;

        public Line(Guid? id, Graphics graphics, Point start, Point end, int width, Color color, Color fillColor, LineType type)
            : base(id, graphics, width, color, fillColor, type)
        {
            this.start = start;
            this.end = end;
        }

        public override void Change(Point markerPoint, Point point)
        {
            if (IsInStartMarker(markerPoint))
            {
                this.start = point;
            }
            else if (IsInEndMarker(markerPoint))
            {
                this.end = point;
            }

            this.Draw(this.pen);
        }

        private bool IsInEndMarker(Point point)
        {
            return (this.end.X - MarkerWidth < point.X && this.end.Y - MarkerWidth < point.Y
                                                       && this.end.X + MarkerWidth > point.X &&
                                                       this.end.Y + MarkerWidth > point.Y);
        }

        private bool IsInStartMarker(Point point)
        {
            return this.start.X - MarkerWidth < point.X && this.start.Y - MarkerWidth < point.Y
                                                        && this.start.X + MarkerWidth > point.X &&
                                                        this.start.Y + MarkerWidth > point.Y;
        }

        public override void Draw(Pen pen)
        {
            base.Draw(pen);


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

            var newStart = new Point(null, this.graphics, newPosition.X, Math.Abs(this.start.Y - this.end.Y));
            var newEnd = new Point(null, this.graphics, Math.Abs(this.start.X - this.end.X), newPosition.Y);

            return new Line(null, this.graphics, newStart, newEnd, this.width, this.color, this.fillColor, this.type);
        }

        public override void Select()
        {
            this.DrawMarkers();
        }


        public override bool IsInMarkers(Point point)
        {
            var isInMarkers = this.IsInStartMarker(point) || this.IsInEndMarker(point);

            return isInMarkers;
        }

        protected override Bounds GetBounds()
        {
            var xMax = this.start.X > this.end.X ? this.start.X : this.end.X;
            var yMax = this.start.Y > this.end.Y ? this.start.Y : this.end.Y;
            var xMin = this.start.X < this.end.X ? this.start.X : this.end.X;
            var yMin = this.start.Y < this.end.Y ? this.start.Y : this.end.Y;

            return new Bounds
            {
                Left = new Point(null, this.graphics, xMin, yMin),
                Top = new Point(null, this.graphics, xMax, yMax)
            };
        }

        private void DrawMarkers()
        {
            this.graphics.DrawRectangle(selectionMarkerPen, this.start.X - MarkerWidth / 2, this.start.Y - MarkerWidth / 2, MarkerWidth, MarkerWidth);
            this.graphics.DrawRectangle(selectionMarkerPen, this.end.X - MarkerWidth / 2, this.end.Y - MarkerWidth / 2, MarkerWidth, MarkerWidth);
        }
    }
}
