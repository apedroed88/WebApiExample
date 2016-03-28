using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Threading;

namespace AutenticacaoWebApi
{
    public class AutorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //habilita a Api a receber todos os verbos http de qualquer origem.
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            try
            {            
                // extrai o usuario e senha do contexto da requisição
                var usuario = context.UserName;
                var senha = context.Password; 
                //implementação de autenticação do usuario, aqui se verifica se o usuario existe.
                if (usuario != "apedroed88" || senha != "apedroed88")
                {
                    context.SetError("invalid_grant","Usuario ou senha invalido");
                    return;
                }
            
                //Claims server para nomedar items da identificação do usuario
                //adiciono um claims no nome do usuario para poder reduperar no controler com User.Identity.Name
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name,usuario));

                // os roles(papeis) devem ser chamados do banco onde deve haver uma tabela com idusuario e idRole que mostra o papel do usuario no sistema
                var roles = new List<string>();
                roles.Add("User");

                foreach (var role in roles) 
                {
                    //em um projeto completo devese adicionar o role conforme role do usuario no banco;
                    identity.AddClaim(new Claim(ClaimTypes.Role,role));
                }
                GenericPrincipal principal = new GenericPrincipal(identity,roles.ToArray());
                Thread.CurrentPrincipal = principal;

                context.Validated(identity);
            }
            catch (Exception)
            {
                context.SetError("invalid_grant","Falha ao autenticar");
            }
        }
    }
}