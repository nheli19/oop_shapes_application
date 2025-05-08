using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace proekt_formichki_klasove
{
    public class Circle : Shape
    {
        private int radius;
        public int Radius
        {
            get { return radius; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Radius must be positive");
                }
                else
                {
                    radius = value;
                }
            }
        }
        public Circle() : base(0, 0) { }
        public Circle(int x, int y, int radius) : base(x, y) 
        { 
            Radius = radius;
        }
        public override double Perimeter()
        {
            return 2.0 * Math.PI * Radius;
        }
        public override double Area()
        {
            return Math.PI * Radius * Radius;
        }
        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(DrawColor, 3))
            {
                g.DrawEllipse(pen, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            }
            using (SolidBrush brush = new SolidBrush(FillColor))
            {
                g.FillEllipse(brush, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            }
        }
        public override bool Contains(Point point)
        {
            double dx = point.X - X;
            double dy = point.Y - Y;
            return Math.Sqrt(dx * dx + dy * dy) <= Radius;
        }
        public override void DrawSelection(Graphics g)
        {
            using (Pen selectionPen = new Pen(Color.Blue, 1))
            {
                selectionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawEllipse(selectionPen, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            }
        }
        public override void DrawResizeHandles(Graphics g)
        {
            DrawHandle(g, X, Y - Radius);
            DrawHandle(g, X + Radius, Y);
            DrawHandle(g, X, Y + Radius);
            DrawHandle(g, X - Radius, Y);
        }
        private void DrawHandle(Graphics g, int x, int y)
        {
            using (SolidBrush brush = new SolidBrush(Color.White))
            using (Pen pen = new Pen(Color.Black, 1))
            {
                g.FillRectangle(brush, x - HANDLE_SIZE / 2, y - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE);
                g.DrawRectangle(pen, x - HANDLE_SIZE / 2, y - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE);
            }
        }
        public override int GetResizeHandleUnderMouse(Point point)
        {
            Point[] handlePoints = new Point[4]
            {
                new Point(X, Y - Radius),
                new Point(X + Radius, Y),
                new Point(X, Y + Radius),
                new Point(X - Radius, Y)
            };

            for (int i = 0; i < 4; i++)
            {
                int dx = Math.Abs(point.X - handlePoints[i].X);
                int dy = Math.Abs(point.Y - handlePoints[i].Y);

                if (dx <= HANDLE_SIZE && dy <= HANDLE_SIZE)
                {
                    return i;
                }
            }

            return -1;
        }

        public override void Resize(int handleIndex, Point newPoint, Point lastPoint)
        {
            Point center = new Point(X, Y);
            int minRadius = 5;

            double distanceNew = Math.Sqrt(Math.Pow(newPoint.X - center.X, 2) + Math.Pow(newPoint.Y - center.Y, 2));

            switch (handleIndex)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    if (distanceNew >= minRadius)
                    {
                        Radius = (int)distanceNew;
                    }
                    break;
            }
        }
    }
}
