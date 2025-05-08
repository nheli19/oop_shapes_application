using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proekt_formichki_klasove
{
    public class MoveShapeCommand : ICommand
    {
        private readonly MainForm _form;
        private readonly Shape _shape;
        private readonly int _deltaX;
        private readonly int _deltaY;

        public MoveShapeCommand(MainForm form, Shape shape, int deltaX, int deltaY)
        {
            _form = form;
            _shape = shape;
            _deltaX = deltaX;
            _deltaY = deltaY;
        }

        public void Execute()
        {
            _shape.MoveBy(_deltaX, _deltaY);
            _form.RefreshCanvas();
        }

        public void Undo()
        {
            _shape.MoveBy(-_deltaX, -_deltaY);
            _form.RefreshCanvas();
        }
        public void Redo()
        {
            Execute();
        }
    }
}
