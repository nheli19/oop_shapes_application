using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace proekt_formichki_klasove
{
    public class Rectangle : Shape
    {
        private int width;
        private int height;
        public int Width
        {
            get { return width; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Width must be positive");
                }
                else
                {
                    width = value;
                }
            }
        }
        public int Height
        {
            get { return height; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Height must be positive");
                }
                else
                {
                    height = value;
                }
            }
        }
        public Rectangle() : base(0,0) { }
        public Rectangle(int x, int y, int width, int height) : base(x,y)
        {                                           
            Width = width;
            Height = height;
        }
        public override double Area()
        {
            return Width * Height;
        }
        public override double Perimeter()
        {
            return 2.0 * (Width + Height);
        }
        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(DrawColor, 3))
            {
                g.DrawRectangle(pen, X, Y, Width, Height);
            }
            using (SolidBrush brush = new SolidBrush(FillColor))
            {
                g.FillRectangle(brush, X, Y, Width, Height);
            }
        }
        public override bool Contains(Point point)
        {
            return point.X >= X && point.X <= X + Width &&
                   point.Y >= Y && point.Y <= Y + Height;
        }
        public override void DrawSelection(Graphics g)
        {
            using (Pen selectionPen = new Pen(Color.Blue, 1))
            {
                selectionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawRectangle(selectionPen, X, Y, (float)Width, (float)Height);
            }
        }
        public override void DrawResizeHandles(Graphics g)
        {
            DrawHandle(g, X, Y); 
            DrawHandle(g, X + Width / 2, Y); 
            DrawHandle(g, X + Width, Y); 
            DrawHandle(g, X, Y + Height / 2); 
            DrawHandle(g, X + Width, Y + Height / 2); 
            DrawHandle(g, X, Y + Height); 
            DrawHandle(g, X + Width / 2, Y + Height); 
            DrawHandle(g, X + Width, Y + Height);
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
            Point[] handlePoints = new Point[8]
            {
                new Point(X, Y),
                new Point(X + Width / 2, Y),
                new Point(X + Width, Y),
                new Point(X, Y + Height / 2),
                new Point(X + Width, Y + Height / 2),
                new Point(X, Y + Height),
                new Point(X + Width / 2, Y + Height),
                new Point(X + Width, Y + Height)
            };

            for (int i = 0; i < 8; i++)
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
            int minDimension = 5;            // safety for negative side

            switch (handleIndex)
            {
                case 0:
                    int deltaXTopLeft = newPoint.X - lastPoint.X;
                    int deltaYTopLeft = newPoint.Y - lastPoint.Y;
                    if (Width - deltaXTopLeft >= minDimension)
                    {
                        X += deltaXTopLeft;
                        Width -= deltaXTopLeft;
                    }
                    if (Height - deltaYTopLeft >= minDimension)
                    {
                        Y += deltaYTopLeft;
                        Height -= deltaYTopLeft;
                    }
                    break;

                case 1:
                    int deltaYTopMiddle = newPoint.Y - lastPoint.Y;
                    if (Height - deltaYTopMiddle >= minDimension)
                    {
                        Y += deltaYTopMiddle;
                        Height -= deltaYTopMiddle;
                    }
                    break;

                case 2:
                    int deltaXTopRight = newPoint.X - lastPoint.X;
                    int deltaYTopRight = newPoint.Y - lastPoint.Y;
                    if (Width + deltaXTopRight >= minDimension)
                    {
                        Width += deltaXTopRight;
                    }
                    if (Height - deltaYTopRight >= minDimension)
                    {
                        Y += deltaYTopRight;
                        Height -= deltaYTopRight;
                    }
                    break;

                case 3:
                    int deltaXMiddleLeft = newPoint.X - lastPoint.X;
                    if (Width - deltaXMiddleLeft >= minDimension)
                    {
                        X += deltaXMiddleLeft;
                        Width -= deltaXMiddleLeft;
                    }
                    break;

                case 4:
                    int deltaXMiddleRight = newPoint.X - lastPoint.X;
                    if (Width + deltaXMiddleRight >= minDimension)
                    {
                        Width += deltaXMiddleRight;
                    }
                    break;

                case 5:
                    int deltaXBottomLeft = newPoint.X - lastPoint.X;
                    int deltaYBottomLeft = newPoint.Y - lastPoint.Y;
                    if (Width - deltaXBottomLeft >= minDimension)
                    {
                        X += deltaXBottomLeft;
                        Width -= deltaXBottomLeft;
                    }
                    if (Height + deltaYBottomLeft >= minDimension)
                    {
                        Height += deltaYBottomLeft;
                    }
                    break;

                case 6:
                    int deltaYBottomMiddle = newPoint.Y - lastPoint.Y;
                    if (Height + deltaYBottomMiddle >= minDimension)
                    {
                        Height += deltaYBottomMiddle;
                    }
                    break;

                case 7:
                    int deltaXBottomRight = newPoint.X - lastPoint.X;
                    int deltaYBottomRight = newPoint.Y - lastPoint.Y;
                    if (Width + deltaXBottomRight >= minDimension)
                    {
                        Width += deltaXBottomRight;
                    }
                    if (Height + deltaYBottomRight >= minDimension)
                    {
                        Height += deltaYBottomRight;
                    }
                    break;
            }
        }
    }
}
