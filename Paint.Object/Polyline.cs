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
        protected List<Point> points;
        private List<Rectangle> markers;

        public override string Name => "Полилиния";

        public Point[] Points => this.points.ToArray();

        public Polyline(Graphics graphics, List<Point> points, int width, Color color, Color fillColor, LineType type)
            : this(graphics, width, color, fillColor, type)
        {
            this.points = points;
            BuildMarkers();
        }

        private void BuildMarkers()
        {
            this.markers.Clear();
            foreach (var point in this.points)
            {
                this.markers.Add(new Rectangle
                {
                    X = point.X - MarkerWidth / 2,
                    Y = point.Y - MarkerWidth / 2,
                    Height = MarkerWidth,
                    Width = MarkerWidth
                });
            }
        }

        public Polyline(Graphics graphics, int width, Color color, Color fillColor, LineType type)
            : base(graphics, width, color, fillColor, type)
        {
            this.points = new List<Point>();
            this.markers = new List<Rectangle>();
        }

        public override void Change(Point markerPoint, Point point)
        {
            var marker = this.markers.First(p => p.Contains(markerPoint.X, markerPoint.Y));
            var changedPoint = this.points.First(p => marker.Contains(p.X, p.Y));
            changedPoint.SetPosition(point.X, point.Y);

            
            marker.X = point.X - MarkerWidth / 2;
            marker.Y = point.Y - MarkerWidth / 2;
            marker.Height = MarkerWidth;
            marker.Width = MarkerWidth;

            
            var markerIndex = this.markers.FindIndex(p => p.Contains(markerPoint.X, markerPoint.Y));
            this.markers[markerIndex] = marker;

            this.Draw();
        }

        public override void Select()
        {
            this.DrawMarkers();
        }

        public override bool IsInMarkers(Point point)
        {
            return this.markers.Any(p => p.Contains(point.X, point.Y));
        }

        public void AddPoint(Point point)
        {
            this.points.Add(point);
            this.markers.Add(new Rectangle
            {
                X = point.X - MarkerWidth / 2,
                Y = point.Y - MarkerWidth / 2,
                Height = MarkerWidth,
                Width = MarkerWidth
            });
        }
        
        public override void Draw()
        {
            base.Draw();
            this.graphics.DrawLines(this.pen, this.points.Select(p => 
                new System.Drawing.Point(p.X, p.Y)).ToArray());
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
            return new Polyline(this.graphics, newPoints, this.width, this.color, this.fillColor, this.type);
        }

        protected override Bounds GetBounds()
        {
            var xMin = this.points.Min(p => p.X);
            var yMin = this.points.Min(p => p.Y);
            var xMax = this.points.Max(p => p.X);
            var yMax = this.points.Max(p => p.Y);

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
                this.graphics.DrawRectangle(selectionMarkerPen, marker);
            }
        }
    }
}
