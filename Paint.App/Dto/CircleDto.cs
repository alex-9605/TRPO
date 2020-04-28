using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.App.Dto
{
    public class CircleDto : ShapeDto
    {
        public PointDto Position { get; set; }

        public int RectWidth { get; set; }
    }
}
