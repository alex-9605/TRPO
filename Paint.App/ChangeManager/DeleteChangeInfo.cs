using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paint.Object;

namespace Paint.App.ChangeManager
{
    class DeleteChangeInfo : BaseShapeChangeInfo
    {
        public override string Description => $"Удалён {this.shape.Name}";

        public DeleteChangeInfo(IShape shape, List<IShape> commonList)
            : base(shape, commonList)
        {
        }

        public override void Redo()
        {
            this.commonList.Remove(this.shape);
        }

        public override void Undo()
        {
            this.commonList.Add(this.shape);
        }
    }
}
