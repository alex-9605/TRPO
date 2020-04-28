using System.Collections.Generic;
using System.Linq;
using Paint.Object;

namespace Paint.App.ChangeManager
{
    class ChangeManager
    {
        private readonly Stack<ChangeInfo> undoStack;
        private readonly Stack<ChangeInfo> redoStack;
        private System.Windows.Forms.ListBox.ObjectCollection historyCollection;

        public ChangeInfo[] UndoItems => this.undoStack.ToArray();
        public Stack<ChangeInfo> RedoItems => this.redoStack;

        public ChangeManager(System.Windows.Forms.ListBox.ObjectCollection historyCollection, IEnumerable<ChangeInfo> undo) 
            : this(historyCollection)
        {
            foreach (var changeInfo in undo.Reverse())
            {
                changeInfo.Redo();
                this.historyCollection.Add(changeInfo.Description);
                this.undoStack.Push(changeInfo);
            }
        }

        public ChangeManager(System.Windows.Forms.ListBox.ObjectCollection historyCollection)
        {
            this.undoStack = new Stack<ChangeInfo>();
            this.redoStack = new Stack<ChangeInfo>();
            this.historyCollection = historyCollection;
        }

        public void Clear()
        {
            this.undoStack.Clear();
            this.redoStack.Clear();
            this.historyCollection.Clear();
        }

        public void SaveChange(ChangeInfo changeInfo)
        {
            this.undoStack.Push(changeInfo);
            this.historyCollection.Add(changeInfo.Description);
        }

        public void Undo()
        {
            if (!this.undoStack.Any())
            {
                return;
            }

            var change = this.undoStack.Pop();
            change.Undo();
            this.redoStack.Push(change);
            this.historyCollection.RemoveAt(this.historyCollection.Count - 1);
        }

        public void Redo()
        {
            if (!this.redoStack.Any())
            {
                return;
            }

            var change = this.redoStack.Pop();
            change.Redo();
            this.undoStack.Push(change);
            this.historyCollection.Add(change.Description);
        }
    }
}
