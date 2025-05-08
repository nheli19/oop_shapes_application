using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proekt_formichki_klasove
{
    public class DeleteShapeCommand : ICommand
    {
        private readonly MainForm _form;
        private readonly Shape _shape;
        private readonly List<Shape> _shapesList;
        private int _index;

        public DeleteShapeCommand(MainForm form, Shape shape, List<Shape> shapesList)
        {
            _form = form;
            _shape = shape;
            _shapesList = shapesList;
        }

        public void Execute()
        {
            _index = _shapesList.IndexOf(_shape);
            if (_index != -1)
            {
                _shapesList.RemoveAt(_index);
                _form.UpdateShapesCount();
                _form.RefreshCanvas();
            }
        }

        public void Undo()
        {
            if (_index != -1)
            {
                _shapesList.Insert(Math.Min(_index, _shapesList.Count), _shape);
                _form.UpdateShapesCount();
                _form.RefreshCanvas();
            }
        }
        public void Redo()
        {
            Execute();
        }
    }
}
