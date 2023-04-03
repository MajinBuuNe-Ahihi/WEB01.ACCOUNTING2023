using Microsoft.Extensions.Configuration;
using WEB01.ACCOUNTING2023.API.Extensions.Installer;
using WEB01.ACCOUNTING2023.CORE.Configurations;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Services;
using WEB01.ACCOUNTING2023.CORE.Services;
using WEB01.ACCOUNTING2023.INFRASTRUCTURE.Respository;


    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.InstallerServicesInAssembly(builder.Configuration);

    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                policy =>
                {
                    policy.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
                });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseCors(MyAllowSpecificOrigins);

    app.MapControllers();

    app.Run();


