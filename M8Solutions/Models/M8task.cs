using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M8Solutions.Models
{
    public class M8task : IEnumerable
    {
        [Required]        
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Please write your subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please write your description")]
        public string Description { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Please write your footer.")]
        public string Footer { get; set; }

        public DateTime Date_pub { get; set; }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public IEnumerable<M8task> M8taskEnum { get; set; }

    }
}