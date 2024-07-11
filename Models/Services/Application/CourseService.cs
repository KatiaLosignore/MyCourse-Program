using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCourse.Models.ViewModels;
using MyCourse.Models.Enums;
using MyCourse.Models.InputModels;

namespace MyCourse.Models.Services.Application
{
    public class CourseService:ICourseService
    {
        //metodo che deve tornare la lista dei corsi 
        //per adesso creiamo un algoritmo che crea dei corsi random
        //successivamente leggeremo i corsi da database
        public List<CourseViewModel> GetCourses()
        {
            // throw new NotImplementedException();
            var courseList = new List<CourseViewModel>();//oggetto che conterrà la lista dei corsi
            var rand = new Random();
            for (int i = 1; i <= 20; i++)//creo 20 corsi random
            {
                var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
                var course = new CourseViewModel//creo un oggetto di tipo CourseViewModel e lo popolo in maniera random
                {
                    Id = i,
                    Title = $"Corso {i}",
                    CurrentPrice = new Money(Currency.EUR, price),
                    FullPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 1),
                    Author = "Nome cognome",
                    Rating = rand.Next(10, 50) / 10.0,
                    ImagePath = "/logo.svg"
                };
                courseList.Add(course);
            }
            return courseList;
        }

        //metodo che deve creare un oggetto di tipo CourseDetailViewModel
        //e deve essere popolato con i dati del corso con id = id parametro
        public CourseDetailViewModel GetCourse(int id)
        {
            var rand = new Random();
            var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
            var course = new CourseDetailViewModel
            {
                Id = id,
                Title = $"Corso {id}",
                CurrentPrice = new Money(Currency.EUR, price),
                FullPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 1),
                Author = "Nome cognome",
                Rating = rand.Next(10, 50) / 10.0,
                ImagePath = "/logo.svg",
                Description = $"Descrizione {id}",
                Lessons = new List<LessonViewModel>()
            };

            for (var i = 1; i <= 5; i++)
            {
                var lesson = new LessonViewModel
                {
                    Title = $"Lezione {i}",
                    Duration = TimeSpan.FromSeconds(rand.Next(40, 90))
                };
                course.Lessons.Add(lesson);
            }

            return course;
        }

        public CourseDetailViewModel CreateCourse(CourseCreateInputModel input){
            throw new NotImplementedException();
        }

          public CourseDetailViewModel UpdateCourse(CourseUpdateInputModel input) {
               throw new NotImplementedException();
         }

         public void DeleteCourse(int id){
            throw new NotImplementedException();
        }

    }
}