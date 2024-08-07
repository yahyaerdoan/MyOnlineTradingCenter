﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Queries.Get;

public class GetImageFileQueryRequest : IRequest<List<GetImageFileQueryResponse>>
{
    public string Id { get; set; }
}
