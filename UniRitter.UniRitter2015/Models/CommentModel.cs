using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniRitter.UniRitter2015.Models
{
    public class CommentModel : IModel
    {
        public Guid? id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string body;

        [Required]
        [MaxLength(50)]
        public string title;

        public Guid author;
    }
}