using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEV_2_SORU_1
{
    public class CircularArrayTypedQueue:IQueue
    {
        public object[] Queue;
        private int front = -1;
        private int rear = -1;
        private int size = 0;
        private int count = 0;
        public decimal toplamSure = 0;

        public CircularArrayTypedQueue(int size)
        {
            this.size = size;
            Queue = new object[size];
        }
        public void Insert(object o)
        {
            if (count == size)
                throw new Exception("Queue dolu.");

            if (front == -1)
                front = 0;
            if (rear == size - 1)
            {
                rear = 0;
                Queue[rear] = (Musteri)o;
            }
            else
                Queue[++rear] = (Musteri)o;

            count++;
        }

        public object Remove()
        {
            if (IsEmpty())
                throw new Exception("Queue boş.");

            object temp = Queue[front];
            Queue[front] = null;

            if (front == size - 1)
                front = 0;
            else
                front++;

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

        public string Listele()
        {
            string temp = "";

            for (int i = 0; i < size; i++)
            {
                //Listeleme işleminde ToplamSureHesapla(iSure) çağırılarak her müşterinin kuyrukta kalma süresi hesaplandı ve yazdırıldı.
                temp += "Müşteri no :             " + ((Musteri)Queue[i]).MusteriNo.ToString() + Environment.NewLine + "İşlem süresi :           " + ((Musteri)Queue[i]).IslemSuresi.ToString() + " sn." + Environment.NewLine + "İşinin bitme süresi :  " + ToplamSureHesapla(((Musteri)Queue[i]).IslemSuresi) + " sn." + Environment.NewLine + Environment.NewLine;
            }
            return temp;
        }
    }
}
