﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Paint.Object
{
    public class Point : IShape
    {
        protected int x;
        protected int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw()
        {
        }
    }
}
