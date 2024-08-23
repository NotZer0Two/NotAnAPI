using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.Features.Logger
{
    public class Theme
    {

        public ConsoleColor InfoColor { get; set; } = ConsoleColor.Cyan;

        public ConsoleColor ErrorColor { get; set; } = ConsoleColor.Red;

        public ConsoleColor DebugColor { get; set; } = ConsoleColor.Green;

        public ConsoleColor WarningColor { get; set; } = ConsoleColor.Yellow;
    }
}
