using Exiled.API.Interfaces;
using NotAnAPI.Features.Base;
using NotAnAPI.Features.Logger;
using NotAnAPI.Features.UI;
using System.Collections.Generic;
using System.ComponentModel;

namespace NotAnAPI
{
    public class NotAConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;


        [Description("Disable and Enable Features")]
        public Dictionary<string, bool> EnabledFeatures { get; set; } = new()
        {
            ["UI"] = true,

        };

        [Description("This sets the color of the Console")]
        public Theme Theme { get; set; } = new Theme();
    }
}
