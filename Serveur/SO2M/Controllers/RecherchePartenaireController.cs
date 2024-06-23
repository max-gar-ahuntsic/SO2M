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
        [HttpGet("/api/recherchePart/getCriteres")]
        public async Task<ActionResult<List<CritereRecherche>>> getCriteres()
        {
            Utilisateur currentUser;
            try
            {
                var userIdString = HttpContext.Session.GetString("UserId");
                int userIdInt = Int32.Parse(userIdString);
                currentUser = await ctx.Utilisateurs.FindAsync(userIdInt);
                Console.WriteLine("Id utilisateur connecté:  " + userIdString);
            }
            catch (Exception ex)
            {
                string errMsg = "Utilisateur non-connecté ou non-existant. Erreur 'attrapée. Détails erreur: " + ex.GetType().Name + "\n" + ex.Message;
                Console.WriteLine(errMsg);
                return NotFound(errMsg);
            }

            Utilisateur user = currentUser;

            try
            {
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
        [HttpPost("/api/recherchePart/modifierCriteres")]
        public async Task<ActionResult<List<Utilisateur>>> modifierCriteres(int userId, [FromForm] CritereRechercheAsAReturnObject newCriteres)
        {

                Utilisateur currentUser;
                try
                {
                    var userIdString = HttpContext.Session.GetString("UserId");
                    int userIdInt = Int32.Parse(userIdString);
                    currentUser = await ctx.Utilisateurs.FindAsync(userIdInt);
                    Console.WriteLine("Id utilisateur connecté:  " + userIdString);
                }
                catch (Exception ex)
                {
                    string errMsg = "Utilisateur non-connecté ou non-existant. Erreur 'attrapée. Détails erreur: " + ex.GetType().Name + "\n" + ex.Message;
                    Console.WriteLine(errMsg);
                    return NotFound(errMsg);
                }

                Utilisateur user = currentUser;
                CritereRecherche cr;

                if (user.CritereRechercheId == null)
                {
                    user.CritereRecherche = new CritereRecherche();
                    cr = user.CritereRecherche;
                    user.CritereRechercheId = cr.Id;
                }
                else
                {
                    int? critRechId = user.CritereRechercheId;
                    cr = await ctx.CritereRecherches.FindAsync(critRechId);
                }
 

                cr.AgeMin = newCriteres.AgeMin;
                cr.AgeMax = newCriteres.AgeMax;
                cr.NiveauAcademique = newCriteres.NiveauAcademique;
                cr.Genre = newCriteres.Genre;
                cr.OrientationS = newCriteres.OrientationS;
                //cr.Ville = newCriteres.Ville;
                //cr.RayonRecherche = newCriteres.RayonRecherche;

                await ctx.SaveChangesAsync();

                return Ok(user);
            
        }



        /// <summary>
        /// Appelé par la page Match à son ouverture; Retourne une Liste d'Utilisateurs -- du meilleur au moins bon match. 
        /// L'attribut 'MatchScore_ModelePsy1' informe sur le niveau de match suivant le modèle psycho
        /// </summary>
        [HttpGet("/api/recherchePart/getListeProfilsPublics")]
         public async Task<ActionResult<List<Utilisateur>>> getListeProfilsPublics()
        {

            Utilisateur currentUser;
            try
            {
                var userIdString = HttpContext.Session.GetString("UserId");
                int userIdInt = Int32.Parse(userIdString);
                currentUser = await ctx.Utilisateurs.FindAsync(userIdInt);
                Console.WriteLine("Id utilisateur connecté:  " + userIdString);
            }
            catch (Exception ex)
            {
                string errMsg = "Utilisateur non-connecté ou non-existant. Erreur 'attrapée. Détails erreur: " + ex.GetType().Name + "\n" + ex.Message;
                Console.WriteLine(errMsg);
                return NotFound(errMsg);
            }
            Utilisateur user = currentUser;
            CritereRecherche cr;

            int? critRechId = user.CritereRechercheId;
            cr = await ctx.CritereRecherches.FindAsync(critRechId);
            var listeUtilisateurs = ctx.Utilisateurs
                     .ToList();

            List<Utilisateur> listeMatchs;


            // - - - - - début application critères recherche


            //------- exclusion de l'Utilisateur qui fait la recherche des Résultats
            currentUser.MatchScore_Total = 0;
            Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

            foreach (var ut in listeUtilisateurs)
            {
                ut.MatchScore_Total = 0;

                //--------  critères "absolument" exclusifs (les profils qui ne répondent
                //... pas à ces critères sont totalement exclus des résultats -- eg: genre :
                //... --> on ne présentera pas des profils d'homme à un homme qui cherche femme)
                Console.WriteLine($"NOM: {ut.Prenom} {ut.Nom}");

                if (ut.Genre != cr.Genre)
                {
                    Console.WriteLine($"     mauvais genre ! {ut.Genre}, exclus !");
                }
                else
                {
                    Console.WriteLine($"     bon genre ! {ut.Genre}, inclus ! ");

                    //--------  critères "plutôt" exclusifs (les profils qui ne répondent
                    //... pas à ces critères seront présentés à la fin des résultats -- eg: Age :
                    //... --> si une Utilisatrice cherche un Homme entre 30 et 40, les hommes
                    //... de 20 ans ou de 50 ans lui seront présentés à la toute fin)

                    //------ critere Orientation S
                    if (ut.OrientationS == cr.OrientationS)
                    {
                        ut.MatchScore_Total += 80;
                        Console.WriteLine($"     bonne orientation sexuelle! {cr.OrientationS}, 80 points!");
                    }

                    //------ critère Age
                    if ((ut.Age > cr.AgeMin) && (ut.Age < cr.AgeMax)) {
                        ut.MatchScore_Total += 50;
                        Console.WriteLine($"     âge satisfait les critères! {ut.Age}, 50 points!");
                    }
                    //------ critère Age
                    if (ut.NiveauAcademique == cr.NiveauAcademique)
                    {
                        ut.MatchScore_Total += 30;
                        Console.WriteLine($"     match au niveau scolaire! {cr.NiveauAcademique}, 30 points!");
                    }
                    //------ calcul du Score au modèle psycho


                }

                Console.WriteLine($"     score total: {ut.MatchScore_Total} points !!!");

                Console.WriteLine("-----------------------------");


                  

            }
            Console.WriteLine($"<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");

 

            var result = await ctx.Utilisateurs.ToListAsync();
            result = result.OrderByDescending(p => p.MatchScore_Total).Where(p => p.MatchScore_Total != 0).ToList();

            return Ok(result);

 
        
       

        }

 

        /// <summary>
        /// Retourne le profil public de l'Id soumis  (l'id doit être stocké dans le Storage du navigateur
        /// lorsque l'Utilisateur a choisi un Avatar à la page précédente [page Galerie] ).
        /// </summary>
        [HttpGet("/api/recherchePart/getProfilPublic")]
        public async Task<ActionResult<List<Utilisateur>>> getProfilPublic(int idProfilChoisi)
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







