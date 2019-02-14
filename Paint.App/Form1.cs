using Paint.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = Paint.Object.Point;

namespace Paint.App
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private List<IShape> shapes;
        private Pen pen;
        private ToolType toolType;
        private Point startPoint;
        private Polyline polyline;
        private Polygon polygon;
        private IShape selectedShape;

        public Form1()
        {
            InitializeComponent();
            this.shapes = new List<IShape>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pen = new System.Drawing.Pen(Color.Blue, 2F);
            this.graphics = this.pictureBox1.CreateGraphics();
        }
        
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (this.toolType)
            {
                case ToolType.Line:
                    {
                        var line = new Line(this.graphics, this.startPoint, new Point(this.graphics, e.X, e.Y), 1,
                            Color.Aqua, Color.Aqua, LineType.Solid);
                        this.shapes.Add(line);
                        line.Draw();
                        this.startPoint = null;
                    }
                    break;

                case ToolType.Ellipse:
                    {
                        var radiusA = (e.X - this.startPoint.X) / 2D;
                        var radiusB = (e.Y - this.startPoint.Y) / 2D;
                        var centre = new Point(this.graphics, this.startPoint.X + (int)radiusA, this.startPoint.Y + (int)radiusB);
                        var ellipse = new Ellipse(this.graphics, centre, Math.Abs(radiusA), Math.Abs(radiusB), 1, Color.Aqua, Color.Aqua, LineType.Solid);
                        this.shapes.Add(ellipse);
                        ellipse.Draw();
                        this.startPoint = null;
                    }
                    break;


                case ToolType.Polyline:
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            this.polyline.AddPoint(new Point(this.graphics, e.X, e.Y));
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            this.shapes.Add(this.polyline);
                            this.polyline.Draw();
                            this.polyline = new Polyline(this.graphics, 1, Color.Aqua, Color.Aqua,LineType.Solid);
                            this.startPoint = null;
                        }
                    }
                    break;

                case ToolType.Polygon:
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            this.polygon.AddPoint(new Point(this.graphics, e.X, e.Y));
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            this.shapes.Add(this.polygon);
                            this.polygon.Draw();
                            this.polygon = new Polygon(this.graphics, 1,Color.Aqua, Color.Aqua, LineType.Solid);
                            this.startPoint = null;
                        }
                    }
                    break;

                case ToolType.Circle:
                    {
                        var centre2 = new Point(this.graphics, this.startPoint.X, this.startPoint.Y);
                        var radius = (e.X - this.startPoint.X) / 2;
                        var circle = new Circle(this.graphics, centre2, radius*2, 1, Color.Aqua, Color.Aqua, LineType.Solid);
                        this.shapes.Add(circle);
                        circle.Draw();
                        this.startPoint = null;
                        break;
                    }

                case ToolType.Delete:
                    {
                        var point = new Point(this.graphics, e.X, e.Y);
                        var deleted = this.shapes.LastOrDefault(p => p.IsInBounds(point));
                        if (deleted != null)
                        {
                            this.shapes.Remove(deleted);
                            this.Draw();
                        }
                    }
                    break;

                case ToolType.Selection:
                    {
                        var point = new Point(this.graphics, e.X, e.Y);
                        var shape = this.shapes.LastOrDefault(p => p.IsInBounds(point));
                        shape?.Select();
                    }
                    break;

                case ToolType.Copy:
                    if (this.selectedShape == null)
                    {
                        var point = new Point(this.graphics, e.X, e.Y);
                        this.selectedShape = this.shapes.LastOrDefault(p => p.IsInBounds(point));
                    }
                    else
                    {
                        //switch (this.toolType)
                        //{
                        //    case ToolType.Delete:
                                
                        //        break;
                        //}
                    }
                    break;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.startPoint = new Point(this.graphics, e.X, e.Y);
        }
        
        private void Line_button1_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Line;
        }

        private void Ellipse_button2_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Ellipse;
        }

        private void Polyline_button3_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Polyline;
            this.polyline = new Polyline(this.graphics, 1, Color.Aqua, Color.Aqua, LineType.Solid);
        }

        private void Circle_button4_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Circle;
        }

        private void Polygon_button5_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Polygon;
            this.polygon = new Polygon(this.graphics,1,Color.Aqua,Color.Aqua,LineType.Solid);
        }

        private void Clear_button6_Click(object sender, EventArgs e)
        {
            this.graphics.Clear(Color.White);
            Refresh();
            this.shapes.Clear();
            this.selectedShape = null;
            this.startPoint = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Copy;
            this.selectedShape = null;
        }

        private void Draw()
        {
            this.graphics.Clear(Color.White);
            Refresh();

            foreach (var shape in this.shapes)
            {
                shape.Draw();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Delete;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Selection;
        }
    }
}
