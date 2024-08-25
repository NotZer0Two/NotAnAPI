using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.Features.PAPI.API.Abstract
{
    public abstract class PlaceholderExpansion
    {

        public abstract string Author { get; set; }
        public abstract string Identifier { get; set; }

        public abstract string onRequest(Player player, string param);

        public void Register()
        {
            PAPIFeature.Instance.Placeholders.Add(Identifier, this);
        }

        public void Unregister()
        {
            PAPIFeature.Instance.Placeholders.Remove(Identifier);
        }
    }
}
