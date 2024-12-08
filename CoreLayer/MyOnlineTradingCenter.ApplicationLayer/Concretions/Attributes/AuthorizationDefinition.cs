namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizationDefinition :  Attribute
{
    public AuthorizationDefinition(string menu, string definition, ActionType actionType)
    {
        Menu = menu;
        Definition = definition;
        ActionType = actionType;
    }

    public string Menu { get; }
    public string Definition { get; }
    public ActionType ActionType { get; }
}
public enum ActionType
{
    Read,
    Create,
    Update,
    Delete
}

