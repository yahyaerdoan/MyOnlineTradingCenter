using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IInvoiceFileRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.GenericRepositories;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.InvoiceFileRepository;

public class InvoiceFileReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository
{
    public InvoiceFileReadRepository(MyOnlineTradingCenterPostgreSqlDbContext context) : base(context)
    {
    }
}
