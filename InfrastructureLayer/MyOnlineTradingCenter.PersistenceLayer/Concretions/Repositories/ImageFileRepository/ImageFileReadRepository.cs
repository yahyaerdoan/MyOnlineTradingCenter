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

public class ImageFileReadRepository : ReadRepository<ImageFile>, IImageFileReadRepository
{
    public ImageFileReadRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
    {
    }
}
