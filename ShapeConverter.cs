using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace proekt_formichki_klasove
{
    public class ShapeConverter : JsonConverter<Shape>
    {
        public override Shape Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                JsonElement root = doc.RootElement;

                string type = root.GetProperty("ShapeType").GetString();

                int x = root.GetProperty("X").GetInt32();
                int y = root.GetProperty("Y").GetInt32();

                string drawColorStr = root.GetProperty("DrawColor").GetString();
                string fillColorStr = root.GetProperty("FillColor").GetString();

                Color drawColor = ColorTranslator.FromHtml(drawColorStr);
                Color fillColor = ColorTranslator.FromHtml(fillColorStr);

                Shape shape = null;

                switch (type)
                {
                    case "Rectangle":
                        int width = root.GetProperty("Width").GetInt32();
                        int height = root.GetProperty("Height").GetInt32();
                        shape = new Rectangle(x, y, width, height);
                        break;

                    case "Circle":
                        int radius = root.GetProperty("Radius").GetInt32();
                        shape = new Circle(x, y, radius);
                        break;

                    case "Square":
                        int size = root.GetProperty("Size").GetInt32();
                        shape = new Square(x, y, size);
                        break;

                    case "Triangle":

                        shape = new Triangle(x, y);

                        if (root.TryGetProperty("Vertices", out JsonElement verticesElement))
                        {
                            if (verticesElement.GetArrayLength() == 3)
                            {
                                int x1 = verticesElement[0].GetProperty("X").GetInt32();
                                int y1 = verticesElement[0].GetProperty("Y").GetInt32();
                                int x2 = verticesElement[1].GetProperty("X").GetInt32();
                                int y2 = verticesElement[1].GetProperty("Y").GetInt32();
                                int x3 = verticesElement[2].GetProperty("X").GetInt32();
                                int y3 = verticesElement[2].GetProperty("Y").GetInt32();

                                Triangle triangle = (Triangle)shape;
                                triangle.SetVertices( new Point(x1, y1), new Point(x2, y2), new Point(x3, y3) );
                            }
                        }
                        break;
                }

                if (shape != null)
                {
                    shape.DrawColor = drawColor;
                    shape.FillColor = fillColor;
                }

                return shape;
            }
        }

        public override void Write(Utf8JsonWriter writer, Shape value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("ShapeType", value.GetType().Name);
            writer.WriteNumber("X", value.X); 
            writer.WriteNumber("Y", value.Y);
            writer.WriteString("DrawColor", ColorTranslator.ToHtml(value.DrawColor));
            writer.WriteString("FillColor", ColorTranslator.ToHtml(value.FillColor));

            if (value is Square square)
            {
                writer.WriteNumber("Size", square.Size);
            }
            else if (value is Rectangle rect)
            {
                writer.WriteNumber("Width", rect.Width); 
                writer.WriteNumber("Height", rect.Height);
            }
            else if (value is Circle circle)
            {
                writer.WriteNumber("Radius", circle.Radius);
            }
            else if (value is Triangle triangle)
            {

                writer.WriteStartArray("Vertices");

                writer.WriteStartObject();
                writer.WriteNumber("X", triangle.P1.X);
                writer.WriteNumber("Y", triangle.P1.Y);
                writer.WriteEndObject();

                writer.WriteStartObject();
                writer.WriteNumber("X", triangle.P2.X);
                writer.WriteNumber("Y", triangle.P2.Y);
                writer.WriteEndObject();

                writer.WriteStartObject();
                writer.WriteNumber("X", triangle.P3.X);
                writer.WriteNumber("Y", triangle.P3.Y);
                writer.WriteEndObject();

                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }
    }
}
