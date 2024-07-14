using Application;
using Application.Services.Behaviors;
using Application.Services.Managers.Queries;
using Carter;
using Domain.Abstractions;
using FluentValidation;
using Infrastructure;
using Infrastructure.Environment;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApi.Middelewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();

builder.Services.AddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSnakeCaseNamingConvention();
    options.UseLazyLoadingProxies();
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllManagersQuery).Assembly));

builder.Services.AddAutoMapper(ApplicationAssembly.Instance);

builder.Services.AddTransient<ExceptionHandlerMiddleware>();
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipelineWithValidationCommandBehavior<,>));
builder.Services.AddValidatorsFromAssembly(ApplicationAssembly.Instance);

builder.Services.Configure<PicturesDatabaseSettings>(
    builder.Configuration.GetSection(nameof(PicturesDatabaseSettings)));

builder.Services.AddSingleton<IPicturesDatabaseSettings>(options =>
                options.GetRequiredService<IOptions<PicturesDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoRepository, MongoRepository>();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapCarter();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();