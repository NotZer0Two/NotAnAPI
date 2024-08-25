using Discord;
using Exiled.API.Features;
using NotAnAPI.API.Extensions;
using NotAnAPI.Features.PAPI.API.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine.Windows;

namespace NotAnAPI.Features.PAPI.API
{
    public static class PlaceholderAPI
    {

        private const string pattern = @"%(?<identifier>[a-zA-Z0-9]+)_(?<params>[^%]+)%";

        private static readonly Regex regex = new Regex(pattern);

        public static string SetPlaceholders(Player player, string text)
        {
            return regex.Replace(text, match =>
            {
                string identifier = match.Groups["identifier"].Value;
                string parameters = match.Groups["params"].Value;

                if (PAPIFeature.Instance.Placeholders.TryGetValue(identifier, out PlaceholderExpansion replacement))
                {
                    string final = replacement.onRequest(player, parameters);

                    return final == null ? "NaN" : final;
                }
                else
                {
                    return match.Value;
                }
            });
        }

    }
}
