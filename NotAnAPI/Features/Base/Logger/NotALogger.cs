using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.Features.Logger
{
    /// <summary>
    /// This class lets add support to the logger for both Exiled
    /// </summary>
    public static class NotALogger
    {

        public static Theme ConfigTheme = NotAnAPIMain.Instance.Config.Theme;

        public static void Info(string message) => ServerConsole.AddLog("[ Message ] " + message, ConfigTheme.InfoColor);

        public static void Error(string message) => ServerConsole.AddLog("[ ! ] " + message, ConfigTheme.ErrorColor);

        public static void Debug(string message) => ServerConsole.AddLog("[ # ] " + message, ConfigTheme.DebugColor);

        public static void Warning(string message) => ServerConsole.AddLog("[ /!\\ ] " + message, ConfigTheme.WarningColor);
    }
}
