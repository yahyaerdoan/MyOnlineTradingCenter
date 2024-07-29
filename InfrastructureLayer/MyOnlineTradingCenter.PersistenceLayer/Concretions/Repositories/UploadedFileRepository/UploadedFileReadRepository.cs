using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IUploadedFileRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.UploadedFileRepository;

public class UploadedFileReadRepository : ReadRepository<UploadedFile>, IUploadedFileReadRepository
{
    public UploadedFileReadRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
    {
    }
}
