using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proekt_formichki_klasove
{
    public class ClearAllCommand : ICommand
    {
        private readonly MainForm _form;
        private readonly List<Shape> _shapesList;
        private List<Shape> _backup;

        public ClearAllCommand(MainForm form, List<Shape> shapesList)
        {
            _form = form;
            _shapesList = shapesList;
        }

        public void Execute()
        {
            _backup = new List<Shape>(_shapesList);
            _shapesList.Clear();
            _form.UpdateShapesCount();
            _form.RefreshCanvas();
        }

        public void Undo()
        {
            _shapesList.AddRange(_backup);
            _form.UpdateShapesCount();
            _form.RefreshCanvas();
        }
        public void Redo()
        {
            Execute();
        }
    }
}
