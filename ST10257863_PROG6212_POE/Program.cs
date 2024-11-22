using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add<AuthFilter>(); // Apply the AuthFilter globally
});

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add HttpContextAccessor to enable session checks
builder.Services.AddHttpContextAccessor();

// Add session services
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Set timeout for session
	options.Cookie.HttpOnly = true; // Make cookie accessible only by the server
	options.Cookie.IsEssential = true; // Required for session state
});

var app = builder.Build();

// Use session in the middleware
app.UseSession();

// Middleware to handle session-based access and avoid redirection loops
app.Use(async (context, next) =>
{
	// Get the UserID from session
	var userId = context.Session.GetInt32("UserID");

	// Exclude static files and resources from session checks
	if (context.Request.Path.StartsWithSegments("/css") ||
		context.Request.Path.StartsWithSegments("/js") ||
		context.Request.Path.StartsWithSegments("/lib") ||
		context.Request.Path.StartsWithSegments("/images"))
	{
		await next();
		return;
	}

	// Avoid redirect loop on login page and handle unauthorized access
	if (!userId.HasValue && !context.Request.Path.Value.Contains("/Login"))
	{
		context.Response.Redirect("/Login/Login");
		return;
	}

	await next(); // Proceed with the request
});

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts(); // Security headers for production
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Set the default route to the Login page
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Login}/{action=Login}/{id?}");

// Redirect unhandled routes to login
app.MapFallbackToController("Login", "Login");

app.Run();
