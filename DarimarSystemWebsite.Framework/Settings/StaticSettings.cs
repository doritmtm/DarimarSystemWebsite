using DarimarSystemWebsite.Framework.Interfaces.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection;

namespace DarimarSystemWebsite.Framework.Settings
{
    public static class StaticSettings
    {
        public static IComponentRenderMode GlobalRenderMode { get; set; } = RenderMode.InteractiveAuto;

        public static Assembly AppAssembly { get; set; } = typeof(StaticSettings).Assembly;

        public static IEnumerable<Assembly> AdditionalAssemblies { get; set; } = [];

        public static Type? ResourcesClass { get; set; }

        public static HashSet<LanguageEnum> SupportedLanguages { get; set; } = [];

        public static string Version { get; set; } = "0.0.0";
    }
}
