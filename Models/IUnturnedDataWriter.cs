using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnturnedDataParser.Models
{
    public interface IUnturnedDataWriter
    {
        void WriteTo(Stream stream);
    }
}
