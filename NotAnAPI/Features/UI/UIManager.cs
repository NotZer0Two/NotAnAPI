using Exiled.API.Features;
using Hints;
using NotAnAPI.API.Translator;
using NotAnAPI.Features.UI.API.Abstract;
using NotAnAPI.Features.UI.API.Enums;
using NotAnAPI.Features.UI.API.Notification;
using NotAnAPI.Features.UI.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace NotAnAPI.Features.UI
{
    /// <summary>
    /// Main Class and Manager for the entire UI Framework
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        private readonly Dictionary<UIScreenZone, float> _timers = new() { [UIScreenZone.Top] = -1, [UIScreenZone.CenterTop] = -1, [UIScreenZone.Center] = -1, [UIScreenZone.CenterBottom] = -1, [UIScreenZone.Bottom] = -1, [UIScreenZone.SubclassAlert] = -1, [UIScreenZone.InteractionMessage] = -1, [UIScreenZone.KillMessage] = -1 };
        private readonly Dictionary<UIScreenZone, string> _messages = new() { [UIScreenZone.Top] = string.Empty, [UIScreenZone.CenterTop] = string.Empty, [UIScreenZone.Center] = string.Empty, [UIScreenZone.CenterBottom] = string.Empty, [UIScreenZone.Bottom] = string.Empty, [UIScreenZone.SubclassAlert] = string.Empty, [UIScreenZone.InteractionMessage] = string.Empty, [UIScreenZone.KillMessage] = string.Empty };
        private DateTime _startTime;
        private float _counter;
        private Player _player;
        private string PFP;
        public GameDisplayBuilder _mainDisplay;
        public int GetSeconds => (int)(DateTime.Now - _startTime).TotalSeconds;
        private void Start()
        {
            if (UIFeature.Instance == null || !UIFeature.Instance.IsEnabled) 
            {
                Destroy(this);
                return;
            }

            _startTime = DateTime.Now;
            _player = Player.Get(gameObject);
            Task.Run(() =>
            {
                if (String.IsNullOrEmpty(PFP))
                {
                    PFP = SteamLoader.FetchPlayerIcon(_player.UserId.Split('@')[0], 50, 50).Result;
                    _mainDisplay.WithPFP(PFP);
                }
            });
            _mainDisplay.WithPlayer(_player);
        }
        private void OnDestroy() => _mainDisplay = null;
        private void Update()
        {
            _counter += Time.deltaTime;
            if (_counter < .5f) return;
            DrawUI();
            _counter = 0;
        }
        private async void DrawUI()
        {
            string msg = await Task.Run(() =>
            {
                try
                {
                    UpdateMessage();
                    UpdateNotifications();
                    if (_player.Role.Type == PlayerRoles.RoleTypeId.Overwatch || _player.Role.Type == PlayerRoles.RoleTypeId.Spectator) return _mainDisplay.BuildForSpectator();
                    return _mainDisplay.BuildForHuman();
                } catch(Exception ex)
                {
                    Log.Error(ex);
                    return string.Empty;
                }
            });

            _player.Connection.Send(new HintMessage(new TextHint(msg, new HintParameter[] { new StringHintParameter(string.Empty) }, new HintEffect[] { HintEffectPresets.TrailingPulseAlpha(1, 1, 1) }, 2)));
        }
        public void AddMessage(UIScreenZone zone, string message, float time = 10f)
        {
            if (zone == UIScreenZone.Notifications)
            {
                _notifications.Add(new UINotification(message));
                return;
            }
            _messages[zone] = message;
            _timers[zone] = time;
        }
        public void ClearZone(UIScreenZone zone)
        {
            if (zone == UIScreenZone.Notifications)
            {
                _notifications.Clear();
                return;
            }
            _messages[zone] = string.Empty;
            _timers[zone] = -1;
        }
        private void UpdateMessage()
        {
            string color = _player.Role.Color.ToHex();
            _mainDisplay.Clear();
            _mainDisplay.WithColor(color);
            for (int i = 0; i < _timers.Count; i++)
            {
                UIScreenZone zone = (UIScreenZone)i;
                if (_timers[zone] >= 0) _timers[zone] -= 0.5f;
                if (_timers[zone] < 0) _messages[zone] = string.Empty;

                string message = _messages[zone].TrimEnd('\n');

                if (string.IsNullOrEmpty(message)) message = '\n' + message;

                _mainDisplay.WithContent(zone, message);
            }
        }

        private readonly List<UINotification> _notifications = new();

        private void UpdateNotifications()
        {
            List<string> queue = new();

            for (int i = 0; i < (_notifications.Count > 5 ? 6 : _notifications.Count); i++)
            {
                queue.Add(_notifications[i].Message);
                _notifications[i].Duration -= 0.5f;

                if (_notifications[i].Duration <= 0) _notifications.Remove(_notifications[i]);
            }

            _mainDisplay.WithNotifications(queue);
        }
    }
}
