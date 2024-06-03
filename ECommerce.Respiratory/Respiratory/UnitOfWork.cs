using ECommerce.Respiratory.Entities;
using ECommerce.Respiratory.Repository;
using System;

namespace ECommerce.Respiratory.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private GenericRepository<Category> _categoryRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<User> _userRepository;
        private GenericRepository<BoughtItem> _boughtItemRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new GenericRepository<Category>(_context);
                }
                return _categoryRepository;
            }
        }
        public GenericRepository<BoughtItem> BoughtItemRepository
        {
            get
            {
                if (_boughtItemRepository == null)
                {
                    _boughtItemRepository = new GenericRepository<BoughtItem>(_context);
                }
                return _boughtItemRepository;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new GenericRepository<Product>(_context);
                }
                return _productRepository;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new GenericRepository<Order>(_context);
                }
                return _orderRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<User>(_context);
                }
                return _userRepository;
            }
        }



        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
