using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI
{
    public class NotATranslation : ITranslation
    {

        public string NoPermission { get; set; } = "You don't have the permission required.\nRequires the permission: %perm%";

        public string CommandFailed { get; set; } = "Command Failed";

    }
}
