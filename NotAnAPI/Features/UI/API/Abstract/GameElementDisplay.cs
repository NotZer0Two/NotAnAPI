using Exiled.API.Features;
using NorthwoodLib.Pools;
using NotAnAPI.Features.UI.API.Elements;
using NotAnAPI.Features.UI.API.Enums;
using NotAnAPI.Features.UI.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NotAnAPI.Features.UI.API.Abstract
{
    /// <summary>
    /// The core and power of the UI
    /// </summary>
    public abstract class GameElementDisplay : GameDisplayBuilder, IElements
    {
        private readonly StringBuilder _builder;
        public abstract List<Element> Elements { get; set; }
 
        public GameElementDisplay(StringBuilder builder) : base(builder)
        {
            _builder = builder;
        }

        ~GameElementDisplay() => StringBuilderPool.Shared.Return(_builder);

        public void Renderer(StringBuilder builder, UIScreenZone zone, UIType type = UIType.Alive)
        {
            foreach (Element elements in Elements)
            {
                if (elements.UI != type) continue;
                if (elements.Zone != zone) continue;

                builder.Append(elements.OnRender(_player).Replace("PFP", PFP));
            }

        }

        public override void CustomUIAlive(StringBuilder builder, UIScreenZone zone)
        {
            Renderer(builder, zone);
        }

        public override void CustomUISpectator(StringBuilder builder, UIScreenZone zone)
        {
            Renderer(builder, zone, UIType.Spectator);
        }
    }
}
