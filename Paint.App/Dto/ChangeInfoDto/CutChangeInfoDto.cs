using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.App.Dto.ChangeInfoDto
{
    public class CutChangeInfoDto : BaseChangeInfoDto
    {
        public int OldIndex { get; set; }

        public int NewIndex { get; set; }

        public ShapeDto OldShape { get; set; }
    }
}
