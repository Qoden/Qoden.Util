using System;
using System.Reflection;
using Qoden.Validation;
using DeviceInfo = Plugin.DeviceInfo.CrossDeviceInfo;

namespace Qoden.Util
{
    public class Plugin 
    {
        public static T Load<T>(string prefix, string name)
        {
            Assert.Argument(prefix, nameof(prefix)).NotEmpty();
            Assert.Argument(name, nameof(name)).NotEmpty();

            var deviceInfo = DeviceInfo.Current;
            var assemblyName = new AssemblyName($"{prefix}.{deviceInfo.Platform}");
            var assembly = Assembly.Load(assemblyName);
            var t = assembly.GetType($"{prefix}.{deviceInfo.Platform}.{name}", true, true);
            return (T)Activator.CreateInstance(t);
        }
    }
}
