using Amazon.S3;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Pastebin.Application.Services;
using Pastebin.Core.Abstractions;
using Pastebin.DataAccess;
using Pastebin.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var s3Config = configuration.GetSection("AWS");

    var config = new AmazonS3Config
    {
        ServiceURL = "https://s3.storage.selcloud.ru", 
        ForcePathStyle = true
    };

    return new AmazonS3Client(
        s3Config["AccessKey"],
        s3Config["SecretKey"],
        config);
});

builder.Services.AddDbContext<PastebinDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(PastebinDbContext)));
    });

builder.Services.AddScoped<IS3Repository>(sp =>
{
    var s3Client = sp.GetRequiredService<IAmazonS3>();
    var configuration = sp.GetRequiredService<IConfiguration>();
    var bucketName = configuration["AWS:BucketName"];

    return new S3Repository(s3Client, bucketName);
});

builder.Services.AddScoped<INotesMetadataService, NotesMetadataService>();
builder.Services.AddScoped<INotesMetadataRepository, NotesMetadataRepository>();

builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

using (var scope = app.Services.CreateScope())
{
    var noteService = scope.ServiceProvider.GetRequiredService<INotesMetadataService>();
    RecurringJob.AddOrUpdate(
        "delete-expired-notes",
        () => noteService.DeleteExpiredNotesAsync(),
        Cron.Minutely); // Runs every minute
}

app.Run();
