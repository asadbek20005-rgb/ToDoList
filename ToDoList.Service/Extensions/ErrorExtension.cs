using Microsoft.AspNetCore.Mvc.ModelBinding;
using StatusGeneric;

namespace ToDoList.Service.Extensions;

public static class ErrorExtension
{
    public static void CopyToModelState(this IStatusGeneric status, ModelStateDictionary modelState)
    {
        if (!status.HasErrors)
            return;

        foreach (ErrorGeneric error in status.Errors)
        {
            modelState.AddModelError(
                key: error.ErrorResult.MemberNames.Count() == 1 ? error.ErrorResult.MemberNames.First() : "",
                error.ToString());
        }
    }
}
