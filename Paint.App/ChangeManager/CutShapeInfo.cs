using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paint.Object;

namespace Paint.App.ChangeManager
{
    class CutShapeInfo : BaseShapeChangeInfo
    {
        private readonly int oldIndex;
        private readonly int newIndex;
        private readonly IShape oldShape;

        public override string Description => $"Вырезан {this.shape.Name}";

        public int OldIndex => this.oldIndex;

        public int NewIndex => this.newIndex;

        public IShape OldShape => this.oldShape;

        public CutShapeInfo(IShape oldShape, IShape shape, List<IShape> commonList, int oldIndex, int newIndex) 
            : base(shape, commonList)
        {
            this.oldIndex = oldIndex;
            this.newIndex = newIndex;
            this.oldShape = oldShape;
        }

        public override void Redo()
        {
            this.commonList.RemoveAt(this.oldIndex);
            this.commonList.Insert(this.newIndex, this.shape);
        }

        public override void Undo()
        {
            this.commonList.RemoveAt(this.newIndex);
            this.commonList.Insert(this.oldIndex, this.oldShape);
        }
    }
}
