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
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAll();
            var dtos = _mapper.Map<List<ProductDTO>>(products);
            return Ok(new ServerResponse<List<ProductDTO>>(dtos));
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            if (product == null) return NotFound(new ServerResponse<ProductDTO>("Not found", false));
            var dto = _mapper.Map<ProductDTO>(product);
            return Ok(new ServerResponse<ProductDTO>(dto));
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductDTO productDto)
        {
            var product = _mapper.Map<Products>(productDto);
            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.SaveChanges();
            productDto.ID = product.ID;
            return Ok(new ServerResponse<ProductDTO>(productDto, "Product added successfully."));
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody] ProductDTO productDto)
        {
            var product = _mapper.Map<Products>(productDto);
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.SaveChanges();
            return Ok(new ServerResponse<ProductDTO>(productDto, "Product updated successfully."));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            if (product != null)
            {
                _unitOfWork.ProductRepository.Delete(product);
                _unitOfWork.SaveChanges();
                return Ok(new ServerResponse<bool>(true, "Product deleted successfully."));
            }
            return NotFound(new ServerResponse<bool>(false, "Product not found."));
        }
        
        [HttpGet("search")]
        public IActionResult SearchProducts([FromQuery] string keyword)
        {
            var products = _unitOfWork.ProductRepository.GetByName(keyword);
            var dtos = _mapper.Map<List<ProductDTO>>(products);
            return Ok(new ServerResponse<List<ProductDTO>>(dtos));
        }
    }
}
