using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Common;
using Svetlina.Data.Models;
using Svetlina.Services.Contracts;

namespace Svetlina.Services
{
    public class CartContext : IDb<Cart, int>
    {
        private readonly SvetlinaDbContext dbContext;

        public CartContext(SvetlinaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Cart item)
        {
            try
            {
                // Тук вече не обработваме проекти, а направо добавяме продуктите
                dbContext.Carts.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                var cart = await dbContext.Carts.FindAsync(key);
                if (cart != null)
                {
                    dbContext.Carts.Remove(cart);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Cart>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Cart> query = dbContext.Carts;
                if (useNavigationalProperties)
                {
                    query = query.Include(c => c.Products);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Cart> ReadAsync(int key, bool useNavigationalProperties = false)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return await dbContext.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == key);
                }
                else
                {
                    return await dbContext.Carts.FindAsync(key);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Cart item, bool useNavigationalProperties = false)
        {
            try
            {
                Cart cartFromDb = await ReadAsync(item.Id, useNavigationalProperties);

                // Тъй като Cart съдържа само Id и продукти, промените ще бъдат свързани със списъка продукти
                foreach (var product in item.Products)
                {
                    // Търсене на продуктите по Id, за да се избегне добавянето на дубликати
                    var productFromDb = await dbContext.Products.FindAsync(product.ProductId);
                    if (productFromDb != null)
                    {
                        // Добавяне само на референции към съществуващите продукти
                        cartFromDb.Products.Add(productFromDb);
                    }
                    else
                    {
                        // Ако продуктът не съществува в базата данни, добавяме новия продукт
                        cartFromDb.Products.Add(product);
                    }
                }


                // Запазване на промените в базата данни
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

