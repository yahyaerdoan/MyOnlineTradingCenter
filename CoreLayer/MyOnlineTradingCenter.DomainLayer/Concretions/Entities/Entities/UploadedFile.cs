using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

public class UploadedFile : BaseEntity
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Storage { get; set; }

    [NotMapped]
    public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
}
