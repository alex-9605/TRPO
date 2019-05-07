using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Paint.Object
{
    public abstract class Shape : IShape
    {
        protected const int MarkerWidth = 4;

        protected static Color selectionColor = Color.FromArgb(255, 0, 120, 215);
        protected static Pen selectionPen = new Pen(selectionColor, 1)
        {
            DashStyle = DashStyle.Dash,
            DashOffset = 2
        };

        protected static Pen selectionMarkerPen = new Pen(selectionColor);
        protected readonly Graphics graphics;
        protected int width;
        protected Color color;
        protected Color fillColor;
        protected LineType type;
        protected bool isSelected;

        protected Pen pen;

        public Shape(Graphics graphics, int width, Color color, Color fillColor, LineType type)
        {
            this.isSelected = false;

            this.graphics = graphics;
            this.width = width;
            this.color = color;
            this.fillColor = fillColor;
            this.type = type;

            this.pen = new Pen(this.color);
        }

        public bool IsSelected => this.isSelected;

        public abstract IShape Copy(Point newPosition);

        public virtual void Draw()
        {
            this.isSelected = false;
        }

        // TODO: сделать абстрактным после реализациии во всех производных классах
        public virtual bool IsInMarkers(Point point) 
        {
            return false;
        }

        public bool IsInBounds(Point point)
        {
            var bounds = this.GetBounds();

            if (point.X > bounds.Left.X && point.X < bounds.Top.X && point.Y > bounds.Left.Y && point.Y < bounds.Top.Y)
            {
                return true;
            }

            return false;
        }

        public virtual void Select()
        {
            this.isSelected = true;
            var bounds = this.GetBounds();

            this.graphics.DrawRectangle(selectionPen, new Rectangle(bounds.Left.X, bounds.Left.Y, Math.Abs(bounds.Top.X - bounds.Left.X), Math.Abs(bounds.Top.Y - bounds.Left.Y)));
        }

        protected abstract Bounds GetBounds();
    }
}
