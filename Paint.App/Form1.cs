using Paint.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Line;
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
                    var centre = new Point(this.graphics, this.startPoint.X + (e.X - this.startPoint.X) / 2,
                        this.startPoint.Y + (e.Y - this.startPoint.Y) / 2);
                    var radiusA = Math.Abs(e.X - this.startPoint.X) / 2;
                    var radiusB = Math.Abs(e.Y - this.startPoint.Y) / 2;
                    var ellipse = new Ellipse(this.graphics, centre, radiusA, radiusB, 1, Color.AliceBlue, Color.AliceBlue, LineType.Solid);
                    this.shapes.Add(ellipse);
                    ellipse.Draw();
                    this.startPoint = null;
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
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.startPoint = new Point(this.graphics, e.X, e.Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Ellipse;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Polyline;
            this.polyline = new Polyline(this.graphics, 1, Color.Aqua, Color.Aqua, LineType.Solid);
        }
    }
}
