using BLUEY.Data;
using BLUEY.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container mariadb.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//config identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
//mapeamento de clase para interface
builder.Services.AddScoped<IAspNetUsersRepository, AspNetUsersRepository>();
//polices
builder.Services.AddAuthorizationBuilder().AddPolicy("AdminPolicy", 
    policy => policy.RequireRole("Admin"));
//claims
builder.Services.AddAuthorizationBuilder().AddPolicy("AdminClaimPolicy",
    policy => policy.RequireClaim("IsAdmin", "true"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//midleware de identity polices claims
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


// Criação do escopo e atribuição do role "Admin"
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Certifique-se de que o role "Admin" existe
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Encontre o usuário e o adicione ao role "Admin", caso ainda não esteja
    var user = await userManager.FindByEmailAsync("isabela@gmail.com");
    if (user != null && !(await userManager.IsInRoleAsync(user, "Admin")))
    {
        await userManager.AddToRoleAsync(user, "Admin");
    }

    // Adiciona a claim "IsAdmin" ao usuário, caso ainda não tenha
    var claims = await userManager.GetClaimsAsync(user);
    if (!claims.Any(c => c.Type == "IsAdmin" && c.Value == "true"))
    {
        await userManager.AddClaimAsync(user, new Claim("IsAdmin", "true"));
    }

}

app.Run();
