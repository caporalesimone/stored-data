using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StoredData data = new StoredData(".", "UOAlive", "Giandomenico Fracchia");

            int serial = 0;

            data.StoreData(serial, "serial_global", StoredData.StoreType.Global);
            data.StoreData(serial, "serial_server", StoredData.StoreType.Server);
            data.StoreData(serial, "serial_char", StoredData.StoreType.Character);
        }
    }
}
