using System;
using Microsoft.Extensions.Configuration;

namespace MyBlazorApp.Services
{
    public class ApiService
    {
        private readonly IConfiguration _configuration;

        public ApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetApiUrl()
        {
            return _configuration["ApiSettings:BaseUrl"] ?? throw new NullReferenceException("ApiSettings BaseUrl is not configured properly.");
        }
    }
}