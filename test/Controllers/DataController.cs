using Microsoft.AspNetCore.Mvc;
using test.Services;

namespace test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly DataMergeService _dataMergeService;

        public DataController(DataMergeService dataMergeService)
        {
            _dataMergeService = dataMergeService;
        }

        [HttpGet("fetch-save")]
        public async Task<IActionResult> FetchAndSaveData()
        {
            try
            {
                await _dataMergeService.SaveFetchedDataAsync();
                return Ok(new { message = "Data fetched and saved into separate fields successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
