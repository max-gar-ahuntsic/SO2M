//--------- ajouté pour rajouter infos à Swagger ----
//using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.OpenApi.Models;
using System.Reflection;
//using static System.Runtime.InteropServices.JavaScript.JSType;
//using System.Text;
//--------------------------------------------------

using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSwaggerGen(c =>
{
    c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.RelativePath}");
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        TermsOfService = new Uri("http://localhost:5163/page/accueil")

    });

});

//---- ajout par MG pour eviter erreur cause par 2 entitees qui se renvoient l'une a l'autre
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
//----- fin ajout par MG


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//--------------------------
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "View")),
    RequestPath = "/View"
});
//-----------------------------


app.Run();
