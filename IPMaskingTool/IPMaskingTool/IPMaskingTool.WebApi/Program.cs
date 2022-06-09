using IPMaskingToll.Logic.Classes;
using IPMaskingToll.Logic.Interfaces;
using IPMaskingTool.Service.Classes;
using IPMaskingTool.Service.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(c => c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IFileReaderLogic, FileReaderLogic>();
builder.Services.AddScoped<IFileService, FileService>();
var app = builder.Build();

app.UseRouting();
app.UseCors(builder => builder
         .AllowAnyHeader()
         .AllowAnyMethod()
         .SetIsOriginAllowed((host) => true)
         .AllowCredentials()
     );

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run(context => context.Response.WriteAsJsonAsync("no such path"));

app.Run();
