using Domain.Entities;
using Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Abstract;

namespace Service
{
    public class BrandService : IBrandService
    {
        IRepository<Brand> _brandRepository;
        public BrandService(IRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<IEnumerable<Brand>> GetAllBrand()
        {
            return await _brandRepository.GetAllAsync();
        }
        public async Task<Brand> GetById(int id)
        {
            return await _brandRepository.GetById(id);
        }
        public  Task InsertAsync(IEnumerable<Brand> brand)
        {
            return  _brandRepository.InsertAsync(brand);
        }




    }
}
