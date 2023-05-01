using Event.Web.Areas.Identity.Data;
using Events.API.Extensions;
using Events.API.Infrastructure.Auth;
using Events.API.Mapping;
using Events.API.MiddleWare;
using Events.Persistance;
using Events.Persistance.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    //  option.OperationFilter<SwaggerDefaultValues>();
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Event Api",
        Version = "v1",
        Description = "Store your Events here!",
        Contact = new OpenApiContact
        {
            Email = "Events@Tbc.com",
            Name = "Best Events Ever",
            Url = new Uri("https://tbcacademy.ge")
        }
    });

    option.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        //Type = SecuritySchemeType.Http,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {                          new OpenApiSecurityScheme

                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });

    option.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddTokenAuthentication(builder.Configuration.GetSection(nameof(JWTConfiguration)).GetSection(nameof(JWTConfiguration.Secret)).Value);

builder.Services.AddServices();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));
builder.Services.Configure<JWTConfiguration>(builder.Configuration.GetSection(nameof(JWTConfiguration)));

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ConnectionStrings.DefaultConnection))));
builder.Services.AddScoped<DbContext, UserContext>();

builder.Services.RegisterMaps();

var app = builder.Build();

app.UseMiddleware<MyExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//UserSeed.Initialize(app.Services);

app.Run();