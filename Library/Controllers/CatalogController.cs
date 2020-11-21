using Library.Models.Catalog;
using LibraryDate;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {

        private ILibraryAsset _assets;
        public CatalogController(ILibraryAsset assets)
        {
            _assets = assets;
        }

        public IActionResult Index()
        {
            var assetModels = _assets.GetAll();

            var listingResult = assetModels
                .Select(r => new AssetIndexListingModel
                {
                    Id = r.Id,
                    ImageUrl = r.ImageUrl,
                    AuthorOrDirector = _assets.GetAuthorOrDirector(r.Id),
                    DeweyCallNumber = _assets.GetDeweyIndex(r.Id),
                    Title = r.Title,
                    Type = _assets.GetType(r.Id),

                });

            var model = new AssetIndexModel()
            {
                Assets = listingResult
            };

            return View(model);
        }
    }
}
