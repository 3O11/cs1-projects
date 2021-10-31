using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Nezarka.Net
{
    class ViewStore
    {
        public ViewStore()
        {
        }

        public string GenerateErrorPage()
        {
            return "";
        }

        public string GenerateCart(Customer cust)
        {
            return "";
        }

        public string GenerateBookList(IList<Book> books)
        {
            return "";
        }

        public string GenerateBookDetail(Book book)
        {
            return "";
        }

        string generateCommonHeader()
        {
            return "";
        }

        string generateCommonFooter()
        {
            return "";
        }
    }
}
