using APITest.UI.Elements;
using NorthwoodLib.Pools;
using NotAnAPI.Features.UI.API.Abstract;
using NotAnAPI.Features.UI.API.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITest.UI
{
    public class PersonalElementDisplay : GameElementDisplay
    {
        private readonly StringBuilder _builder;

        public PersonalElementDisplay(StringBuilder builder) : base(builder)
        {
            _builder = builder;
        }

        ~PersonalElementDisplay() => StringBuilderPool.Shared.Return(_builder);

        public override List<Element> Elements { get; set; } = new()
        {
            //Alive Elements
            new HelloWorldElement(),

            //Both
            new ProfilePictureElement(),
        };
    }
}
