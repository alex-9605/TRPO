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
        void Draw();

        bool IsInBounds(Point point);

        IShape Copy(Point newPosition);

        void Select();
    }
}
