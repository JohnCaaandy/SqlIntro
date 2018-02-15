using System.Collections.Generic;

namespace SqlIntro
{
    interface IDapperProductRepository
    {
        void DeleteProduct(int id);
        IEnumerable<Product> GetProducts();
        void InsertProduct(Product prod);
        void UpdateProduct(Product prod);
    }
}