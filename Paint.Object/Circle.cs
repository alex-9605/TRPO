using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    public sealed class Circle : Shape, IShape
    {
        private Point left;
        private Point bottom;

        public Circle (Graphics graphics, Point left, Point bottom, 
            int width, Color color, Color fillColor, LineType type)
            : base(graphics, width, color, fillColor, type)
        {
            this.left = left;
            this.bottom = bottom;
        }

        public override void Draw()
        {
            this.Select();

            var heightAndWidth = this.bottom.Y - this.left.Y;
            var rect = new Rectangle(this.left.X, this.left.Y, heightAndWidth, heightAndWidth);
            this.graphics.DrawEllipse(this.pen, rect);
        }


        public override IShape Copy(Point newPosition)
        {
            var heightAndWidth = this.bottom.Y - this.left.Y;
            var newLeft = new Point(this.graphics, newPosition.X, newPosition.Y - heightAndWidth);
            var newBottom = new Point(this.graphics, newLeft.X + heightAndWidth, newLeft.Y + heightAndWidth);

            return new Circle(this.graphics, newLeft, newBottom, this.width, this.color, this.fillColor, this.type);
        }

        protected override Bounds GetBounds()
        {
            return new Bounds
            {
                Left = new Point(this.graphics, this.left.X, this.left.Y),
                Top = new Point(this.graphics, this.bottom.X, this.bottom.Y)
            };
        }
    }
}
