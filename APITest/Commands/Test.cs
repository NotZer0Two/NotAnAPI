using APITest.UI;
using APITest.UI.Elements;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Hints;
using InventorySystem.Items.Firearms;
using MEC;
using Mirror;
using NorthwoodLib.Pools;
using NotAnAPI.API.Commands;
using NotAnAPI.Features.UI;
using NotAnAPI.Features.UI.API.Abstract;
using RelativePositioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YamlDotNet.Core.Tokens;
using static EncryptedChannelManager;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

namespace APITest.Commands
{

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Test : NotCommands
    {
        public override string[] GetAliases() => [];

        public override string GetCommandName() => "test";

        public override string GetDescription() => "test.";

        public override string[] GetPerms() => null;

        public override bool GetRequirePlayer() => true;

        public override string[] GetUsage() => ["string"];

        public override bool PlayerBasedFunction(Player player, string[] args, out string result)
        {


            SceneMessage message = new()
            {
                sceneName = "Facility",
            };

            player.Connection.Send(message);

            player.SendConsoleMessage("Hello World", "red");

            result = $"PersonalElement Enabled";
            return false;
        }

    }
}
