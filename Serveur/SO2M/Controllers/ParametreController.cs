using Microsoft.AspNetCore.Mvc;
using SO2M.Data;
using SO2M.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SO2M.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametreController : ControllerBase
    {
        private readonly DataContext _context;

        public ParametreController(DataContext context)
        {
            _context = context;
        }

        // Méthode pour récupérer les informations du profil de l'utilisateur connecté
        [HttpGet("profil")]
        public IActionResult GetProfilUtilisateur()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (userIdString == null)
            {
                Console.WriteLine("Utilisateur non connecté.");
                return Unauthorized("Utilisateur non connecté.");
            }

            Console.WriteLine("ID utilisateur dans la session : " + userIdString);

            if (!int.TryParse(userIdString, out int userId))
            {
                Console.WriteLine("ID utilisateur invalide.");
                return BadRequest("ID utilisateur invalide.");
            }

            var utilisateur = _context.Utilisateurs.Find(userId);
            if (utilisateur == null)
            {
                Console.WriteLine("Utilisateur non trouvé.");
                return NotFound("Utilisateur non trouvé.");
            }

            Console.WriteLine("Utilisateur trouvé : " + utilisateur.Username);
            return Ok(new
            {
                utilisateur.Nom,
                utilisateur.Prenom,
                utilisateur.Username,
                utilisateur.Age,
                utilisateur.Genre,
                utilisateur.NiveauAcademique,
                utilisateur.OrientationS,
                utilisateur.Courriel,
                utilisateur.ProfilePhoto,
                Photo1_data = utilisateur.Photo1_data // assuming it's base64 encoded
            });
        }

        // Méthode pour mettre à jour les informations du profil de l'utilisateur connecté
        [HttpPut("modifierProfil")]
        public async Task<IActionResult> ModifierProfil([FromForm] Utilisateur updatedUtilisateur)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (userIdString == null)
            {
                return Unauthorized("Utilisateur non connecté.");
            }

            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest("ID utilisateur invalide.");
            }

            var utilisateur = await _context.Utilisateurs.FindAsync(userId);
            if (utilisateur == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            // Mise à jour des informations de l'utilisateur connecté
            utilisateur.Nom = updatedUtilisateur.Nom;
            utilisateur.Prenom = updatedUtilisateur.Prenom;
            utilisateur.Genre = updatedUtilisateur.Genre;
            utilisateur.NiveauAcademique = updatedUtilisateur.NiveauAcademique;
            utilisateur.Age = updatedUtilisateur.Age;
            utilisateur.OrientationS = updatedUtilisateur.OrientationS;
            utilisateur.Courriel = updatedUtilisateur.Courriel;
            utilisateur.Username = updatedUtilisateur.Username;
            utilisateur.MotDePasse = HashPassword(updatedUtilisateur.MotDePasse);
            utilisateur.Photo1_data = updatedUtilisateur.Photo1_data;

            if (!string.IsNullOrEmpty(updatedUtilisateur.MotDePasse))
            {
                utilisateur.MotDePasse = HashPassword(updatedUtilisateur.MotDePasse);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log l'erreur pour le débogage
                Console.WriteLine($"Erreur lors de la mise à jour du profil : {ex.Message}");
                return StatusCode(500, "Erreur interne du serveur. Veuillez réessayer plus tard.");
            }

            return NoContent();
        }

        // Méthode pour hasher le mot de passe
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
