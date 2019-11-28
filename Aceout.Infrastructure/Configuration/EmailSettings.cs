using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Configuration
{
    public class EmailSettings
    {
        public string EmailAddress { get; set; }
        public string Domain { get; set; }
        public string ApiKey { get; set; }
    }
}
