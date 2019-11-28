using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Configuration
{
    public class LanguageSettings
    {
        public IEnumerable<Language> Languages { get; set; }
        public string FilesPath { get; set; }
    }
}
