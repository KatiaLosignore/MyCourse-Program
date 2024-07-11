using System;
using System.Collections.Generic;

namespace MyCourse.Models.Entities
{
    //classe entità che mappa la tabella Lessons nel database
    public partial class Lesson
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }

        public Course Course { get; set; }//oggetto che rappresenta la relazione molti a uno tra lezione e corso

        public Lesson(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("A lesson must have a title");
            }
            Title = title;
        }
    }
}
