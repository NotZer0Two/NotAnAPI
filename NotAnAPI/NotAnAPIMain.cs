using Exiled.API.Features;
using HarmonyLib;
using NotAnAPI.API.Translator;
using NotAnAPI.Features.Base;
using NotAnAPI.Features.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI
{
    public class NotAnAPIMain : Plugin<NotAConfig, NotATranslation>
    {

        public override string Author => "NotZer0Two";
        public override string Name => "NotAnAPI";

        public override Version Version => new Version(0, 0, 1);

        public static NotAnAPIMain Instance = null;

        public static Harmony harmony = null;

        public override void OnEnabled()
        {
            Instance = this;

            harmony = new Harmony($"net.{Author}.{Name}_{Version}");
            harmony.PatchAll();

            APIFeaturesManager.EnableFeatures();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {

            APIFeaturesManager.DisableFeatures();
            Instance = null;

            harmony.UnpatchAll();
            harmony = null;
            base.OnDisabled();
        }

    }
}
