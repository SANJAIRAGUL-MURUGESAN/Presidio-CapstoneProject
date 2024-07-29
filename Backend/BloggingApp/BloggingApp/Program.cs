using BloggingApp.Contexts;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Repositories;
using BloggingApp.Repositories.CommentRequest;
using BloggingApp.Repositories.RetweetCommentRequest;
using BloggingApp.Repositories.TweetRequest;
using BloggingApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static BloggingApp.Services.TokenService;

namespace BloggingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                  {
                      ValidateIssuer = false,
                      ValidateAudience = false,
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey:JWT"]))
                  };

              });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                     policy => policy.RequireRole("Admin"));
            });

            #region Contexts
            builder.Services.AddDbContext<BloggingAppContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
            );
            #endregion

            builder.Services.AddHttpClient("BloggingApp", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7186/");
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });


            #region Repositories
            builder.Services.AddScoped<IRepository<int, User>, UserRepository>();
            builder.Services.AddScoped<IRepository<int, Tweet>, TweetRepository>();
            builder.Services.AddScoped<IRepository<int, TweetFiles>, TweetFilesRepository>();
            builder.Services.AddScoped<IRepository<int, TweetHashTags>, TweetHashTagsRepository>();
            builder.Services.AddScoped<IRepository<int, TweetMentions>, TweetMentionsRepository>();
            builder.Services.AddScoped<IRepository<int, HashTags>, HashTagRepository>();
            builder.Services.AddScoped<IRepository<int, Retweet>, RetweetRepository>();
            builder.Services.AddScoped<IRepository<int, TweetLikes>, TweetLikesRepository>();
            builder.Services.AddScoped<IRepository<int, RetweetLikes>, RetweetLikesRepository>();
            builder.Services.AddScoped<IRepository<int, Comment>, CommentRepository>();
            builder.Services.AddScoped<IRepository<int, Reply>, ReplyRepository>();
            builder.Services.AddScoped<IRepository<int, RetweetComment>, RetweetCommentRepository>();
            builder.Services.AddScoped<IRepository<int, RetweetCommentReply>, RetweetCommentReplyRepository>();
            builder.Services.AddScoped<IRepository<int, TweetCommentLikes>, TweetCommentLikeRepository>();
            // Repository for getting TweetFiles for a particular tweet
            builder.Services.AddScoped<TweetRequestForTweetFilesRepository>();
            builder.Services.AddScoped<CommentRequestForRepliesRepository>();
            builder.Services.AddScoped<RetweetCommentRequestforRepliesRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<ITweetServices, TweetServices>();
            builder.Services.AddScoped<ITokenServices, TokenService>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<ICommentServices, CommentServices>();
            builder.Services.AddScoped<ITweetLikesServices, TweetLikesServices>();
            builder.Services.AddScoped<IAzureBlobService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("AzureBlobStorage");
                var containerName = configuration.GetValue<string>("BlobContainerName");
                return new AzureBlobService(connectionString, containerName);
            });
            #endregion

            #region CORS
            builder.Services.AddCors(opts => {
                opts.AddPolicy("MyCors", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyCors");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpLogging();
            app.MapControllers();


            app.MapControllers();

            app.Run();
        }
    }
}
