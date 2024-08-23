using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.Features.UI.API.Enums
{
    public enum UIScreenZone
    {
        Notifications = -1,
        //Center
        Top,
        CenterTop,
        Center,
        CenterBottom,
        Bottom,
        //MISC
        InteractionMessage,
        KillMessage,
        SubclassAlert,
    }
}
