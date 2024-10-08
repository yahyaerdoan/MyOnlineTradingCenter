using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Contracts.Cofigurations;

public class StorageSettings
{
    public string LocalStorageOrigin { get; set; } = string.Empty;
}
