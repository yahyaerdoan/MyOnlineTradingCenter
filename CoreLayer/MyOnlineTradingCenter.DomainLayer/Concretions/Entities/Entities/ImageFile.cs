using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

public class ImageFile : UploadedFile
{
    public bool ShowcasePicture { get; set; } = default!;
    public ICollection<Product> Products { get; set; } = default!;

}
