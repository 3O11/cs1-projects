using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

        public void Run ()
        {
            string currentCommand = _input.ReadLine();

            while (currentCommand != null)
            {
                // TODO:
                // -> read input
                // -> parse command
                // -> update model
                // -> generate view
                // -> pass view to output
                // -> read new command

                currentCommand = _input.ReadLine();
            }
        }

        ModelStore _model;
        ViewStore _view;
        TextReader _input;
        TextWriter _output;
    }
}
