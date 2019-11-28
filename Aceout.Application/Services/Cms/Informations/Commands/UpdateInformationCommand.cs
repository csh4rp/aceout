using Aceout.Domain.Model.Cms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Cms.Informations.Commands
{
    public class UpdateInformationCommand : IRequest<Information>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<int> GroupIds { get; set; }
    }
}
