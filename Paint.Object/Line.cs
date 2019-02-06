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
    class Line : Shape, IShape
    {
        private readonly Graphics graphics;
        private Point start;
        private Point end;

        private Line(Graphics graphics, Point start, Point end, int width, Color color, Color fillColor, LineType type)
            : base(width, color, fillColor, type)
        {
            this.graphics = graphics;
            this.start = start;
            this.end = end;
        }

        public void Draw()
        {
            this.graphics.DrawLine(this.pen, new System.Drawing.Point(this.start.X, this.start.Y), new System.Drawing.Point(this.end.X, this.end.Y));
        }
    }
}
