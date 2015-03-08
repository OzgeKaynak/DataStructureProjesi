using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace _2.proje
{  
    class Program
    {
        static Random rnd = new Random();
        static Ülke[] ülke = new Ülke[9];
        static int ülke_sıra=0;
        static void Main(string[] args)
        {
             int ögr_say = 0;
             Ülke[] ülkeler=kontenjanAl();
           
             Ögrenci[] bilgi =  ögrenciBilgileri();
             foreach (object a in bilgi)
             {
                 ögr_say++;
             }

             ögrenci_yerlesdir(ülkeler, bilgi, ögr_say);

             Console.ReadLine();
        }


        public static void ögrenci_yerlesdir(Ülke[] ü, Ögrenci[] bilgi, int sayi)
        {
            int j, toplam_kontenjan = 0, gecenÖgr = 0, say = 0;
            double[] kznÖgr = new double[9];
            double x;


            for (j = 0; j < 9; j++)
            {
                toplam_kontenjan += ü[j].kontenjan;
            }

            int[] not_dizisi = new int[sayi];
            for (int o = 0; o < sayi; o++)
            {
                if (bilgi[o].not > 60)
                {
                    gecenÖgr++;
                    not_dizisi[say] = bilgi[o].not;
                    say++;
                }
            }

            int[] notSıralaması = new int[gecenÖgr];

            for (int m = 0; m < gecenÖgr; m++)
            {
                for (int n = m + 1; n < gecenÖgr; n++)
                {
                    if (not_dizisi[n] > not_dizisi[m])
                    {
                        notSıralaması[m] = not_dizisi[n];
                        not_dizisi[n] = not_dizisi[m];
                        not_dizisi[m] = notSıralaması[m];
                    } 
                 
                } bilgi[m].not = not_dizisi[m];
            }
         
            string[] kontenjan_olmayan= new string[9];
            double basarılı = 0;
            for (int c = 0; c < 9; c++)
            {
                x = ü[c].kontenjan;
                kznÖgr[c] = Math.Round((x * gecenÖgr / toplam_kontenjan));
                if (ü[c].kontenjan == 0)
                {
                    kontenjan_olmayan[c] = ü[c].adı;
                }
                basarılı += kznÖgr[c];
   
            }
            int[] sıralı_ülke_kontenjan = new int[9];
            double[] sıralı_kznöğr = new double[9];
            string[] ülke_ad_sırala = new string[9];
            for (int m = 0; m < 9; m++)
            {
                for (int n = m + 1; n < 9; n++)
                {
                    if (kznÖgr[n] > kznÖgr[m])
                    {
                        sıralı_kznöğr[m] = kznÖgr[n];
                        kznÖgr[n] = kznÖgr[m];
                        kznÖgr[m] = sıralı_kznöğr[m];
                        ülke_ad_sırala[m] = ü[n].adı;
                        ü[n].adı = ü[m].adı;
                        ü[m].adı = ülke_ad_sırala[m];
                        sıralı_ülke_kontenjan[m] = ü[n].kontenjan;
                        ü[n].kontenjan = ü[m].kontenjan;
                        ü[m].kontenjan = sıralı_ülke_kontenjan[m];
                    }
                }
            }
                for (int k = 0; k < 9; k++)
                {
                    if (kznÖgr[k] > ü[k].kontenjan)
                    {
                        kznÖgr[k] = ü[k].kontenjan;
                    }
                   
                    while (basarılı > gecenÖgr)
                    {
                        if (kznÖgr[k] > kznÖgr[k + 1])
                            kznÖgr[k]--;
                        else
                            kznÖgr[k + 1]--;
                        basarılı--;
                    }

                     if (kznÖgr[k] == 0 && kontenjan_olmayan[k] != null)
                         Console.WriteLine("***" + ü[k].adı + " ülkesi bu sene kontenjan açmamıştır!!!");
                     else
                     {
                         if (kznÖgr[k] == 0)
                             Console.WriteLine("***" + ü[k].adı + "  ülkesine bu sene  ögrenci gitmemektedir...");
                         else
                             Console.WriteLine("***" + ü[k].adı + "  ülkesine " + kznÖgr[k] + "  ögrenci gitmektedir...");
                         öğr_yazdır(kznÖgr[k], bilgi, gecenÖgr);   
                     }         
                        Console.WriteLine("\n");  
                }

                Console.WriteLine("********************************************************");
                if (gecenÖgr - basarılı == 0)
                    Console.WriteLine("Tüm öğrenciler yerleşmiştir!!!");

                else
                {
                    Console.WriteLine("***Yerleştirilemeyen öğrenciler***");
                    öğr_yazdır(gecenÖgr-basarılı, bilgi, gecenÖgr);
                }
            }
        
        public static void öğr_yazdır(double kontenjan, Ögrenci[] bilgi,int gecen_ögr)
        {
            ülke[ülke_sıra] = new Ülke();
                int say = 0, i = 0;

                if (kontenjan != 0)
                {
                    Console.WriteLine("  AD           SOYAD         NOT");
                    Console.WriteLine("*******    *************   *******");
                    do
                    {
                        if (bilgi[i].isim != null && bilgi[i].soyad != null && bilgi[i].not != 0  )
                        {
                            
                            ülke[ülke_sıra].talebeler[say] = bilgi[i];
                            Console.WriteLine("{0,-15}{1,-10}{2,7}", ülke[ülke_sıra].talebeler[say].isim, ülke[ülke_sıra].talebeler[say].soyad, ülke[ülke_sıra].talebeler[say].not);
                            say++;
                        }
                        bilgi[i].isim = null;
                        bilgi[i].soyad = null;
                        bilgi[i].not = 0;

                        i++;
                    } while (say != kontenjan && i <= gecen_ögr);
                }
            if(ülke_sıra<8)
            ülke_sıra++;
        }
        public static  Ögrenci[]  ögrenciBilgileri()
        {
            int ogrSay, say1, say2;
            string[] ad_dizisi = { "Elanur", "Özge", "Gül", "Nebahat", "Rabiye", "Songül", "Dilek", "Aybüke", "Betül", "Taner", "Ersan", "İhsan", "Tolga", "Oğuz", "Burak", "Hasan", "Hüseyin", "Mahmut", "Umut", "Selim", "Erdi" };
            string[] soyad_dizisi = { "Kaynak", "Bilgin", "Aydoğan", "Çolak", "Oral", "Bayram", "Kara", "Hazer", "Sönmez", "Öztürk", "Kurt", "Onay", "Gümüş", "Aktaş", "Şimşek", "Uysal", "Yaprak", "Yüksel", "Çolak", "Gürsoy" };
           
            do
            {
                Console.WriteLine("[1-150] arasında başvurabilecek öğrenci sayısını giriniz:");
                ogrSay = Convert.ToInt16(Console.ReadLine());
            } while (ogrSay >150 || ogrSay <1);
          
            Ögrenci[] kişiler = new Ögrenci[ogrSay];
            for(int i=0;i<ogrSay;i++)
            { 
                say1 = rnd.Next(0, 20);
                say2 = rnd.Next(0, 20);
                kişiler[i] = new Ögrenci();
                kişiler[i].isim=ad_dizisi[say1];
                kişiler[i].soyad=soyad_dizisi[say2];
                kişiler[i].not = rnd.Next(40, 100);   
            }
            return kişiler;
        }
        public static Ülke[] kontenjanAl()
        {  
            Ülke[] ülkeler=new Ülke[9];

            string[] ad = { "ENG", " GER", "FRE", "ITA", "ESP", "USA", "JAP", "CHN", "RUS" };
            int[] sayi = new int[9];
            string cevap;
            int i, j;
        Console.WriteLine("Kontenjan degerini atamak ister misiniz?{e/E/H/h}");
        cevap = Console.ReadLine();
        if (cevap != "e" && cevap != "E" && cevap != "h" && cevap != "H")
        {
            do
            {
                Console.WriteLine("Lütfen 'e','E','H' ya da 'h' giriniz!");
                cevap =Console.ReadLine();
            } while (cevap != "e" && cevap != "E" && cevap != "h" && cevap != "H");
        }
        
        if (cevap == "e" || cevap == "E")
        {
            for (i = 0; i < 9; i++)
            {
                ülkeler[i] = new Ülke();
                do
                {
                    Console.WriteLine(ad[i] + " ülkesinin kontenjan sayısını [0,10] arasında giriniz:");
                    sayi[i] = Convert.ToInt32(Console.ReadLine());
                } while (sayi[i] < 0 || sayi[i] > 10);

                ülkeler[i].kontenjan=sayi[i];
                
                ülkeler[i].adı = ad[i];
            }
        }
        else if (cevap == "h" || cevap == "H")
        {
            for (j = 0; j < 9; j++)
            {
                ülkeler[j] = new Ülke();
               sayi[j]= rnd.Next(0, 10);
               ülkeler[j].kontenjan = sayi[j];
               ülkeler[j].adı = ad[j];
            }
        }
        return ülkeler;
    }
      
 }
    class Ülke
    {
        public string adı;
        public int kontenjan;
        public Ögrenci[] talebeler=new Ögrenci[400];
    }
    class Ögrenci
    {
        public string isim;
        public string soyad;
        public int not;
    }
}
