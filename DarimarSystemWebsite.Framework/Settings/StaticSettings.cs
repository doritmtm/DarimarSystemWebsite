using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection;

namespace DarimarSystemWebsite.Framework.Settings
{
    public static class StaticSettings
    {
        public static IComponentRenderMode GlobalRenderMode = RenderMode.InteractiveAuto;

        public static Assembly AppAssembly { get; set; } = typeof(StaticSettings).Assembly;

        public static IEnumerable<Assembly> AdditionalAssemblies { get; set; } = [];
    }
}
