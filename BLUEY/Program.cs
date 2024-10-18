using BLUEY.Data;
using BLUEY.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container for MariaDB.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Map interface to implementation classes
builder.Services.AddScoped<IAspNetUsersRepository, AspNetUsersRepository>();
builder.Services.AddScoped<IAspnetUserRolesRepository, AspnetUserRolesRepository>();
builder.Services.AddScoped<IDebiteRepository, DebiteRepository>();
// Add policies with hard-coded examples
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"))
    .AddPolicy("AdminClaimPolicy", policy => policy.RequireClaim("IsAdmin", "true"));

// Register dynamic policies based on roles in AspNetRoles table
var serviceProvider = builder.Services.BuildServiceProvider();
using (var scope = serviceProvider.CreateScope())
{
    
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var authorizationBuilder = builder.Services.AddAuthorizationBuilder();
    
    foreach (var role in roleManager.Roles.ToList())
    {
        authorizationBuilder.AddPolicy(role.Name, policy => policy.RequireRole(role.Name));
    }
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Middleware for Identity, policies, and claims
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Scope for assigning the "Admin" role and claims to a user
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Ensure "Admin" role exists
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Find the user and add to "Admin" role, if not already assigned
    var user = await userManager.FindByEmailAsync("isabela@gmail.com");
    if (user != null && !(await userManager.IsInRoleAsync(user, "Admin")))
    {
        await userManager.AddToRoleAsync(user, "Admin");
    }

    // Add the "IsAdmin" claim to the user, if not already present
    var claims = await userManager.GetClaimsAsync(user);
    if (!claims.Any(c => c.Type == "IsAdmin" && c.Value == "true"))
    {
        await userManager.AddClaimAsync(user, new Claim("IsAdmin", "true"));
    }
}

app.Run();
