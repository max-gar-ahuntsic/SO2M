using SO2M.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//---- ajout par MG pour eviter erreur cause par 2 entitees qui se renvoient l'une a l'autre
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
//-----------------

// Configuration des services CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None; // Si vous avez besoin de SameSite=None
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Assurez-vous que le cookie est sécurisé
});

builder.Services.AddDistributedMemoryCache();

//---- ajout par MG: documentation Swagger-----------------------------
var xmlDocsPath = Path.Combine(AppContext.BaseDirectory, typeof(Startup).Assembly.GetName().Name + ".xml");
builder.Services.AddSwaggerGen(options => { options.IncludeXmlComments(xmlDocsPath); });
//-----------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("AllowAllOrigins"); // Utiliser la politique CORS

//string contentRootPath = builder.Environment.ContentRootPath;
//string staticFilesPath = Path.Combine(contentRootPath, "wwwroot");

//if (!Directory.Exists(staticFilesPath))
//{
//    Console.WriteLine($"Directory not found: {staticFilesPath}");
//    Directory.CreateDirectory(staticFilesPath); // Créez le répertoire si nécessaire
//}

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(staticFilesPath),
//    RequestPath = ""
//});




app.UseCors("AllowAllOrigins");

app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // Utilisez le middleware de session avant l'authentification

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
