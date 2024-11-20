using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Apply the custom AuthFilter globally
app.Use(async (context, next) =>
{
	var userId = context.Session.GetInt32("UserID");
	if (!userId.HasValue && !context.Request.Path.Value.Contains("Login")) // If user is not logged in
	{
		context.Response.Redirect("/Login/Login"); // Redirect to login page
		return; // Stop further processing
	}

	await next(); // Call the next middleware
});

// Configure the HTTP request pipeline.
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

app.Run();