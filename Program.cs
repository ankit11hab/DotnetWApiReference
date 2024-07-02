using Blog.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddDbContext<BloggingContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("LocalSqliteConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MigrateDb();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
