using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthFilter : IActionFilter
{
	public void OnActionExecuting(ActionExecutingContext context)
	{
		// Get the current controller and action names
		var controllerName = context.RouteData.Values["controller"]?.ToString();
		var actionName = context.RouteData.Values["action"]?.ToString();

		// Skip session validation for the LoginController and its actions
		if (controllerName == "Login" && (actionName == "Login" || actionName == "Logout"))
		{
			return; // Do not apply the filter
		}

		// Check if the UserID is stored in the session
		var userId = context.HttpContext.Session.GetInt32("UserID");

		if (!userId.HasValue) // If UserID is not present, redirect to login
		{
			context.Result = new RedirectToActionResult("Login", "Login", null);
			return;
		}

		// Role-based access validation
		if (!HasAccess(context, controllerName))
		{
			// Redirect unauthorized users to the login page or access denied page
			context.Result = new RedirectToActionResult("Login", "Login", null);
		}
	}

	public void OnActionExecuted(ActionExecutedContext context)
	{
		// This can be left empty if not needed
	}

	private bool HasAccess(ActionExecutingContext context, string controller)
	{
		var session = context.HttpContext.Session;

		// Retrieve role IDs from session
		var lecturerId = session.GetInt32("LecturerID");
		var managerId = session.GetInt32("AcademicManagerID");
		var coordinatorId = session.GetInt32("CoordinatorID");
		var humanResourcesId = session.GetInt32("HumanResourcesID");

		// Role-based access rules
		return controller switch
		{
			"Claims" => lecturerId.HasValue,
			"Verification" => coordinatorId.HasValue,
			"Approval" => managerId.HasValue,
			"HumanResources" => humanResourcesId.HasValue,
			_ => true, // Allow access to other pages (e.g., Login, Privacy) by default
		};
	}
}
