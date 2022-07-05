using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products
{
    public class Program
    {     
            services.Build();        
        services.AddControllers();
        if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            builder.app.UseHttpsRedirection();

            builder.app.UseRouting();

            builder.app.UseAuthorization();

            builder.app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
    }
}
