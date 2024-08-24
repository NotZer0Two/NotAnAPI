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
    public class HelloWorldElement : Element
    {
        public override string Name { get; set; } = "Hello World";
        public override string Text { get; set; } = "Hello World";
        public override Vector2 Position { get; set; } = new Vector2(-500, 300);
        public override float Size { get; set; } = 60;
        public override UIScreenZone Zone { get; set; } = UIScreenZone.Center;
        public override UIType UI { get; set; } = UIType.Alive;

        public override string OnRender()
        {
            Position = new Vector2(UnityEngine.Random.Range(0, 301), UnityEngine.Random.Range(0, 301));

            return base.OnRender();
        }
    }
}
