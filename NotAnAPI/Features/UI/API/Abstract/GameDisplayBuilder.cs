using Exiled.API.Features;
using NorthwoodLib.Pools;
using NotAnAPI.Features.UI.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NotAnAPI.Features.UI.API.Abstract
{
    /// <summary>
    /// The core and power of the UI
    /// </summary>
    public abstract class GameDisplayBuilder
    {
        public virtual string PinnedMessage { get; set; } = string.Empty;

        public virtual int Offset { get; set; } = 14;

        private readonly StringBuilder _builder;

        public GameDisplayBuilder(StringBuilder builder) => _builder = builder;

        ~GameDisplayBuilder() => StringBuilderPool.Shared.Return(_builder);

        private readonly Dictionary<UIScreenZone, string> _saved = new();
        private List<string> _notifications = new();
        private string _color = "#ffffff";
        public Player _player;
        public string PFP;

        public void Clear()
        {
            _saved.Clear();
            _notifications.Clear();
        }

        public void WithContent(UIScreenZone zone, string content)
        {
            if (_saved.ContainsKey(zone))
            {
                _saved[zone] = content;
                return;
            }

            _saved.Add(zone, content);
        }

        public void WithNotifications(List<string> notifications) => _notifications = notifications;
        public void WithColor(string color) => _color = color;
        public void WithPlayer(Player player) => _player = player;
        public void WithPFP(string pfp) => PFP = pfp;

        public abstract void CustomUIAlive(StringBuilder builder, UIScreenZone zone);
        public abstract void CustomUISpectator(StringBuilder builder, UIScreenZone zone);

        public string BuildForHuman()
        {
            _builder.Clear();
            _builder.Append($"<size=60%><line-height=100%><voffset={Offset}em>");

            CustomUIAlive(_builder.Append(RenderZone(UIScreenZone.Top)), UIScreenZone.Top);

            _builder.Append("\n\n\n");

            CustomUIAlive(_builder.Append(RenderZone(UIScreenZone.CenterTop)), UIScreenZone.CenterTop);

            int i = 0;

            for (; i < _notifications.Count; i++)
                _builder.AppendLine(_notifications[i]);

            CustomUIAlive(_builder.Append(RenderZone(UIScreenZone.Center)), UIScreenZone.Center);

            CustomUIAlive(_builder.Append(RenderZone(UIScreenZone.CenterBottom)), UIScreenZone.CenterBottom);

            CustomUIAlive(_builder.Append(FormatStringForHud(GetZone(UIScreenZone.InteractionMessage), 1)), UIScreenZone.InteractionMessage);
            CustomUIAlive(_builder.Append(FormatStringForHud(GetZone(UIScreenZone.KillMessage), 1)), UIScreenZone.KillMessage);

            _builder.AppendLine(PinnedMessage);

            _builder.AppendLine();

            CustomUIAlive(_builder.Append(RenderZone(UIScreenZone.Bottom)), UIScreenZone.Bottom);

            return _builder.ToString();
        }


        public string BuildForSpectator()
        {
            _builder.Clear();
            _builder.Append($"<size=60%><line-height=100%><voffset={Offset}em>");

            CustomUISpectator(_builder.Append(RenderZone(UIScreenZone.Top)), UIScreenZone.Top);

            _builder.Append("\n\n\n");

            CustomUISpectator(_builder.Append(RenderZone(UIScreenZone.CenterTop)), UIScreenZone.CenterTop);

            int i = 0;

            for (; i < _notifications.Count; i++)
                _builder.AppendLine(_notifications[i]);

            CustomUISpectator(_builder.Append(RenderZone(UIScreenZone.Center)), UIScreenZone.Center);

            CustomUISpectator(_builder.Append(RenderZone(UIScreenZone.CenterBottom)), UIScreenZone.CenterBottom);

            CustomUISpectator(_builder.Append(FormatStringForHud(GetZone(UIScreenZone.InteractionMessage), 1)), UIScreenZone.InteractionMessage);
            CustomUISpectator(_builder.Append(FormatStringForHud(GetZone(UIScreenZone.KillMessage), 1)), UIScreenZone.KillMessage);

            _builder.AppendLine(PinnedMessage);

            _builder.AppendLine();

            CustomUISpectator(_builder.Append(RenderZone(UIScreenZone.Bottom)), UIScreenZone.Bottom);

            return _builder.ToString();
        }


        private string GetZone(UIScreenZone zone) => _saved.ContainsKey(zone) ? _saved[zone] : string.Empty;

        private string RenderZone(UIScreenZone zone) => FormatStringForHud(GetZone(zone));

        private static string FormatStringForHud(string text, int linesNeeded = 5)
        {
            int textLines = text.Count(x => x == '\n');

            for (int i = 0; i < linesNeeded - textLines; i++)
                text += '\n';

            return text;
        }
    }
}
