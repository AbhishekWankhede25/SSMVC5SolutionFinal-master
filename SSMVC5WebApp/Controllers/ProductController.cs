using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

using SSMVC5WebApp.Infrastructure.Abstract;
using SSMVC5WebApp.Infrastructure.Entities;

namespace SSMVC5WebApp.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository _productRespository;
        IPhotoService _photoService;

        public ProductController(IProductRepository productRepository, IPhotoService photoService)
        {
            _productRespository = productRepository;
            _photoService = photoService;
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "ProductName, Description, Price, Category, PhotoUrl")] Product product, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                product.PhotoUrl = await _photoService.UploadPhotoAsync(product.Category, photo);
                await _productRespository.CreateAsync(product);
                return View("Success");
            }
            return View(product);
        }

        public ActionResult Success()
        {
            return View();
        }

        public async Task<ActionResult> Index()
        {
            List<Product> result = await _productRespository.GetAllProductsAsync();
            return View(result);
        }

        public async Task<ActionResult> GetByCategory(string category)
        {
            ViewBag.category = category;
            var result = await _productRespository.FindProductsByCategoryAsync(category);
            return View(result);
        }

        public async Task<ActionResult> Edit(int productId)
        {
            var result = await _productRespository.FindProductByIDAsync(productId);
            return View(result);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Product product, HttpPostedFileBase photo)
        {
            if (photo == null) { }
            else
            {
                if (await _photoService.DeletePhotoAsync(product.Category, product.PhotoUrl))
                {
                    product.PhotoUrl = await _photoService.UploadPhotoAsync(product.Category, photo);
                }
            }
            await _productRespository.UpdateAsync(product);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int productId)
        {
            var result = await _productRespository.FindProductByIDAsync(productId);
            if (await _photoService.DeletePhotoAsync(result.Category, result.PhotoUrl))
            {
                await _productRespository.DeleteAsync(productId);
            }
            return RedirectToAction("Index");
        }

    }
}