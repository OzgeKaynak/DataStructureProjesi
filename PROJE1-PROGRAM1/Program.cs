using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ilk1
{
    class Program
    {
        static Random rnd = new Random();
        static ArrayList array = new ArrayList();
        
        static void Main(string[] args)
        {

            Console.WriteLine("Görmek istediğiniz işlemin numarasını giriniz:");
            Console.WriteLine("1-(01.01.1995-31.12.1997) yılları arasında bir tarih");
            Console.WriteLine("2-(01.01.1995-31.12.1997) yılları arasında istediginiz kadar tarih");
            Console.WriteLine("3-50,100,500,1000 kişilik doğum yılıları dahil çakışma listesi ve çakışan tarihlerin gösterimi");
            Console.WriteLine("4-50,100,500,1000 kişilik çakışma listesi ve çakışan tarihlerin gösterimi");
            Console.Write("Şıkkınız :");
            int sayi = Convert.ToInt16(Console.ReadLine());
            int gun, ay, yıl;

            int[] gunler = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            switch (sayi)
            {
                case 1:
                    ay = rnd.Next(0, 12);
                    gun = rnd.Next(0, gunler[ay]);
                    yıl = rnd.Next(1995, 1998);
                    Console.WriteLine(gun + "." + ay + "." + yıl);
                    Console.ReadLine();
                   
                    break;
                case 2:
                    Console.WriteLine("görmek istediginiz tarih sayısını giriniz:");
                    int n = Convert.ToInt16(Console.ReadLine());
                    for (int i = 0; i < n; i++)
                    {
                        ay = rnd.Next(0, 12);
                        gun = rnd.Next(0, gunler[ay]);
                        yıl = rnd.Next(1995, 1998);
                        Console.WriteLine(gun + "." + ay + "." + yıl);

                    }
                    Console.ReadLine();
                    break;
                case 3:
                    int[,] mtr2 = new int[4, 11];
                    int[, ,] mtr = new int[12, 31, 3];
                    int[] dizi1 = { 50, 100, 500, 1000 };
                    int toplam = 0;
                    int ort = 0;
                    

                    for (int index = 0; index < 4; index++)
                    {
                        Console.WriteLine("n=" + dizi1[index] + "  için degerler:");
                        
                        for (int i = 0; i <10; i++)
                        {
                            ucboyutmatrisSfr(mtr, 12, 31, 3);
                            for (int j = 0; j < dizi1[index]; j++)
                            {

                                ay = rnd.Next(0, 12);
                                gun = rnd.Next(0, gunler[ay]);
                                yıl = rnd.Next(1995, 1998);
                                mtr[ay, gun, yıl - 1995]++;                               
                            }

                            for (int a = 0; a < 11; a++)
                            {
                                for (int b = 0; b < 31; b++)
                                {
                                    for (int c = 0; c < 3; c++)
                                    {
                                        if (mtr[a, b, c] > 1)
                                        {
                                            toplam += mtr[a, b, c] - 1;

                                        }
                                    }
                                }
                            }
                            mtr2[index, i] = toplam;
                            toplam = 0;
                            ucBoyutlumatris_atama(mtr);                           
                        }
                        ucBoyutluYazdir();
                        Console.ReadLine();
                    }
                 
                    for (int c = 0; c < 4; c++)
                    {
                        Console.WriteLine("\n");
                        Console.Write(dizi1[c] + "kişilik-->");
                        for (int i = 0; i < 10; i++)
                        {
                            Console.Write("{0,2}", mtr2[c, i] + " ");
                            ort += mtr2[c, i];
                        }
                        Console.WriteLine((float)ort / 10);
                        ort = 0;
                    }

                    Console.ReadLine();

                    break;

                case 4:
                    int[,] matris2 = new int[4, 11];
                    int[,] matris = new int[12, 31];
                    int[] dizi = { 50, 100, 500, 1000 };
                    int tpl = 0;
                    int ortalama = 0;


                    for (int index = 0; index < 4; index++)
                    {
                        Console.WriteLine("n=" + dizi[index] + "  için degerler:");
                        for (int i = 0; i <10; i++)
                        {
                            matrisSıfırla(matris, 12, 31);
                            for (int j = 0; j < dizi[index]; j++)
                            {

                                ay = rnd.Next(0, 12);
                                gun = rnd.Next(0, gunler[ay]);
                                matris[ay, gun]++;
                            }

                            for (int a = 0; a < 11; a++)
                            {
                                for (int b = 0; b < 31; b++)
                                {

                                    if (matris[a, b] > 1)
                                    {
                                        tpl += matris[a, b] - 1;
                                    }
                                }
                            }
                            matris2[index, i] = tpl;
                            tpl = 0;
                            matris_yazdır(matris);
                        }
                        Console.ReadLine();

                    }
                    for (int c = 0; c < 4; c++)
                    {
                        Console.WriteLine("\n");
                        Console.Write(dizi[c] + "kişilik-->");
                        for (int i = 0; i < 10; i++)
                        {
                            Console.Write(matris2[c, i] + " ");
                            ortalama += matris2[c, i];
                        }
                        Console.WriteLine((float)ortalama / 10);
                        ortalama = 0;
                    }

                    Console.ReadLine();
                    break;
            }
        }
        public static void matris_yazdır(int[,] matris)
        {
            string[] aylar = {"Ocak","Şubat","Mart","Nisan","Mayıs","Haziran","Temmuz","Ağustos","Eylül","Ekim","Kasım","Aralık" };
            for (int i = 0; i < 12; i++)
            {
                Console.Write("{0,7}",aylar[i]);
                for (int j = 0; j < 31; j++)
                {
                    Console.Write("{0,2}", matris[i, j]);
                }

                Console.WriteLine(); 
            }
            Console.WriteLine("\n" + "\n");
        }
        public static void ucBoyutlumatris_atama(int[, ,] matris)
        {
            int[,] mtr = new int[12, 31];
            for (int k = 0; k < 3; ++k)
            {
                matrisSıfırla(mtr,12,31);
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 31; j++)
                    {
                        mtr[i, j] = matris[i, j, k];
                    } 
                }
                array.Add(mtr);
            }
        }
        public static void ucBoyutluYazdir()
        {
            string[] aylar = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
            int i=0;
           
            Console.WriteLine("1995 Yılında Dogmus Kisiler \n");
              while(i<array.Count)
             {
                 if (array[i] != null)
                 {
                     for (int k = 0; k < 12; k++)
                     {
                         Console.Write("{0,7}", aylar[k]);
                         for (int j = 0; j < 31; j++)
                         {

                             Console.Write("{0,2}", ((int[,])array[i])[k, j]);

                         }

                         Console.WriteLine();
                     }
                     Console.WriteLine("\n" + "\n");
                 }
                array[i] = null;
                i += 3;
                
            }
              Console.ReadLine();

              i = 1;
              Console.WriteLine("1996 Yılında Dogmus Kisiler \n");
             
            
            while (i < array.Count)
              {
                  if (array[i] != null)
                  {
                      for (int k = 0; k < 12; k++)
                      {
                          Console.Write("{0,7}", aylar[k]);
                          for (int j = 0; j < 31; j++)
                          {

                              Console.Write("{0,2}", ((int[,])array[i])[k, j]);

                          }

                          Console.WriteLine();
                      }
                      Console.WriteLine("\n" + "\n");
                  }
                  array[i] = null;
                  i += 3;
                 
              }

              Console.ReadLine();
              i = 2;
              Console.WriteLine("1997 Yılında Dogmus Kisiler \n");
              
            
            while (i < array.Count)
              {
                  if (array[i] != null)
                  {
                      for (int k = 0; k < 12; k++)
                      {

                          Console.Write("{0,7}", aylar[k]);
                          for (int j = 0; j < 31; j++)
                          {

                              Console.Write("{0,2}", ((int[,])array[i])[k, j]);

                          }
                          Console.WriteLine();
                      }

                   Console.WriteLine("\n" + "\n");   
                  }
                  array[i] = null;
                  i += 3;
                  
              }
        }
        
        public static void matrisSıfırla(int[,] matris2, int satir, int sutun)
        {
            for (int i = 0; i < satir; i++)
            {
                for (int j = 0; j < sutun; j++)
                    matris2[i, j] = 0;
            }
        }
        public static void ucboyutmatrisSfr(int[, ,] matris2, int ayi, int gunu, int yılı)
        {
            for (int i = 0; i < ayi; i++)
            {
                for (int j = 0; j < gunu; j++)
                {
                    for (int k = 0; k < yılı; k++)
                        matris2[i, j, k] = 0;
                }
            }
        }
    }
}
