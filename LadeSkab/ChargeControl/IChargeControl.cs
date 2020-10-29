using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{
    public interface IChargeControl
    {
        //IUsbCharger Charger { get; set; }

        void StartCharge();
        void StopCharge();
        bool Connected();
    }
}
