﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ActionFilters;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("/{id}")]
        [ServiceFilter(typeof(LoginFilter))]
        [ClaimRequirementFilter(Roles = new string[] { "Admin", "SuperAdmin" })]
        public async Task<IActionResult> GetCartByUserId(int id)
        {
            ResponseDataDTO<Cart> result = await _cartService.GetByUserId(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
