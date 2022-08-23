using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;

namespace BitBuddy.Pages.Components
{
    public class IntInputSelect<TValue> : InputSelect<TValue>
    {
        protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            if(typeof(TValue) == typeof(int))
            {
                if(int.TryParse(value, out var val))
                {
                    result = (TValue) (object) val;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage = "This is not a valid nr";
                    return false;
                }
            }
            else
            {
               return base.TryParseValueFromString(value, out result, out validationErrorMessage);
            }
        }
    }
}
