using Exiled.API.Features;
using HarmonyLib;
using Hints;
using NotAnAPI.API.Translator;
using NotAnAPI.Features.Logger;
using NotAnAPI.Features.UI.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.Features.UI.Patch
{
    [HarmonyPatch(typeof(HintDisplay), nameof(HintDisplay.Show))]
    public static class UIRedirect
    {

        public static bool Prefix(HintDisplay __instance, Hints.Hint hint)
        {
            try
            {
                if (UIFeature.Instance == null || !UIFeature.Instance.IsEnabled) return true;

                Type type = hint.GetType();

                if (type == typeof(TranslationHint))
                    return true;

                if (type == typeof(TextHint))
                {
                    TextHint t = hint as TextHint;
                    var ply = Player.Get(__instance.gameObject);

                    if (!ply.GameObject.TryGetComponent(out UIManager hud))
                    {
                        NotALogger.Warning($"Attempted to redirect for {ply.Nickname} but failed: HUD element missing.");
                        return true;
                    }

                    hud.AddMessage(UIScreenZone.Top, t.Text, t.DurationScalar);
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error($"Error when trying to redirect hint:\n{e}");
                return true;
            }

        }

    }
}
