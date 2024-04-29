using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FacultyController : Controller
    {
       







            private readonly MDContext context;
            IHostEnvironment environment;
            public FacultyController(MDContext context, IHostEnvironment env = null)
            {
                this.context = context;
                this.environment = env;
            }
            public IActionResult Index()
            {
                return View(context.Faculty.Include(a => a.Students).ToList());
            }
            public ActionResult Create()
            {
                Faculty faculty = new Faculty();
                faculty.Students.Add(new Student()
                {
                    Name = "",
                    Address = ""
                });
                return View(faculty);
            }
            [HttpPost]
            public IActionResult Create(Faculty faculty, string btn)
            {
                if (btn == "ADD")
                {
                    faculty.Students.Add(new Student());
                }
                if (btn == "Create")
                {
                    if (ModelState.IsValid)
                    {
                        if (faculty.Picture != null)
                        {
                            // var ext = Path.GetExtension(faculty.Picture.FileName);
                            var rootPath = this.environment.ContentRootPath;
                            var fileToSave = Path.Combine(rootPath, "wwwroot/Pictures", faculty.Picture.FileName);
                            using (var fileStream = new FileStream(fileToSave, FileMode.Create))
                            {
                                faculty.Picture.CopyToAsync(fileStream);
                            }
                            faculty.PicPath = "~/Pictures/" + faculty.Picture.FileName;

                            context.Faculty.Add(faculty);
                            if (context.SaveChanges() > 0)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Please Provide Profile Picture");
                            return View(faculty);
                        }
                    }
                    else
                    {
                        var message = string.Join(" | ", ModelState.Values
                                                    .SelectMany(v => v.Errors)
                                                    .Select(e => e.ErrorMessage));
                        ModelState.AddModelError("", message);
                    }
                }
                return View(faculty);
            }
            public IActionResult Edit(int id)
            {

                return View(context.Faculty.Include(f => f.Students).Where(f => f.Id.Equals(id)).FirstOrDefault());
            }
            [HttpPost]
            public IActionResult Edit(Faculty faculty, string btn)
            {
                if (btn == "ADD")
                {
                    faculty.Students.Add(new Student());
                }
                if (btn == "Create")
                {
                    //if (ModelState.IsValid)
                    //{
                    var oldFaculty = context.Faculty.Find(faculty.Id);
                    if (faculty.Picture != null)
                    {
                        // var ext = Path.GetExtension(faculty.Picture.FileName);
                        var rootPath = this.environment.ContentRootPath;
                        var fileToSave = Path.Combine(rootPath, "wwwroot/Pictures", faculty.Picture.FileName);
                        using (var fileStream = new FileStream(fileToSave, FileMode.Create))
                        {
                            faculty.Picture.CopyToAsync(fileStream);
                        }
                        faculty.PicPath = "wwwroot/Pictures/" + faculty.Picture.FileName;

                    }
                    else
                    {
                        oldFaculty.PicPath = oldFaculty.PicPath;
                    }
                    oldFaculty.Name = faculty.Name;
                    oldFaculty.CourseName = faculty.CourseName;
                    oldFaculty.StartDate = faculty.StartDate;
                    context.Students.RemoveRange(context.Students.Where(s => s.Id == faculty.Id));
                    context.SaveChanges();
                    oldFaculty.Students = faculty.Students;
                    context.Entry(oldFaculty).State = EntityState.Modified;
                    if (context.SaveChanges() > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    //}
                    else
                    {
                        var message = string.Join(" | ", ModelState.Values
                                                    .SelectMany(v => v.Errors)
                                                    .Select(e => e.ErrorMessage));
                        ModelState.AddModelError("", message);
                    }
                }
                return View(faculty);
            }
        public IActionResult Delete(int id)
        {
            context.Faculty.Remove(context.Faculty.Find(id));
            context.SaveChanges();
            return RedirectToAction("Index");
        }




















}
}
