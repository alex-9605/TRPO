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
        private Rectangle contour;
        private List<Rectangle> markers;

        public Ellipse(Graphics graphics, Point left, int rectWidth, int rectHeight, 
            int width, Color color, Color fillColor, LineType type)
            : base(graphics, width, color, fillColor, type)
        {
            this.markers = new List<Rectangle>();

            this.contour = new Rectangle(left.X, left.Y, rectWidth, rectHeight);

            BuildMarkers();
        }

        private void BuildMarkers()
        {
            this.markers.Clear();
            var bound = this.GetBounds();
            this.markers.Add(new Rectangle(bound.Left.X - MarkerWidth / 2, bound.Left.Y - MarkerWidth / 2, MarkerWidth,
                MarkerWidth));
            this.markers.Add(new Rectangle(bound.Top.X - MarkerWidth / 2, bound.Top.Y - MarkerWidth / 2, MarkerWidth,
                MarkerWidth));
            this.markers.Add(new Rectangle(bound.Left.X + Math.Abs(bound.Top.X - bound.Left.X) - MarkerWidth / 2,
                bound.Left.Y - MarkerWidth / 2, MarkerWidth, MarkerWidth));
            this.markers.Add(new Rectangle(bound.Top.X - Math.Abs(bound.Top.X - bound.Left.X) - MarkerWidth / 2,
                bound.Left.Y + Math.Abs(bound.Top.Y - bound.Left.Y) - MarkerWidth / 2, MarkerWidth, MarkerWidth));
        }

        public void Change(Point markerPoint, Point point)
        {

            if (this.markers[0].Contains(markerPoint.X, markerPoint.Y))
            {
                var newWidth = this.contour.Width + this.contour.X - point.X;
                var newHeight = this.contour.Height + this.contour.Y - point.Y;
                this.contour.Size = new Size(newWidth, newHeight);
                this.contour.X = point.X;
                this.contour.Y = point.Y;
            }

            if (this.markers[1].Contains(markerPoint.X, markerPoint.Y))
            {
                var newWidth = Math.Abs(this.contour.X - point.X); // this.contour.Width + this.contour.Right + point.X;
                var newHeight = Math.Abs(this.contour.Y - point.Y); //this.contour.Height + this.contour.Bottom + point.Y;
                this.contour.Size = new Size(newWidth, newHeight);
            }

            if (this.markers[2].Contains(markerPoint.X, markerPoint.Y))
            {
                var newWidth = Math.Abs(this.markers[3].X - point.X);
                var newHeight = Math.Abs(this.markers[3].Y - point.Y);

                this.contour.Size = new Size(newWidth, newHeight);
                this.contour.X = point.X - newWidth;
                this.contour.Y = point.Y;
            }

            if (this.markers[3].Contains(markerPoint.X, markerPoint.Y))
            {
                var newWidth = Math.Abs(markers[2].X - point.X);
                var newHeight = Math.Abs(markers[2].Y - point.Y);
                this.contour.Size = new Size(newWidth, newHeight);
                this.contour.X = point.X;
            }

            this.BuildMarkers();
            this.Draw();
        }


        public override void Select()
        {
            base.Select();
            this.DrawMarkers();
        }

        public override void Draw()
        {
            this.graphics.DrawEllipse(this.pen, this.contour);
        }

        public override IShape Copy(Point newPosition)
        {
            return new Ellipse(this.graphics, newPosition, this.contour.Width, this.contour.Height, this.width, this.color, this.fillColor, this.type);
        }

        public override bool IsInMarkers(Point point)
        {
            return this.markers.Any(p => p.Contains(point.X, point.Y));
        }

        protected override Bounds GetBounds()
        {
            return new Bounds
            {
                Left = new Point(this.graphics, this.contour.X, this.contour.Y),
                Top = new Point(this.graphics, this.contour.Right, this.contour.Bottom)
            };
        }

        private void DrawMarkers()
        {
            foreach (var marker in this.markers)
            {
                this.graphics.DrawRectangle(this.pen, marker);
            }
        }
    }
}
