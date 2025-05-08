using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;

namespace proekt_formichki_klasove
{
    public class Triangle : Shape
    {
        private Point p1;
        private Point p2;
        private Point p3;

        public Point P1
        {
            get { return p1; }
            set
            {
                p1 = value;
            }
        }
        public Point P2
        {
            get { return p2; }
            set
            {
                p2 = value;
            }
        }
        public Point P3
        {
            get { return p3; }
            set
            {
                p3 = value;
            }
        }

        public double SideA => CalculateDistance(P1, P2);
        public double SideB => CalculateDistance(P2, P3);
        public double SideC => CalculateDistance(P3, P1);

        public Triangle() : base()
        {
            p1 = new Point(10, 10);
            p2 = new Point(50, 50);
            p3 = new Point(30, 80);

            X = (int)(P1.X + P2.X + P3.X) / 3;      // setting the base position to the centroid
            Y = (int)(P1.Y + P2.Y + P3.Y) / 3;
        }
        public Triangle(Point p1, Point p2, Point p3) : base()
        {
            this.P1 = p1;
            this.P2 = p2;
            this.P3 = p3;

            X = (P1.X + P2.X + P3.X) / 3;
            Y = (P1.Y + P2.Y + P3.Y) / 3;

        }
        public Triangle(int x, int y)
        {
            P1 = new Point(x, y);
            P2 = new Point(x + 50, y + 80);
            P3 = new Point(x - 50, y + 80);

            X = (P1.X + P2.X + P3.X) / 3;
            Y = (P1.Y + P2.Y + P3.Y) / 3;
        }
        public void SetVertices(Point p1, Point p2, Point p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;

            X = (P1.X + P2.X + P3.X) / 3;
            Y = (P1.Y + P2.Y + P3.Y) / 3;
        }
        public override void MoveBy(int deltaX, int deltaY)
        {
            P1 = new Point(P1.X + deltaX, P1.Y + deltaY);
            P2 = new Point(P2.X + deltaX, P2.Y + deltaY);
            P3 = new Point(P3.X + deltaX, P3.Y + deltaY);

            X = (P1.X + P2.X + P3.X) / 3;
            Y = (P1.Y + P2.Y + P3.Y) / 3;
        }

        public override void SetPosition(int x, int y)
        {
            int deltaX = x - X;
            int deltaY = y - Y;

            P1 = new Point(P1.X + deltaX, P1.Y + deltaY);
            P2 = new Point(P2.X + deltaX, P2.Y + deltaY);
            P3 = new Point(P3.X + deltaX, P3.Y + deltaY);

            X = x;
            Y = y;
        }
        public override double Perimeter()
        {
            return SideA + SideB + SideC;
        }
        public override double Area()   // heron's formula
        {
            double s = (SideA + SideB + SideC) / 2;
            return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
        }
        public override void Draw(Graphics g)
        {
            Point[] points = { p1, p2, p3 };
            using (Pen pen = new Pen(DrawColor, 3))
            {
                g.DrawPolygon(pen, points);
            }
            using (SolidBrush brush = new SolidBrush(FillColor))
            {
                g.FillPolygon(brush, points);
            }
        }
        public override bool Contains(Point point)
        {
            float d1 = Sign(point, P1, P2);     // 2-d cross product
            float d2 = Sign(point, P2, P3);     
            float d3 = Sign(point, P3, P1);

            bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(hasNeg && hasPos);
        }

        private float Sign(Point p1, Point p2, Point p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }
        public override void DrawSelection(Graphics g)
        {
            Point[] points = { P1, P2, P3 };
            using (Pen selectionPen = new Pen(Color.Blue, 1))
            {
                selectionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawPolygon(selectionPen, points);
            }
        }
        public override void DrawResizeHandles(Graphics g)
        {
            DrawHandle(g, P1.X, P1.Y);
            DrawHandle(g, P2.X, P2.Y);
            DrawHandle(g, P3.X, P3.Y);
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
            Point[] vertices = { P1, P2, P3 };

            for (int i = 0; i < 3; i++)
            {
                int dx = Math.Abs(point.X - vertices[i].X);
                int dy = Math.Abs(point.Y - vertices[i].Y);

                if (dx <= HANDLE_SIZE && dy <= HANDLE_SIZE)
                {
                    return i;
                }
            }

            return -1;
        }
        public override void Resize(int handleIndex, Point newPoint, Point lastPoint)
        {
            switch (handleIndex)
            {
                case 0:
                    P1 = new Point(newPoint.X, newPoint.Y);
                    break;
                case 1:
                    P2 = new Point(newPoint.X, newPoint.Y);
                    break;
                case 2:
                    P3 = new Point(newPoint.X, newPoint.Y);
                    break;
            }

            X = (P1.X + P2.X + P3.X) / 3;
            Y = (P1.Y + P2.Y + P3.Y) / 3;
        }
        private double CalculateDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}
