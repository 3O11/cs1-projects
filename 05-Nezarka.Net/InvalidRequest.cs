using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Nezarka.Net
{
    class InvalidRequest : IRequest
    {
        static InvalidRequest _invalidRequest = new InvalidRequest();
        public static InvalidRequest GetInstance
        {
            get =>_invalidRequest;
        }
        InvalidRequest() { }

        public new RequestType GetType()
        {
            return RequestType.INVALID;
        }

        public Customer GetCustomer()
        {
            return null;
        }

        public Book GetBook()
        {
            return null;
        }

        public IList<Book> GetBooks()
        {
            return null;
        }

        public bool UpdateModel()
        {
            return true;
        }
    }
}
