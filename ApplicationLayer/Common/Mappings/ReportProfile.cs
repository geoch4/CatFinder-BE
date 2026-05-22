using ApplicationLayer.Reports.DTOs;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Common.Mappings
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report, ReportResponseDto>();
        }
    }
}
