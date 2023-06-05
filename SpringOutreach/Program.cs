// <ms_docref_import_types>
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using SpringOutreach.Data;
// </ms_docref_import_types>

// <ms_docref_add_msal>

var builder = WebApplication.CreateBuilder(args);
IEnumerable<string>? initialScopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ');

// Retrieve the connection string 
string connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext");

// Load configuration from Azure App Configuration
//builder.Configuration.AddAzureAppConfiguration(connectionString);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ApplicationDbContext")));

builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAd")
    .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
        .AddDownstreamWebApi("DownstreamApi", builder.Configuration.GetSection("DownstreamApi"))
        .AddInMemoryTokenCaches();

// <ms_docref_add_default_controller_for_sign-in-out>
builder.Services.AddRazorPages().AddMvcOptions(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                  .RequireAuthenticatedUser()
                  .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddMicrosoftIdentityUI();
// </ms_docref_add_default_controller_for_sign-in-out>

// <ms_docref_enable_authz_capabilities>
WebApplication app = builder.Build();

// Migrate latest database changes during startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    // Here is the migration executed
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
// </ms_docref_enable_authz_capabilities>

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Places}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapControllers();

app.Run();
