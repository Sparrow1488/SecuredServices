using Microsoft.EntityFrameworkCore;
using SecuredServices.Example.Api.Data;
using SecuredServices.Example.Api.Extensions;
using SecuredServices.Example.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("SecuredServicesDatabase"));

builder.Services.AddTransient<DataInit>();
builder.Services.BuildServiceProvider()!.GetService<DataInit>()!.Init();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.UseGroupSecuredServices();
builder.Services.UseAlternativeGroupSecuredServices();
builder.Services.AddTransient<GroupsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
