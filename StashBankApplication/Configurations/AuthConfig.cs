using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace StashBankApplication.Configurations
{
    public static class AuthConfig
    {
        public static IServiceCollection AddAuthConfiguration (
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var tokenConfigurations = new Auth.Config.TokenConfiguration();
           
            configuration.GetSection("TokenConfigurations")
                .Bind(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme 
                    = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            return services;
        }
    }
}
