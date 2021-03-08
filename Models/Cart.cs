using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();


        //method to add item to cart
        public virtual void AddItem(Book bo, int qty)
            {
                CartLine line = Lines.Where(b => b.Book.BookId == bo.BookId)
                    .FirstOrDefault();

                if (line ==null)
                {
                    Lines.Add(new CartLine
                    {
                        Book = bo,
                        Quantity = qty

                    });
                }

                else
                {
                    line.Quantity += qty;
                }
        }

        //method to remove an item
        public virtual void RemoveLine(Book bo) =>
            Lines.RemoveAll(x => x.Book.BookId == bo.BookId);

        //method to remove all items from cart
        public virtual void Clear() => Lines.Clear();

        //method to get cart total
        //compute the total for the cart by multiplying the book price by the book quantity
        public double ComputeTotalSum() => Lines.Sum(e => e.Book.Price * e.Quantity);
       

        public class CartLine
        { 
            public int CartLineID { get; set; }
            public Book Book { get; set; }
            public int Quantity { get; set; }

        }

    }
}
