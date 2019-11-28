using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Test.Utils
{
    public class SchemaScripts
    {
        public const string MaterialCategory = @"
            CREATE TABLE MaterialCategory(
            Id INT NOT NULL PRIMARY KEY,
            Language VARCHAR(2) NOT NULL,
            Name varchar(255) NOT NULL);";


    }
}
