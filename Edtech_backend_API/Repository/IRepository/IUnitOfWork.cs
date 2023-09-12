using Edtech_backend_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        ILanguageRepository Languages { get; }
        ICourseInstructorRepository CourseInstructors { get; }
        ICourseRepository Courses { get; }
        ICourseVideoRepository CourseVideos { get; }
        ICourseReviewRepository CourseReviews { get; }
        IOrderHeaderRepository OrderHeaders { get; }
        IOrderDetailRepository OrderDetails { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        ILevelRepository Levels { get; }
        IApplicationUserRespository ApplicationUser { get; }
        void Save();
    }

}
