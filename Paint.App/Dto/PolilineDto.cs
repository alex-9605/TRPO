using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.App.Dto
{
    public class PolylineDto : ShapeDto
    {
        public PointDto[] Points { get; set; }
    }
}
