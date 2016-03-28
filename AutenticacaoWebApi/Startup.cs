using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

namespace AutenticacaoWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            //configuração das rotas 
            ConfigureWebApi(config);
            //uso do autentication provider configurado na classe AutorizationServerProvider
            ConfigureOAuth(app);

            //configuração para usar Cors
            app.UseCors(CorsOptions.AllowAll);
            //Configuração para usar o WebApi com as rotas configuradas
            app.UseWebApi(config);

            //PARA SOLICITAR UM TOKEN
            //parametros da requisição:
            // tipo : x-www-form-urlencoded
            //parametros:
            //grant_type: password
            //username: usuario
            //password: senha
            // será retornado "access_token": valor do token

            //PARA ACESSAR COM O TOKEN
            // passar no cabeçalho da requisição os parametros abaixo
            // Authorization: Bearer valor do token
            //

            //IMPORTANTE
            //uma vez solicitado o token pode ser armazenao em cookie ou em uma variavel do javascript ;)

        }

        public static void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional }
                
            );
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/security/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                // provider pode ser externor como facebook twiter ou Google Acount
                Provider = new AutorizationServerProvider()         

            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


        }
    }
}