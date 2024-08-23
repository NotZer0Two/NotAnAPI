using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.Features.UI.API.Notification
{
    public class UINotification
    {
        public readonly string Message;
        public float Duration = 4f;

        public UINotification(string message) => Message = message;
    }
}
