using Aceout.Domain;
using Aceout.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.DataModel.App
{
    public class File
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; protected set; }
        public string Name { get; protected set; }
        public string Container { get; protected set; }
        public string Extension { get; protected set; }
        public long Size { get; protected set; }
        public string ContentType { get; set; }

        public string Path
        {
            get
            {
                return System.IO.Path.Combine(Container, CreatedDate.Year.ToString(), CreatedDate.Month.ToString(), $"{Id}.{Extension}");
            }
        }

        public File(string name, string container, string extension, long size)
        {
            if (string.IsNullOrEmpty(container))
                throw new ArgumentException(nameof(container));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            if (string.IsNullOrEmpty(extension))
                throw new ArgumentException(nameof(extension));

            if(size <= 0)
                throw new ArgumentException(nameof(size));

            Name = name;
            Container = container;
            CreatedDate = DateTime.Now;
            Extension = extension;
            Size = size;
        }

    }
}
