using Exiled.API.Features;
using NotAnAPI.Features.PAPI.API.Abstract;

namespace NotAnAPI.Features.PAPI.impl
{
    public class PlayerPlaceholder : PlaceholderExpansion
    {
        public override string Author { get; set; } = "NotZer0Two";
        public override string Identifier { get; set; } = "player";

        public override string onRequest(Player player, string param)
        {
            switch(param.ToLower())
            {
                case "name":
                    return player.Nickname;
                case "displayname":
                    return player.DisplayNickname;
                case "ip":
                    return player.IPAddress;
                case "dnt":
                    return player.DoNotTrack ? "Enabled" : "Disabled";
                case "health":
                    return player.Health.ToString();
                case "maxhealth":
                    return player.MaxHealth.ToString();
                case "ping":
                    return player.Ping.ToString();
                case "id":
                    return player.RawUserId;
                case "fullid":
                    return player.UserId;
                case "x":
                    return player.Position.x.ToString();
                case "y":
                    return player.Position.y.ToString();
                case "z":
                    return player.Position.z.ToString();
            }

            return null;
        }
    }
}
