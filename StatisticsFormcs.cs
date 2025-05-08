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
    public partial class StatisticsForm : Form
    {
        private List<Shape> shapes;

        public StatisticsForm()
        {
            InitializeComponent();
        }

        public void SetShapes(List<Shape> shapesList)
        {
            shapes = shapesList;
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            if (shapes == null || shapes.Count == 0)
            {
                lblMCSRes.Text = "No shapes found";
                lblLargestShapeRes.Text = "No shapes found";
                lblAverageAreaRes.Text = "0";
                lblShapesByTypeRes.Text = "No shapes found";
                return;
            }

            lblMCSRes.Text = GetMostCommonShape();
            lblLargestShapeRes.Text = GetLargestShape();
            lblAverageAreaRes.Text = GetAverageArea().ToString("F2");
            lblShapesByTypeRes.Text = CountShapesByType();
        }

        private string GetMostCommonShape()
        {
            return shapes
                .GroupBy(s => s.GetType().Name)
                .OrderByDescending(g => g.Count())
                .Select(g => $"{g.Key} (Count: {g.Count()})")
                .FirstOrDefault() ?? "No shapes found";
        }

        private string GetLargestShape()
        {
            var largest = shapes.OrderByDescending(s => s.Area()).FirstOrDefault();
            if (largest == null) 
                return "No shapes found";

            return $"{largest.GetType().Name} at ({largest.X}, {largest.Y}) with area {largest.Area():F2}";
        }

        private double GetAverageArea()
        {
            return shapes.Count > 0 ? shapes.Average(s => s.Area()) : 0;
        }

        private string CountShapesByType()
        {
            var counts = shapes
                .GroupBy(s => s.GetType().Name)
                .Select(g => $"{g.Key}: {g.Count()}");

            return string.Join(", ", counts);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
