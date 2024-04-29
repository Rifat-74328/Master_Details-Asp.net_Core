using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Faculty
    {
        public Faculty()
        {
            this.Students=new List<Student>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseName { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime StartDate { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
        [ValidateNever]
        public string PicPath { get; set; }
        public virtual List<Student> Students {  get; set; }
    }
}
