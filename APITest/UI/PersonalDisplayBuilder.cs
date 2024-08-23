using Exiled.API.Enums;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using NotAnAPI.Features.UI.API.Abstract;
using NotAnAPI.Features.UI.API.Enums;
using NotAnAPI.API.Extensions;
using System.Text;
using UnityEngine;
using System.Drawing;
using Color = System.Drawing.Color;

namespace APITest.UI
{
    public class PersonalDisplayBuilder : GameDisplayBuilder
    {
        private readonly StringBuilder _builder;

        private string ServerName = "MySCPSL Server";


        public PersonalDisplayBuilder(StringBuilder builder) : base(builder)
        {
            _builder = builder;
        }

        ~PersonalDisplayBuilder() => StringBuilderPool.Shared.Return(_builder);

        public override void CustomUIAlive(StringBuilder builder, UIScreenZone zone)
        {
            if (zone == UIScreenZone.Bottom)
            {
                StringBuilder sb = new StringBuilder()
                .SetSize(60, MeasurementUnit.Percentage)
                .SetAlignment(AlignStyle.Left);

                sb.SetIndent(-25, MeasurementUnit.Percentage)
                    .SetSize(1.5f, MeasurementUnit.Pixels)
                    .Append(PFP)
                    .CloseSize()
                    .CloseIndent();

                sb.SetIndent(-15, MeasurementUnit.Percentage)
                    .AddVOffset(15, MeasurementUnit.Ems)
                    .Append(ServerName)
                    .CloseVOffset()
                    .CloseIndent();

                builder.Append(sb.ToString());
            }
        }

        public override void CustomUISpectator(StringBuilder builder, UIScreenZone zone)
        {

        }
    }
}
