﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BPA.BusinessObject.Entities;
using BPA.DAO.Context;
using BPA.Service.IServices;
using BPA.Service.Services;
using Microsoft.AspNetCore.Authorization;
using BPA.BusinessObject.Dtos.Account;
using BPA.BusinessObject.Enums;
using BPA.BusinessObject.Dtos.Product;
using Microsoft.IdentityModel.Tokens;

namespace BPA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public IActionResult GetAllProducts()
        {
            try
            {
                var list = _productService.GetAll().Where(x => x.is_deleted == false).ToList();
                if (!list.Any())
                {
                    return NotFound("No Data");
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllByBrand")]
        [AllowAnonymous]
        public IActionResult GetAllProductsByBrandId(Guid id)
        {
            try
            {
                var list = _productService.GetAll().Where(x => x.BrandId == id && x.is_deleted == false).ToList();
                if (!list.Any())
                {
                    return NotFound("No Data");
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById")]
        [AllowAnonymous]
        public IActionResult GetProductById(Guid id)
        {
            try
            {
                var product = _productService.GetById(id);
                if (product == null || product.is_deleted == true)
                {
                    return NotFound("Cannot Find id");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Search")]
        [AllowAnonymous]
        public IActionResult SearchAccountByName(string input)
        {
            try
            {
                var listByName = _productService.GetAll().Where(x => x.ProductName!.Contains(input, StringComparison.OrdinalIgnoreCase) && x.is_deleted == false).ToList();
                IList<Product> list = new List<Product>();
                if (!listByName.Any())
                {
                    return NotFound("Cannot Find Product");
                }
                else if (listByName.Any())
                {
                    list = listByName;
                }
                else
                {
                    list = _productService.GetAll().Where(x => x.is_deleted == false).ToList();
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        //[Authorize(Roles = "staff")]
        public IActionResult CreateProduct(ProductRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Input");
                }
                var listByName = _productService.GetAll().Where(x => x.ProductName!.Equals(request.ProductName) && x.is_deleted == false);
                if (listByName.Any())
                {
                    return BadRequest("Product With Name " + request.ProductName + " Already Exist");
                }

                var newProduct = new Product
                {
                    ProductName = request.ProductName,
                    Price = request.Price,
                    Quantity = request.Quantity,
                    Description = request.Description,
                    BrandId = request.BrandId,
                    Status = ProductStatus.InStock,
                    created_on = DateTime.Now,
                    is_deleted = false,
                };
                _productService.Add(newProduct);

                return Ok("Add Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        //[Authorize(Roles = "staff")]
        public IActionResult UpdateProduct([FromRoute] Guid id, ProductRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Input");
                }
                var foundProduct = _productService.GetById(id);
                if (foundProduct == null || foundProduct.is_deleted == true)
                {
                    return NotFound("Cannot Find Product");
                }

                var existingProductByName = _productService.GetAll().FirstOrDefault(x => x.ProductName!.Equals(request.ProductName) && x.is_deleted == false);

                if (request.ProductName != null && !request.ProductName.Equals(foundProduct.ProductName))
                {
                    if (existingProductByName != null)
                        return BadRequest("Name Is Already Used");
                    foundProduct.ProductName = request.ProductName;
                }
                foundProduct.Price = request.Price;
                foundProduct.Quantity = request.Quantity;
                foundProduct.Description = request.Description ?? foundProduct.Description;

                _productService.Update(foundProduct);

                return Ok("Update Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ChangeStatus/{id}")]
        //[Authorize(Roles = "staff")]
        public IActionResult ChangeStatus(Guid id)
        {
            try
            {
                var foundProduct = _productService.GetById(id);
                if (foundProduct == null || foundProduct.is_deleted == true)
                {
                    return NotFound("Cannot Find Product");
                }
                switch (foundProduct.Status)
                {
                    case ProductStatus.InStock:
                        foundProduct.Status = ProductStatus.OutOfStock;
                        break;
                    case ProductStatus.OutOfStock:
                        foundProduct.Status = ProductStatus.InStock;
                        break;
                    default:
                        foundProduct.Status = ProductStatus.OutOfStock;
                        break;
                }
                _productService.Update(foundProduct);

                return Ok("Change Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Delete/{id}")]
        //[Authorize(Roles = "staff")]
        public IActionResult DeleteProduct(Guid id)
        {
            try
            {
                var foundProduct = _productService.GetById(id);
                //var hasOrder = _productService.GetAll().Where(x => x.BrandId == id && x.is_deleted == false);
                if (foundProduct == null || foundProduct.is_deleted == true)
                {
                    return NotFound("Cannot Find Product");
                }
                //if (hasProduct.IsNullOrEmpty())
                //{
                foundProduct.is_deleted = true;
                _productService.Update(foundProduct);
                return Ok("Delete Successfully");
                //}
                //return BadRequest("Cannot Delete Brand");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
