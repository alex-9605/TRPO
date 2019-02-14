﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    public class Polygon : Polyline
    {
        public Polygon(Graphics graphics, int width, Color color, Color fillColor, LineType type) 
            : base(graphics, width, color, fillColor, type)
        {
        }

        public override void Draw()
        {
            base.Draw();

            var lastPoint = this.points.Last();
            var firstPoint = this.points.First();

            this.graphics.DrawLine(this.pen, new System.Drawing.Point(lastPoint.X, lastPoint.Y), 
                new System.Drawing.Point(firstPoint.X, firstPoint.Y));
        }
    }
}
