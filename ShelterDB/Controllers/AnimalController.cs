using Microsoft.AspNetCore.Mvc;
using ShelterDB.Models;
using ShelterDB.Services;

namespace ShelterDB.Controllers
{
    public class AnimalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllAnimals(int page = 1, int pageSize = 10, int currentPage = 1)
        {

            
            AnimalsDAO animals = new AnimalsDAO();

            var totalCount = animals.GetAllAnimals().Count();
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize) + 1;
            var animalsPerPage = animals.GetAllAnimals().Skip((page -1) * pageSize).Take(pageSize).ToList();
            var numberOfPaginationButtons = totalCount / 200;
            ViewBag.TotalPages = totalPages;
            var pagesForward = currentPage + 10;
            var NumberOfPaginationButtonsBack = currentPage - 10;
            if(NumberOfPaginationButtonsBack < 1)
            {
                ViewBag.NumberOfPaginationButtonsBack = 1;
            }
            else
            {
                ViewBag.NumberOfPaginationButtonsBack = NumberOfPaginationButtonsBack;

            }   
            var xMorePages = (totalPages - currentPage) + currentPage;
            if(pagesForward > totalPages)
            {
                
                ViewBag.numberOfPaginationButtons = xMorePages;
            }
            else
            {
                ViewBag.numberOfPaginationButtons = pagesForward;


            }


            //ViewBag.NumberOfPaginationButtonsBack = currentPage - 10;
            ViewBag.currentPage = currentPage;

            return View("AllAnimals", animalsPerPage);
        }
        public IActionResult CreateAnimal()
        {
            return View();
        }
        public IActionResult InsertAnimal(AnimalModel animal)
        {
            AnimalsDAO animals = new AnimalsDAO();
            animals.InsertAnimal(animal);
            // var animalId = animal.Id;
            var lastId = animals.GetLastAnimalId();
            return View("SingleAnimalDetails", animals.GetAnimalById(lastId));
        }
        public IActionResult SearchById()
        {
            return View();
        } 
        public IActionResult SearchVetTreatmnetsByDueDate()
        {
            return View();
        }
        public IActionResult SingleAnimalDetails(int id)
        {
            AnimalsDAO animals = new AnimalsDAO();
            //check and see if animal exsits
            animals.SeeIfExists(id);
            var exsits = animals.SeeIfExists(id);

            if (exsits == true)
            {
                return View(animals.GetAnimalById(id));
            }
            else
            {
                //ViewBag.doesnotexits = "doesnotexits";
                //  return View("Index");
                TempData["error"] = "does not exsist";
                return RedirectToAction("index");


            }
        }
        public IActionResult MainSearchResultPage()
        {
            return View();
        }
        public IActionResult MainSearchResults(String searchName, String searchStatus)
        {
            AnimalsDAO animals = new AnimalsDAO();

            List<AnimalModel> animalList = animals.SearchAnimals(searchName, searchStatus);

            return View(animalList);
        }

        public IActionResult EditAnimal(int id)
        {
            AnimalsDAO animals = new AnimalsDAO();
            AnimalModel foundAnimal = animals.GetAnimalById(id);
            return View(foundAnimal);
        }
        public IActionResult ProcessEdit(AnimalModel animal)
        {
            AnimalsDAO animals = new AnimalsDAO();
            animals.ProcessEditAnimal(animal);
            return View("SingleAnimalDetails", animals.GetAnimalById(animal.Id));
        }

        public IActionResult DeleteAnimal(int id)
        {
            AnimalsDAO animals = new AnimalsDAO();
            AnimalModel animal = animals.GetAnimalById(id);
            animals.DeleteAnimal(animal);
            TempData["deleted"] = "Animal Deleted";
            return RedirectToAction("index");
        }

        public IActionResult AddVetTreatment(int AnimalId)
        {
            return View();
        }
        public IActionResult ProcessAddVetTreatment(VetTreatmentModel vetTreatment)
        {
            AnimalsDAO vetTreatments = new AnimalsDAO();
            vetTreatments.InsertVetTreatments(vetTreatment);
            // var animalId = animal.Id;
            //var lastId = animals.GetLastAnimalId();
            return View("SingleAnimalDetails", vetTreatments.GetAnimalById(vetTreatment.AnimalId));
        }
        public IActionResult AllAnimalVetTreatments(int AnimalId)
        {
            AnimalsDAO vetTreatments = new AnimalsDAO();
            List<VetTreatmentModel> vetTreatmentList = vetTreatments.GetAllAnimalVetTreatments(AnimalId);

            return View(vetTreatmentList);
        }

        public IActionResult GetAllVetTreamentsByDueDate(string DateDue)
        {
            {
                AnimalsDAO vetTreatments = new AnimalsDAO();

                List<AllVetTreatmentsModel> vetTreatmentList = vetTreatments.GetAllAnimalVetTreatmentsByDueDate(DateDue);
                return View(vetTreatmentList);
            }
        }            
    
    }

    }

