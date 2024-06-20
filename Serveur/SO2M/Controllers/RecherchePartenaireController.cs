using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SO2M.Data;
using SO2M.Models;


namespace SO2M.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RecherchePartenaireController : Controller
    {

        
        private readonly DataContext ctx;
        public RecherchePartenaireController(DataContext context)
        {
            ctx = context;
        }



        /// <summary>
        /// Appelé lorsque la page de Recherche s'ouvre afin de populer les champs
        /// </summary>
        [HttpGet("/api/recherchePart/getCriteres/{Id}")]
        public async Task<ActionResult<List<CritereRecherche>>> getCriteres(int Id)
        {
            // mesure temporaire pour tester:  Id = 1
            Id=1;
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


        /// <summary>
        /// Cette méthode est appelée lorsqu'une Recherche est lancée afin de stocker les Critères de
        /// Recherche pour la prochaine Recherche
        /// </summary>
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

                return Ok(user);
            
        }



        /// <summary>
        /// Retourne la liste des matchs quand une Recherche a été lancée (Le navigateur doit gèrer les 
        /// items vus et/ou à voir et soumettre le 1er et le dernier item qu'il veut recevoir (pas encore implémenté)
        /// --- à défaut de quoi la liste de tous les matchs est retournée)
        /// </summary>
        [HttpGet("/api/recherchePart/getListeProfilsPublics/{userId}")]
        public async Task<ActionResult<List<Utilisateur>>> getListeProfilsPublics(int userId, int firstItem, int lastItem)
        {
            // retourne liste de tous les Utilisateurs - temporaire pour tests frontend
            return Ok(await ctx.Utilisateurs.ToListAsync());

        }

 

        /// <summary>
        /// Retourne le profil public de l'Id soumis  (l'id doit être stocké dans le Storage du navigateur
        /// lorsque l'Utilisateur a choisi un Avatar à la page précédente [page Galerie] ).
        /// </summary>
        [HttpGet("/api/recherchePart/getProfilPublic/{idConnectedUser}")]
        public async Task<ActionResult<List<Utilisateur>>> getProfilPublic(int idConnectedUser, int idProfilChoisi)
        {
                var profil = await ctx.Utilisateurs.FindAsync(idProfilChoisi);
                if (profil is null)
                {
                    return NotFound("Utilisateur non-existant");
                }
            return Ok(profil);
        }

    }
}







