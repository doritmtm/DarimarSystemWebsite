﻿using Microsoft.AspNetCore.Components;

namespace DarimarSystemWebsite.Framework.Components
{
    public partial class PreRenderingContent : DarimarSystemComponent
    {
        [CascadingParameter]
        public PageDefinitions? PageDefinitions { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (PageDefinitions != null)
            {
                PageDefinitions.PreRenderingContent = this;
            }
        }
    }
}
