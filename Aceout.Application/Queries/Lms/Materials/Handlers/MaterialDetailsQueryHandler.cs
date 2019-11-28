using Aceout.Application.Queries.Lms.Materials.Models;
using Aceout.Application.Queries.Lms.Materials.Results;
using Aceout.Domain.Factories.Trainings;
using Aceout.Domain.Model.Lms;
using MediatR;
using NHibernate;
using NHibernate.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Lms.Materials.Handlers
{
    public class MaterialDetailsQueryHandler : IRequestHandler<MaterialDetailsQuery, MaterialDetailsDto>
    {
        private readonly ISession _session;
        private readonly IMaterialModelConverterFactory _materialConverterFactory;

        public MaterialDetailsQueryHandler(ISession session, IMaterialModelConverterFactory materialConverterFactory)
        {
            _session = session;
            _materialConverterFactory = materialConverterFactory;
        }

        public async Task<MaterialDetailsDto> Handle(MaterialDetailsQuery request, CancellationToken cancellationToken)
        {
            var material = await _session.Query<Material>()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            var dto = new MaterialDetailsDto
            {
                Id = material.Id,
                CategoryId = material.CategoryId,
                Name = material.Name,
                Type = material.Type,
                Content = material.Content,
                IsActive = material.IsActive
            };

            var converter = _materialConverterFactory.Create(material.Type);

            dto.DataModels = converter.ConvertDataModel(material);
            dto.AnswerModels = converter.ConvertAnswerModel(material);

            return dto;
        }
    }
}
