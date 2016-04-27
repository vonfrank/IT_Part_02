using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace IT_Part_02.Models
{
    public static class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Image.Any())
            {
                var img01 = context.Image.Add(new Image { Title = "Img01", Description = "Den er god med dig" }).Entity;
                var img02 = context.Image.Add(new Image { Title = "Img02", Description = "Den er god med dig" }).Entity;
                var img03 = context.Image.Add(new Image { Title = "Img03", Description = "Den er god med dig" }).Entity;

                context.SaveChanges();
            }
        }
    }
}
