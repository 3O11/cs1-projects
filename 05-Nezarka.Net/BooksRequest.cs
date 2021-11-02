using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Nezarka.Net
{
    class BooksRequest : IRequest
    {
        public BooksRequest(Customer customer, Book book)
        {
            _customer = customer;
            _book = book;
        }

        public new RequestType GetType()
        {
            return RequestType.GET_BOOKS;
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
            return true;
        }

        Customer _customer;
        Book _book;
    }
}
