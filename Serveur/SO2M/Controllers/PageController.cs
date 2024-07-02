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
    public class PageController : Controller
    {
        private readonly DataContext ctx;
        public PageController(DataContext context)
        {
            ctx = context;
        }


        //--- CI-BAS LES ROUTES 



        [HttpGet("/page/recherchePartenaire")]
        public IActionResult recherchePartenaire()
        {
            return Redirect("~/view/recherche/recherchePartenaire.html");
        }

        [HttpGet("/page/listeProfilsPublics")]
        public IActionResult listeProfilsPublics()
        {
            return Redirect("~/view/recherche/listeProfilsPublics.html");
        }

        [HttpGet("/page/login")]
        public IActionResult login()
        {
            return Redirect("~/view/wwwroot/login.html");
        }

        [HttpGet("/page/inscription")]
        public IActionResult inscription()
        {
            return Redirect("~/view/wwwroot/inscription.html");
        }

        [HttpGet("/page/photos")]
        public IActionResult photos()
        {
            return Redirect("~/view/photos/photos.html");
        }

       [HttpGet("/page/modifierProfil")]
        public IActionResult modifierProfil()
        {
            return Redirect("~/view/parametre/modifierProfil.html");
        }

        [HttpGet("/page/matchs")]
        public IActionResult accueilProfil()
        {
            return Redirect("~/view/matchs/matchs.html");
        }

        [HttpGet("/page/accueil")]
        public IActionResult accueil()
        {
            return Redirect("~/view/accueil/accueil.html");
        }

        [HttpGet("/page/favoris")]
        public IActionResult favoris()
        {
            return Redirect("~/view/favoris/favoris.html");
        }

        [HttpGet("/page/messagerie")]
        public IActionResult messagerie()
        {
            return Redirect("~/view/messagerie/messagerie.html");
        }

        [HttpGet("/page/swagger")]
        public IActionResult swagger()
        {
            return Redirect("/swagger/index.html");
        }

        [HttpGet("/page/galerieProfils")]
        public IActionResult galerieProfils()
        {
            return Redirect("~/view/galerieProfils/galerieProfils.html");
        }

        [HttpGet("/page/profilPublic")]
        public IActionResult profilPublic()
        {
            return Redirect("~/view/profilPublic/profilPublic.html");
        }

    }
}







