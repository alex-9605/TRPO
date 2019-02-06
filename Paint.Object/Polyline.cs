using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    class Polyline : Shape, IShape 
    {
        private readonly Graphics graphics;
        protected List<Point> points;

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
            this.graphics.DrawLines(this.pen, this.points.Select(p => new System.Drawing.Point(p.X, p.Y)).ToArray());
        }
    }
}
