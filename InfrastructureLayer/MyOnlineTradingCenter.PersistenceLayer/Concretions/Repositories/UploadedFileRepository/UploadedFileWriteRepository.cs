using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IUploadedFileRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.UploadedFileRepository;

public class UploadedFileWriteRepository : WriteRepository<UploadedFile>, IUploadedFileWriteRepository
{
    public UploadedFileWriteRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
    {
    }
}
