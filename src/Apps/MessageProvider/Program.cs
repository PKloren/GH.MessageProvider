using MessageProvider.Handlers;
using MessageProvider.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<MessageServiceOptions>()
    .Bind(builder.Configuration.GetSection(MessageServiceOptions.Name))
    .ValidateDataAnnotations();

builder.Services
    .AddAdapters()
    .AddTransient<IMessageHandler, MessageHandler>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
