using KafkaProducer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafkaProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProduceMessage _produceMessage;
        public ProducerController(IProduceMessage produceMessage)
        {
            _produceMessage = produceMessage;
        }

        [HttpGet]
        [Route("test-producer")]
        public async Task<IActionResult> TestProducer()
        {
            _produceMessage.ProduceAsync().Wait();

            return Ok();
        }
    }
}
