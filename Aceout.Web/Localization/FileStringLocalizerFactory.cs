using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Aceout.Web.Localization
{
    public class FileStringLocalizerFactory : IStringLocalizerFactory, IDisposable
    {
        private readonly IMemoryCache _cache;
        private readonly CultureInfo _defaultCulture;
        private IDirectoryManager _directoryManager;
        private readonly CultureInfo[] _supportedCultures;
        private FileSystemEventHandler _changeHandler;

        private readonly object cacheLock = new object();
        private const string CacheKey = "LocalizationCacheKey";

        public FileStringLocalizerFactory(IMemoryCache cache, IDirectoryManager directoryManager, CultureInfo defaultCulture, CultureInfo[] supportedCultures)
        {
            _cache = cache;
            _directoryManager = directoryManager;
            _defaultCulture = defaultCulture;
            _supportedCultures = supportedCultures;

            _changeHandler = (s, e) => LoadCache();
            _directoryManager.Changed += _changeHandler;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            var container = _cache.Get<LocalizationContainer>(CacheKey);

            if(container == null)
            {
                container = LoadCache();
            }

            return new FileStringLocalizer(container, _defaultCulture);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return Create(null);
        }

        private LocalizationContainer LoadCache()
        {
            lock (cacheLock)
            {
                var files = _directoryManager.GetFiles();
                var supportedFiles = _supportedCultures.Select(x => $"{x.Name}.json");

                var notExisting = supportedFiles.Except(files).ToArray();
                if (notExisting.Length > 0)
                {
                    var fileNames = string.Join(",", notExisting);
                    throw new InvalidOperationException($"Not all supported files can be found in the selected directory. Missing files: {fileNames}");
                }

                var translations = new Dictionary<string, Dictionary<string, LocalizedString>>();
                foreach (var file in files)
                {
                    var content = _directoryManager.ReadFile(file);
                    var messages = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

                    var localizedMessages = new Dictionary<string, LocalizedString>();
                    foreach (var msg in messages)
                    {
                        localizedMessages[msg.Key] = new LocalizedString(msg.Key, msg.Value);
                    }

                    var culture = Path.GetFileNameWithoutExtension(file);
                    translations[culture] = localizedMessages;
                }

                var container = new LocalizationContainer(translations);
                return _cache.Set(CacheKey, container);
            }

        }

        public void Dispose()
        {
            _directoryManager.Changed -= _changeHandler;
        }
    }
}
