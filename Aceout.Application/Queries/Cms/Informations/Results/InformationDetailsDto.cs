using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Cms.Informations.Results
{
    public class InformationDetailsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<GroupInformationDto> Groups { get; set; }
    }
}
