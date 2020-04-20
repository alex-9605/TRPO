using Paint.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Paint.App.ChangeManager;
using Paint.App.Dto;
using Point = Paint.Object.Point;
using Paint.App.Dto.ChangeInfoDto;

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
            this.changeManager = new ChangeManager.ChangeManager(this.listBox1.Items);
            this.shapes = new List<IShape>();
            TakeColor();
            TakeFillColor();
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
            if (cbxLineType.Items.Count > 0)
                cbxLineType.SelectedIndex = 0;
        }

        void TakeFillColor()
        {
            comboBox2.Items.Insert(0, Color.White);
            comboBox2.Items.Insert(1, Color.Coral);
            comboBox2.Items.Insert(2, Color.Blue);
            comboBox2.Items.Insert(3, Color.Chartreuse);
            comboBox2.Items.Insert(4, Color.Fuchsia);
            comboBox2.Items.Insert(5, Color.Gold);
            if (comboBox2.Items.Count != 0)
                comboBox2.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pen = new System.Drawing.Pen(Color.Blue, 2F);

            var bitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.pictureBox1.Image = bitmap;
            this.graphics = Graphics.FromImage(this.pictureBox1.Image);
            this.pictureBox1.Invalidate();
        }
        
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (this.toolType)
            {
                case ToolType.Line:
                    {
                        var line = new Line(null, this.graphics, this.startPoint, new Point(null, this.graphics, e.X, e.Y),  (int)this.lineWidthSpinBtn.Value,
                            (Color)comboBox1.Items[comboBox1.SelectedIndex], Color.Aqua, LineType.Solid);

                        this.changeManager.SaveChange(new AddShapeInfo(line, this.shapes));

                        this.shapes.Add(line);
                        line.Draw(new Pen((Color)comboBox1.Items[comboBox1.SelectedIndex], (int)this.lineWidthSpinBtn.Value)
                        {
                            DashStyle = (DashStyle)this.cbxLineType.SelectedIndex
                        });
                        this.startPoint = null;
                    }
                    break;

                case ToolType.Ellipse:
                    {
                        var xMin = this.startPoint.X < e.X ? this.startPoint.X : e.X;
                        var yMin = this.startPoint.Y < e.Y ? this.startPoint.Y : e.Y;
                        var top = new Point(null, this.graphics, xMin, yMin);
                        
                        var ellipse = new Ellipse(null, this.graphics, top, Math.Abs(e.X - this.startPoint.X), Math.Abs(e.Y - this.startPoint.Y)
                            , (int)this.lineWidthSpinBtn.Value, (Color)comboBox1.Items[comboBox1.SelectedIndex], (Color)comboBox2.Items[comboBox2.SelectedIndex], LineType.Solid);

                        this.changeManager.SaveChange(new AddShapeInfo(ellipse, this.shapes));

                        this.shapes.Add(ellipse);
                        ellipse.Draw(new Pen((Color)comboBox1.Items[comboBox1.SelectedIndex], (int)this.lineWidthSpinBtn.Value)
                        {
                            DashStyle = (DashStyle)this.cbxLineType.SelectedIndex
                        });
                        this.startPoint = null;
                    }
                    break;


                case ToolType.Polyline:
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            this.polyline.AddPoint(new Point(null, this.graphics, e.X, e.Y));
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            this.changeManager.SaveChange(new AddShapeInfo(this.polyline, this.shapes));
                            this.shapes.Add(this.polyline);
                            this.polyline.Draw(new Pen((Color)comboBox1.Items[comboBox1.SelectedIndex], (int)this.lineWidthSpinBtn.Value)
                            {
                                DashStyle = (DashStyle)this.cbxLineType.SelectedIndex
                            });
                            this.polyline = new Polyline(null, this.graphics, (int)this.lineWidthSpinBtn.Value, (Color)comboBox1.Items[comboBox1.SelectedIndex], Color.Aqua,LineType.Solid);

                            this.startPoint = null;
                        }
                    }
                    break;

                case ToolType.Polygon:
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            this.polygon.AddPoint(new Point(null, this.graphics, e.X, e.Y));
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            this.changeManager.SaveChange(new AddShapeInfo(this.polygon, this.shapes));
                            this.shapes.Add(this.polygon);
                            this.polygon.Draw(new Pen((Color)comboBox1.Items[comboBox1.SelectedIndex], (int)this.lineWidthSpinBtn.Value)
                            {
                                DashStyle = (DashStyle)this.cbxLineType.SelectedIndex
                            });
                            this.polygon = new Polygon(null, this.graphics, (int)this.lineWidthSpinBtn.Value, (Color)comboBox1.Items[comboBox1.SelectedIndex], (Color)comboBox2.Items[comboBox2.SelectedIndex], LineType.Solid);
                            this.startPoint = null;
                        }
                    }
                    break;

                case ToolType.Circle:
                    {
                        var xMin = this.startPoint.X < e.X ? this.startPoint.X : e.X;
                        var yMin = this.startPoint.Y < e.Y ? this.startPoint.Y : e.Y;

                        var circle = new Circle(null, this.graphics, new Point(null, this.graphics, xMin, yMin), Math.Abs(e.Y - this.startPoint.Y), (int)this.lineWidthSpinBtn.Value, (Color)comboBox1.Items[comboBox1.SelectedIndex], (Color)comboBox2.Items[comboBox2.SelectedIndex], LineType.Solid);

                        this.changeManager.SaveChange(new AddShapeInfo(circle, this.shapes));

                        this.shapes.Add(circle);
                        circle.Draw(new Pen((Color)comboBox1.Items[comboBox1.SelectedIndex], (int)this.lineWidthSpinBtn.Value)
                        {
                            DashStyle = (DashStyle)this.cbxLineType.SelectedIndex
                        });
                        this.startPoint = null;
                    }
                    break;

                case ToolType.Delete:
                    {
                        var point = new Point(null, this.graphics, e.X, e.Y);
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
                        var point = new Point(null, this.graphics, e.X, e.Y);

                        if (this.selectedShape != null)
                        {
                            if (this.selectedShape.IsInMarkers(this.startPoint))
                            {
                                switch (this.selectedShape)
                                {
                                    case Ellipse ellipse:
                                        ellipse.Change(this.startPoint, point);
                                        this.changeManager.SaveChange(new ShapeChangeInfo(ellipse, this.shapes, this.startPoint, point));
                                        break;

                                    case Line line:
                                        line.Change(this.startPoint, point);
                                        this.changeManager.SaveChange(new ShapeChangeInfo(line, this.shapes, this.startPoint, point));
                                        break;

                                    case Circle circle:
                                        circle.Change(this.startPoint, point);
                                        this.changeManager.SaveChange(new ShapeChangeInfo(circle, this.shapes, this.startPoint, point));
                                        break;

                                    case Polyline pline:
                                        pline.Change(this.startPoint, point);
                                        this.changeManager.SaveChange(new ShapeChangeInfo(pline, this.shapes, this.startPoint, point));
                                        break;
                                }

                                this.selectedShape = null;
                                this.graphics.Clear(SystemColors.ButtonFace);
                                Refresh();

                                foreach (var shape in this.shapes)
                                {
                                    shape.Draw(new Pen((Color)comboBox1.Items[comboBox1.SelectedIndex], (int)this.lineWidthSpinBtn.Value)
                                    {
                                        DashStyle = (DashStyle)this.cbxLineType.SelectedIndex
                                    });
                                }

                                break;
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
                            this.graphics.Clear(SystemColors.ButtonFace);
                            Refresh();

                            foreach (var shape in this.shapes)
                            {
                                shape.Draw(new Pen((Color)comboBox1.Items[comboBox1.SelectedIndex], (int)this.lineWidthSpinBtn.Value)
                                {
                                    DashStyle = (DashStyle)this.cbxLineType.SelectedIndex
                                });
                            }
                        }
                    }
                    break;

                case ToolType.Copy:
                case ToolType.Cut:
                    if (this.selectedShape == null)
                    {
                        var point = new Point(null, this.graphics, e.X, e.Y);
                        this.selectedShape = this.shapes.LastOrDefault(p => p.IsInBounds(point));
                    }
                    else
                    {
                        switch (this.toolType)
                        {
                            case ToolType.Copy:
                                {
                                    var copiedShape = this.selectedShape.Copy(new Point(null, this.graphics, e.X, e.Y));
                                    this.changeManager.SaveChange(new CopyShapeChangeInfo(copiedShape, this.shapes));
                                    this.shapes.Add(copiedShape);
                                    copiedShape.Draw(new Pen((Color)comboBox1.Items[comboBox1.SelectedIndex], (int)this.lineWidthSpinBtn.Value)
                                    {
                                        DashStyle = (DashStyle)this.cbxLineType.SelectedIndex
                                    });
                                }
                                break;

                            case ToolType.Cut:
                                {
                                    var copiedShape = this.selectedShape.Copy(new Point(null, this.graphics, e.X, e.Y));
                                    this.shapes.Add(copiedShape);

                                    this.changeManager.SaveChange(new CutShapeInfo(this.selectedShape, copiedShape, this.shapes, this.shapes.IndexOf(this.selectedShape), this.shapes.IndexOf(copiedShape) - 1));
                                    
                                    this.shapes.Remove(this.selectedShape);
                                    this.graphics.Clear(SystemColors.ButtonFace);
                                    Refresh();

                                    foreach (var shape in this.shapes)
                                    {
                                        shape.Draw(new Pen((Color)comboBox1.Items[comboBox1.SelectedIndex], (int)this.lineWidthSpinBtn.Value)
                                        {
                                            DashStyle = (DashStyle)this.cbxLineType.SelectedIndex
                                        });
                                    }
                                }
                                break;
                        }
                        
                        this.selectedShape = null;
                    }
                    break;
            }
            this.pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.startPoint = new Point(null, this.graphics, e.X, e.Y);
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
            this.polyline = new Polyline(null, this.graphics, (int)this.lineWidthSpinBtn.Value, (Color)comboBox1.Items[comboBox1.SelectedIndex], (Color)comboBox2.Items[comboBox2.SelectedIndex], LineType.Solid);
        }

        private void Circle_button4_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Circle;
        }

        private void Polygon_button5_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Polygon;
            this.polygon = new Polygon(null, this.graphics, (int)this.lineWidthSpinBtn.Value, (Color)comboBox1.Items[comboBox1.SelectedIndex], (Color)comboBox2.Items[comboBox2.SelectedIndex], LineType.Solid);
        }

        private void Clear_button6_Click(object sender, EventArgs e)
        {
            this.graphics.Clear(default(Color));
            Refresh();
            this.shapes.Clear();
            this.selectedShape = null;
            this.startPoint = null;
            this.changeManager.Clear();
            this.pictureBox1.Invalidate();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Copy;
            this.selectedShape = null;
        }

        private void Draw()
        {
            this.graphics.Clear(default(Color));
            Refresh();

            foreach (var shape in this.shapes)
            {
                shape.Draw(new Pen((Color)comboBox1.Items[comboBox1.SelectedIndex], (int)this.lineWidthSpinBtn.Value)
                {
                    DashStyle = (DashStyle)this.cbxLineType.SelectedIndex
                });
            }
            this.pictureBox1.Invalidate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Delete;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Selection;
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            this.toolType = ToolType.Cut;
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            this.changeManager.Undo();
            this.Draw();
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            this.changeManager.Redo();
            this.Draw();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;

            
            var dtos = AutomapperConfig.Mapper.DefaultContext.Mapper.Map<BaseChangeInfoDto[]>(changeManager.UndoItems);
            //AutomapperConfig.Mapper.DefaultContext.Mapper.Map<ShapeDto[]>(AutomapperConfig.Mapper.DefaultContext.Mapper.Map<BaseChangeInfoDto[]>(changeManager.UndoItems));

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            var result = JsonConvert.SerializeObject(dtos, Formatting.Indented, settings);


            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, result);

        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            string fileText = System.IO.File.ReadAllText(filename);

            this.shapes.Clear();

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };


            var newDtos = JsonConvert.DeserializeObject<BaseChangeInfoDto[]>(fileText, settings);

            this.graphics.Clear(SystemColors.ButtonFace);

            var again = AutomapperConfig.Mapper.DefaultContext.Mapper.Map<ChangeInfo[]>(newDtos, opt =>
            {
                opt.Items["graphic"] = this.graphics;
                opt.Items["commonList"] = this.shapes;
            });

            
            this.changeManager = new ChangeManager.ChangeManager(this.listBox1.Items, again);

            this.Draw();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.shapes.Any())
                return;

            var result = MessageBox.Show("Хотите сохранить данные?", "Сохранение", MessageBoxButtons.YesNoCancel);
            switch (result)
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;

                case DialogResult.Yes:
                    this.saveButton_Click(sender, null);
                    break;
            }
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            var saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files(*.bmp)|*.bmp";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            
            this.pictureBox1.Invalidate();
            this.pictureBox1.Image.Save(filename);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.L:
                    this.toolType = ToolType.Line;
                    break;

                case Keys.E:
                    this.toolType = ToolType.Ellipse;
                    break;

                case Keys.D0:
                    this.toolType = ToolType.Circle;
                    break;

                case Keys.P:
                    this.Polygon_button5_Click(null, null);
                    break;

                case Keys.D1:
                    this.Polyline_button3_Click(null, null);
                    break;

                case Keys.C:
                    this.toolType = ToolType.Copy;
                    break;

                case Keys.X:
                    this.toolType = ToolType.Cut;
                    break;

                case Keys.D:
                    this.toolType = ToolType.Delete;
                    break;

                case Keys.A:
                    this.Clear_button6_Click(null, null);
                    break;

                case Keys.Z:
                    this.undoButton_Click(null, null);
                    break;

                case Keys.R:
                    this.redoButton_Click(null, null);
                    break;
            }
        }

        private void lineWidthSpinBtn_ValueChanged(object sender, EventArgs e)
        {
            this.pen = new System.Drawing.Pen((Color)comboBox1.Items[comboBox1.SelectedIndex], (float)this.lineWidthSpinBtn.Value);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
