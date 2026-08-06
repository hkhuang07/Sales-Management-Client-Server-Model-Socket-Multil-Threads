using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DatabaseController : ControllerBase
    {
        private readonly DatabaseRepository _databaseRepository;

        public DatabaseController()
        {
            _databaseRepository = new DatabaseRepository();
        }

        [HttpPost("backup")]
        public IActionResult Backup([FromBody] string folderPath)
        {
            try
            {
                bool success = _databaseRepository.BackupDatabase(folderPath);
                if (success)
                {
                    return Ok(new ServerResponse<bool>(true, "Database backup completed successfully."));
                }
                return BadRequest(new ServerResponse<bool>(false, "Database backup failed."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServerResponse<bool>(false, $"Backup error: {ex.Message}"));
            }
        }

        [HttpPost("restore")]
        public IActionResult Restore([FromBody] string filePath)
        {
            try
            {
                bool success = _databaseRepository.RestoreDatabase(filePath);
                if (success)
                {
                    return Ok(new ServerResponse<bool>(true, "Database restore completed successfully."));
                }
                return BadRequest(new ServerResponse<bool>(false, "Database restore failed."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServerResponse<bool>(false, $"Restore error: {ex.Message}"));
            }
        }
    }
}
