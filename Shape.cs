using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace proekt_formichki_klasove
{
    public abstract class Shape
    {
        public int X { get; protected set; } = 0;
        public int Y { get; protected set; } = 0;
        public Color DrawColor { get; set; } = Color.Black;
        public Color FillColor { get; set; } = Color.White;
        protected const int HANDLE_SIZE = 6;
        protected Shape(int x = 0, int y = 0) 
        {
            X = x;
            Y = y;
        }
        public abstract double Area();
        public abstract double Perimeter();
        public abstract void Draw(Graphics g);
        public abstract bool Contains(Point point);
        public virtual void MoveBy(int deltaX, int deltaY)
        {
            X = X + deltaX;
            Y = Y + deltaY;
        }
        public virtual void SetPosition(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }
        public abstract void DrawSelection(Graphics g);
        public abstract void DrawResizeHandles(Graphics g);
        public abstract int GetResizeHandleUnderMouse(Point point);
        public abstract void Resize(int handleIndex, Point newPoint, Point lastPoint);
    }
}
