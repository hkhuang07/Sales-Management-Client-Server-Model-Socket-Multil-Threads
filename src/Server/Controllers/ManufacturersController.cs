using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManufacturersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManufacturersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetManufacturers()
        {
            var manufacturers = _unitOfWork.ManufacturerRepository.GetAll();
            var dtos = _mapper.Map<List<ManufacturerDTO>>(manufacturers);
            return Ok(new ServerResponse<List<ManufacturerDTO>>(dtos));
        }
        
        [HttpPost]
        public IActionResult AddManufacturer([FromBody] ManufacturerDTO manufacturerDto)
        {
            var manufacturer = _mapper.Map<Manufacturers>(manufacturerDto);
            _unitOfWork.ManufacturerRepository.Add(manufacturer);
            _unitOfWork.SaveChanges();
            manufacturerDto.ID = manufacturer.ID;
            return Ok(new ServerResponse<ManufacturerDTO>(manufacturerDto, "Added successfully."));
        }

        [HttpPut]
        public IActionResult UpdateManufacturer([FromBody] ManufacturerDTO manufacturerDto)
        {
            var manufacturer = _mapper.Map<Manufacturers>(manufacturerDto);
            _unitOfWork.ManufacturerRepository.Update(manufacturer);
            _unitOfWork.SaveChanges();
            return Ok(new ServerResponse<ManufacturerDTO>(manufacturerDto, "Updated successfully."));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteManufacturer(int id)
        {
            var manufacturer = _unitOfWork.ManufacturerRepository.GetById(id);
            if (manufacturer != null)
            {
                _unitOfWork.ManufacturerRepository.Delete(manufacturer);
                _unitOfWork.SaveChanges();
                return Ok(new ServerResponse<bool>(true, "Deleted successfully."));
            }
            return NotFound(new ServerResponse<bool>(false, "Not found"));
        }
    }
}
