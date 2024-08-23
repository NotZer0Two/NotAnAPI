using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.Features.Base
{
    public class APIFeaturesManager
    {

        public static List<Type> features = null;

        public static void EnableFeatures()
        {
            features = Assembly.GetExecutingAssembly()
                                                .GetTypes()
                                                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(APIFeatures)))
                                                .ToList();

            foreach (Type feature in features)
            {
                APIFeatures instance = (APIFeatures)Activator.CreateInstance(feature);

                if (!instance.IsEnabled) continue;

                instance.OnEnable();
            }
        }

        public static void DisableFeatures()
        {
            foreach(Type feature in features)
            {
                APIFeatures instance = (APIFeatures)Activator.CreateInstance(feature);

                if (!instance.IsEnabled) continue;

                instance.OnDisable();
            }
        }

    }
}
