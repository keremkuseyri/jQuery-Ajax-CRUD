using jQuery_Ajax_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using static jQuery_Ajax_CRUD.Helper;
using System.IO;





namespace jQuery_Ajax_CRUD.Controllers
{

    public class TransactionController : Controller
    {


        private readonly TransactionDbContext _context;

        public TransactionController(TransactionDbContext context)
        {
            _context = context;
            List<TransactionController> transactions = new List<TransactionController>();

        }





        //CLASS EDIT BUTTON
        [NoDirectAccess]
        public async Task<IActionResult> ChangeClass(int id = 0)
        {
            if (id == 0)
                return View(new TransactionModel());
            else
            {
                var transactionModel = await _context.Transactions.FindAsync(id);
                if (transactionModel == null)
                {
                    return NotFound();
                }
                return View(transactionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeClass(int id, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date,Class,Age")] TransactionModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    transactionModel.Date = DateTime.Now;
                    _context.Add(transactionModel);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(transactionModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TransactionModelExists(transactionModel.TransactionId))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "ChangeClass", transactionModel) });
        }

        [NoDirectAccess]
        public async Task<IActionResult> ChangeClassT(int id = 0)
        {
            if (id == 0)
                return View(new TransactionModel());
            else
            {
                var teachersModel = await _context.Teachers.FindAsync(id);
                if (teachersModel == null)
                {
                    return NotFound();
                }
                return View(teachersModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeClassT(int id, [Bind("ID,Name,Surname,DateOfBirth,Gender,Subject,Date,Class,A")] TeachersModel teachersModel)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    teachersModel.Date = DateTime.Now;

                    _context.Add(teachersModel);



                    await _context.SaveChangesAsync();
                }
                //Update
                else
                {
                    try
                    {

                        _context.Update(teachersModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TeachersModelExistsT(teachersModel.ID))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllT", _context.Teachers.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "ChangeClassT", teachersModel) });
        }
        // GET: Transaction

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {

            ViewData["SurnameSortParm"] = sortOrder == "surname_desc" ? "surname" : "surname_desc";
            ViewData["GenderSortParm"] = sortOrder == "gender_desc" ? "gender" : "gender_desc";
            ViewData["NumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "name_desc" ? "name" : "name_desc";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var students = from s in _context.Transactions
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.AccountNumber.Contains(searchString)
                                       || s.BeneficiaryName.Contains(searchString));
            }
            switch (sortOrder)
            {


                case "surname_desc":
                    students = students.OrderByDescending(s => s.BeneficiaryName);
                    break;

                case "surname":
                    students = students.OrderBy(s => s.BeneficiaryName);
                    break;

                case "name":
                    students = students.OrderBy(s => s.AccountNumber);
                    break;

                case "name_desc":
                    students = students.OrderByDescending(s => s.AccountNumber);
                    break;

                case "gender":
                    students = students.OrderBy(s => s.BankName);
                    break;

                case "gender_desc":
                    students = students.OrderByDescending(s => s.BankName);
                    break;

                case "number_desc":
                    students = students.OrderByDescending(s => s.Amount);
                    break;

                case "Date":
                    students = students.OrderBy(s => s.Date);
                    break;

                case "date_desc":
                    students = students.OrderByDescending(s => s.Date);
                    break;

                default:

                    students = students.OrderBy(s => s.Amount);
                    break;


            }
            return View(await students.AsNoTracking().ToListAsync());
        }


        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new TransactionModel());
            else
            {
                var transactionModel = await _context.Transactions.FindAsync(id);
                if (transactionModel == null)
                {
                    return NotFound();
                }
                return View(transactionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date,Class,Age")] TransactionModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                //Insert // 
                if (id == 0)
                {
                    int currentyear = DateTime.Now.Year;
                    int dob = transactionModel.SWIFTCode.Year;

                    if (transactionModel.SWIFTCode.Month > 6)
                    { transactionModel.Age = currentyear - dob - 1; }
                    else
                    { transactionModel.Age = currentyear - dob; }

                    transactionModel.Date = DateTime.Now;
                    _context.Add(transactionModel);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        int currentyear = DateTime.Now.Year;
                        int dob = transactionModel.SWIFTCode.Year;

                        if (transactionModel.SWIFTCode.Month > 6)
                        { transactionModel.Age = currentyear - dob - 1; }
                        else
                        { transactionModel.Age = currentyear - dob; }

                        transactionModel.Date = DateTime.Now;
                        _context.Update(transactionModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TransactionModelExists(transactionModel.TransactionId))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", transactionModel) });
        }
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEditT(int id = 0)
        {
            if (id == 0)
                return View(new TeachersModel());
            else
            {
                var teachersModel = await _context.Teachers.FindAsync(id);
                if (teachersModel == null)
                {
                    return NotFound();
                }
                return View(teachersModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditT(int id, [Bind("ID,Name,Surname,DateOfBirth,Gender,Subject,Date,Class,A")] TeachersModel teachersModel)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    teachersModel.Date = DateTime.Now;
                    int dob = teachersModel.DateOfBirth.Year;
                    int currentyear = teachersModel.Date.Year;
                    if (teachersModel.DateOfBirth.Month > 6)
                    { teachersModel.Age = currentyear - dob - 1; }
                    else
                    { teachersModel.Age = currentyear - dob; }


                    if (teachersModel.Subject == 1)
                    { teachersModel.A = "Mathematics"; }
                    else
                    if (teachersModel.Subject == 2)
                    { teachersModel.A = "Physics"; }
                    else
                    if (teachersModel.Subject == 3)
                    { teachersModel.A = "Chemistry"; }
                    else if
                    (teachersModel.Subject == 4)
                    { teachersModel.A = "English"; }
                    else
                    { teachersModel.A = "n/a"; }
                    _context.Add(teachersModel);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        teachersModel.Date = DateTime.Now;
                        int dob = teachersModel.DateOfBirth.Year;
                        int currentyear = teachersModel.Date.Year;
                        if (teachersModel.DateOfBirth.Month > 6)
                        { teachersModel.Age = currentyear - dob - 1; }
                        else
                        { teachersModel.Age = currentyear - dob; }
                        if (teachersModel.Subject == 1)
                        { teachersModel.A = "Mathematics"; }
                        else
                        if (teachersModel.Subject == 2)
                        { teachersModel.A = "Physics"; }
                        else
                        if (teachersModel.Subject == 3)
                        { teachersModel.A = "Chemistry"; }
                        else
                        if (teachersModel.Subject == 4)
                        { teachersModel.A = "English"; }
                        else
                        { teachersModel.A = "n/a"; }
                        _context.Update(teachersModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TeachersModelExistsT(teachersModel.ID))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllT", _context.Teachers.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEditT", teachersModel) });
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionModel = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transactionModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
        }

        private bool TransactionModelExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
        // GET: Transaction/Delete/5/TEACHERS
        public async Task<IActionResult> DeleteT(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachersModel = await _context.Teachers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (teachersModel == null)
            {
                return NotFound();
            }

            return View(teachersModel);
        }

        // POST: Transaction/Delete/5//Teachers
        [HttpPost, ActionName("DeleteT")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedT(int id)
        {
            var teachersModel = await _context.Teachers.FindAsync(id);
            _context.Teachers.Remove(teachersModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllT", _context.Teachers.ToList()) });
        }

        private bool TeachersModelExistsT(int id)
        {
            return _context.Teachers.Any(e => e.ID == id);
        }
        // GET: Transaction/Delete/5
        public async Task<IActionResult> DeleteN(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradesModel = await _context.Grades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradesModel == null)
            {
                return NotFound();
            }

            return View(gradesModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("DeleteN")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedN(int id)
        {
            var gradesModel = await _context.Grades.FindAsync(id);
            _context.Grades.Remove(gradesModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "Grades", _context.Grades.ToList()) });
        }

        // GET: Transaction/Deletetest
        public async Task<IActionResult> DeleteTest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testModel = await _context.Test
                .FirstOrDefaultAsync(m => m.TestID == id);
            if (testModel == null)
            {
                return NotFound();
            }

            return View(testModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("DeleteTest")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedTest(int id)
        {
            var testModel = await _context.Test.FindAsync(id);
            _context.Test.Remove(testModel);
            await _context.SaveChangesAsync();
            return View("IndexTest", testModel);

        }

        private bool TestModelExists(int id)
        {
            return _context.Test.Any(e => e.TestID == id);
        }
        public async Task<IActionResult> IndexL()
        {
            var lessons = from l in _context.Lessons
                          select l;



            return View(await lessons.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> IndexTest()
        {


            var tests = from t in _context.Test

                        select t;





            return View(await tests.AsNoTracking().ToListAsync());
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEditTest(int id = 0)
        {
            if (id == 0)
                return View(new TestModel());
            else
            {
                var testModel = await _context.Test.FindAsync(id);
                if (testModel == null)
                {
                    return NotFound();
                }
                return View(testModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditTest(int id, [Bind("TestID,QNumber,QText,QAnswer,option1,option2,option3,option4")] TestModel testModel)
        {
            if (ModelState.IsValid)
            {
                //Insert // 
                if (id == 0)
                {

                    _context.Add(testModel);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {


                        _context.Update(testModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TestModelExists(testModel.TestID))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_Tests", _context.Test.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEditTest", testModel) });
        }

        public ActionResult ClassA(string sortOrder, string searchString)
        {

            ViewData["SurnameSortParm"] = sortOrder == "surname_desc" ? "surname" : "surname_desc";
            ViewData["GenderSortParm"] = sortOrder == "gender_desc" ? "gender" : "gender_desc";
            ViewData["NumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "name_desc" ? "name" : "name_desc";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var students = from s in _context.Transactions
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.AccountNumber.Contains(searchString)
                                       || s.BeneficiaryName.Contains(searchString));
            }
            switch (sortOrder)
            {


                case "surname_desc":
                    students = students.OrderByDescending(s => s.BeneficiaryName);
                    break;

                case "surname":
                    students = students.OrderBy(s => s.BeneficiaryName);
                    break;

                case "name":
                    students = students.OrderBy(s => s.AccountNumber);
                    break;

                case "name_desc":
                    students = students.OrderByDescending(s => s.AccountNumber);
                    break;

                case "gender":
                    students = students.OrderBy(s => s.BankName);
                    break;

                case "gender_desc":
                    students = students.OrderByDescending(s => s.BankName);
                    break;

                case "number_desc":
                    students = students.OrderByDescending(s => s.Amount);
                    break;

                case "Date":
                    students = students.OrderBy(s => s.Date);
                    break;

                case "date_desc":
                    students = students.OrderByDescending(s => s.Date);
                    break;

                default:

                    students = students.OrderBy(s => s.Amount);
                    break;


            }
            var studentsA = students.Where(c => c.Class == 1).Select(c => c).ToList();
            return View(studentsA);
        }
        public ActionResult ClassB(string sortOrder, string searchString)
        {

            ViewData["SurnameSortParm"] = sortOrder == "surname_desc" ? "surname" : "surname_desc";
            ViewData["GenderSortParm"] = sortOrder == "gender_desc" ? "gender" : "gender_desc";
            ViewData["NumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "name_desc" ? "name" : "name_desc";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var students = from s in _context.Transactions
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.AccountNumber.Contains(searchString)
                                       || s.BeneficiaryName.Contains(searchString));
            }
            switch (sortOrder)
            {


                case "surname_desc":
                    students = students.OrderByDescending(s => s.BeneficiaryName);
                    break;

                case "surname":
                    students = students.OrderBy(s => s.BeneficiaryName);
                    break;

                case "name":
                    students = students.OrderBy(s => s.AccountNumber);
                    break;

                case "name_desc":
                    students = students.OrderByDescending(s => s.AccountNumber);
                    break;

                case "gender":
                    students = students.OrderBy(s => s.BankName);
                    break;

                case "gender_desc":
                    students = students.OrderByDescending(s => s.BankName);
                    break;

                case "number_desc":
                    students = students.OrderByDescending(s => s.Amount);
                    break;

                case "Date":
                    students = students.OrderBy(s => s.Date);
                    break;

                case "date_desc":
                    students = students.OrderByDescending(s => s.Date);
                    break;

                default:

                    students = students.OrderBy(s => s.Amount);
                    break;


            }
            var studentsB = students.Where(c => c.Class == 2).Select(c => c).ToList();
            return View(studentsB);
        }
        public ActionResult ClassC(string sortOrder, string searchString)
        {
            ViewData["SurnameSortParm"] = sortOrder == "surname_desc" ? "surname" : "surname_desc";
            ViewData["GenderSortParm"] = sortOrder == "gender_desc" ? "gender" : "gender_desc";
            ViewData["NumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "name_desc" ? "name" : "name_desc";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var students = from s in _context.Transactions
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.AccountNumber.Contains(searchString)
                                       || s.BeneficiaryName.Contains(searchString));
            }
            switch (sortOrder)
            {

                case "surname_desc":
                    students = students.OrderByDescending(s => s.BeneficiaryName);
                    break;

                case "surname":
                    students = students.OrderBy(s => s.BeneficiaryName);
                    break;

                case "name":
                    students = students.OrderBy(s => s.AccountNumber);
                    break;

                case "name_desc":
                    students = students.OrderByDescending(s => s.AccountNumber);
                    break;

                case "gender":
                    students = students.OrderBy(s => s.BankName);
                    break;

                case "gender_desc":
                    students = students.OrderByDescending(s => s.BankName);
                    break;

                case "number_desc":
                    students = students.OrderByDescending(s => s.Amount);
                    break;

                case "Date":
                    students = students.OrderBy(s => s.Date);
                    break;

                case "date_desc":
                    students = students.OrderByDescending(s => s.Date);
                    break;

                default:

                    students = students.OrderBy(s => s.Amount);
                    break;


            }
            var studentsC = students.Where(c => c.Class == 3).Select(c => c).ToList();
            return View(studentsC);
        }


        public async Task<IActionResult> IndexT(string sortOrder, int? searchString)
        {
            var teachers = from t in _context.Teachers
                           select t;
            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {

                teachers = teachers.Where(t => t.Class.Equals(searchString));
            }

            return View(await teachers.AsNoTracking().ToListAsync());






        }

        public IActionResult TeacherslistA(string sortOrder, string searchString)
        {
            ViewData["SurnameSortParm"] = sortOrder == "surname_desc" ? "surname" : "surname_desc";
            ViewData["GenderSortParm"] = sortOrder == "gender_desc" ? "gender" : "gender_desc";
            ViewData["SubjectSortParm"] = String.IsNullOrEmpty(sortOrder) ? "subject_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "name_desc" ? "name" : "name_desc";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var teachers = from s in _context.Teachers
                           select s;
            var students = from m in _context.Transactions
                           select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                teachers = teachers.Where(s => s.Name.Contains(searchString)
                                       || s.Surname.Contains(searchString));
            }
            switch (sortOrder)
            {


                case "surname_desc":
                    teachers = teachers.OrderByDescending(s => s.Surname);
                    break;

                case "surname":
                    teachers = teachers.OrderBy(s => s.Surname);
                    break;

                case "name":
                    teachers = teachers.OrderBy(s => s.Name);
                    break;

                case "name_desc":
                    teachers = teachers.OrderByDescending(s => s.Name);
                    break;

                case "gender":
                    teachers = teachers.OrderBy(s => s.Gender);
                    break;

                case "gender_desc":
                    teachers = teachers.OrderByDescending(s => s.Gender);
                    break;

                case "subject_desc":
                    teachers = teachers.OrderByDescending(s => s.Subject);
                    break;

                case "Date":
                    teachers = teachers.OrderBy(s => s.Date);
                    break;

                case "date_desc":
                    teachers = teachers.OrderByDescending(s => s.Date);
                    break;

                default:

                    teachers = teachers.OrderBy(s => s.Subject);
                    break;



            }
            var teachersA = teachers.Where(t => t.Class == 1).Select(t => t).ToList();
            var studentsA = students.Where(m => m.Class == 1).Select(m => m);
            if (studentsA.Count() < 5)
            {
                return View("Parameter2");

            }
            if (teachersA.Count() > 2)
            {

                return View("Parameter");
            }

            return View(teachersA);

        }
        public ActionResult Parameter()
        {
            return View();
        }
        public ActionResult Parameter2()
        {
            return View();
        }
        public IActionResult TeacherslistB(string sortOrder, string searchString)
        {

            ViewData["SurnameSortParm"] = sortOrder == "surname_desc" ? "surname" : "surname_desc";
            ViewData["GenderSortParm"] = sortOrder == "gender_desc" ? "gender" : "gender_desc";
            ViewData["SubjectSortParm"] = String.IsNullOrEmpty(sortOrder) ? "subject_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "name_desc" ? "name" : "name_desc";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var teachers = from s in _context.Teachers
                           select s;
            var students = from m in _context.Teachers
                           select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                teachers = teachers.Where(s => s.Name.Contains(searchString)
                                       || s.Surname.Contains(searchString));
            }
            switch (sortOrder)
            {


                case "surname_desc":
                    teachers = teachers.OrderByDescending(s => s.Surname);
                    break;

                case "surname":
                    teachers = teachers.OrderBy(s => s.Surname);
                    break;

                case "name":
                    teachers = teachers.OrderBy(s => s.Name);
                    break;

                case "name_desc":
                    teachers = teachers.OrderByDescending(s => s.Name);
                    break;

                case "gender":
                    teachers = teachers.OrderBy(s => s.Gender);
                    break;

                case "gender_desc":
                    teachers = teachers.OrderByDescending(s => s.Gender);
                    break;

                case "subject_desc":
                    teachers = teachers.OrderByDescending(s => s.Subject);
                    break;

                case "Date":
                    teachers = teachers.OrderBy(s => s.Date);
                    break;

                case "date_desc":
                    teachers = teachers.OrderByDescending(s => s.Date);
                    break;

                default:

                    teachers = teachers.OrderBy(s => s.Subject);
                    break;



            }
            var teachersB = teachers.Where(t => t.Class == 2).Select(t => t).ToList();
            var studentsB = students.Where(m => m.Class == 2).Select(m => m);
            if (studentsB.Count() < 5)
            {
                return View("Parameter2");

            }
            if (teachersB.Count() > 2)
            {

                return View("Parameter");
            }
            return View(teachersB);
        }
        public IActionResult TeacherslistC(string sortOrder, string searchString)
        {

            ViewData["SurnameSortParm"] = sortOrder == "surname_desc" ? "surname" : "surname_desc";
            ViewData["GenderSortParm"] = sortOrder == "gender_desc" ? "gender" : "gender_desc";
            ViewData["SubjectSortParm"] = String.IsNullOrEmpty(sortOrder) ? "subject_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "name_desc" ? "name" : "name_desc";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var teachers = from s in _context.Teachers
                           select s;
            var students = from m in _context.Transactions
                           select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                teachers = teachers.Where(s => s.Name.Contains(searchString)
                                       || s.Surname.Contains(searchString));
            }
            switch (sortOrder)
            {


                case "surname_desc":
                    teachers = teachers.OrderByDescending(s => s.Surname);
                    break;

                case "surname":
                    teachers = teachers.OrderBy(s => s.Surname);
                    break;

                case "name":
                    teachers = teachers.OrderBy(s => s.Name);
                    break;

                case "name_desc":
                    teachers = teachers.OrderByDescending(s => s.Name);
                    break;

                case "gender":
                    teachers = teachers.OrderBy(s => s.Gender);
                    break;

                case "gender_desc":
                    teachers = teachers.OrderByDescending(s => s.Gender);
                    break;

                case "subject_desc":
                    teachers = teachers.OrderByDescending(s => s.Subject);
                    break;

                case "Date":
                    teachers = teachers.OrderBy(s => s.Date);
                    break;

                case "date_desc":
                    teachers = teachers.OrderByDescending(s => s.Date);
                    break;

                default:

                    teachers = teachers.OrderBy(s => s.Subject);
                    break;


            }

            var teachersC = teachers.Where(t => t.Class == 3).Select(t => t).ToList();
            var studentsC = students.Where(m => m.Class == 3).Select(m => m);
            if (studentsC.Count() < 5)
            {
                return View("Parameter2");

            }
            if (teachersC.Count() > 2)
            {

                return View("Parameter");
            }
            return View(teachersC);
        }
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEditL(int id = 0)
        {
            if (id == 0)
                return View(new LessonsModel());
            else
            {
                var lessonsModel = await _context.Lessons.FindAsync(id);
                if (lessonsModel == null)
                {
                    return NotFound();
                }
                return View(lessonsModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditL(int id, [Bind("ID,Class,A,Teacher,ClassStart,ClassEnd")] LessonsModel lessonsModel)
        {
            if (ModelState.IsValid)
            {
                //Insert // 
                if (id == 0)
                {
                    _context.Add(lessonsModel);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(lessonsModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!LessonsModelExists(lessonsModel.ID))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_Lessonslist", _context.Lessons.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEditL", lessonsModel) });
        }
        private bool LessonsModelExists(int id)
        {
            return _context.Lessons.Any(e => e.ID == id);
        }




        public IActionResult Grades(string sortOrder)
        {
            ViewData["NumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";

            decimal MaxNote = _context.Grades.Max(p => p.Note);
            decimal MinNote = _context.Grades.Min(p => p.Note);
            decimal Avarage = ((MaxNote + MinNote) / 2);


            var GradesModel =

                    (

                    from t in _context.Transactions
                    join g in _context.Grades
                    on t.Amount equals g.Number
                    select new GradesModel



                    {
                        Number = t.Amount,
                        Name = t.AccountNumber + ' ' + t.BeneficiaryName,
                        A = g.A,
                        Class = g.Class,
                        Visa1 = g.Visa1,
                        Visa2 = g.Visa2,
                        Final = g.Final,
                        Note = g.Note,
                        Passed = (g.Note >= Avarage) ? "Passed" :
                        (g.Note < Avarage) ? "Failed" : null,
                        Teacher = g.Teacher,
                        Date = g.Date


                    }).ToList();








            return View(GradesModel);
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEditN(int id = 0)
        {
            if (id == 0)
                return View(new GradesModel());
            else
            {
                var gradesModel = await _context.Grades.FindAsync(id);
                if (gradesModel == null)
                {
                    return NotFound();
                }
                return View(gradesModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditN(int id, [Bind("Id,Number,Name,A,Class,Teacher,Visa1,Visa2,Final,Note,Date,Passed")] GradesModel gradesModel)
        {
            if (ModelState.IsValid)
            {
                //Insert // 
                if (id == 0)
                {

                    gradesModel.Note = (gradesModel.Visa1 / 5) + (gradesModel.Visa2 / 5) + ((gradesModel.Final * 3) / 5);
                    gradesModel.Date = DateTime.Now;
                    _context.Add(gradesModel);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        gradesModel.Note = (gradesModel.Visa1 / 5) + (gradesModel.Visa2 / 5) + ((gradesModel.Final * 3) / 5);
                        gradesModel.Date = DateTime.Now;
                        _context.Update(gradesModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!GradesModelExists(gradesModel.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Grades", _context.Grades.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEditN", gradesModel) });
        }
        private bool GradesModelExists(int id)
        {
            return _context.Grades.Any(g => g.Id == id);
        }
        [HttpGet]
        public JsonResult BulkDrop2()
        {
            var bulkdata = _context.BulkData.ToList();


            return Json(bulkdata);
           
            
        }
        public IActionResult BulkDrop()
        {
            
            return View();
        }
      
    



        public IActionResult TakeTest()
        {


            var test = from t in _context.Test
                       select t;




            return View(test);
        }
        //CLASS EDIT BUTTON
        [NoDirectAccess]
        public async Task<IActionResult> ChangeAnswer(int id = 0)
        {
            if (id == 0)
                return View(new TestModel());
            else
            {
                var testModel = await _context.Test.FindAsync(id);
                if (testModel == null)
                {
                    return NotFound();
                }
                return View(testModel);
            }
        }


        public IActionResult TestValidation()
        {
            var test = from q in _context.Test
                       select q;
            return View(test);

        }
        //CLASS EDIT BUTTON
        [NoDirectAccess]
        public async Task<IActionResult> ValidationName(int id = 0)
        {
            if (id == 0)
                return View(new TestModel());
            else
            {
                var testModel = await _context.Test.FindAsync(id);
                if (testModel == null)
                {
                    return NotFound();
                }
                return View(testModel);
            }
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidationName(int id, [Bind("TestID,QNumber,QText,QAnswer,option1,option2,option3,option4,StdAnswer,StdName,StdNumber")] TestModel testModel)
        {


            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {

                    _context.Add(testModel);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {

                        _context.Update(testModel);
                        await _context.SaveChangesAsync();

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TestModelExists(testModel.TestID))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_TestValidation", _context.Test.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "ValidationName", testModel) });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeAnswer(int id, [Bind("TestID,QNumber,QText,QAnswer,option1,option2,option3,option4,StdAnswer,StdName,StdNumber")] TestModel testModel)
        {

            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {

                    _context.Add(testModel);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Answer Submitted!";
                }
                //Update
                else
                {
                    try
                    {

                        _context.Update(testModel);
                        await _context.SaveChangesAsync();
                        ViewBag.Message = "Answer Changed Successfully!";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TestModelExists(testModel.TestID))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_TakeTestR", _context.Test.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "ChangeAnswer", testModel) });
        }






        private bool TestResultModelExists(int id)
        {
            return _context.TestResults.Any(g => g.ResultID == id);
        }
        private bool TestResultModelExistsN(string name)
        {
            return _context.TestResults.Any(g => g.StdName == name);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitTest(int id, [Bind("ResultID,StdNumber,StdName,Score,StdScore,Note,TestScore")] TestResults testResults, [Bind("TestID,QNumber,QText,QAnswer,option1,option2,option3,option4,StdAnswer")] TestModel testModel)

        {

            if (ModelState.IsValid)
            {
                if (_context.TestResults.Any(k => k.StdNumber == testResults.StdNumber))
                {
                    ViewBag.Message = "You have submitted your test already!";

                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_TakeTestR", _context.Test.ToList()) });
                }
                //Insert
                if (id == 0)
                {
                    Random rnd = new Random();
                    var Notes = from g in _context.Grades
                                select g.Note;

                    var QAnswers = from q in _context.Test
                                   select q.QAnswer;
                    var StdAnswers = from s in _context.Test
                                     select s.StdAnswer;


                    testResults.TestScore = 0;
                    int score = 0;
                    foreach (var StdAnswer in StdAnswers)
                    {
                        foreach (var QAnswer in QAnswers)
                            if (StdAnswer == QAnswer)
                            {
                                score++;
                            }

                    }



                    testResults.Score = score;
                    for (int i = 0; i < score; i++)
                    {
                        testResults.TestScore = testResults.TestScore + (decimal)rnd.Next(1, 50);
                    }

                    foreach (var item in _context.Test.Where(w => w.StdAnswer != null))
                    {
                        item.StdAnswer = null;
                    }

                    ViewBag.Message = "Test Submitted Successfully! New Candidate.";

                    _context.Add(testResults);

                    await _context.SaveChangesAsync();

                }

                //Updateempty
                else
                {
                    try

                    {

                        _context.Update(testResults);
                        await _context.SaveChangesAsync();
                        ViewBag.Message = "Test Updated Successfully!";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TestResultModelExistsN(testResults.StdName))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }




                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_TakeTestR", _context.Test.ToList()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "SubmitTest", testResults) });


        }
        [NoDirectAccess]
        public async Task<IActionResult> SubmitTest(int id = 0)
        {
            if (id == 0)
                return View(new TestResults());
            else
            {
                var testResults = await _context.Test.FindAsync(id);
                if (testResults == null)
                {
                    return NotFound();
                }
                return View(testResults);
            }
        }
        public IActionResult TestResultsView()
        {



            var Results = from q in _context.TestResults
                          join g in _context.Grades
                          on q.StdNumber equals g.Number
                          select new TestResults
                          {
                              ResultID = q.ResultID,
                              StdNumber = q.StdNumber,
                              StdName = q.StdName,
                              Note = g.Note,
                              Score = q.Score,
                              TestScore = q.TestScore

                          };


            return View(Results);




        }
        private List<TeachersModel> GetTeachers()
        {
            List<TeachersModel> teachers = _context.Teachers.ToList();
            return teachers;

        }
        private List<TransactionModel> GetStudents()
        {
            List<TransactionModel> students= _context.Transactions.ToList();
            return students;
        }
        private List<SyncModel> GetSyncs()
        {
            List<SyncModel> syncs = _context.Syncs.ToList();
            return syncs;
        }
        private List<SyncModel1> GetSyncs1()
        {
            List<SyncModel1> syncs1 = _context.Syncs1.ToList();
            return syncs1;
        }
        private List<SyncModel2> GetSyncs2()
        {
            List<SyncModel2> syncs2 = _context.Syncs2.ToList();
            return syncs2;
        }
        
        
        public ActionResult MultipleEditor()
        {
          
            dynamic mymodel = new ExpandoObject();
            mymodel.Teachers = GetTeachers();
            mymodel.Students = GetStudents();
            mymodel.Syncs1 = GetSyncs1();
            mymodel.Syncs = GetSyncs();
            mymodel.Syncs2 = GetSyncs();
            
            return View(mymodel);
        }



        public  FileResult SyncCommand()
        {
           
            string fileName = "output.txt";
            string content = System.IO.File.ReadAllText(@"C://Users/user/Desktop/output.txt");
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"C://Users/user/Desktop/output.txt");
            

            using (SqlConnection conn = new SqlConnection("Server = (local)\\sqlexpress; Database = testdb2; Trusted_Connection = True; MultipleActiveResultSets = True; "))
            {
            
            
           

                conn.Close();

                conn.Open();



             
                SqlCommand cmd = conn.CreateCommand();
                
                        cmd.CommandText = "exec [dbo].[tablecomparator]";
                        cmd.CommandTimeout = 35;
                        cmd.CommandType = CommandType.Text;
                

                
                
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)

                            content = content + " " + rdr.GetValue(i);



                    }
                }
              

                content = "=>DB Sync" + DateTime.Now.ToString() + "\t" + content;
                using StreamWriter sw = new StreamWriter(@"C://Users/user/Desktop/output.txt", append: true);
                
                sw.WriteLine(content);
                sw.Close();
                
                byte[] newfileBytes = System.IO.File.ReadAllBytes(@"C://Users/user/Desktop/output.txt");
                return File(newfileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);


            }
            

            }

      



        public ActionResult IbanValidation()
        {
            return View();
        }
        
        public async Task<IActionResult> Ticket()
        {
            var Tickets = from t in _context.Tickets
                          select t;

            return View(await Tickets.AsNoTracking().ToListAsync());
        }
        private bool TicketModelExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
        [NoDirectAccess]
        public async Task<IActionResult> AddorEditTicket(int id = 0)
        {
            if (id == 0)
                return View(new TicketModel());
            else
            {
                var ticketModel = await _context.Tickets.FindAsync(id);
                if (ticketModel == null)
                {
                    return NotFound();
                }
                return View(ticketModel);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEditTicket(int id, [Bind("Id,TicketContent,TicketAnswer,TicketHeader,Date,Condition,UserName")] TicketModel ticketModel)
        {
            if (ModelState.IsValid)
            {
                //Insert // 
                if (id == 0)
                {
                    

                    ticketModel.Date = DateTime.Now;
                    _context.Add(ticketModel);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        
                        ticketModel.Date = DateTime.Now;
                        _context.Update(ticketModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TicketModelExists(ticketModel.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_Ticket", _context.Tickets.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddorEditTicket", ticketModel) });
        }

        // GET: Transaction/DeleteTicket
        public async Task<IActionResult> DeleteTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketModel = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketModel == null)
            {
                return NotFound();
            }

            return View(ticketModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("DeleteTicket")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedTicket(int id)
        {
            var ticketModel = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticketModel);
            await _context.SaveChangesAsync();
            return View("Ticket", ticketModel);

        }


    }

}

