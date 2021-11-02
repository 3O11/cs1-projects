using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Nezarka.Net
{
    class CartRequest : IRequest
    {
        public CartRequest(Customer customer, Book book, int updateQuantity)
        {
            _customer = customer;
            _book = book;
            _quantityDelta = updateQuantity;
        }

        public new RequestType GetType()
        {
            return RequestType.GET_CART;
        }

        public Customer GetCustomer()
        {
            return _customer;
        }

        public Book GetBook()
        {
            return _book;
        }

        public bool UpdateModel()
        {
            if (_book == null) return true;

            int itemIndex = _customer.ShoppingCart.Items.FindIndex(item => item.BookId == _book.Id);
            if (_quantityDelta > 0)
            {
                if (itemIndex == -1)
                {
                    _customer.ShoppingCart.Items.Add(new ShoppingCartItem { BookId = _book.Id, Count = 1 });
                    return true;
                }
                else
                {
                    _customer.ShoppingCart.Items[itemIndex].Count += _quantityDelta;
                    return true;
                }
            }
            else if (_quantityDelta < 0)
            {
                if (itemIndex == -1 || _customer.ShoppingCart.Items[itemIndex].Count < _quantityDelta)
                {
                    return false;
                }
                else
                {
                    if (_customer.ShoppingCart.Items[itemIndex].Count + _quantityDelta > 0)
                        _customer.ShoppingCart.Items[itemIndex].Count += _quantityDelta;
                    else
                        _customer.ShoppingCart.Items.RemoveAt(itemIndex);
                    return true;
                }
            }
            else return true;
        }

        Book _book;
        Customer _customer;
        int _quantityDelta;
    }
}
