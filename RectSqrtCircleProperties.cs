using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proekt_formichki_klasove
{
    public partial class RectSqrtCircleProperties : Form
    {
        private Shape _shape;

        public RectSqrtCircleProperties(Shape shape)
        {
            InitializeComponent();
            _shape = shape;

            LoadShapeProperties();
        }

        private void LoadShapeProperties()
        {
            txtX.Text = _shape.X.ToString();
            txtY.Text = _shape.Y.ToString();

            if (_shape is Rectangle rect && !(_shape is Square sq))
            {
                txtDimension1.Text = rect.Width.ToString();
                txtDimension2.Text = rect.Height.ToString();

                lblDimension1.Text = "Width:";
                lblDimension2.Text = "Height:";
                lblDimension2.Visible = true;
                txtDimension2.Visible = true;
            }
            else if (_shape is Square square)
            {
                txtDimension1.Text = square.Size.ToString();
                lblDimension1.Text = "Size:";
                lblDimension2.Visible = false;
                txtDimension2.Visible = false;
            }
            else if (_shape is Circle circle)
            {
                txtDimension1.Text = circle.Radius.ToString();
                lblDimension1.Text = "Radius:";
                lblDimension2.Visible = false;
                txtDimension2.Visible = false;
            }

            btnDrawColor.BackColor = _shape.DrawColor;
            btnFillColor.BackColor = _shape.FillColor;

            lblAreaRes.Text = $"{_shape.Area():F2}";

            if (_shape is Circle cr)
            {
                lblPerimeter.Text = "Circumference: ";
            }
            else
            {
                lblPerimeter.Text = "Perimeter: ";
            }
            lblPerimeterRes.Text = $"{_shape.Perimeter():F2}";
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                int x = int.Parse(txtX.Text);
                int y = int.Parse(txtY.Text);

                int deltaX = x - _shape.X;
                int deltaY = y - _shape.Y;
                _shape.MoveBy(deltaX, deltaY);

                if (_shape is Rectangle rect && !(_shape is Square sq))
                {
                    rect.Width = int.Parse(txtDimension1.Text);
                    rect.Height = int.Parse(txtDimension2.Text);
                }
                else if (_shape is Square square)
                {
                    square.Size = int.Parse(txtDimension1.Text);
                }
                else if (_shape is Circle circle)
                {
                    circle.Radius = int.Parse(txtDimension1.Text);
                }

                _shape.DrawColor = btnDrawColor.BackColor;
                _shape.FillColor = btnFillColor.BackColor;

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying properties: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void btnDrawColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = btnDrawColor.BackColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    btnDrawColor.BackColor = colorDialog.Color;
                }
            }
        }
        private void btnFillColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = btnFillColor.BackColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    btnFillColor.BackColor = colorDialog.Color;
                }
            }
        }
    }
}
