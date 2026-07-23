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
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            var dtos = _mapper.Map<List<CategoryDTO>>(categories);
            return Ok(new ServerResponse<List<CategoryDTO>>(dtos));
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Categories>(categoryDto);
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.SaveChanges();
            categoryDto.ID = category.ID;
            return Ok(new ServerResponse<CategoryDTO>(categoryDto, "Category added successfully."));
        }

        [HttpPut]
        public IActionResult UpdateCategory([FromBody] CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Categories>(categoryDto);
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.SaveChanges();
            return Ok(new ServerResponse<CategoryDTO>(categoryDto, "Category updated successfully."));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category != null)
            {
                _unitOfWork.CategoryRepository.Delete(category);
                _unitOfWork.SaveChanges();
                return Ok(new ServerResponse<bool>(true, "Category deleted successfully."));
            }
            return NotFound(new ServerResponse<bool>(false, "Not found"));
        }
    }
}
