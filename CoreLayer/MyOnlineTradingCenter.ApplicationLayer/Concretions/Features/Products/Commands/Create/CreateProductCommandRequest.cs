using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Create;

public class CreateProductCommandRequest : IRequest<Unit>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
}
