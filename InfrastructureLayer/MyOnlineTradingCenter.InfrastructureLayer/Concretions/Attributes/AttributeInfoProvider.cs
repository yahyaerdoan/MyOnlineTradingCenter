using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IAttributes;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Attributes;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Attributes.MetadataInformations;
using ResultHandler.Implementations.ErrorResults;
using ResultHandler.Implementations.SuccessResults;
using ResultHandler.Interfaces.Contracts;
using System.Net;
using System.Reflection;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.Attributes;

public class AttributeInfoProvider : IAttributeInfoProvider
{
    public IDataResult<List<Menu>> GetAuthorizedDefinitionEndpointsAsync(Type type)
    {
        // Validate the input type
        if (type == null)
            return new ErrorDataResult<List<Menu>>("The provided type is null.", HttpStatusCode.BadRequest);

        // Get the assembly containing the specified type
        var assembly = Assembly.GetAssembly(type);
        if (assembly == null)
            return new ErrorDataResult<List<Menu>>("Assembly could not be loaded.", HttpStatusCode.BadRequest);

        // Find all controllers in the assembly
        var controllers = assembly.GetTypes()
            .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && !t.IsAbstract)
            .ToList();

        if (!controllers.Any())
            return new ErrorDataResult<List<Menu>>("No controllers found in the assembly.", HttpStatusCode.BadRequest);

        var menus = new List<Menu>();

        foreach (var controller in controllers)
        {
            // Get user-defined methods (exclude inherited ones)
            var methods = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(m => !m.IsSpecialName); // Exclude property getters/setters

            foreach (var method in methods)
            {
                // Skip methods without the AuthorizationDefinition attribute
                var authorizeAttribute = method.GetCustomAttribute<AuthorizationDefinition>();
                if (authorizeAttribute == null) continue;

                // Find or create the menu corresponding to the attribute's Menu
                var menu = menus.FirstOrDefault(m => m.Name == authorizeAttribute.Menu);
                if (menu == null)
                {
                    menu = new Menu { Name = authorizeAttribute.Menu, Actions = new List<MenuAction>() };
                    menus.Add(menu);
                }

                // Create a new MenuAction based on the attribute
                var actionDetails = new MenuAction
                {
                    ActionType = authorizeAttribute.ActionType.ToString(),
                    Definition = authorizeAttribute.Definition
                };

                // Find HTTP method (GET, POST, etc.)
                var httpAttribute = method.GetCustomAttribute<HttpMethodAttribute>();
                actionDetails.HttpType = httpAttribute?.HttpMethods.FirstOrDefault() ?? HttpMethods.Get;

                // Generate a unique code for the action
                actionDetails.ActionCode = $"{actionDetails.HttpType}.{actionDetails.ActionType}.{actionDetails.Definition.Replace(" ", "")}";

                menu.Actions.Add(actionDetails);
            }
        }

        return new SuccessDataResult<List<Menu>>(menus, "Authorized attributes have been listed.", HttpStatusCode.OK);
    }
}

