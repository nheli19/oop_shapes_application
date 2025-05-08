using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proekt_formichki_klasove
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        void Redo();
    }
}
