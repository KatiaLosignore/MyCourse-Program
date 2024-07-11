using System;
using System.Collections.Generic;
using MyCourse.Models.Enums;

namespace MyCourse.Models.Entities
{
    //classe entità che mappa la tabella Courses nel database
    public partial class Course
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public double Rating { get; set; }
        public Money FullPrice { get; set; }
        public Money CurrentPrice { get; set; }

        public ICollection<Lesson> Lessons { get; set; }//proprietà che rappresenta la relazione 1 a molti tra corso e lezioni

        //Costruttore con campi obbligatori titolo e autore
        public Course(string title, string author)
        {
            //controllo se i campi titolo e autore non sono vuoti
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Il titolo non può essere vuoto");
            }
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("L'autore non può essere vuoto");
            }
            Title = title;
            Author = author;
            Lessons = new HashSet<Lesson>();

            CurrentPrice = new Money(Currency.EUR, 0);
            FullPrice = new Money(Currency.EUR, 0);
            ImagePath = "/Courses/corso.jpg";
        }


        public void ChangeTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new ArgumentException("The course must have a title");
            }
            Title = newTitle;
        }

        public void ChangePrices(Money newFullPrice, Money newDiscountPrice)
        {
            if (newFullPrice == null || newDiscountPrice == null)
            {
                throw new ArgumentException("Prices can't be null");
            }
            if (newFullPrice.Currency != newDiscountPrice.Currency)
            {
                throw new ArgumentException("Currencies don't match");
            }
            if (newFullPrice.Amount < newDiscountPrice.Amount)
            {
                throw new ArgumentException("Full price can't be less than the current price");
            }

            FullPrice = newFullPrice;
            CurrentPrice = newDiscountPrice;
        }
    }
}
