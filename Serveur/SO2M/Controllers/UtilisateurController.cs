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
    public class UtilisateurController : ControllerBase
    {
        private readonly DataContext _context;

        public UtilisateurController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("inscription")]
        public async Task<IActionResult> Inscription([FromForm] Utilisateur utilisateur)
        {
            if (_context.Utilisateurs.Any(u => u.Username == utilisateur.Username))
            {
                return BadRequest("Le nom d'utilisateur est déjà pris.");
            }

            if (_context.Utilisateurs.Any(u => u.Courriel == utilisateur.Courriel))
            {
                return BadRequest("L'adresse email est déjà utilisée.");
            }

            if (utilisateur.Age < 18)
            {
                return BadRequest("L'utilisateur doit avoir au moins 18 ans.");
            }

            if (!IsValidPassword(utilisateur.MotDePasse))
            {
                return BadRequest("Le mot de passe doit contenir au moins 8 caractères, une majuscule, une minuscule, un chiffre et un caractère spécial.");
            }

            if (utilisateur.ProfilePhoto.Length > 700000)
            {
                Console.WriteLine("fic initial grosseur: " + utilisateur.ProfilePhoto.Length);
                return BadRequest("Le fichier est trop volumineux. 650kb maximum.");
            }
            else
            { 
                using var ms = new MemoryStream();
                await utilisateur.ProfilePhoto.CopyToAsync(ms);
                byte[] photoArray = ms.ToArray();
                string photoBase64 = Convert.ToBase64String(photoArray);
                utilisateur.Photo1_data = photoBase64;

                Console.WriteLine("fic initial grosseur: " + utilisateur.ProfilePhoto.Length);
                Console.WriteLine("grosseur base64: " + photoBase64.Length);
            }


            utilisateur.MotDePasse = HashPassword(utilisateur.MotDePasse);
            utilisateur.DateCreationProfil = DateOnly.FromDateTime(DateTime.Now);
            utilisateur.Est_Active = true;


            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return Ok("Utilisateur créé avec succès");
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

        // Ajout de la méthode de connexion
     [HttpPost("login")]
public IActionResult Login([FromForm] string username, [FromForm] string motDePasse)
{
    var hashedPassword = HashPassword(motDePasse);
    var utilisateur = _context.Utilisateurs.SingleOrDefault(u => u.Username == username && u.MotDePasse == hashedPassword);
    if (utilisateur == null)
    {
        return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
    }

    HttpContext.Session.SetString("UserId", utilisateur.Id.ToString());
    HttpContext.Session.SetString("Username", utilisateur.Username);

    Console.WriteLine($"Session ID après connexion: {HttpContext.Session.Id}");
    Console.WriteLine($"Utilisateur ID après connexion: {HttpContext.Session.GetString("UserId")}");

    return Ok(new { message = "Connexion réussie" });
}

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return Ok(new { message = "Déconnexion réussie" });
        }

        [HttpGet("current")]
        public IActionResult GetCurrentUser()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            Console.WriteLine($"Requête pour l'utilisateur actuel. Session ID: {HttpContext.Session.Id}");

            if (userIdString == null)
            {
                Console.WriteLine("Session invalide ou expirée.");
                return Unauthorized("Utilisateur non connecté.");
            }

            Console.WriteLine($"ID utilisateur dans la session : {userIdString}");

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

            Console.WriteLine($"Utilisateur trouvé : {utilisateur.Username}");
            return Ok(new
            {
                utilisateur.Username,
                ProfilePhotoURL = "/path/to/profile/photo.jpg" // Remplacez par la vraie URL de la photo de profil
            });
        }

        // Ajout d'autres méthodes CRUD pour les utilisateurs
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            return Ok(utilisateur);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUtilisateur(int id, [FromForm] Utilisateur updatedUtilisateur)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            utilisateur.Nom = updatedUtilisateur.Nom;
            utilisateur.Prenom = updatedUtilisateur.Prenom;
            utilisateur.Genre = updatedUtilisateur.Genre;
            utilisateur.NiveauAcademique = updatedUtilisateur.NiveauAcademique;
            utilisateur.Age = updatedUtilisateur.Age;
            utilisateur.OrientationS = updatedUtilisateur.OrientationS;
            utilisateur.Courriel = updatedUtilisateur.Courriel;
            utilisateur.Username = updatedUtilisateur.Username;
            utilisateur.MotDePasse = HashPassword(updatedUtilisateur.MotDePasse);
            utilisateur.Est_Active = updatedUtilisateur.Est_Active;
            utilisateur.CritereRechercheId = updatedUtilisateur.CritereRechercheId;
            utilisateur.ModelPsycologiqueId = updatedUtilisateur.ModelPsycologiqueId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

