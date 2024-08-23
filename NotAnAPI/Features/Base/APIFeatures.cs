using NotAnAPI.Features.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.Features.Base
{
    public abstract class APIFeatures
    {

        public abstract bool IsEnabled { get; set; }

        public virtual void OnEnable()
        {
            NotALogger.Info($"Feature {GetType().Name.Replace("Feature", string.Empty)} is enabled");
        }

        public virtual void OnDisable()
        {
            NotALogger.Info($"Feature {GetType().Name.Replace("Feature", string.Empty)} is disabled");
        }

    }
}
