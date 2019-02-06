using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint.App
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private Pen pen;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pen = new System.Drawing.Pen(Color.Blue, 2F);
            this.graphics = this.pictureBox1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.graphics.DrawEllipse(this.pen, 300, 250, 100, 50);
        }
    }
}
