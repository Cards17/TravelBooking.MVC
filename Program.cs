using TravelBooking.MVC.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Register HttpClient with default configuration
var webApiUrl = builder.Configuration.GetValue<string>("WebApiUrl")?? throw new InvalidOperationException("The 'WebApiUrl' configuration value is not set.");
builder.Services.AddHttpClient<BookingController>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(builder.Configuration.GetValue<int>("WebApiTimeOut"));
    client.BaseAddress = new Uri(webApiUrl);
});


// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Booking}/{action=Index}/{id?}");

app.Run();
