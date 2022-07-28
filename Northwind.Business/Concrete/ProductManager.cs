using Northwind.Business.Abstract;
using FluentValidation;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;
using Northwind.Business.Utilities;

namespace Northwind.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;


        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            //ValidationTool.Validate(new ProductValidator(), product);
            ProductValidator productValidator = new ProductValidator();
            productValidator.Validate(product);
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            try
            {
                _productDal.Delete(product);
            }
            catch
            {
                throw new Exception("Delete Failed!");
            }

        }

        public List<Product> GetAll()
        {
            //business code
            return _productDal.GetAll();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productDal.GetAll(p => p.CategoryId == categoryId);
        }

        public List<Product> GetProductsByPorductName(string productName)
        {
            return _productDal.GetAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));

        }

        public void Update(Product product)
        {
            //ValidationTool.Validate(new ProductValidator(), product);
            ProductValidator productValidator = new ProductValidator();
            productValidator.Validate(product);
            _productDal.Update(product);
        }
    }
}
