using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace proekt_formichki_klasove
{
    public partial class MainForm : Form
    {
        private List<Shape> shapes = new List<Shape>();
        private Shape selectedShape = null;
        private Point lastMousePosition;
        private bool isDragging = false;
        private bool isResizing = false;
        private int resizeHandle = -1;
        private CommandManager commandManager = new CommandManager();

        private Point dragStartPosition;

        private int nextShapeX = 100;
        private int nextShapeY = 100;
        private const int ShapeSpacing = 50;

        public Shape SelectedShape
        {
            get => selectedShape;
            set => selectedShape = value;
        }

        public MainForm()
        {
            InitializeComponent();

        }

        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (Shape shape in shapes)
            {
                shape.Draw(g);
            }

            if (selectedShape != null)
            {
                selectedShape.DrawSelection(g);
                selectedShape.DrawResizeHandles(g);
            }
        }
        private void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastMousePosition = e.Location;
            dragStartPosition = e.Location;

            if (selectedShape != null)
            {
                resizeHandle = selectedShape.GetResizeHandleUnderMouse(e.Location);
                if (resizeHandle >= 0)
                {
                    isResizing = true;
                    return;
                }
            }

            selectedShape = GetShapeAt(e.Location);
            if (selectedShape != null)
            {
                isDragging = true;
                dragStartPosition = new Point(selectedShape.X, selectedShape.Y);
            }

            canvasPictureBox.Invalidate();
        }
        private void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {

            if (isDragging && selectedShape != null)
            {
                int deltaX = e.X - lastMousePosition.X;
                int deltaY = e.Y - lastMousePosition.Y;

                selectedShape.MoveBy(deltaX, deltaY);

                lastMousePosition = e.Location;
                canvasPictureBox.Invalidate();
            }
            else if (isResizing && selectedShape != null)
            {
                selectedShape.Resize(resizeHandle, e.Location, lastMousePosition);
                lastMousePosition = e.Location;
                canvasPictureBox.Invalidate();
            }
        }
        private void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging && selectedShape != null)
            {
                int totalDeltaX = selectedShape.X - dragStartPosition.X;
                int totalDeltaY = selectedShape.Y - dragStartPosition.Y;

                if (totalDeltaX != 0 || totalDeltaY != 0)
                {
                    selectedShape.SetPosition(dragStartPosition.X, dragStartPosition.Y);
                    var moveCommand = new MoveShapeCommand(this, selectedShape, totalDeltaX, totalDeltaY);
                    commandManager.ExecuteCommand(moveCommand);
                }
            }
            else if (isResizing && selectedShape != null)
            {
                var resizeCommand = new ResizeShapeCommand(this, selectedShape, resizeHandle, e.Location, dragStartPosition);
                if (selectedShape is Rectangle rect)
                {
                    rect.Resize(resizeHandle, dragStartPosition, e.Location);   // undo the last resize operation
                }
                commandManager.ExecuteCommand(resizeCommand);
            }

            isDragging = false;
            isResizing = false;
            resizeHandle = -1;
        }
        private Shape GetShapeAt(Point point)
        {
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                if (shapes[i].Contains(point))
                {
                    return shapes[i];
                }
            }
            return null;
        }
        private void btnAddRectangle_Click(object sender, EventArgs e)
        {
            var rect = new Rectangle(nextShapeX, nextShapeY, 100, 60);
            AddShape(rect);
        }

        private void btnAddCircle_Click(object sender, EventArgs e)
        {
            var circle = new Circle(nextShapeX, nextShapeY, 40);
            AddShape(circle);
        }

        private void btnAddSquare_Click(object sender, EventArgs e)
        {
            var square = new Square(nextShapeX, nextShapeY, 80);
            AddShape(square);
        }

        private void btnAddTriangle_Click(object sender, EventArgs e)
        {
            var triangle = new Triangle(nextShapeX, nextShapeY);
            AddShape(triangle);
        }
        private void AddShape(Shape shape)
        {
            var command = new AddShapeCommand(this, shape, shapes);
            commandManager.ExecuteCommand(command);

            nextShapeX += ShapeSpacing;

            if (nextShapeX > canvasPictureBox.Width - 60)    // go to next line
            {
                nextShapeX = 100;
                nextShapeY += ShapeSpacing;
            }

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                var command = new DeleteShapeCommand(this, selectedShape, shapes);
                commandManager.ExecuteCommand(command);
                selectedShape = null;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (shapes.Count > 0)
            {
                var command = new ClearAllCommand(this, shapes);
                commandManager.ExecuteCommand(command);
                selectedShape = null;
            }
        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                var command = new ChangePropertiesCommand(this, selectedShape); // with original state

                if (selectedShape is Triangle triangle)
                {
                    var form = new TriangleProperties(triangle);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        commandManager.ExecuteCommand(command);
                    }
                }
                else
                {
                    var form = new RectSqrtCircleProperties(selectedShape);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        commandManager.ExecuteCommand(command);
                    }
                }
            }
        }
        public void Undo()
        {
            if (commandManager.canUndo)
            {
                commandManager.Undo();
            }
        }

        public void Redo()
        {
            if (commandManager.canRedo)
            {
                commandManager.Redo();
            }
        }
        private void btnUndo_Click(object sender, EventArgs e)
        {
            Undo();
            canvasPictureBox.Invalidate();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            Redo();
            canvasPictureBox.Invalidate();
        }

        private void New()
        {
            if (shapes.Count > 0)
            {
                var result = MessageBox.Show("Do you want to save your current work before creating a new canva?",
                    "New Canvas", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                    return;

                if (result == DialogResult.Yes)
                    Save();
            }

            shapes.Clear();
            selectedShape = null;
            commandManager.Clear();
            nextShapeX = 100;
            nextShapeY = 100;
            UpdateShapesCount();
            canvasPictureBox.Invalidate();
        }
        private void Save()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Shape Files (*.shapes)|*.shapes|All Files (*.*)|*.*",
                DefaultExt = "shapes"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveToFile(saveFileDialog.FileName);
            }
        }

        private void Load()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Shape Files (*.shapes)|*.shapes|All Files (*.*)|*.*",
                DefaultExt = "shapes"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadFromFile(openFileDialog.FileName);
            }
        }
        public void RefreshCanvas()
        {
            canvasPictureBox.Invalidate();
        }

        public void UpdateShapesCount()
        {
            lblShapes.Text = $"Shapes: {shapes.Count}";
        }

        private void SaveToFile(string filename)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new ShapeConverter() }
                };

                string jsonString = JsonSerializer.Serialize(shapes, options);
                File.WriteAllText(filename, jsonString);

                MessageBox.Show("File saved successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFromFile(string filename)
        {
            try
            {
                string jsonString = File.ReadAllText(filename);

                var options = new JsonSerializerOptions
                {
                    Converters = { new ShapeConverter() }
                };

                var loadedShapes = JsonSerializer.Deserialize<List<Shape>>(jsonString, options);

                if (loadedShapes != null)
                {
                    shapes.Clear();
                    shapes.AddRange(loadedShapes);
                    selectedShape = null;
                    commandManager.Clear();
                    UpdateShapesCount();
                    canvasPictureBox.Invalidate();

                    MessageBox.Show("File loaded successfully!", "Load", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void ShowStatisticsForm()
        {
            var statsForm = new StatisticsForm();
            statsForm.SetShapes(shapes);
            statsForm.ShowDialog(this);
        }

        private void statistiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowStatisticsForm();
        }
    }
}
