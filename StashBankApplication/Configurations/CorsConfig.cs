namespace StashBankApplication.Configurations
{
    public static class CorsConfig
    {
        public static void AddCorsConfiguration(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("LocalPolicy",
                    policy => policy.WithOrigins(
                            "http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });
        }

        public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseCors("LocalPolicy");
            return applicationBuilder;
        }
    }
}
