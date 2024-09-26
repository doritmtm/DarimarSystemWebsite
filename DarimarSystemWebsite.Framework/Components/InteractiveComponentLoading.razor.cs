using DarimarSystemWebsite.Framework.Interfaces.Enums;
using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class InteractiveComponentLoading : DarimarSystemComponent
    {
        [Parameter]
        public InteractiveComponentLoadingTypeEnum LoadingType { get; set; } = InteractiveComponentLoadingTypeEnum.Skeleton;
    }
}
