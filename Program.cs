using Microsoft.EntityFrameworkCore;
using RealWorld.Infrastructure;
using RealWorld.Infrastructure.Security;
using FluentValidation;
using RealWorld.Infrastructure.Extensions;
using RealWorld.Features.Profiles;
using RealWorld.Features.Articles;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddJwtAuthentication();

builder.Services.AddDbContext<RealWorldContext>(o => { o.UseSqlite("Filename=RealWorld.db"); });
builder.Services.AddMvc(
    options =>
    {
        options.EnableEndpointRouting = false;
        options.Filters.Add<ErrorActionFilter>();
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    }
);

builder.Services.AddValidatorsFromAssemblyContaining<RealWorld.Features.Users.Login.CommandDTOValidator>();
/* builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); */

builder.Services.AddTransient<IProfileReader, ProfileReader>();
builder.Services.AddTransient<IArticleReader, ArticleReader>();

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();

/* builder.Services.AddSwaggerGen(); */

var app = builder.Build();

/* if (app.Environment.IsDevelopment()) */
/* { */
/*     app.UseSwagger(); */
/*     app.UseSwaggerUI(); */
/* } */
/* app.UseCors(b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()); */
/* app.UseAuthorization(); */
/* app.UseHttpsRedirection(); */

app.UseAuthentication();
app.UseErrorHandling();
app.UseMvc();
app.MapControllers();

app.Run();
