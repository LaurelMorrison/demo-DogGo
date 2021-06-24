using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DogGo.Controllers
{
    public class WalksController : Controller
    {

        private readonly IWalksRepository _walksRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly IDogRepository _dogRepo;

        public WalksController(
            IWalksRepository walksRepository,
            IWalkerRepository walkerRepository,
            IDogRepository dogRepository)
        {
            _walksRepo = walksRepository;
            _walkerRepo = walkerRepository;
            _dogRepo = dogRepository;

        }
        // GET: WalksController
        public ActionResult Index()
        {
            List<Walks> walks = _walksRepo.GetAllWalks();

            return View(walks);
        }

        //// GET: WalksController/Details/5
        //public ActionResult Details(int id)
        //{
        //    Dog dog = _dogRepo.GetDogById(id);

        //    if (dog == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(dog);
        //}

        //// GET: DogsController/Create
        //// GET: /Dogs/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DogsController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Dog dog)
        //{
        //    try
        //    {
        //        _dogRepo.AddDog(dog);

        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        return View(dog);
        //    }
        //}
    }
}