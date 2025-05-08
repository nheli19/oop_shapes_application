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
    public partial class TriangleProperties : Form
    {
        private Triangle _triangle;

        public TriangleProperties(Triangle triangle)
        {
            InitializeComponent();
            _triangle = triangle;

            LoadTriangleProperties();
        }
        private void LoadTriangleProperties()
        {
            txtX1.Text = _triangle.P1.X.ToString();
            txtY1.Text = _triangle.P1.Y.ToString();

            txtX2.Text = _triangle.P2.X.ToString();
            txtY2.Text = _triangle.P2.Y.ToString();

            txtX3.Text = _triangle.P3.X.ToString();
            txtY3.Text = _triangle.P3.Y.ToString();

            txtDimension1.Text = _triangle.SideA.ToString("F2");
            txtDimension2.Text = _triangle.SideB.ToString("F2");
            txtDimension3.Text = _triangle.SideC.ToString("F2");


            btnDrawColor.BackColor = _triangle.DrawColor;
            btnFillColor.BackColor = _triangle.FillColor;

            lblAreaRes.Text = $"{_triangle.Area():F2}";
            lblPerimeterRes.Text = $"{_triangle.Perimeter():F2}";
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                Point vertex1 = new Point(
                    int.Parse(txtX1.Text),
                    int.Parse(txtY1.Text)
                );

                Point vertex2 = new Point(
                    int.Parse(txtX2.Text),
                    int.Parse(txtY2.Text)
                );

                Point vertex3 = new Point(
                    int.Parse(txtX3.Text),
                    int.Parse(txtY3.Text)
                );

                _triangle.SetVertices(vertex1, vertex2, vertex3);

                _triangle.DrawColor = btnDrawColor.BackColor;
                _triangle.FillColor = btnFillColor.BackColor;

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
