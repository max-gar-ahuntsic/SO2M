using Microsoft.AspNetCore.Mvc;
using SO2M.Data;
using SO2M.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace SO2M.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametreController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;

        public ParametreController(DataContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
                utilisateur.Photo1_data // assuming it's base64 encoded
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
            utilisateur.Username = updatedUtilisateur.Username;
            utilisateur.Age = updatedUtilisateur.Age;
            utilisateur.Genre = updatedUtilisateur.Genre;
            utilisateur.NiveauAcademique = updatedUtilisateur.NiveauAcademique;
            utilisateur.OrientationS = updatedUtilisateur.OrientationS;
            utilisateur.Courriel = updatedUtilisateur.Courriel;

            if (!string.IsNullOrEmpty(updatedUtilisateur.MotDePasse))
            {
                utilisateur.MotDePasse = HashPassword(updatedUtilisateur.MotDePasse);
            }

            if (updatedUtilisateur.ProfilePhoto != null && updatedUtilisateur.ProfilePhoto.Length > 0)
            {
                using var ms = new MemoryStream();
                await updatedUtilisateur.ProfilePhoto.CopyToAsync(ms);
                byte[] photoArray = ms.ToArray();
                string photoBase64 = Convert.ToBase64String(photoArray);
                utilisateur.Photo1_data = photoBase64;
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

            return Ok(new { message = "Profil mis à jour avec succès" });
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

        // Méthode pour vérifier si le mot de passe est valide
        private bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasUpperChar = false, hasLowerChar = false, hasDigit = false, hasSpecialChar = false;
            foreach (var ch in password)
            {
                if (char.IsUpper(ch))
                    hasUpperChar = true;
                else if (char.IsLower(ch))
                    hasLowerChar = true;
                else if (char.IsDigit(ch))
                    hasDigit = true;
                else if (!char.IsLetterOrDigit(ch))
                    hasSpecialChar = true;
            }

            return hasUpperChar && hasLowerChar && hasDigit && hasSpecialChar;
        }
    }
}