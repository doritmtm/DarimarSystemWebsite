using DarimarSystemWebsite.Framework.Settings;
using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.RawHTML
{
    public static class RawHTML
    {
        public static MarkupString Head { get; set; } = (MarkupString)
        (
            @"
                <meta charset=""utf-8"" />
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                <base href=""/"" />
            "
            + IncludeStylesheet("https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap").Value
            + IncludeStylesheet("_content/MudBlazor/MudBlazor.min.css").Value
        );

        public static MarkupString IncludeStylesheet(string stylesheetLink)
        {
            return (MarkupString)$"<link rel=\"stylesheet\" href=\"{stylesheetLink}?vers={StaticSettings.Version}\" />";
        }

        public static MarkupString Scripts { get; set; } = (MarkupString)
        (
            IncludeScript("_framework/blazor.web.js").Value +
            IncludeScript("_content/MudBlazor/MudBlazor.min.js").Value +
            IncludeScript("_content/DarimarSystemWebsite.Framework/js/blazorCultureSwitch.js").Value
        );

        public static MarkupString IncludeScript(string scriptLink)
        {
            return (MarkupString)$"<script src=\"{scriptLink}?vers={StaticSettings.Version}\"></script>";
        }

        public static MarkupString IncludeIcon(string iconLink, string iconType)
        {
            return (MarkupString)$"<link rel=\"icon\" type=\"image/{iconType}\" href=\"{iconLink}?vers={StaticSettings.Version}\" />";
        }
    }
}
