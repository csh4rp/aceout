using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Web.Localization
{
    public class LocalizationContainer
    {
        private readonly IDictionary<string, Dictionary<string, LocalizedString>> _localizations;

        public LocalizationContainer(Dictionary<string, Dictionary<string, LocalizedString>> localizations)
        {
            _localizations = localizations;
        }

        public LocalizedString GetString(string culture, string key)
        {
            if (!_localizations.ContainsKey(culture))
            {
                return new LocalizedString(key, key, true);
            }

            if (!_localizations[culture].ContainsKey(key))
            {
                return new LocalizedString(key, key, true);
            }

            return _localizations[culture][key];
        }

        public IEnumerable<LocalizedString> GetAllStrings(string culture)
        {
            return _localizations[culture].Values;
        }
    }
}
