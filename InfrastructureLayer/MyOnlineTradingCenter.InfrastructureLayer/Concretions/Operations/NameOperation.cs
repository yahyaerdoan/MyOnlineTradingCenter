using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.Operations;

public static class NameOperation
{
    public static string CharacterRegulatory(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;

        var replacements = new (string, string)[]
        {
            ("\"", ""), ("!", ""), ("'", ""), ("#", ""), ("^", ""), ("+", ""), ("%", ""), ("&", ""),
            ("/", ""), ("(", ""), (")", ""), ("=", ""), ("?", ""), ("_", ""), ("@", ""), ("€", ""),
            ("~", ""), ("`", ""), (".", ""), ("-", ""), ("<", ""), (">", ""), ("|", ""), (";", ""),
            (":", ""), (",", ""), ("ö", "o"), ("ü", "u"), ("ä", "a"), ("Ö", "O"), ("Ü", "U"), ("Ä", "A"),
            ("İ", "I"), ("ı", "i"), ("ğ", "g"), ("Ğ", "G"), ("ş", "s"), ("Ş", "S"), ("ç", "c"), ("Ç", "C")
        };

        foreach (var (oldChar, newChar) in replacements)
        {
            name = name.Replace(oldChar, newChar);
        }

        name = Regex.Replace(name, "[^a-zA-Z0-9]", "");

        return name;
    }
}
