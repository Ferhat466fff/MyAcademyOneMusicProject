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

//Bu kýsým Bilgisayara Ypacaðýmýz iþlemleri tanýmladýðýmýz yer.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<OneMusicContext>().AddErrorDescriber<CustomErrorDescriber>();

builder.Services.AddScoped<IAboutDal, EfAboutDal>();//DataAccessLayer Abstract ve concreate klasörleri.
builder.Services.AddScoped<IAboutService, AboutManager>();//BusinessLayer Abstract ve Concreate KLsörlerinden cekiyoruz.

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

builder.Services.AddValidatorsFromAssemblyContaining<SingerValidator>();//Validators-->Hata Mesajlarý Verecek.

builder.Services.AddDbContext<OneMusicContext>();//DataAccessLayer Repoisitories  OneMusicContext tanýmladýk.

builder.Services.AddControllersWithViews(option =>//Authorize(Yetkilendirme herkes eriþemeyecek controllerin ustunde authorize yazdýklarýn eriþebilir.)
{

	var authorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

	option.Filters.Add(new AuthorizeFilter(authorizePolicy));
});




builder.Services.ConfigureApplicationCookie(Options =>//giriþe yönlendirecek.
{
    Options.LoginPath = "/Login/Index";//Giriþ yapma yoludur.
    Options.AccessDeniedPath = "/ErorrPage/AccessDenied";//404-40 yetkisi olmayan kiþileri bu sayfaya atýyoruz.
	Options.LogoutPath = "/Login/Logout";//çýkýþ yapma yoludur.
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/ErorrPage/Error404/","?code{0}");//Erorr404 hatasýnýn yolu
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
