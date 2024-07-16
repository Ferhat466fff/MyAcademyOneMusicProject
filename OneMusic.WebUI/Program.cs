using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.BusinessLayer.ConcCrate;
using OneMusic.BusinessLayer.ConCreate;
using OneMusic.BusinessLayer.validators;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.ConCreate;
using OneMusic.DataAccessLayer.Context;
using OneMusic.EntityLayer.Entities;
using System.Reflection;

//Bu k�s�m Bilgisayara Ypaca��m�z i�lemleri tan�mlad���m�z yer.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<OneMusicContext>().AddErrorDescriber<CustomErrorDescriber>();

builder.Services.AddScoped<IAboutDal, EfAboutDal>();//DataAccessLayer Abstract ve concreate klas�rleri.
builder.Services.AddScoped<IAboutService, AboutManager>();//BusinessLayer Abstract ve Concreate KLs�rlerinden cekiyoruz.

builder.Services.AddScoped<IAlbumDal, EFAlbumDal>();
builder.Services.AddScoped<IAlbumService, AlbumManager>();

builder.Services.AddScoped<IBannerDal, EFBannerDal>();
builder.Services.AddScoped<IBannerService, BannerManager>();

builder.Services.AddScoped<ISingerDal, EfSingerDal>();
builder.Services.AddScoped<ISingerService, SingerManager>();

builder.Services.AddScoped<IMessageDal, EFMessageDal>();
builder.Services.AddScoped<IMessageService, MessageManager>();

builder.Services.AddScoped<ISongDAL, EFSongDal>();
builder.Services.AddScoped<ISongService, SongManager>();

builder.Services.AddScoped<ICategoryDal, EfCataegoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<IContactDal, EFContactDal>();
builder.Services.AddScoped<IContactService, ContactManager>();

builder.Services.AddValidatorsFromAssemblyContaining<SingerValidator>();//Validators-->Hata Mesajlar� Verecek.

builder.Services.AddDbContext<OneMusicContext>();//DataAccessLayer Repoisitories  OneMusicContext tan�mlad�k.

builder.Services.AddControllersWithViews(option =>//Authorize(Yetkilendirme herkes eri�emeyecek controllerin ustunde authorize yazd�klar�n eri�ebilir.)
{

	var authorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

	option.Filters.Add(new AuthorizeFilter(authorizePolicy));
});




builder.Services.ConfigureApplicationCookie(Options =>//giri�e y�nlendirecek.
{
    Options.LoginPath = "/Login/Index";//Giri� yapma yoludur.
    Options.AccessDeniedPath = "/ErorrPage/AccessDenied";//404-40 yetkisi olmayan ki�ileri bu sayfaya at�yoruz.
	Options.LogoutPath = "/Login/Logout";//��k�� yapma yoludur.
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/ErorrPage/Error404/","?code{0}");//Erorr404 hatas�n�n yolu
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
