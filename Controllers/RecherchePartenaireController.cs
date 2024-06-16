using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SO2M.Data;
using SO2M.Models;
using System.Net.Http;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration.UserSecrets;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.IO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace SO2M.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RecherchePartenaireController : Controller
    {
        public So2msContext ctx = new So2msContext();



        [HttpGet("/api/recherchePart/getCriteresPageInit")]
        public IActionResult pageRechercheLoadInit()
        {
            return Redirect("~/view/recherche/recherchePartenaire.html");
        }

        [HttpGet("/api/recherchePart/test")]
        public async Task<ActionResult<List<CritereRecherche>>> getCriteres()
        {
            try
            {
                var user = await ctx.Utilisateurs.FindAsync(1);
     

                return Ok(user);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.GetType().Name + "\n" + ex.Message + "\n" + new StringReader(ex.InnerException.ToString()).ReadLine());
            }

        }

        [HttpGet("/api/recherchePart/getCriteres/{Id}")]
        public async Task<ActionResult<List<CritereRecherche>>> getCriteres(int Id)
        {
            try
            {
                var user = await ctx.Utilisateurs.FindAsync(Id);
                int? critRechId = user.CritereRechercheId;
                var cr = await ctx.CritereRecherches.FindAsync(critRechId);

                return Ok(cr);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.GetType().Name + "\n" + ex.Message + "\n" + new StringReader(ex.InnerException.ToString()).ReadLine());
            }

        }


 
        [HttpPost("/api/recherchePart/modifierCriteres/{userId}")]
        public async Task<ActionResult<List<Utilisateur>>> modifierCriteres(int userId, [FromForm] CritereRechercheAsAReturnObject newCriteres)
        {
          

                var user = await ctx.Utilisateurs.FindAsync(userId);
                if (user is null)
                {
                    return NotFound("Utilisateur non-existant");
                }
                int? critRechId = user.CritereRechercheId;
                var cr = await ctx.CritereRecherches.FindAsync(critRechId);


                cr.AgeMin = newCriteres.AgeMin;
                cr.AgeMax = newCriteres.AgeMax;
                cr.NiveauAcademique = newCriteres.NiveauAcademique;
                cr.Genre = newCriteres.Genre;
                cr.OrientationS = newCriteres.OrientationS;
                cr.Ville = newCriteres.Ville;
                cr.RayonRecherche = newCriteres.RayonRecherche;


                await ctx.SaveChangesAsync();

                return Ok();
            
        }




        [HttpGet("/api/recherchePart/getListeProfilsPublics/{userId}")]
        public async Task<ActionResult<List<Utilisateur>>> getListeProfilsPublics(int userId, [FromForm] Utilisateur listeMatchs)
        {

            // retourne liste de tous les Utilisateurs - temporaire pour tests frontend
            return Ok(await ctx.Utilisateurs.ToListAsync());

        }

        [HttpGet("/api/recherchePart/setChosenProfilPublic/{idConnectedUser}")]
        public async Task<ActionResult<List<Utilisateur>>> setChosenProfilPublic(int idConnectedUser, int idProfilChoisi)
        {
            //à faire
   
            return Ok();

        }



        [HttpGet("/api/recherchePart/getProfilPublic/{idConnectedUser}")]
        public async Task<ActionResult<List<Utilisateur>>> getProfilPublic(int idConnectedUser, int idProfilChoisi)
        {
 
                var profil = await ctx.Utilisateurs.FindAsync(idProfilChoisi);
                if (profil is null)
                {
                    return NotFound("Utilisateur non-existant");
                }
            // retourne un Utilisateur --  temporaire pour tester le frontend
            return Ok(profil);

        }




    }
}







