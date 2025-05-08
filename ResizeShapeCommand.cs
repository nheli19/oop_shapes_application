using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proekt_formichki_klasove
{
    public class ResizeShapeCommand : ICommand
    {
        private readonly MainForm _form;
        private readonly Shape _shape;
        private readonly int _handleIndex;
        private readonly Point _newPoint;
        private readonly Point _oldPoint;

        public ResizeShapeCommand(MainForm form, Shape shape, int handleIndex, Point newPoint, Point oldPoint)
        {
            _form = form;
            _shape = shape;
            _handleIndex = handleIndex;
            _newPoint = newPoint;
            _oldPoint = oldPoint;
        }

        public void Execute()
        {
            _shape.Resize(_handleIndex, _newPoint, _oldPoint);
            _form.RefreshCanvas();
        }

        public void Undo()
        {
            _shape.Resize(_handleIndex, _oldPoint, _newPoint);
            _form.RefreshCanvas();
        }
        public void Redo()
        {
            Execute();
        }
    }
}
