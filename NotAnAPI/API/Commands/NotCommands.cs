using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using ICommand = CommandSystem.ICommand;

namespace NotAnAPI.API.Commands
{

    /// <summary>
    /// Base system for NotAnAPI Commands
    /// </summary>
    public abstract class NotCommands : ICommand, IUsageProvider
    {
        public string Command => GetCommandName();

        public string[] Aliases => GetAliases();

        public string Description => GetDescription();

        public string[] Usage => GetUsage();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!CanExecute(sender, out response))
                return false;

            return HandleCommand(arguments.Array, sender, out response);
        }

        /// <summary>
        /// Base function checks for permissions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual bool CanExecute(ICommandSender sender, out string message)
        {

            if (GetPerms() != null)
                foreach (string perm in GetPerms())
                {
                    if (!Player.Get(sender).CheckPermission(perm))
                    {
                        message = NotAnAPIMain.Instance.Translation.NoPermission.Replace("%perm%", perm);
                        return false;
                    }
                }

            message = "";

            return true;
        }

        public bool HandleCommand(string[] args, ICommandSender sender, out string result)
        {
            if (GetRequirePlayer())
            {
                if (Player.TryGet(sender, out Player player))
                    return PlayerBasedFunction(player, args, out result);
                else
                {
                    result = NotAnAPIMain.Instance.Translation.CommandFailed;

                    return false;
                }
            }

            return Function(args, sender, out result);
        }

        /// <summary>
        /// Standard execution method, will not execute when GetRequirePlayer() returns true.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="sender"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public virtual bool Function(string[] args, ICommandSender sender, out string result)
        {
            result = "";

            return true;
        }

        /// <summary>
        /// Execution that uses a Player, automatically handles cases where the player is unable to be fetched. Will not execute when GetRequirePlayer() returns false.
        /// </summary>
        /// <param name="player"></param>
        public virtual bool PlayerBasedFunction(Player player, string[] args, out string result) { result = ""; return true; }

        public virtual bool GetRequirePlayer() => false;

        /// <summary>
        /// Tries to get the argument based on an index.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryGetArgument(string[] args, int index, out string result)
        {
            if (args.Length <= index)
            {
                result = "";
                return false;
            }
            else
            {
                result = args[index];
                return true;
            }
        }

        public abstract string GetCommandName();
        public abstract string GetDescription();
        public abstract string[] GetAliases();
        public abstract string[] GetPerms();

        public abstract string[] GetUsage();
    }
}
