using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SO2M.Data;
using SO2M.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SO2M.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<MessageController> _logger;

        public MessageController(DataContext context, ILogger<MessageController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("envoyer")]
        public async Task<ActionResult<Message>> EnvoyerMessage([FromBody] Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return Ok(message);
        }

        [HttpGet("boite_reception/{utilisateurId}")]
        public async Task<ActionResult> BoiteDeReception(int utilisateurId)
        {
            var messages = await _context.Messages
                .Where(m => m.DestinataireId == utilisateurId)
                .OrderByDescending(m => m.DateEnvoyé)
                .ToListAsync();
            return Ok(messages);
        }

        [HttpGet("conversation/{expéditeurId}/{destinataireId}")]
        public async Task<ActionResult> ObtenirConversation(int expéditeurId, int destinataireId)
        {
            var messages = await _context.Messages
                .Where(m => (m.ExpéditeurId == expéditeurId && m.DestinataireId == destinataireId) || (m.ExpéditeurId == destinataireId && m.DestinataireId == expéditeurId))
                .OrderBy(m => m.DateEnvoyé)
                .ToListAsync();
            return Ok(messages);
        }
    }
}
