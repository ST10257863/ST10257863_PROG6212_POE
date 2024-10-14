using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthFilter : IActionFilter
{
	public void OnActionExecuting(ActionExecutingContext context)
	{
		// Check if the UserID is stored in the session
		var userId = context.HttpContext.Session.GetInt32("UserID");

		if (!userId.HasValue) // If UserID is not present, redirect to login
		{
			context.Result = new RedirectToActionResult("Login", "Login", null);
		}
	}

	public void OnActionExecuted(ActionExecutedContext context)
	{
		// This can be left empty if not needed
	}
}
