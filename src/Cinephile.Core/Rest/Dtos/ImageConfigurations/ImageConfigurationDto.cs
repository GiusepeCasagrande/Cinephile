using System;
using System.Collections.Generic;

namespace Cinephile.Core.Rest.Dtos.ImageConfigurations
{
    public class ImageConfigurationDto
    {
        public ImagesDto images { get; set; }
        public IList<string> change_keys { get; set; }
    }
}