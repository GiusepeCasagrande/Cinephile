using System;
using System.Collections.Generic;

namespace Cinephile.Core.Rest.Dtos.ImageConfigurations
{
    public class ImagesDto
    {
        public string base_url { get; set; }
        public string secure_base_url { get; set; }
        public IList<string> backdrop_sizes { get; set; }
        public IList<string> logo_sizes { get; set; }
        public IList<string> poster_sizes { get; set; }
        public IList<string> profile_sizes { get; set; }
        public IList<string> still_sizes { get; set; }
    }
}
