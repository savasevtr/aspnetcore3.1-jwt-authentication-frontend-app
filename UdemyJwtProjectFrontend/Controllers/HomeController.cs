using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UdemyJwtProjectFrontend.ApiServices.Interfaces;
using UdemyJwtProjectFrontend.CustomFilters;
using UdemyJwtProjectFrontend.Models;

namespace UdemyJwtProjectFrontend.Controllers
{
    [JwtAuthorize]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        [JwtAuthorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllAsync());
        }

        [JwtAuthorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductAdd productAdd)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(productAdd);

                return RedirectToAction("Index", "Home");
            }

            return View(productAdd);
        }

        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _productService.GetByIdAsync(id));
        }

        [HttpPost]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(ProductList product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(product);

                return RedirectToAction("Index", "Home");
            }

            return View(product);
        }

        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);

            return RedirectToAction("Index", "Home");
        }
    }
}