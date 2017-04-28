using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEV_2_SORU_1
{
    public class PriorityArrayTypedQueueBK:IQueue
    {
        public object[] Queue;
        private int front = -1;
        private int count = 0;
        private int size = 0;
        public decimal toplamSure = 0;

        public PriorityArrayTypedQueueBK(int size)
        {
            this.size = size;
            this.Queue = new object[size];
        }

        public void Insert(object o)
        {
            if (count == size)
                throw new Exception("Queue dolu");
            if (IsEmpty())
            {
                Queue[++front] = o;
                count++;
            }
            else
            {
                int i;
                Musteri m = (Musteri)o;
                for (i = count - 1; i >= 0; i--)
                {
                    //Büyükten küçüğe sıralama işlemi
                    if (m.IslemSuresi > ((Musteri)Queue[i]).IslemSuresi)
                        Queue[i + 1] = Queue[i];
                    else
                        break;
                }
                Queue[i + 1] = o;
                front++;
                count++;
            }
        }

        public object Remove()
        {
            if (IsEmpty())
                throw new Exception("Queue boş");
            object temp = Queue[front];
            Queue[front] = null;
            front--;
            count--;
            return temp;
        }

        public object Peek()
        {
            return Queue[front];
        }

        public bool IsEmpty()
        {
            return (count == 0);
        }

        public string Listele()
        {
            string temp = "";

            for (int i = 0; i < size; i++)
            {
                //Listeleme işleminde ToplamSureHesapla(iSure) çağırılarak her müşterinin işlem süresi büyükten küçüğe sıralandığında kuyrukta kalma süresi hesaplandı ve yazdırıldı.
                temp += "Müşteri no :             " + ((Musteri)Queue[i]).MusteriNo.ToString() + Environment.NewLine + "İşlem süresi :           " + ((Musteri)Queue[i]).IslemSuresi.ToString() + " sn." + Environment.NewLine + "İşinin bitme süresi :  " + ToplamSureHesapla(((Musteri)Queue[i]).IslemSuresi) + " sn." + Environment.NewLine + Environment.NewLine;
            }
            return temp;
        }

        public decimal ToplamSureHesapla(decimal iSure)
        {
            //Her müşterinin gönderilen işlem süresi toplam süreye eklendi. Böylece toplam bekleme süresi hesaplandı.
            return (toplamSure += iSure);
        }

        public decimal OrtalamaSureHesapla()
        {
            //20 müşteri için toplam geçen sürenin ortalaması hesaplandı.
            return (toplamSure / 20);
        }
    }
}
