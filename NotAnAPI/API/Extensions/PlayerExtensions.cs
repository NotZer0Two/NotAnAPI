using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using NotAnAPI.Utils;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.API.Extensions
{
    public static class PlayerExtensions
    {
        public static SavedPlayer SavePlayer(this Player player)
        {
            SavedPlayer playerSaved = new SavedPlayer()
            {
                Health = player.Health,
                RelativePosition = player.RelativePosition,
                Items = player.Items.ToList(),
                Effects = player.ActiveEffects.Select(x => new Effect(x)).ToList(),
                Name = player.Nickname,
                Role = player.Role,
                Userid = player.UserId,
                CurrentRound = true,
                Ammo = player.Ammo.ToDictionary(x => x.Key.GetAmmoType(), x => x.Value),
            };

            player.Ammo.Clear();
            player.Inventory.SendAmmoNextFrame = true;

            player.ClearInventory(false);

            return playerSaved;
        }

        public static void SavedPlayerToPlayer(this Player player, SavedPlayer saved)
        {
            if (saved.CurrentRound)
            {
                player.Role.Set(saved.Role, RoleSpawnFlags.None);
                try
                {
                    player.ResetInventory(saved.Items);
                    player.Health = saved.Health;
                    player.Position = saved.RelativePosition.Position;

                    foreach (KeyValuePair<AmmoType, ushort> kvp in saved.Ammo)
                        player.Ammo[kvp.Key.GetItemType()] = kvp.Value;

                    player.SyncEffects(saved.Effects);

                    player.Inventory.SendItemsNextFrame = true;
                    player.Inventory.SendAmmoNextFrame = true;
                }
                catch (Exception) { }
            }
            else
            {
                player.Role.Set(RoleTypeId.Spectator);
            }

        }


    }
}
