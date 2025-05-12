namespace RawMaterials.GMP.Api.Controllers;

public static class WebApplicationBuilderExtensions
{
    internal static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables();
        return builder;
    }

    public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwagger(configuration);
        services.AddScoped<IGrantApplicationService, GrantApplicationService>();
        services.AddScoped<IGrantApplicationTypeService, GrantApplicationTypeService>();
    }

    internal static WebApplication UseApi(this WebApplication app)
    {
        IConfiguration configuration = app.Configuration;

        app.UseCultures();
        app.UseGlobalExceptionHandler();

        app.UseSwagger();
        app.UseSwaggerUI(x =>
        {
            x.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
            x.DisplayRequestDuration();
            x.EnableTryItOutByDefault();
            x.RoutePrefix = string.Empty;
            x.SwaggerEndpoint("swagger/account/swagger.json", "Account");
            x.SwaggerEndpoint("swagger/grantapplication/swagger.json", "GrantApplication");
            x.SwaggerEndpoint("swagger/grantapplicationtype/swagger.json", "GrantApplicationType");
            x.OAuthClientId(configuration["System:Swagger:ClientId"]);
            x.OAuthUsePkce();
            x.OAuthScopeSeparator(" ");
            x.DefaultModelsExpandDepth(-1);
            x.DocExpansion(DocExpansion.List);
            x.DocumentTitle = "Grant Management Platform";
            x.DefaultModelRendering(ModelRendering.Example);
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCors("Default");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    private static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        => services.AddSwaggerGen(c =>
        {
            c.UseInlineDefinitionsForEnums();
            c.DescribeAllParametersInCamelCase();
            var contact = new OpenApiContact
            {
                Email = "stefan.ackermann@appsolvia.de",
                Name = "Stefan Ackermann"
            };

            c.SwaggerDoc("account", new OpenApiInfo()
            {
                Title = "Grant Management Platform - Account",
                Version = "v1",
                Contact = contact
            });

            c.SwaggerDoc("grantapplication", new OpenApiInfo()
            {
                Title = "Grant Management Platform - GrantApplication",
                Version = "v1",
                Contact = contact
            });

            c.SwaggerDoc("grantapplicationtype", new OpenApiInfo()
            {
                Title = "Grant Management Platform - GrantApplicationType",
                Version = "v1",
                Contact = contact
            });

            c.EnableAnnotations();
        });

    private static void UseGlobalExceptionHandler(this WebApplication app)
        => app.UseExceptionHandler(err =>
        {
            err.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/csv";
                context.Response.Headers.Append("Sender", Assembly.GetExecutingAssembly().GetName().Name);
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature != null)
                    await context.Response.WriteAsync(contextFeature.Error.Message);
            });
        });

    private static void UseCultures(this WebApplication app)
        => app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-GB"),
            SupportedCultures = [new CultureInfo("en-GB")],
            SupportedUICultures = [new CultureInfo("en-GB")]
        });
}
