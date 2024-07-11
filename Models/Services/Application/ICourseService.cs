using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCourse.Models.InputModels;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    public interface ICourseService
    {
            List<CourseViewModel> GetCourses();
            CourseDetailViewModel GetCourse(int id);

            CourseDetailViewModel CreateCourse(CourseCreateInputModel input);

             // Nuovo metodo per aggiornare una persona
            CourseDetailViewModel UpdateCourse(CourseUpdateInputModel input);

            // Nuovo metodo per eliminare una persona
            void DeleteCourse(int id);
    }
}