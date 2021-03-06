﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Paint.Object
{
    public class Point : IShape
    {
        private readonly Graphics graphics;

        private Point(Graphics graphics, int x, int y)
        {
            this.graphics = graphics;
            this.X = x;
            this.Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public void Draw()
        {
        }
    }
}
