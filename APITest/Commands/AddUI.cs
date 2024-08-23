using APITest.UI;
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

        public override bool Function(string[] args, ICommandSender sender, out string result)
        {
            Player player = Player.Get(sender);

            PersonalDisplayBuilder display = new PersonalDisplayBuilder(StringBuilderPool.Shared.Rent());

            player.GameObject.AddComponent<UIManager>()._mainDisplay = display;
            result = $"Working.";
            return false;
        }

    }
}
