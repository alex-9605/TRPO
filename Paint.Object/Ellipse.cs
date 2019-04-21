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
        private List<Rectangle> markers;

        public Ellipse(Graphics graphics, Point centre, double radiusA, double radiusB, 
            int width, Color color, Color fillColor, LineType type)
            : base(graphics, width, color, fillColor, type)
        {
            this.markers = new List<Rectangle>();


            this.centre = centre;
            this.radiusA = radiusA;
            this.radiusB = radiusB;

            var bound = this.GetBounds();
            this.markers.Add(new Rectangle(bound.Left.X - MarkerWidth / 2, bound.Left.Y - MarkerWidth / 2, MarkerWidth, MarkerWidth));
            this.markers.Add(new Rectangle(bound.Top.X - MarkerWidth / 2, bound.Top.Y - MarkerWidth / 2, MarkerWidth, MarkerWidth));
            this.markers.Add(new Rectangle(bound.Left.X + Math.Abs(bound.Top.X - bound.Left.X) - MarkerWidth / 2, bound.Left.Y - MarkerWidth / 2, MarkerWidth, MarkerWidth));
            this.markers.Add(new Rectangle(bound.Top.X - Math.Abs(bound.Top.X - bound.Left.X) - MarkerWidth / 2, bound.Left.Y + Math.Abs(bound.Top.Y - bound.Left.Y) - MarkerWidth / 2, MarkerWidth, MarkerWidth));
        }

        public void Change(Point markerPoint, Point point)
        {
        }


        public override void Select()
        {
            base.Select();
            this.DrawMarkers();
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

        public override bool IsInMarkers(Point point)
        {
            return this.markers.Any(p => p.Contains(point.X, point.Y));
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

        private void DrawMarkers()
        {
            foreach (var marker in this.markers)
            {
                this.graphics.DrawRectangle(this.pen, marker);
            }
        }
    }
}
