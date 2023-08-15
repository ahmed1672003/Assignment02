using Assignment02.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment02.Controllers
{
    public class StudentSubjectsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentSubjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StudentSubjects
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StudentsSubjects
                                                .Include(s => s.Student)
                                                .Include(s => s.Subject)
                                                .OrderBy(ss => ss.Student.FirstName)
                                                .ThenBy(ss => ss.Student.LastName);
            return View((await appDbContext.ToListAsync()));
        }

        // GET: StudentSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentsSubjects == null)
            {
                return NotFound();
            }

            var studentSubject = await _context.StudentsSubjects
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentSubject == null)
            {
                return NotFound();
            }

            return View(studentSubject);
        }

        // GET: StudentSubjects/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(
                   _context.Student,
                   nameof(Student.Id),
                   nameof(Student.SSN));

            ViewData["SubjectId"] = new SelectList(
                _context.Subjects,
                nameof(Subject.Id),
                nameof(Subject.Name));
            return View();
        }

        // POST: StudentSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,SubjectId")] StudentSubject studentSubject)
        {
            if (await _context.StudentsSubjects.AnyAsync(ss =>
            ss.SubjectId.Equals(studentSubject.SubjectId) &&
            ss.StudentId.Equals(studentSubject.StudentId)))
            {
                ViewData["StudentId"] = new SelectList(
                    _context.Student,
                    nameof(Student.Id),
                    nameof(Student.SSN));

                ViewData["SubjectId"] = new SelectList(
                    _context.Subjects,
                    nameof(Subject.Id),
                    nameof(Subject.Name));

                return View(studentSubject);
            }

            if (ModelState.IsValid)
            {
                _context.Add(studentSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            ViewData["StudentId"] = new SelectList(
               _context.Student,
               nameof(Student.Id),
               nameof(Student.SSN),
               studentSubject.StudentId
               );
            ViewData["SubjectId"] = new SelectList(
                _context.Subjects,
                nameof(Subject.Id),
                nameof(Subject.Name),
                studentSubject.SubjectId);

            return View(studentSubject);
        }

        // GET: StudentSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentsSubjects == null)
            {
                return NotFound();
            }

            var studentSubject = await _context.StudentsSubjects.FindAsync(id);
            if (studentSubject == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(
                _context.Student,
                nameof(Student.Id),
                nameof(Student.SSN),
                studentSubject.StudentId
                );
            ViewData["SubjectId"] = new SelectList(
                _context.Subjects,
                nameof(Subject.Id),
                nameof(Subject.Name),
                studentSubject.SubjectId);

            return View(studentSubject);
        }

        // POST: StudentSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,SubjectId")] StudentSubject studentSubject)
        {
            if (id != studentSubject.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentSubjectExists(studentSubject.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(
                _context.Student,
                nameof(Student.Id),
                nameof(Student.SSN),
                studentSubject.StudentId
                );
            ViewData["SubjectId"] = new SelectList(
                _context.Subjects,
                nameof(Subject.Id),
                nameof(Subject.Name),
                studentSubject.SubjectId);
            return View(studentSubject);
        }

        // GET: StudentSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentsSubjects == null)
            {
                return NotFound();
            }

            var studentSubject = await _context.StudentsSubjects
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentSubject == null)
            {
                return NotFound();
            }

            return View(studentSubject);
        }

        // POST: StudentSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentsSubjects == null)
            {
                return Problem("Entity set 'AppDbContext.StudentsSubjects'  is null.");
            }
            var studentSubject = await _context.StudentsSubjects.FindAsync(id);
            if (studentSubject != null)
            {
                _context.StudentsSubjects.Remove(studentSubject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentSubjectExists(int id)
        {
            return (_context.StudentsSubjects?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
