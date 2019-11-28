using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.DataModel.App
{
    public class AuthLog
    {
        private List<string> _errors;

        public long Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public AuthenticationStatus Status { get; protected set; }
        public string Errors { get; protected set; }
        public string UserName { get; set; }
        public string IpAddress { get; set; }


        public AuthLog(string userName, string ipAddress, AuthenticationStatus status)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException(nameof(userName));

            if (string.IsNullOrEmpty(ipAddress))
                throw new ArgumentException(nameof(ipAddress));

            UserName = userName;
            IpAddress = ipAddress;
            Status = status;
            CreatedDate = DateTime.UtcNow;
        }

        public void AddError(string error)
        {
            if (string.IsNullOrEmpty(error))
                throw new ArgumentException(nameof(error));

            DeserializeErrors();
            _errors.Add(error);
            SerializeErrors();
        }

        public IEnumerable<string> GetErrors()
        {
            DeserializeErrors();
            return _errors;
        }

        private void DeserializeErrors()
        {
            if(_errors == null)
            {
                _errors = JsonConvert.DeserializeObject<List<string>>(Errors);
            }
        }

        private void SerializeErrors()
        {
            if(_errors != null && _errors.Count > 0)
            {
                Errors = JsonConvert.SerializeObject(_errors);
            }
        }
    }
}
