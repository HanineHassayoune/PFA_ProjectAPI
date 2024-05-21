using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PFA_ProjectAPI.Repositories
{
    public class TokenRepository:ITokenRepository
    {
        private readonly IConfiguration configuration;
        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string CreateJWTToken(IdentityUser user , List<string>roles)
        {
            //Create claims (des reclamations)
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

           // Pour chaque rôle dans la liste des rôles, une réclamation de type Role est ajoutée à la liste des réclamations.
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));   
            }

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            //un algorithme de hachage
            var credentials =new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"], //L'émetteur du jeton 
                configuration["Jwt:Audience"], //Le destinataire(public) du jeton
                claims,
                expires: DateTime.Now.AddMinutes(15), //La date d'expiration du jeton (15 minutes à partir de maintenant)
                signingCredentials: credentials); 

            return new JwtSecurityTokenHandler().WriteToken(token); //retourner un string of JWT token securisé(JSON Web Tokens)  
        }

    }
}
