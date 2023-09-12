using Edtech_backend_API.Data;
using Edtech_backend_API.Model;
using Edtech_backend_API.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
            Languages = new LanguageRepository(_context);
            CourseInstructors = new CourseInstructorRepository(_context);
            Courses = new CourseRepository(_context);
            CourseVideos = new CourseVideoRepository(_context);
            CourseReviews = new CourseReviewRepository(_context);
            OrderHeaders = new OrderHeaderRepository(_context);
            OrderDetails = new OrderDetailRepository(_context);
            ShoppingCarts = new ShoppingCartRepository(_context);
            Levels = new LevelRepository(_context);
            ApplicationUser = new ApplicationUserRepository(_context); 
        }

        public ICategoryRepository Categories { get; }
        public ILanguageRepository Languages { get; }
        public ICourseInstructorRepository CourseInstructors { get; }
        public ICourseRepository Courses { get; }
        public ICourseVideoRepository CourseVideos { get; }
        public ICourseReviewRepository CourseReviews { get; }
        public IOrderHeaderRepository OrderHeaders { get; }
        public IOrderDetailRepository OrderDetails { get; }
        public IShoppingCartRepository ShoppingCarts { get; }
        public ILevelRepository Levels { get; }
        public IApplicationUserRespository ApplicationUser { get; }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
