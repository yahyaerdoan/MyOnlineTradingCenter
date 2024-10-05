using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Queries.Get;

public class GetImageFileQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }
    public string Path { get; set; }
    public bool ShowcasePicture { get; set; }
}
