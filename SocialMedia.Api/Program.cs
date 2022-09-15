using SocialMedia.Infrastructure;
using SocialMedia.Infrastructure.Filters;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

{
    builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    }).ConfigureApiBehaviorOptions(_ => { });

    builder.Services.AddInfrastructure(builder.Configuration);
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
    app.Run();
}
