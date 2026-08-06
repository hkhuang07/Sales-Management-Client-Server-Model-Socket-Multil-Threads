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

        [HttpPost("upload-image")]
        public IActionResult UploadProductImage([FromBody] ProductImageUploadDTO dto)
        {
            if (dto == null || dto.ImageData == null || dto.ImageData.Length == 0)
            {
                return BadRequest(new ServerResponse<bool>(false, "Invalid image data."));
            }

            try
            {
                string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                string safeFileName = string.IsNullOrWhiteSpace(dto.FileName) ? $"product_{dto.ProductID}.jpg" : Path.GetFileName(dto.FileName);
                string filePath = Path.Combine(imagesFolder, safeFileName);
                System.IO.File.WriteAllBytes(filePath, dto.ImageData);

                var product = _unitOfWork.ProductRepository.GetById(dto.ProductID);
                if (product != null)
                {
                    product.Image = safeFileName;
                    _unitOfWork.ProductRepository.Update(product);
                    _unitOfWork.SaveChanges();
                }

                return Ok(new ServerResponse<bool>(true, "Image uploaded successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServerResponse<bool>(false, $"Image upload failed: {ex.Message}"));
            }
        }

        [HttpGet("images/{fileName}")]
        [AllowAnonymous]
        public IActionResult GetProductImage(string fileName)
        {
            string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            string filePath = Path.Combine(imagesFolder, Path.GetFileName(fileName));

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "image/jpeg");
        }
    }
}
