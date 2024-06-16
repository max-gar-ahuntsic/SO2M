using Microsoft.AspNetCore.Mvc;
using SO2M.Data;
using SO2M.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SO2M.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : Controller
    {

     
        
        public So2msContext _context = new So2msContext();

     

        [HttpPost("inscription")]
        public async Task<IActionResult> Inscription([FromForm] Utilisateur utilisateur)
        {

            
            if (_context.Utilisateurs.Any(u => u.Username == utilisateur.Username))
            {
                return BadRequest("Le nom d'utilisateur est déjà pris.");
            }
            

            utilisateur.DateCreationProfil = DateOnly.FromDateTime(DateTime.Now);
            utilisateur.EstActive = true;

            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return Ok("Utilisateur créé avec succès");
        }

        // Ajout de la méthode de connexion
        [HttpPost("login")]
        public IActionResult Login([FromForm] string username, [FromForm] string motDePasse)
        {
            
            var utilisateur = _context.Utilisateurs.SingleOrDefault(u => u.Username == username && u.MotDePasse == motDePasse);
            if (utilisateur == null)
            {
                return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
            }
            
            return Ok("Connexion réussie");
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
            utilisateur.MotDePasse = updatedUtilisateur.MotDePasse;
            utilisateur.EstActive = updatedUtilisateur.EstActive;
            utilisateur.CritereRechercheId = updatedUtilisateur.CritereRechercheId;

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
