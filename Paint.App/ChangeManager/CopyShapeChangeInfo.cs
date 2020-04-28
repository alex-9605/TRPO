using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paint.Object;

namespace Paint.App.ChangeManager
{
    class CopyShapeChangeInfo : BaseShapeChangeInfo
    {
        public override string Description => $"Скопирован {this.shape.Name}";

        public CopyShapeChangeInfo(IShape shape, List<IShape> commonList) : base(shape, commonList)
        {
        }

        public override void Redo()
        {
            this.commonList.Add(this.shape);
        }

        public override void Undo()
        {
            this.commonList.Remove(this.shape);
        }


    }
}
