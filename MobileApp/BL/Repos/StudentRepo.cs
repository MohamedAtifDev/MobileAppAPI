using Microsoft.EntityFrameworkCore;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class StudentRepo:IStudent
    {
        private readonly DataContext db;

        public StudentRepo(DataContext db)
        {
            this.db = db;
        }

        public void Add(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }

        public int Count()
        {
            return db.Students.Count();
        }

        public void Delete(int id)
        {
            var data = db.Students.Find(id);
            db.Students.Remove(data);
            db.SaveChanges();
        }

        public IEnumerable<Student> GetAll()
        {
            return db.Students.AsNoTracking().ToList();
        }

        public Student GetById(int id)
        {
            var data = db.Students.Find(id);
            return data;
        }

        public void Update(Student student)
        {
            db.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();

        }
    }
}
