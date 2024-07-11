using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;
using MyCourse.Models.InputModels;





namespace MyCourse.Controllers
{
    // [Route("[controller]")]
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public IActionResult Index()
        {
            //var courseService = new CourseService();//Il controller crea un oggetto del servizio applicativo che deve utilizzare
            List<CourseViewModel> courses = courseService.GetCourses();//recupero la lista dei corsi 
            ViewData["Title"] = "Catalogo dei corsi";
            return View(courses);//passo l'oggetto contenente la lista dei corsi alla view per mostrare i dati
        }

        //azione che deve recuperare i dati del corso con id = id parametro
        //e popolare la view Detail.cshtml con i dati del corso recuperato
        public IActionResult Detail(int id)
        {
            //var courseService = new CourseService();
            CourseDetailViewModel viewModel = courseService.GetCourse(id);
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Nuovo corso";
            var input = new CourseCreateInputModel();
            return View(input);
        }

        [HttpPost]
        public IActionResult Create(CourseCreateInputModel input)
        {
            ViewData["Title"] = "Nuovo corso";
            CourseDetailViewModel course = courseService.CreateCourse(input);//metodo che deve eseguire la query INSERT INTO nel db usando il titolo che ho inserito nel form
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Update/{id:int}")]
        public IActionResult Update(int id)
        {
            var courseDetail = courseService.GetCourse(id);
            if (courseDetail == null)
            {
                return NotFound();
            }

            var model = new CourseUpdateInputModel
            {
                Id = courseDetail.Id,
                Title = courseDetail.Title
            };

            return View(model);
        }


        [HttpPost]
        [Route("Update/{id:int}")]
        public IActionResult Update(CourseUpdateInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

             var existingCourse = courseService.GetCourse(input.Id);
            if (existingCourse == null)
            {
                return NotFound();
            }

            courseService.UpdateCourse(input);
            
            return RedirectToAction(nameof(Detail), new { id = input.Id });
        }




        public IActionResult Delete(int id)
        {

            courseService.DeleteCourse(id);

            return RedirectToAction(nameof(Index));
        }


    }
}