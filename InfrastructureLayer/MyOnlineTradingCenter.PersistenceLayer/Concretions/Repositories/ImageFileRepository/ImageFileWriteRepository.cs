using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IImageFileRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.ImageFileRepository;

public class ImageFileWriteRepository : WriteRepository<ImageFile>, IImageFileWriteRepository
{
    public ImageFileWriteRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
    {
    }
}
