using FastEndpoints;

namespace Dashome.Api.Extensions;

public static class WebApplicationExtensions
{
    public static async Task<WebApplication> UseApiAsync(this WebApplication app)
    {
        await app.InitAsync();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors(cp =>
        {
            cp.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });

        app.UseAuthorization();
        app.UseFastEndpoints();

        app.MapControllers();


        return app;
    }
}