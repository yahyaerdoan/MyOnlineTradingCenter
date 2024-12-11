using MyOnlineTradingCenter.ApplicationLayer.Concretions.Attributes.MetadataInformations;
using ResultHandler.Interfaces.Contracts;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IAttributes;

public interface IAttributeInfoProvider
{
    IDataResult<List<Menu>> GetAuthorizedDefinitionEndpointsAsync(Type type);
}
