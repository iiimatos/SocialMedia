using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Filters;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

{
    builder.Services.AddSwaggerGen(doc =>
    {
        doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Social Media Api v1", Version = "v1" });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        doc.IncludeXmlComments(xmlPath);
    });
    builder.Services.AddControllers(options =>
    {
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        options.Filters.Add<GlobalExceptionFilter>();
    }).AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    }).ConfigureApiBehaviorOptions(_ => { });

    builder.Services.Configure<PaginationOptions>(builder.Configuration.GetSection("Pagination"));

    builder.Services.AddTransient<IPostService, PostService>();
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
    builder.Services.AddSingleton<IUriService>(provider =>
    {
        var accessor = provider.GetRequiredService<IHttpContextAccessor>();
        var request = accessor.HttpContext.Request;
        var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
        return new UriService(absoluteUri);
    });
    builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddDbContext<SocialMediaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SocialMedia")));
    builder.Services.AddMvc(options =>
    {
        options.Filters.Add<ValidationFilter>();
    }).AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    });
}

var app = builder.Build();

{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = String.Empty;
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Social Media Api v1");
    });
    app.Run();
}
