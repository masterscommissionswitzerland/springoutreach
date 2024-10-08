// <ms_docref_import_types>
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using SpringOutreach.Controllers;
using SpringOutreach.Data;
using Microsoft.Extensions.Configuration;
// </ms_docref_import_types>

// <ms_docref_add_msal>s


var builder = WebApplication.CreateBuilder(args);
IEnumerable<string>? initialScopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ');

// Retrieve the connection string
string connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));

// <ms_docrefv_add_default_controller_for_sign-in-out>

builder.Services.AddRazorPages();
AppContext.SetSwitch("UseSqlServer.EnableLegacyTimestampBehavior", true);

// <ms_docref_enable_authz_capabilities>
WebApplication app = builder.Build();

//Migrate latest database changes during startup
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider
//        .GetRequiredService<ApplicationDbContext>();
//
    // Migration gets executed 
//    dbContext.Database.Migrate();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Places}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapControllers();

app.Run();