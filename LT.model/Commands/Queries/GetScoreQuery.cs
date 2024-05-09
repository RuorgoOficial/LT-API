﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.model.Commands.Queries
{
    public class GetScoreQuery : IRequest<Result<IEnumerable<EntityScoreDto>, EntityErrorResponseDto<String>>>
    {
    }
}
