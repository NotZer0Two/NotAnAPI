using NotAnAPI.Features.Base;
using NotAnAPI.Features.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.Features.UI
{
    public class UIFeature : APIFeatures
    {

        public static UIFeature Instance { get; set; }

        public static string Name { get; set; } = "UI";
        public override bool IsEnabled { get; set; } = NotAnAPIMain.Instance.Config.EnabledFeatures[Name];

        public override void OnEnable()
        {

            NotALogger.Info("UI Loaded");
            Instance = this;

            base.OnEnable();
        }

        public override void OnDisable()
        {
            NotALogger.Info("UI Unloaded");

            Instance = null;
            IsEnabled = false;
            base.OnDisable();
        }
    }
}
