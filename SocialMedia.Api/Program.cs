using SocialMedia.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

{
    builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
    builder.Services.AddInfrastructure(builder.Configuration);
}

var app = builder.Build();

{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
