using AutoMapper;
using Edtech_backend_API.DTOs;
using Edtech_backend_API.Model;
using Edtech_backend_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Controllers
{
    [Route("api/shoppingcart")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ShoppingCartController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ShoppingCartDto>> GetShoppingCarts()
        {
            var shoppingCarts = _unitOfWork.ShoppingCarts.GetAll(includeproperties: "Course");
            var shoppingCartsDto = _mapper.Map<IEnumerable<ShoppingCartDto>>(shoppingCarts);
            return Ok(shoppingCartsDto);
        }
        [HttpGet("{id}")]
        public ActionResult<ShoppingCartDto> GetShoppingCart(int id)
        {
            var shoppingCart = _unitOfWork.ShoppingCarts.Get(id);

            if (shoppingCart == null)
            {
                return NotFound();
            }

            var shoppingCartDto = _mapper.Map<ShoppingCartDto>(shoppingCart);
            return Ok(shoppingCartDto);
        }
        [HttpPost]
        public ActionResult<ShoppingCartDto> CreateShoppingCart(ShoppingCartDto shoppingCartDto)
        {
            if (shoppingCartDto == null)
            {
                return BadRequest("Invalid data");
            }

            var shoppingCart = _mapper.Map<ShoppingCart>(shoppingCartDto);
            _unitOfWork.ShoppingCarts.Add(shoppingCart);
            _unitOfWork.Save();

            shoppingCartDto = _mapper.Map<ShoppingCartDto>(shoppingCart);
            return CreatedAtAction(nameof(GetShoppingCart), new { id = shoppingCartDto.ShoppingCartId }, shoppingCartDto);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateShoppinCart(int id, ShoppingCartDto shoppingCartDto)
        {
            if(id!=shoppingCartDto.ShoppingCartId)
            {
                return BadRequest();
            }
            var cart = _mapper.Map<ShoppingCart>(shoppingCartDto);
            _unitOfWork.ShoppingCarts.Update(cart);
            _unitOfWork.Save();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteShoppingCart(int id)
        {
            var shoppingCart = _unitOfWork.ShoppingCarts.Get(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            _unitOfWork.ShoppingCarts.Remove(shoppingCart);
            _unitOfWork.Save();

            return NoContent();
        }

    }
}
