using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace proekt_formichki_klasove
{
    public class Square : Rectangle
    {
        public int Size
        {
            get { return Width; }
            set
            {
                Width = value;
                Height = value;
            }
        }
        public Square() : base() { }
        public Square(int x, int y, int size) : base(x, y, size, size) { }
        public override void Resize(int handleIndex, Point newPoint, Point lastPoint)
        {
            base.Resize(handleIndex, newPoint, lastPoint);

            int average = (Width + Height) / 2;
            Width = average;
            Height = average;
        }
    }
}

