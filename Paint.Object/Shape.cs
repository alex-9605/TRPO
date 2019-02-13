using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint.Object
{
    public abstract class Shape
    {
        protected int width;
        protected Color color;
        protected Color fillColor;
        protected LineType type;

        protected Pen pen;

        public Shape(int width, Color color, Color fillColor, LineType type)
        {
            this.width = width;
            this.color = color;
            this.fillColor = fillColor;
            this.type = type;

            this.pen = new Pen(this.color);
        }

        public abstract void Select();
    }
}
