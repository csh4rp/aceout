using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Aceout.Web.Localization
{
    public class FileStringLocalizer : IStringLocalizer
    {
        private readonly LocalizationContainer _localizationContainer;
        private readonly CultureInfo _cultureInfo;

        public LocalizedString this[string name] => _localizationContainer.GetString(_cultureInfo.Name, name);

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var localizedString = _localizationContainer.GetString(_cultureInfo.Name, name);
                return new LocalizedString(localizedString.Name, string.Format(localizedString.Value, arguments));
            }
        }

        public FileStringLocalizer(LocalizationContainer localizationContainer, CultureInfo cultureInfo)
        {
            _localizationContainer = localizationContainer;
            _cultureInfo = cultureInfo;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _localizationContainer.GetAllStrings(_cultureInfo.Name);
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new FileStringLocalizer(_localizationContainer, culture);
        }
    }
}
