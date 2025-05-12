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
            x.SwaggerEndpoint("swagger/admin/swagger.json", "Admin");
            x.SwaggerEndpoint("swagger/applicant/swagger.json", "Applicant");
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
            //c.OperationFilter<DefaultStartAndEndValues>();
            //c.OperationFilter<PlanumTypesOperationFilter>();
            //c.OperationFilter<CountryOperationFilter>();
            //c.OperationFilter<AreaOperationFilter>();
            //c.OperationFilter<ExchangeOperationFilter>();
            //c.OperationFilter<RemoveVersionParameters>();

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

            c.SwaggerDoc("admin", new OpenApiInfo()
            {
                Title = "Grant Management Platform - Admin",
                Version = "v1",
                Contact = contact
            });

            c.SwaggerDoc("applicant", new OpenApiInfo()
            {
                Title = "Grant Management Platform - Applicant",
                Version = "v1",
                Contact = contact
            });

            //Uri tokenUrl = new($"{configuration["System:AzureAD:Instance"]}{configuration["System:AzureAD:TenantId"]}/oauth2/v2.0/token");
            //Uri authorizationUrl = new($"{configuration["System:AzureAD:Instance"]}{configuration["System:AzureAD:TenantId"]}/oauth2/v2.0/authorize");

            //string accessAsUserScope = $"api://{configuration["System:AzureAD:ClientId"]}/access.as.user";
            //string definitionName = "oauth2";

            //c.AddSecurityDefinition(definitionName, new OpenApiSecurityScheme
            //{
            //    Name = "OAuth 2",
            //    Type = SecuritySchemeType.OAuth2,
            //    Flows = new OpenApiOAuthFlows
            //    {
            //        AuthorizationCode = new OpenApiOAuthFlow
            //        {
            //            AuthorizationUrl = authorizationUrl,
            //            TokenUrl = tokenUrl,
            //            Scopes = new Dictionary<string, string>
            //            {
            //                { accessAsUserScope, "Access as User" }
            //            },
            //        }
            //    },
            //});

            //c.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference
            //            {
            //                Type = ReferenceType.SecurityScheme,
            //                Id = definitionName
            //            }
            //    },
            //        new[] {
            //           accessAsUserScope
            //        }
            //    }
            //});

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
