using ApplicationLayer.CatReport.Interfaces;
using ApplicationLayer.Common.Interfaces;
using ApplicationLayer.Reports.DTOs;
using ApplicationLayer.Reports.Interfaces;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Reports.Commands.CreateReport
{
    public class CreateReportCommandHandler
        : IRequestHandler<CreateReportCommand, OperationResult<ReportResponseDto>>
    {
        private readonly IReportRepository _reportRepo;
        private readonly IAdvertisementRepository _adRepo;
        private readonly IUserContextService _userContext;
        private readonly IMapper _mapper;

        public CreateReportCommandHandler(
            IReportRepository reportRepo,
            IAdvertisementRepository adRepo,
            IUserContextService userContext,
            IMapper mapper)
        {
            _reportRepo = reportRepo;
            _adRepo = adRepo;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<OperationResult<ReportResponseDto>> Handle(
            CreateReportCommand request, CancellationToken cancellationToken)
        {
            var accountId = _userContext.AccountId;
            if (accountId is null)
                return OperationResult<ReportResponseDto>.Failure("Du måste vara inloggad för att rapportera.");

            var ad = await _adRepo.GetByIdAsync(request.AdvertisementId);
            if (ad is null)
                return OperationResult<ReportResponseDto>.Failure("Advertisement not found.");

            if (await _reportRepo.ExistsAsync(request.AdvertisementId, accountId.Value))
                return OperationResult<ReportResponseDto>.Failure("Du har redan rapporterat den här annonsen.");

            var report = new Report
            {
                AdvertisementId = request.AdvertisementId,
                AccountId = accountId.Value,
                Comment = request.Dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _reportRepo.AddAsync(report);
            return OperationResult<ReportResponseDto>.Success(_mapper.Map<ReportResponseDto>(report));
        }
    }
}
