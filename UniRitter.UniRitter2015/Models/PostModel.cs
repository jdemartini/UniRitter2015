using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniRitter.UniRitter2015.Models
{
    public class PostModel
    {
        public Guid? id;

        [Required]
        [MaxLength(4000)]
        public string body;

        [Required]
        [MaxLength(100)]
        public string title;

        public PersonModel author;

        public string[] tags;
    }
}