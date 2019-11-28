using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aceout.Domain.Factories.Trainings;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Services.Lms.Access;

namespace Aceout.Domain.Services.Lms.UserElements
{
    public class UserElementService : IUserElementService
    {
        private readonly ILmsAccessService _accessService;
        private readonly IUserElementInfoRepository _accessInfoRepository;
        private readonly IUserElementRepository _userElementRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IAnswerProcessorFactory _answerProcessorFactory;

        public UserElementService(ILmsAccessService accessService, 
            IUserElementInfoRepository accessInfoRepository,
            IUserElementRepository userElementRepository,
            IMaterialRepository materialRepository, 
            IAnswerProcessorFactory answerProcessorFactory)
        {
            _accessService = accessService;
            _accessInfoRepository = accessInfoRepository;
            _userElementRepository = userElementRepository;
            _materialRepository = materialRepository;
            _answerProcessorFactory = answerProcessorFactory;
        }

        public async Task<UserElement> SaveAsync(int userId, int elementId, IEnumerable<AnswerModel> answers, CancellationToken cancellationToken = default(CancellationToken))
        {
            var accessInfo = await _accessInfoRepository.GetAsync(userId, elementId, cancellationToken);

            await _accessService.CheckElementAccessAsync(accessInfo, cancellationToken);

            var material = await _materialRepository.GetByElementIdAsync(elementId, cancellationToken);

            var answerProcessor = _answerProcessorFactory.Create(material.Type);
            var processedAnswer = answerProcessor.ProcessAnswer(material, answers);

            var userElement = await _userElementRepository.GetByUserLessonId(accessInfo.UserLessonId, elementId);

            if(userElement != null)
            {
                userElement.SetUserAnswer(processedAnswer.AnswerModel, processedAnswer.Result);
                await _userElementRepository.UpdateAsync(userElement);
            }
            else
            {
                userElement = new UserElement(accessInfo.UserLessonId, elementId);
                userElement.SetUserAnswer(processedAnswer.AnswerModel, processedAnswer.Result);
                await _userElementRepository.AddAsync(userElement);
            }

            return userElement;
        }
    }
}
