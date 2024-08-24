using Exiled.API.Features;
using NotAnAPI.API.Extensions;
using NotAnAPI.Features.UI.API.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public abstract TextSettings Settings { get; set; }

        public abstract UIScreenZone Zone { get; set; }

        public abstract UIType UI { get; set; }

        public virtual string OnRender(Player player) => OnRender();
        public virtual string OnRender()
        {
            StringBuilder sb = new StringBuilder();
            var lineList = Text.Split('\n');
            float yOffset = 0;

            foreach (var line in lineList)
            {
                //Basic
                float xCoordinate = Position.x;
                float yCoordinate = GetVOffset(Zone) - yOffset;

                if (xCoordinate != 0) sb.AddHorizontalPos(xCoordinate);
                if (Settings.LineHeight >= 0) sb.SetLineHeight(Settings.LineHeight);
                if (yCoordinate != 0) sb.AddVOffset(yCoordinate);
                if (Settings.Size > 0) sb.SetSize(Settings.Size);
                //Other options

                sb.Append(line);

                //Close Basic Settings
                if (Settings.Size > 0) sb.CloseSize();
                if (yCoordinate != 0) sb.CloseVOffset();

                sb.Append("\n");
                yOffset += Settings.Size;
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
                case UIScreenZone.CenterTop:
                    sizeOffset = -GetTextHeight();
                    break;
                case UIScreenZone.Center:
                    sizeOffset = -GetTextHeight() / 2;
                    break;
                case UIScreenZone.CenterBottom:
                    sizeOffset = -GetTextHeight();
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
