using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEV_2_SORU_1
{
    public interface IQueue
    {
        void Insert(object o);
        object Remove();
        object Peek();
        Boolean IsEmpty();
        decimal OrtalamaSureHesapla();
        decimal ToplamSureHesapla(decimal iSure);
        string Listele();
    }
}
