using Microsoft.AspNetCore.Mvc;
using SO2M.Data;
using SO2M.Models;
using System.Data;


namespace SO2M.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class QuestionnaireController : Controller
    {

      
        private readonly DataContext ctx;
        public QuestionnaireController(DataContext context)
        {
            ctx = context;
        }



        /// <summary>
        /// Retourne l'utilisateur en cours -- donc également les Scores au Modèle psycho
        /// </summary>
        [HttpGet("/api/questionnaire/getScoresModele1")]
        public async Task<ActionResult<List<Utilisateur>>> getScoresModele1()
        {
            //   http://localhost:5160/View/questionnaire/questionnaire.html

            try
            {
                var userIdString = HttpContext.Session.GetString("UserId");
                int userIdInt = Int32.Parse(userIdString);
                var utilis = await ctx.Utilisateurs.FindAsync(userIdInt);
                Console.WriteLine("Id utilisateur connecté:  " + userIdString);
                return Ok(utilis);
            }
            catch (Exception ex)
            {
                string errMsg = "Utilisateur non-connecté ou non-existant. Erreur 'attrapée' et anticipée. Détails erreur: " + ex.GetType().Name + "\n" + ex.Message;
                Console.WriteLine(errMsg);
                return NotFound(errMsg);
             }
        }




        /// <summary>
        /// Retourne les Scores au backend pour stockage dans la bd
        /// </summary>
        [HttpPost("/api/questionnaire/setScoresModele1")]
        public async Task<ActionResult<List<Utilisateur>>> setScoresModele1([FromForm] Utilisateur userFromFrontend)
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
                string errMsg = "Utilisateur non-connecté ou non-existant. Erreur 'attrapée' et anticipée. Détails erreur: " + ex.GetType().Name + "\n" + ex.Message;
                Console.WriteLine(errMsg);
                return NotFound(errMsg);
            }

            currentUser.Modele1Axe1 = userFromFrontend.Modele1Axe1;
            currentUser.Modele1Axe2 = userFromFrontend.Modele1Axe2;
            currentUser.Modele1Axe3 = userFromFrontend.Modele1Axe3;

            await ctx.SaveChangesAsync();

            return Ok(currentUser);
            
        }

    }
}







