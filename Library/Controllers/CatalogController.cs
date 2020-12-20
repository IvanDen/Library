using Library.Models.Catalog;
using Library.Models.CheckoutModels;
using LibraryDate;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
        #region private properties

        private ILibraryAsset _assets;
        private readonly ICheckout _checkouts;

        #endregion

        #region Constructor

        public CatalogController(ILibraryAsset assets, ICheckout checkouts)
        {
            _assets = assets;
            _checkouts = checkouts;
        }

        #endregion

        #region Actions
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

        public IActionResult Detail(int id)
        {
            var asset = _assets.GetById(id);

            var currentHolds = _checkouts.GetCurrentHolds(id)
                .Select(a => new AssetHoldModel
                {
                    HoldPlaced = _checkouts.GetCurrentHoldPlaced(a.Id).ToString("d"),
                    PatronName = _checkouts.GetCurrentHoldPatronName(a.Id)
                });

            var model = new AssetDetailModel
            {
                AssetId = id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDirector = _assets.GetAuthorOrDirector(id),
                CurrentLocation = _assets.GetCurrentLocation(id).Name,
                DeweyCallNumber = _assets.GetDeweyIndex(id),
                Dewey = _assets.GetDeweyIndex(id),
                CheckoutHistory = _checkouts.GetCheckoutHistories(id),
                ISBN = _assets.GetIsbn(id),
                LastCheckout = _checkouts.GetLatestCheckout(id),
                PatronName = _checkouts.GetCurrentCheckoutPatron(id),
                CurrentHolds = currentHolds
            };

            return View(model);
        }

        public IActionResult Checkout(int id)
        {
            var assets = _assets.GetById(id);

            var model = new CheckoutModel
            {
                AssetId = id,
                ImageUrl = assets.ImageUrl,
                Title = assets.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkouts.IsCheckedOut(id)
            };

            return View(model);
        }

        public IActionResult Checkin(int id)
        {
            _checkouts.CheckInItem(id);
            return RedirectToAction(ActionConstants.DETAIL, new { id });
        }

        public IActionResult Hold(int id)
        {
            var assets = _assets.GetById(id);

            var model = new CheckoutModel
            {
                AssetId = id,
                ImageUrl = assets.ImageUrl,
                Title = assets.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkouts.IsCheckedOut(id),
                HoldCount = _checkouts.GetCurrentHolds(id).Count()
            };

            return View(model);
        }


        public IActionResult MarkLost(int assetId)
        {
            _checkouts.MarkLost(assetId);
            return RedirectToAction(ActionConstants.DETAIL, new { id = assetId });
        }

        public IActionResult MarkFound(int assetId)
        {
            _checkouts.MarkFound(assetId);
            return RedirectToAction(ActionConstants.DETAIL, new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int libraryCardId)
        {
            _checkouts.CheckOutItem(assetId, libraryCardId);
            return RedirectToAction(ActionConstants.DETAIL, new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            _checkouts.PlaceHold(assetId, libraryCardId);
            return RedirectToAction(ActionConstants.DETAIL, new { id = assetId });
        }

        #endregion
    }
}
