using Paint.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paint.App.ChangeManager;
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
        private ChangeManager.ChangeManager changeManager;

        public Form1()
        {
            InitializeComponent();
            this.changeManager = new ChangeManager.ChangeManager();
            this.shapes = new List<IShape>();
            TakeColor();
            TakeWidth();
        }

        void TakeColor()
        {
            comboBox1.Items.Insert(0, Color.Black);
            comboBox1.Items.Insert(1, Color.Coral);
            comboBox1.Items.Insert(2, Color.Blue);
            comboBox1.Items.Insert(3, Color.Chartreuse);
            comboBox1.Items.Insert(4, Color.Fuchsia);
            comboBox1.Items.Insert(5, Color.Gold);
            if (comboBox1.Items.Count != 0)
                comboBox1.SelectedIndex = 0;
        }

        void TakeWidth()
        {
            comboBox2.Items.Insert(0, Width=1);
            comboBox2.Items.Insert(1,Width=2);
            comboBox2.Items.Insert(2, Width=3);
            if (comboBox2.Items.Count != 0)
                comboBox2.SelectedIndex = 0;
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
                        var line = new Line(this.graphics, this.startPoint, new Point(this.graphics, e.X, e.Y), (int)comboBox2.Items[comboBox2.SelectedIndex],
                            (Color)comboBox1.Items[comboBox1.SelectedIndex], Color.Aqua, LineType.Solid);

                        this.changeManager.SaveChange(new AddShapeInfo(line, this.shapes));

                        this.shapes.Add(line);
                        line.Draw();
                        this.startPoint = null;
                    }
                    break;

                case ToolType.Ellipse:
                    {
                        var xMin = this.startPoint.X < e.X ? this.startPoint.X : e.X;
                        var yMin = this.startPoint.Y < e.Y ? this.startPoint.Y : e.Y;
                        var top = new Point(this.graphics, xMin, yMin);
                        
                        var ellipse = new Ellipse(this.graphics, top, Math.Abs(e.X - this.startPoint.X), Math.Abs(e.Y - this.startPoint.Y)
                            , 15, (Color)comboBox1.Items[comboBox1.SelectedIndex], Color.Aqua, LineType.Solid);

                        this.changeManager.SaveChange(new AddShapeInfo(ellipse, this.shapes));

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
                            this.changeManager.SaveChange(new AddShapeInfo(this.polyline, this.shapes));
                            this.shapes.Add(this.polyline);
                            this.polyline.Draw();
                            this.polyline = new Polyline(this.graphics, 1, (Color)comboBox1.Items[comboBox1.SelectedIndex], Color.Aqua,LineType.Solid);

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
                            this.changeManager.SaveChange(new AddShapeInfo(this.polygon, this.shapes));
                            this.shapes.Add(this.polygon);
                            this.polygon.Draw();
                            this.polygon = new Polygon(this.graphics, 1, (Color)comboBox1.Items[comboBox1.SelectedIndex], Color.Aqua, LineType.Solid);
                            this.startPoint = null;
                        }
                    }
                    break;

                case ToolType.Circle:
                    {
                        var xMin = this.startPoint.X < e.X ? this.startPoint.X : e.X;
                        var yMin = this.startPoint.Y < e.Y ? this.startPoint.Y : e.Y;

                        var circle = new Circle(this.graphics, new Point(this.graphics, xMin, yMin), Math.Abs(e.Y - this.startPoint.Y), 1, (Color)comboBox1.Items[comboBox1.SelectedIndex], Color.Aqua, LineType.Solid);

                        this.changeManager.SaveChange(new AddShapeInfo(circle, this.shapes));

                        this.shapes.Add(circle);
                        circle.Draw();
                        this.startPoint = null;
                    }
                    break;

                case ToolType.Delete:
                    {
                        var point = new Point(this.graphics, e.X, e.Y);
                        var deleted = this.shapes.LastOrDefault(p => p.IsInBounds(point));
                        if (deleted != null)
                        {
                            this.changeManager.SaveChange(new DeleteChangeInfo(deleted, this.shapes));
                            this.shapes.Remove(deleted);
                            this.Draw();
                        }
                    }
                    break;

                case ToolType.Selection:
                    {
                        var point = new Point(this.graphics, e.X, e.Y);

                        if (this.selectedShape != null)
                        {
                            if (this.selectedShape.IsInMarkers(this.startPoint))
                            {
                                switch (this.selectedShape)
                                {
                                    case Ellipse ellipse:
                                        ellipse.Change(this.startPoint, point);
                                        break;

                                    case Line line:
                                        line.Change(this.startPoint, point);
                                        break;

                                    case Circle circle:
                                        circle.Change(this.startPoint, point);
                                        break;

                                    case Polyline pline:
                                        pline.Change(this.startPoint, point);
                                        break;
                                }

                                this.selectedShape = null;
                                this.graphics.Clear(Color.White);
                                Refresh();

                                foreach (var shape in this.shapes)
                                {
                                    shape.Draw();
                                }

                                return;
                            }
                        }

                        
                        var foundShape = this.shapes.LastOrDefault(p => p.IsInBounds(point));
                        if (foundShape != null)
                        {
                            foundShape.Select();
                            this.selectedShape = foundShape;
                        }
                        else
                        {
                            this.selectedShape = null;
                            this.graphics.Clear(Color.White);
                            Refresh();

                            foreach (var shape in this.shapes)
                            {
                                shape.Draw();
                            }
                        }
                    }
                    break;

                case ToolType.Copy:
                case ToolType.Cut:
                    if (this.selectedShape == null)
                    {
                        var point = new Point(this.graphics, e.X, e.Y);
                        this.selectedShape = this.shapes.LastOrDefault(p => p.IsInBounds(point));
                    }
                    else
                    {
                        switch (this.toolType)
                        {
                            case ToolType.Copy:
                                {
                                    var copiedShape = this.selectedShape.Copy(new Point(this.graphics, e.X, e.Y));
                                    this.changeManager.SaveChange(new CopyShapeChangeInfo(copiedShape, this.shapes));
                                    this.shapes.Add(copiedShape);
                                    copiedShape.Draw();
                                }
                                break;

                            case ToolType.Cut:
                                {
                                    var copiedShape = this.selectedShape.Copy(new Point(this.graphics, e.X, e.Y));
                                    this.shapes.Add(copiedShape);

                                    this.changeManager.SaveChange(new CutShapeInfo(this.selectedShape, this.shapes, this.shapes.IndexOf(this.selectedShape), this.shapes.IndexOf(copiedShape) - 1));
                                    
                                    this.shapes.Remove(this.selectedShape);
                                    this.graphics.Clear(Color.White);
                                    Refresh();

                                    foreach (var shape in this.shapes)
                                    {
                                        shape.Draw();
                                    }
                                }
                                break;
                        }
                        
                        this.selectedShape = null;
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
            this.polyline = new Polyline(this.graphics, 1, (Color)comboBox1.Items[comboBox1.SelectedIndex], Color.Aqua, LineType.Solid);
        }

        private void Circle_button4_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Circle;
        }

        private void Polygon_button5_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Polygon;
            this.polygon = new Polygon(this.graphics,1, (Color)comboBox1.Items[comboBox1.SelectedIndex], Color.Aqua,LineType.Solid);
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

        private void button10_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Cut;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.changeManager.Undo();
            this.Draw();
        }
    }
}
