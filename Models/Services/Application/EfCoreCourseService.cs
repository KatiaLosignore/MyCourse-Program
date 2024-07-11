using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCourse.Models.ViewModels;
using MyCourse.Models.InputModels;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.Entities;

namespace MyCourse.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {

        //tramite questo oggetto eseguiremo le operazioni CRUD per i Corsi e le Lezioni
        private readonly MyCourseDbContext dbContext;

        public EfCoreCourseService(MyCourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Deve recuperare tutti i corsi presenti nella tabella Courses del db
        //SELECT * FROM Courses
        public List<CourseViewModel> GetCourses()
        {
            List<CourseViewModel> courses = dbContext.Courses.Select(course =>
            new CourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                ImagePath = course.ImagePath,
                Author = course.Author,
                Rating = course.Rating,
                CurrentPrice = course.CurrentPrice,
                FullPrice = course.FullPrice
            }).ToList();//dopo che ha recuperarto tutte le righe della tabella inserirsci tutto nella lista courses

            return courses;
        }
        public CourseDetailViewModel GetCourse(int id)
        {
            CourseDetailViewModel viewModel = dbContext.Courses
            .Where(course => course.Id == id)
            .Select(course => new CourseDetailViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Author = course.Author,
                ImagePath = course.ImagePath,
                Rating = course.Rating,
                CurrentPrice = course.CurrentPrice,
                FullPrice = course.FullPrice,
                Lessons = course.Lessons.Select(lesson => new LessonViewModel
                {
                    Id = lesson.Id,
                    Title = lesson.Title,
                    Description = lesson.Description,
                    Duration = lesson.Duration
                }).ToList()
            }).Single();
            
            return viewModel;
        }
            // return null;
        

        public CourseDetailViewModel CreateCourse(CourseCreateInputModel input)
        {
            string title = input.Title;
            string author = "Mario Rossi";
            var course = new Course(title, author);
            dbContext.Add(course); //tramite il metodo add eseguo una INSERT INTO nella tabella Courses aggiungendo il nuovo oggetto Course
            dbContext.SaveChanges(); //tramite il metodo SaveChanges() eseguo l'INSERT INTO

            return CourseDetailViewModel.FromEntity(course);
        }


        public CourseDetailViewModel UpdateCourse(CourseUpdateInputModel input){
        
            // Trovo la persona da aggiornare
            var courseToUpdate = dbContext.Courses.Where(course => course.Id == input.Id).FirstOrDefault();

            if (courseToUpdate != null)
            {
                // Aggiorno la proprietà del titolo corso
                courseToUpdate.Title = input.Title;
                

                // Salvo i cambiamenti
                dbContext.SaveChanges();

                // Restituisco il modello di dettaglio aggiornato
                return CourseDetailViewModel.FromEntity(courseToUpdate);
            }
            else
            {
                // Se il Corso non esiste lancio un'eccezione
                throw new Exception("Il Corso da aggiornare non è stato trovato!");
            }

        } 

        public void DeleteCourse(int id){
            
             // Trovo il corso da eliminare
            var courseToDelete = dbContext.Courses.Where(course => course.Id == id).FirstOrDefault();

            if (courseToDelete != null)
            {
                // Rimuovo la persona
                dbContext.Courses.Remove(courseToDelete);
                // Salvo la modifica nel Db
                dbContext.SaveChanges();
            }
            else 
            {
                //Se il corso non esiste lancio un'eccezione
                throw new Exception("Il corso da eliminare non è stato trovato!");
            }
        }


        
    }}
