using APITest.UI;
using APITest.UI.Elements;
using CommandSystem;
using Exiled.API.Features;
using MEC;
using NorthwoodLib.Pools;
using NotAnAPI.API.Commands;
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
    public class AddUI : NotCommands
    {
        public override string[] GetAliases() => [];

        public override string GetCommandName() => "addUi";

        public override string GetDescription() => "Adds the UI.";

        public override string[] GetPerms() => null;

        public override bool GetRequirePlayer() => true;

        public override string[] GetUsage() => ["string"];

        public override bool PlayerBasedFunction(Player player, string[] args, out string result)
        {
            if (!TryGetArgument(args, 1, out string arg1))
            {
                PersonalDisplayBuilder display1 = new PersonalDisplayBuilder(StringBuilderPool.Shared.Rent());

                player.GameObject.AddComponent<UIManager>()._mainDisplay = display1;

                result = "PersonalDisplay enabled!";
                return true;
            }

            PersonalElementDisplay display = new PersonalElementDisplay(StringBuilderPool.Shared.Rent());

            player.GameObject.AddComponent<UIManager>()._mainDisplay = display;

            result = $"PersonalElement Enabled";
            return false;
        }

    }
}
