using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    public interface IShape
    {
        Guid Id { get; }

        string Name { get; }

        void Draw();

        bool IsInBounds(Point point);

        IShape Copy(Point newPosition);

        void Select();

        bool IsInMarkers(Point point);

        void Change(Point markerPoint, Point point);
    }
}
