﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mango.Services.ProductAPI.Extentions
{
    public static class WebApplicationBuilderExtentions
    {
        public static WebApplicationBuilder AddAppAuthenticationBuilder(this  WebApplicationBuilder builder)
        {

            //Authenticate API
            var settingSection=builder.Configuration.GetSection("ApiSettings");
            var secret = settingSection.GetValue<string>("Secret");
            var issuer = settingSection.GetValue<string>("Issuer");
            var audience = settingSection.GetValue<string>("Audience");
            var key = Encoding.ASCII.GetBytes(secret);
           
            builder.Services.AddAuthentication(
                x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        ValidateAudience = true,


                    };
                });

            //Authenticate close
            return builder;

        }

    }
}