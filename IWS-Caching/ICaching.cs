using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWS_Caching
{
    public interface ICaching
    {
        List<string> getContract();

        void updateContracts(List<string> c);

        void updateStations(string contract, List<string>[] s);

        void updateAvailableBike(string contract, string station, int number);

        int getAvailableBike(string contract, string station);

        List<string>[] getStations(string c);
    }
}
