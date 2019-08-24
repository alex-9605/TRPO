using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Paint.Object
{
    public class Point : IShape
    {
        private readonly Graphics graphics;

        public string Name => "Точка";

        public int X { get; private set; }
        public int Y { get; private set; }

        public Guid Id { get; }

        public Point(Guid? id, Graphics graphics, int x, int y)
        {
            this.Id = id ?? Guid.NewGuid();

            this.graphics = graphics;
            this.X = x;
            this.Y = y;
        }
        
        public void Draw()
        {
        }

        public bool IsInBounds(Point point)
        {
            if (this.X == point.X && this.Y == point.Y)
            {
                return true;
            }

            return false;
        }

        public void SetPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public IShape Copy(Point newPosition)
        {
            return new Point(null, this.graphics, newPosition.X, newPosition.Y);
        }

        public void Select()
        {
            throw new NotImplementedException();
        }

        public bool IsInMarkers(Point point)
        {
            throw new NotImplementedException();
        }

        public void Change(Point markerPoint, Point point)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            var anotherShape = other as IShape;
            if (anotherShape == null)
                return false;

            return this.Id == anotherShape.Id;
        }
    }
}
