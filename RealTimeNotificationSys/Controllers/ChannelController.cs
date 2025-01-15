using Microsoft.AspNetCore.Mvc;
using RealTimeNotificationSys.Core.Dto;
using RealTimeNotificationSys.Core.Interfaces;
using RealTimeNotificationSys.Core.Services;
using System.Threading.Tasks;



namespace RealTimeNotificationSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelService _channelService;

        public ChannelController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscribeDto subscribeDto)
        {
            try
            {
                await _channelService.SubscribeUserToChannelAsync(subscribeDto.UserId, subscribeDto.ChannelIds);
                return Ok(new { message = "Subscription successful!" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
