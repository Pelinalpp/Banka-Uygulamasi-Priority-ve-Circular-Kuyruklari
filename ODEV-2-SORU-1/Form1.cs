using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ODEV_2_SORU_1
{
    public partial class frmSoru1 : Form
    {
        public frmSoru1()
        {
            InitializeComponent();
        }

        CircularArrayTypedQueue cirArr = new CircularArrayTypedQueue(20);
        PriorityArrayTypedQueueKB priArr = new PriorityArrayTypedQueueKB(20);
        PriorityArrayTypedQueueBK priArrBK = new PriorityArrayTypedQueueBK(20);

        public void MusteriEkle()
        {
            //Müşteri numarası ve random işlem süreleri atandı.
            Random r = new Random();

            for (int i = 0; i < 20; i++)
            {
                Musteri m = new Musteri();
                m.MusteriNo = i + 1;
                m.IslemSuresi = r.Next(60, 600);
                cirArr.Insert(m);
                priArr.Insert(m);
                priArrBK.Insert(m);
            }
        }

        private void frmSoru1_Load(object sender, EventArgs e)
        {
            MusteriEkle();
        }
        
        private void btnRasgeleMusteriKB_Click(object sender, EventArgs e)
        {
            txtRasgeleListeleKB.Text = "";
            txtRasgeleListeleKB.Text += cirArr.Listele();
        }

        private void btnOncelikliMusteriKB_Click(object sender, EventArgs e)
        {
            txtOncelikliListeleKB.Text = "";
            txtOncelikliListeleKB.Text += priArr.Listele();
        }

        public string KisalanSureleriBul()
        {
            string temp = "";
            //Listeleme işlemini yaparken toplamSure değişkeni 20 müşterinin toplam süresini tutmuştu.
            //Kısalan süreleri hesaplamak için toplam süre tekrar hesaplanacak. Bu yüzden sıfırladık.
            cirArr.toplamSure = 0;

            for (int i = 0; i < 20; i++)
            {
                //Listeleme işlemini yaparken toplamSure değişkeni 20 müşterinin toplam süresini ve
                //Kısalan süre bulunurken her for'a giridğinde o müşteriye kadar olan toplam süre tutulmuştu
                //Kısalan süreleri hesaplamak için her müşteri için öncelikli sıradaki toplam süresi tekrar hesaplanacak. 
                //Bu yüzden sıfırladık.
                priArr.toplamSure = 0;
                //Döngüsel kuyruktaki her müşterinin toplam işlem süresi hesaplandı.
                decimal donguselToplamSure = cirArr.ToplamSureHesapla(((Musteri)cirArr.Queue[i]).IslemSuresi);

                for (int j = 0; j < 20; j++)
                {
                    decimal oncelikToplamSure = priArr.ToplamSureHesapla(((Musteri)priArr.Queue[j]).IslemSuresi);
                    //Döngüsel kuyruktaki müşteri, öncelikli kuyrukta bulunana kadar geçen süre hesaplandı.
                    if (((Musteri)priArr.Queue[j]).MusteriNo == i + 1)
                    {
                        //Her bir müşterinin dongusel kuyrukta geçirdiği süre ile oncelikli kuyrukta geçirdiği süre karşılaştırıldı. Kısaldıysa yazma işlemi gerçekleşti.
                        if (oncelikToplamSure < donguselToplamSure)
                        {
                            temp += "Müşteri no :             " + ((Musteri)priArr.Queue[j]).MusteriNo.ToString() + Environment.NewLine + "İşlem süresi :           " + ((Musteri)priArr.Queue[j]).IslemSuresi.ToString() + " sn." + Environment.NewLine + "Kazanç (fark) :        " + (donguselToplamSure - oncelikToplamSure).ToString() + Environment.NewLine + "Kazanç (yüzde) :      " + String.Format("{0:0.00}", ((oncelikToplamSure * 100) / donguselToplamSure)) + Environment.NewLine + Environment.NewLine;
                        }
                        break;
                    }
                }
            }
            return temp;
        }

        private void btnKisalanSureBulKB_Click(object sender, EventArgs e)
        {
            txtKisalanSureListesiKB.Text += KisalanSureleriBul();
        }

        private void btnOrtalamaTamamlanmaKB_Click(object sender, EventArgs e)
        {
            txtOrtalamaTamamlanmaKB.Text += "Ortalama işlem tamamlanma süresi : " + cirArr.OrtalamaSureHesapla().ToString() + " sn.";
        }

        private void btnOncelikliMusteriBK_Click(object sender, EventArgs e)
        {
            txtOncelikliListeleBK.Text = "";
            txtOncelikliListeleBK.Text += priArrBK.Listele();
        }

        public string KisalanSureleriBulBK()
        {
            string temp = "";
            //Listeleme işlemini yaparken toplamSure değişkeni 20 müşterinin toplam süresini tutmuştu.
            //Kısalan süreleri hesaplamak için toplam süre tekrar hesaplanacak. Bu yüzden sıfırladık.
            cirArr.toplamSure = 0;

            for (int i = 0; i < 20; i++)
            {
                //Listeleme işlemini yaparken toplamSure değişkeni 20 müşterinin toplam süresini ve
                //Kısalan süre bulunurken her for'a giridğinde o müşteriye kadar olan toplam süre tutulmuştu
                //Kısalan süreleri hesaplamak için her müşteri için öncelikli sıradaki toplam süresi tekrar hesaplanacak. 
                //Bu yüzden sıfırladık.
                priArrBK.toplamSure = 0;
                //Döngüsel kuyruktaki her müşterinin toplam işlem süresi hesaplandı.
                decimal donguselToplamSure = cirArr.ToplamSureHesapla(((Musteri)cirArr.Queue[i]).IslemSuresi);

                for (int j = 0; j < 20; j++)
                {
                    decimal oncelikToplamSure = priArrBK.ToplamSureHesapla(((Musteri)priArrBK.Queue[j]).IslemSuresi);
                    //Döngüsel kuyruktaki müşteri, öncelikli kuyrukta bulunana kadar geçen süre hesaplandı.
                    if (((Musteri)priArrBK.Queue[j]).MusteriNo == i + 1)
                    {
                        //Her bir müşterinin dongusel kuyrukta geçirdiği süre ile oncelikli kuyrukta geçirdiği süre karşılaştırıldı. Kısaldıysa yazma işlemi gerçekleşti.
                        if (oncelikToplamSure < donguselToplamSure)
                        {
                            temp += "Müşteri no :             " + ((Musteri)priArrBK.Queue[j]).MusteriNo.ToString() + Environment.NewLine + "İşlem süresi :           " + ((Musteri)priArrBK.Queue[j]).IslemSuresi.ToString() + " sn." + Environment.NewLine + "Kazanç (fark) :        " + (donguselToplamSure - oncelikToplamSure).ToString() + Environment.NewLine + "Kazanç (yüzde) :      " + String.Format("{0:0.00}", ((oncelikToplamSure * 100) / donguselToplamSure)) + Environment.NewLine + Environment.NewLine;
                        }
                        break;
                    }
                }
            }
            return temp;
        }

        private void btnKisalanSureBulBK_Click(object sender, EventArgs e)
        {
            txtKisalanSureListesiBK.Text += KisalanSureleriBulBK();
        }

        private void btnOrtalamaTamamlanmaBK_Click(object sender, EventArgs e)
        {
            txtOrtalamaTamamlanmaBK.Text += "Ortalama işlem tamamlanma süresi : " + cirArr.OrtalamaSureHesapla().ToString() + " sn.";
        }


    }
}
