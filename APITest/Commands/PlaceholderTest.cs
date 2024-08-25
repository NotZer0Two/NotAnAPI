using APITest.UI;
using APITest.UI.Elements;
using CommandSystem;
using Exiled.API.Features;
using MEC;
using NorthwoodLib.Pools;
using NotAnAPI.API.Commands;
using NotAnAPI.Features.PAPI.API;
using NotAnAPI.Features.UI;
using NotAnAPI.Features.UI.API.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITest.Commands
{

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class PlaceholderTest : NotCommands
    {
        public override string[] GetAliases() => [];

        public override string GetCommandName() => "placeholder";

        public override string GetDescription() => "Test placeholder.";

        public override string[] GetPerms() => null;

        public override bool GetRequirePlayer() => true;

        public override string[] GetUsage() => ["string"];

        public override bool PlayerBasedFunction(Player player, string[] args, out string result)
        {
            if (!TryGetArgument(args, 1, out string arg1))
            {
                result = "No Args!";
                return true;
            }

            result = $"{PlaceholderAPI.SetPlaceholders(player, $"Hello %player_{arg1}%")}";
            return false;
        }

    }
}
