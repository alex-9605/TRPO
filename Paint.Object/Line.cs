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
        protected Point start;
        protected Point end;

        public Line(Point start, Point end, int width, Color color, Color fillColor, LineType type)
            : base(width, color, fillColor, type)
        {
            this.start = start;
            this.end = end;
        }

        public void Draw()
        {
        }
    }
}
