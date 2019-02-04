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
        protected List<Point> points;

        public Polyline(int width, Color color, Color fillColor, LineType type)
            : base(width, color, fillColor, type)
        {
            this.points = new List<Point>();
        }

        public void AddPoint(Point point)
        {
            this.points.Add(point);
        }

        public void Draw()
        {
        }
    }
}
