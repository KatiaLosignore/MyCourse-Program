using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCourse.Models.ViewModels;
using System.Data;
using MyCourse.Models.Entities;


namespace MyCourse.Models.ViewModels
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }//proprietà per settare la durata della lezione
        public string Description {get; set;}


        public static LessonViewModel FromDataRow(DataRow dataRow)
        {
            var lessonViewModel = new LessonViewModel
            {
                Id = Convert.ToInt32(dataRow["Id"]),
                Title = Convert.ToString(dataRow["Title"]),
                Duration = TimeSpan.Parse(Convert.ToString(dataRow["Duration"])),
            };
            return lessonViewModel;
        }

         public static LessonViewModel FromEntity(Lesson lesson)
        {
            return new LessonViewModel
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Duration = lesson.Duration,
                Description = lesson.Description
            };
        }



    }
}