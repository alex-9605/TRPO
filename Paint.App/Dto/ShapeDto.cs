using System;
using System.Drawing;

namespace Paint.App.Dto
{
    public class ShapeDto
    {
        public Guid Id { get; set; }

        public int Width { get; set; }

        public Color Color { get; set; }

        public Color FillColor { get; set; }

        public LineTypeDto LineType { get; set; }
    }
}
