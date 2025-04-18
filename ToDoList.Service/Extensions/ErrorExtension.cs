using Microsoft.AspNetCore.Mvc.ModelBinding;
using StatusGeneric;

namespace ToDoList.Service.Extensions;

public static class ErrorExtension
{
    public static void CopyToModelState(this IStatusGeneric status, ModelStateDictionary modelState)
    {
        if (!status.HasErrors)
            return;

        foreach (var error in status.Errors)
        {
            modelState.AddModelError(error.ErrorResult.MemberNames.Count() == 0 ? error.ErrorResult.MemberNames.First() : string.Empty, error.ToString());
        }
    }
}
