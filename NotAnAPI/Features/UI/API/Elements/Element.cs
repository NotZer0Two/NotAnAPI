using NotAnAPI.Features.UI.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

namespace NotAnAPI.Features.UI.API.Elements
{
    public abstract class Element
    {

        public abstract string Name { get; set; }

        public abstract string Text { get; set; }

        public abstract Vector2 Position { get; set; }

        public abstract float Size { get; set; }

        public abstract UIScreenZone Zone { get; set; }

        public abstract UIType UI { get; set; }

        public virtual string OnRender()
        {
            StringBuilder sb = new StringBuilder();
            var lineList = Text.Split('\n');
            float yOffset = 0;

            foreach (var line in lineList)
            {
                float xCoordinate = Position.x;
                float yCoordinate = GetVOffset(Zone) - yOffset;

                if (xCoordinate != 0) sb.Append($"<pos={xCoordinate:0.#}>");
                sb.Append("<Line-height=0>");
                if (yCoordinate != 0) sb.Append($"<voffset={yCoordinate:0.#}>");
                sb.Append($"<size={Size}>");

                sb.Append(line);

                sb.Append("</size>");
                if (yCoordinate != 0) sb.Append("</voffset>");

                sb.Append('\n');

                yOffset += Size;
            }

            return sb.ToString();
        }

        //Thanks to HintServiceMeow for the screen calculation
        //Credits to them
        public float GetVOffset(UIScreenZone align)
        {
            float sizeOffset;

            switch (align)
            {
                case UIScreenZone.Top:
                    sizeOffset = -GetTextHeight();
                    break;
                case UIScreenZone.Center:
                    sizeOffset = -GetTextHeight() / 2;
                    break;
                case UIScreenZone.Bottom:
                    sizeOffset = 0;
                    break;
                default:
                    sizeOffset = 0;
                    break;
            }

            return 700 - Position.y + sizeOffset;
        }

        public float GetTextHeight()
        {
            if (string.IsNullOrEmpty(Text))
                return 0;

            var height = Text.Split('\n').Length;

            return height;
        }
    }
}
