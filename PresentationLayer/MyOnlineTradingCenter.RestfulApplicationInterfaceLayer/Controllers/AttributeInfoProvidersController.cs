using Microsoft.AspNetCore.Mvc;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IAttributes;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeInfoProvidersController : ControllerBase
    {
        private readonly IAttributeInfoProvider _attributeInfoProvider;

        public AttributeInfoProvidersController(IAttributeInfoProvider attributeInfoProvider)
        {
            _attributeInfoProvider = attributeInfoProvider;
        }
        [HttpGet]      
        public  IActionResult GetAuthorizedEndpoints()
        {
           var response =  _attributeInfoProvider.GetAuthorizedDefinitionEndpointsAsync(typeof(Program));
            return Ok(response);
        }
    }
}
