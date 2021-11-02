using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _05_Nezarka.Net
{
    class ViewStore
    {
        public ViewStore()
        {
        }

        public string Generate(IRequest request, ModelStore model)
        {
            switch(request.GetType())
            {
                case RequestType.GET_CART:
                    return generateCart((CartRequest)request, model);
                case RequestType.GET_BOOKS:
                    if (request.GetBook() == null) return generateBookList((BooksRequest)request, model);
                    else return generateBookDetail((BooksRequest)request);
                case RequestType.INVALID:
                    return generateInvalidRequest();
            }

            // The compiler would refuse to do its job without this. :(
            return generateInvalidRequest();
        }

        string generateCart(CartRequest request, ModelStore model)
        {
            if (request.GetCustomer().ShoppingCart.Items.Count == 0)
            {
                return
                    getHead() +
                    generateHeader(request) +
                    "	Your shopping cart is EMPTY.\n" +
                    getFoot();
            }

            decimal priceTotal = 0;
            StringBuilder cartContent = new StringBuilder();
            foreach(var item in request.GetCustomer().ShoppingCart.Items)
            {
                Book book = model.GetBook(item.BookId);
                string price = item.Count > 1 ? item.Count + " * " + book.Price + " = " + book.Price * item.Count : book.Price.ToString();
                priceTotal += book.Price * item.Count;
                cartContent.Append( "		<tr>\n");
                cartContent.Append($"			<td><a href=\"/Books/Detail/{item.BookId}\">{book.Title}</a></td>\n");
                cartContent.Append($"			<td>{item.Count}</td>\n");
                cartContent.Append($"			<td>{price} EUR</td>\n");
                cartContent.Append($"			<td>&lt;<a href=\"/ShoppingCart/Remove/{item.BookId}\">Remove</a>&gt;</td>\n");
                cartContent.Append( "		</tr>\n");
            }

            return
                 getHead() +
                 generateHeader(request) +
                 "	Your shopping cart:\n" +
                 "	<table>\n" +
                 "		<tr>\n" +
                 "			<th>Title</th>\n" +
                 "			<th>Count</th>\n" +
                 "			<th>Price</th>\n" +
                 "			<th>Actions</th>\n" +
                 "		</tr>\n" +
                 cartContent.ToString() +
                 "	</table>\n" +
                $"	Total price of all items: {priceTotal} EUR\n" +
                 getFoot();
        }

        string generateBookList(BooksRequest request, ModelStore model)
        {
            StringBuilder bookList = new StringBuilder();
            int i = 0;
            while (i * 3 < model.GetBooks().Count)
            {
                bookList.Append("		<tr>\n");
                for (int j = 0; j < 3 && j + i * 3 < model.GetBooks().Count; j++)
                {
                    Book book = model.GetBooks()[i * 3 + j];
                    bookList.Append( "			<td style=\"padding: 10px;\">\n");
                    bookList.Append($"				<a href=\"/Books/Detail/{book.Id}\">{book.Title}</a><br />\n");
                    bookList.Append($"				Author: {book.Author}<br />\n");
                    bookList.Append($"				Price: {book.Price} EUR &lt;<a href=\"/ShoppingCart/Add/{book.Id}\">Buy</a>&gt;\n");
                    bookList.Append( "			</td>\n");
                }
                bookList.Append("		</tr>\n");
                i++;
            }

            return
                 getHead() +
                 generateHeader(request) +
                 "	Our books for you:\n" +
                 "	<table>\n" +
                 bookList.ToString() +
                 "	</table>\n" +
                 getFoot();

        }

        string generateBookDetail(BooksRequest request)
        {
            return
                 getHead() +
                 generateHeader(request) +
                 "	Book details:\n" +
                $"	<h2>{request.GetBook().Title}</h2>\n" +
                 "	<p style=\"margin-left: 20px\">\n" +
                $"	Author: {request.GetBook().Author}<br />\n" +
                $"	Price: {request.GetBook().Price} EUR<br />\n" +
                 "	</p>\n" +
                $"	<h3>&lt;<a href=\"/ShoppingCart/Add/{request.GetBook().Id}\">Buy this book</a>&gt;</h3>\n" +
                 getFoot();
        }

        string generateInvalidRequest()
        {
            return
                getHead() +
                "<p>Invalid request.</p>\n" +
                getFoot();
        }

        string getHead()
        {
            return
                "<!DOCTYPE html>\n" +
                "<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">\n" +
                "<head>\n" +
                "	<meta charset=\"utf-8\" />\n" +
                "	<title>Nezarka.net: Online Shopping for Books</title>\n" +
                "</head>\n" +
                "<body>\n";
        }

        string getFoot()
        {
            return
                "</body>\n" +
                "</html>\n";
        }

        string generateHeader(IRequest request)
        {
            return
                 "	<style type=\"text/css\">\n" +
                 "		table, th, td {\n" +
                 "			border: 1px solid black;\n" +
                 "			border-collapse: collapse;\n" +
                 "		}\n" +
                 "		table {\n" +
                 "			margin-bottom: 10px;\n" +
                 "		}\n" +
                 "		pre {\n" +
                 "			line-height: 70%;\n" +
                 "		}\n" +
                 "	</style>\n" +
                 "	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>\n" +
                $"	{request.GetCustomer().FirstName}, here is your menu:\n" +
                 "	<table>\n" +
                 "		<tr>\n" +
                 "			<td><a href=\"/Books\">Books</a></td>\n" +
                $"			<td><a href=\"/ShoppingCart\">Cart ({request.GetCustomer().ShoppingCart.Items.Count})</a></td>\n" +
                 "		</tr>\n" +
                 "	</table>\n";
        }
    }
}
