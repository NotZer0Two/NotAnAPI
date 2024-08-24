using NotAnAPI.Features.UI.API.Elements;
using NotAnAPI.Features.UI.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace APITest.UI.Elements
{
    public class ProfilePictureElement : Element
    {
        public override string Name { get; set; } = "Profile Picture";
        public override string Text { get; set; } = "PFP";
        public override Vector2 Position { get; set; } = new Vector2(-900, 300);

        public override TextSettings Settings { get; set; } = new()
        {
            Size = 1.5f,
        };

        public override UIScreenZone Zone { get; set; } = UIScreenZone.Center;
        public override UIType UI { get; set; } = UIType.Both;

        public override string OnRender()
        {
            Position = new Vector2(UnityEngine.Random.Range(0, 501), UnityEngine.Random.Range(0, 501));

            return base.OnRender();
        }
    }
}
