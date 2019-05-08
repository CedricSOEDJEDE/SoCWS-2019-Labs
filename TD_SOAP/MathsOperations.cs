using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace TD_SOAP
{
    public class MathsOperations : IMathsOperations
    {
        public int Add(int num1, int num2)
        {
            return (num1 + num2);
        }
        public int Multiply(int num1, int num2)
        {
            return (num1 * num2);
        }
    }
}
