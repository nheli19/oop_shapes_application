using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proekt_formichki_klasove
{
    public class ChangePropertiesCommand : ICommand
    {
        private MainForm form;
        private Shape shape;

        private Color oldDrawColor;
        private Color oldFillColor;
        private Color newDrawColor;
        private Color newFillColor;

        private int oldX, oldY;
        private int newX, newY;

        private int oldWidth, oldHeight, oldRadius, oldSize;
        private int newWidth, newHeight, newRadius, newSize;

        private Point oldP1, oldP2, oldP3;
        private Point newP1, newP2, newP3;

        public ChangePropertiesCommand(MainForm form, Shape shape)
        {
            this.form = form;
            this.shape = shape;

            oldDrawColor = shape.DrawColor;
            oldFillColor = shape.FillColor;
            oldX = shape.X;
            oldY = shape.Y;

            if (shape is Rectangle rect)
            {
                oldWidth = rect.Width;
                oldHeight = rect.Height;
            }
            else if (shape is Circle circle)
            {
                oldRadius = circle.Radius;
            }
            else if (shape is Square square)
            {
                oldSize = square.Size;
            }
            else if (shape is Triangle triangle)
            {
                oldP1 = triangle.P1;
                oldP2 = triangle.P2;
                oldP3 = triangle.P3;
            }
        }

        public void Execute()
        {
            newDrawColor = shape.DrawColor;
            newFillColor = shape.FillColor;
            newX = shape.X;
            newY = shape.Y;

            if (shape is Rectangle rect)
            {
                newWidth = rect.Width;
                newHeight = rect.Height;
            }
            else if (shape is Circle circle)
            {
                newRadius = circle.Radius;
            }
            else if (shape is Square square)
            {
                newSize = square.Size;
            }
            else if (shape is Triangle triangle)
            {
                newP1 = triangle.P1;
                newP2 = triangle.P2;
                newP3 = triangle.P3;
            }

            form.RefreshCanvas();
        }

        public void Undo()
        {
            shape.DrawColor = oldDrawColor;
            shape.FillColor = oldFillColor;

            if (shape is Rectangle rect)
            {
                rect.Width = oldWidth;
                rect.Height = oldHeight;
                rect.SetPosition(oldX, oldY);
            }
            else if (shape is Circle circle)
            {
                circle.Radius = oldRadius;
                circle.SetPosition(oldX, oldY);
            }
            else if (shape is Square square)
            {
                square.Size = oldSize;
                square.SetPosition(oldX, oldY);
            }
            else if (shape is Triangle triangle)
            {
                triangle.SetVertices(oldP1, oldP2, oldP3);
            }

            form.RefreshCanvas();
        }

        public void Redo()
        {
            shape.DrawColor = newDrawColor;
            shape.FillColor = newFillColor;

            if (shape is Rectangle rect)
            {
                rect.Width = newWidth;
                rect.Height = newHeight;
                rect.SetPosition(newX, newY);
            }
            else if (shape is Circle circle)
            {
                circle.Radius = newRadius;
                circle.SetPosition(newX, newY);
            }
            else if (shape is Square square)
            {
                square.Size = newSize;
                square.SetPosition(newX, newY);
            }
            else if (shape is Triangle triangle)
            {
                triangle.SetVertices(newP1, newP2, newP3);
            }

            form.RefreshCanvas();
        }
    }
}
