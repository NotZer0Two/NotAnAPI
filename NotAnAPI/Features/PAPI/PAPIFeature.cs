using NotAnAPI.Features.Base;
using NotAnAPI.Features.Logger;
using NotAnAPI.Features.PAPI.API.Abstract;
using NotAnAPI.Features.PAPI.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.Features.PAPI
{
    public class PAPIFeature : APIFeatures
    {

        public static PAPIFeature Instance { get; set; }

        public static string Name { get; set; } = "UI";
        public override bool IsEnabled { get; set; } = true;

        public Dictionary<string, PlaceholderExpansion> Placeholders = new();

        public override void OnEnable()
        {
            NotALogger.Info("PAPI Loaded");
            Instance = this;

            new PlayerPlaceholder().Register();

            base.OnEnable();
        }

        public override void OnDisable()
        {
            NotALogger.Info("PAPI Unloaded");

            new PlayerPlaceholder().Unregister();

            Instance = null;
            IsEnabled = false;
            base.OnDisable();
        }
    }
}
