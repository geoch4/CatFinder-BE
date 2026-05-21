using ApplicationLayer.CatReport.DTOs;
using ApplicationLayer.SavedAdvertisements.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer.SavedAdvertisements.Commands
{
    public record CreateSavedAdvertisementCommand(
        CreateSavedAdvertisementDto dto
    ) : IRequest<SavedAdvertisementResponseDto>;
}
