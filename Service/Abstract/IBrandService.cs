using Domain.Entities;

namespace Service.Abstract
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllBrand();
        Task<Brand> GetById(int id);
        Task InsertAsync(IEnumerable<Brand> brand);
    }
}