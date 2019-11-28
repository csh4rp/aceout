using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Configuration
{
    public class AppConfiguration
    {
        public CacheSettings Cache { get; set; }
        public string ImageCacheDirectory { get; set; }
        public AuthenticationSettings Authentication { get; set; }
        public LanguageSettings Language { get; set; }
        public DatabaseSettings Database { get; set; }
        public ImageSettings Images { get; set; }
        public EmailSettings Email { get; set; }

    }
}
