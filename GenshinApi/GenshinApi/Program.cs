using GenshinApi.Context;
using GenshinApi.Repositories;
using GenshinApi.Repositories.ArtifactSetRepositories;
using GenshinApi.Repositories.Characters;
using GenshinApi.Repositories.RegionRepo;
using GenshinApi.Repositories.Team_CharRepositories;
using GenshinApi.Repositories.TeamCompositionRepositories;
using GenshinApi.Repositories.WeaponRepositories;
using GenshinApi.Repositories.WeaponTypeRepositories;
using GenshinApi.Services;
using GenshinApi.Services.ArtifactSetServices;
using GenshinApi.Services.CharacterServices;
using GenshinApi.Services.RegionServices;
using GenshinApi.Services.Team_CharacterServices;
using GenshinApi.Services.TeamCompositionServices;
using GenshinApi.Services.WeaponServices;
using GenshinApi.Services.WeaponTypeServices;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Genshin Impact API",
        Description = "An ASP DOTNET Core Web API Project inspired from Genshin Impact",
        Contact = new OpenApiContact
        {
            Name = "Github Repository",
            Url = new Uri("https://github.com/CITUCCS/csit327-project-group-9-helloworld")
        }

    });
    // Feed generated xml api docs to swagger
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


// Configure Services
ConfigureServices(builder.Services);


void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<DapperContext>();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    // Repository Injections
    services.AddScoped<IWeaponsRepository, WeaponsRepository>();
    services.AddScoped<IWeaponTypeRepository, WeaponTypeRepository>();
    services.AddScoped<ICharacterRepository, CharacterRespository>();
    services.AddScoped<IArtifactSetRepository, ArtifactSetRepository>();
    services.AddScoped<IRegionRepository, RegionRepository>();
    services.AddScoped<ITeamCompositionRepository, TeamCompositionRepository>();
    services.AddScoped<ITeam_CharacterRepository, Team_CharacterRepository>();

    // Service Injections
    services.AddScoped<IWeaponsService, WeaponsService>();
    services.AddScoped<IWeaponTypeService, WeaponTypeService>();
    services.AddScoped<ICharacterService, CharacterService>();
    services.AddScoped<IArtifactSetService, ArtifactSetService>();
    services.AddScoped<IRegionServices, RegionServices>();
    services.AddScoped<ITeamCompositionService, TeamCompositionService>();
    services.AddScoped<ITeam_CharacterService, Team_CharacterService>();
    services.AddScoped<ICreateWeaponUnderTypeService, CreateWeaponUnderTypeService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
