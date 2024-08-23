using Exiled.API.Interfaces;

namespace APITest
{
    public class APITestConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
