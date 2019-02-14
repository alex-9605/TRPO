using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint.Object
{
    public abstract class Shape : IShape
    {
        protected readonly Graphics graphics;
        protected int width;
        protected Color color;
        protected Color fillColor;
        protected LineType type;

        protected Pen pen;

        public Shape(Graphics graphics, int width, Color color, Color fillColor, LineType type)
        {
            this.graphics = graphics;
            this.width = width;
            this.color = color;
            this.fillColor = fillColor;
            this.type = type;

            this.pen = new Pen(this.color);
        }

        public abstract IShape Copy(Point newPosition);

        public abstract void Draw();

        public bool IsInBounds(Point point)
        {
            var bounds = this.GetBounds();

            if (point.X > bounds.Left.X && point.X < bounds.Top.X && point.Y > bounds.Left.Y && point.Y < bounds.Top.Y)
            {
                return true;
            }

            return false;
        }

        public void Select()
        {
            var bounds = this.GetBounds();

            this.graphics.DrawRectangle(this.pen, new Rectangle(bounds.Left.X, bounds.Left.Y, bounds.Top.X - bounds.Left.X, bounds.Top.Y - bounds.Left.Y));
        }

        protected abstract Bounds GetBounds();
    }
}
