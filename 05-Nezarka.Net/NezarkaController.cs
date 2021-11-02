using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace _05_Nezarka.Net
{
    class ControlStore
    {
        public ControlStore (
            ModelStore model,
            ViewStore view,
            TextReader input,
            TextWriter output)
        {
            _model = model;
            _view = view;
            _input = input;
            _output = output;
        }

        public void Run()
        {
            string currentCommand;

            while ((currentCommand = _input.ReadLine()) != null)
            {
                IRequest request = parseRequest(currentCommand);
                if (!request.UpdateModel())
                {
                    _output.Write(_view.Generate(InvalidRequest.GetInstance, _model));
                    _output.WriteLine("====");
                    continue;
                }
                _output.Write(_view.Generate(request, _model));
                _output.WriteLine("====");
            }
        }

        // I'm sorry for anyone who ever reads this after me
        IRequest parseRequest(string rawRequest)
        {
            Regex commRegex = new Regex(@"^GET (?<custID>[0-9]+) (http://www.nezarka.net/)(?<query>[a-zA-Z0-9/]+)$");
            Match m = commRegex.Match(rawRequest);
            if (!m.Success) return InvalidRequest.GetInstance;

            Customer customer = _model.GetCustomer(int.Parse(m.Groups["custID"].Value));
            if (customer == null) return InvalidRequest.GetInstance;

            string[] query = m.Groups["query"].Value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (query.Length == 0) return InvalidRequest.GetInstance;

            switch(query[0])
            {
                case "Books":
                    if (query.Length == 1) return new BooksRequest(customer, null);
                    else if (query.Length == 3 && query[1] == "Detail")
                    {
                        if (!int.TryParse(query[2], out int bookId)) return InvalidRequest.GetInstance;
                        Book book = _model.GetBook(bookId);
                        if (book == null) return InvalidRequest.GetInstance;
                        return new BooksRequest(customer, book);
                    }
                    else return InvalidRequest.GetInstance;
                case "ShoppingCart":
                    if (query.Length == 1) return new CartRequest(customer, null, 0);
                    else if (query.Length == 3)
                    {
                        if (!int.TryParse(query[2], out int bookId)) return InvalidRequest.GetInstance;
                        Book book = _model.GetBook(bookId);
                        if (book == null) return InvalidRequest.GetInstance;
                        switch(query[1])
                        {
                            case "Add":
                                return new CartRequest(customer, book, 1);
                            case "Remove":
                                return new CartRequest(customer, book, -1);
                            default:
                                return InvalidRequest.GetInstance;
                        }
                    }
                    else return InvalidRequest.GetInstance;
                default:
                    return InvalidRequest.GetInstance;
            }
        }

        ModelStore _model;
        ViewStore _view;
        TextReader _input;
        TextWriter _output;
    }
}
