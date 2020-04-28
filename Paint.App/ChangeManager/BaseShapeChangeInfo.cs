using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paint.Object;

namespace Paint.App.ChangeManager
{
    abstract class BaseShapeChangeInfo : ChangeInfo
    {
        protected readonly IShape shape;
        protected readonly List<IShape> commonList;

        public IShape Shape => this.shape;

        protected BaseShapeChangeInfo(IShape shape, List<IShape> commonList)
        {
            this.shape = shape;
            this.commonList = commonList;
        }
    }
}
