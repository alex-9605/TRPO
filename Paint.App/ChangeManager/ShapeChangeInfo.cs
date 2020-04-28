using Paint.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.App.ChangeManager
{
    class ShapeChangeInfo : BaseShapeChangeInfo
    {
        private Point markerPoint;
        private Point point;

        public ShapeChangeInfo(IShape shape, List<IShape> commonList, Point markerPoint, Point point) : base(shape, commonList)
        {
            this.markerPoint = markerPoint;
            this.point = point;
        }

        public override string Description => $"Изменён {this.shape.Name}";

        public Point MarkerPoint => this.markerPoint;

        public Point Point => this.point;

        public override void Redo()
        {
            this.shape.Change(this.markerPoint, this.point);
        }

        public override void Undo()
        {
            this.shape.Change(this.point, this.markerPoint);
        }
    }
}
