using System;
using System.Threading.Tasks;
using AutoMapper;
using TechnicalAssignment.Data.Persistence.Repositories;

namespace TechnicalAssignment.Data.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITechnicalAssignmentDbContext context;
        private readonly IMapper mapper;

        private IOrdersRepository ordersRepository;
        private IProductsRepository productsRepository;

        private bool disposed = false;

        public UnitOfWork(ITechnicalAssignmentDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IOrdersRepository OrdersRepository
        {
            get
            {
                if (ordersRepository == null)
                {
                    ordersRepository = new OrdersRepository(context, mapper);
                }

                return ordersRepository;
            }
        }

        public IProductsRepository ProductsRepository
        {
            get
            {
                if (productsRepository == null)
                {
                    productsRepository = new ProductsRepository(context, mapper);
                }

                return productsRepository;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
