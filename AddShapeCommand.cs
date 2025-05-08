using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proekt_formichki_klasove
{
    public class AddShapeCommand : ICommand
    {
        private readonly MainForm _form;
        private readonly Shape _shape;
        private readonly List<Shape> _shapesList;

        public AddShapeCommand(MainForm form, Shape shape, List<Shape> shapesList)
        {
            _form = form;
            _shape = shape;
            _shapesList = shapesList;
        }

        public void Execute()
        {
            _shapesList.Add(_shape);
            _form.UpdateShapesCount();
            _form.RefreshCanvas();
        }

        public void Undo()
        {
            _shapesList.Remove(_shape);
            _form.UpdateShapesCount();
            _form.RefreshCanvas();
        }
        public void Redo()
        {
            Execute();
        }
    }
}
