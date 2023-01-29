using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaraStore.Infrastructure
{
    public class FavoriteViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            FavoritePageVM model = HttpContext.Session.Get<FavoritePageVM>("Favorite");
            FavoriteNumberVM fav;

            if (model is null)
            {
                fav = null;
            }
            else
            {
                fav = new FavoriteNumberVM
                {
                    NumberOfAdvices = model.LstAdvices.AsQueryable().Count()
                };
            }

            return View(fav);
        }
    }
}
