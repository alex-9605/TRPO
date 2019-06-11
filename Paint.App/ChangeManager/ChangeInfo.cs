using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.App.ChangeManager
{
    abstract class ChangeInfo
    {
        public abstract string Description { get; }

        public abstract void Undo();

        public abstract void Redo();
    }
}
