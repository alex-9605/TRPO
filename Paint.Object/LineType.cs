using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Object
{
    public enum LineType
    {
        //
        // Сводка:
        //     Задает сплошную линию.
        Solid = 0,
        //
        // Сводка:
        //     Задает строку, состоящую из штрихов.
        Dash = 1,
        //
        // Сводка:
        //     Задает строку, состоящую из точек.
        Dot = 2,
        //
        // Сводка:
        //     Задает линию, состоящую из повторяющегося шаблона Штрихпунктирная.
        DashDot = 3,
        //
        // Сводка:
        //     Задает линию, состоящую из повторяющегося шаблона штрих точка точка.
        DashDotDot = 4,
        //
        // Сводка:
        //     Задает определяемые пользователем пользовательских пунктирных линий.
        Custom = 5
    };
}
