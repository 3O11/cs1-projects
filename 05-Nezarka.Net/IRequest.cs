using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Nezarka.Net
{
    enum RequestType
    {
        INVALID,
        GET_CART,
        GET_BOOKS,
    }

    interface IRequest
    {
        RequestType GetType();
        Customer GetCustomer();
        Book GetBook();
        bool UpdateModel();
    }
}
