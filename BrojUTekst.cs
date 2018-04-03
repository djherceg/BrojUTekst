using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrojUTekst
{
    class Program
    {
        enum Pol { m, ž, s };

        static string[] b1 = {"nula", "jedan[jedna]jedno", "dva[dve]dva","tri","četiri","pet","šest","sedam","osam","devet","deset",
            "jedanaest","dvanaest","trinaest","četrnaest","petnaest","šesnaest","sedamnaest","osamnaest","devetnaest"}; // moguće m(..), ž(..) i s(..)
        static string[] b10 = { "dva", "tri", "četr", "pe", "šez", "sedam", "osam", "deve" };    // + deset
        static string[] b100 = { "", "dve", "tri", "četiri", "pet", "šest", "sedam", "osam", "devet" }; // + sto
        static string[] b1000 = { "hiljada", "hiljade" };
        static string[] b1000000 = { "milion", "miliona" };

        static void Main(string[] args)
        {
            string unos;
            long broj;
            while (true)
            {
                unos = Console.ReadLine();
                if (long.TryParse(unos, out broj))
                {
                    Console.WriteLine(BrojUString(broj));
                }
            }
        }

        static string mžs(string broj, Pol p)
        {
            switch (p)
            {
                case Pol.m:
                    return m(broj);
                case Pol.ž:
                    return ž(broj);
                case Pol.s:
                    return s(broj);
            }
            throw new ArgumentException("p");
        }

        static string m(string broj)
        {
            int p = broj.IndexOf('[');
            if (p > 0)
                return broj.Substring(0, p);
            else
                return broj;
        }

        static string ž(string broj)
        {
            int p = broj.IndexOf('[');
            int q = broj.IndexOf(']');
            if (p > 0)
                return broj.Substring(p + 1, q - p - 1);
            else
                return broj;
        }

        static string s(string broj)
        {
            int p = broj.IndexOf(']');
            if (p > 0)
                return broj.Substring(p + 1, broj.Length - p - 1);
            else
                return broj;
        }

        static string BrojUString(long broj, Pol p = Pol.m)
        {
            string prefiks = (broj < 0) ? "minus " : string.Empty;

            if (broj == 0)
                return b1[0];
            else
                return prefiks + Br1000000(Math.Abs( broj), p);
        }

        static string Br1(long broj, Pol p = Pol.m)
        {
            if (broj != 0)
                return mžs(b1[(broj % 20)], p);
            else
                return string.Empty;
        }

        static string Br20(long broj, Pol p = Pol.m)
        {
            if (broj >= 20)
                return b10[(broj % 100) / 10 - 2] + "deset " + Br1(broj % 10, p);
            else
                return Br1(broj, p);
        }

        static string Br100(long broj, Pol p = Pol.m)
        {
            if (broj >= 100)
                return b100[broj / 100 - 1] + "sto " + Br20(broj % 100, p);
            else
                return Br20(broj, p);
        }

        static string Br1000(long broj, Pol p = Pol.m)
        {
            if (broj >= 1000)
                return BrojUString(broj / 1000, Pol.ž).Trim() + " " + Padež(broj / 1000, b1000[1], b1000[0]) + " " + Br100(broj % 1000);
            else
                return Br100(broj, p);
        }

        static string Br1000000(long broj, Pol p = Pol.m)
        {
            if (broj >= 1000000)
                return BrojUString(broj / 1000000).Trim() + " " + Padež2(broj / 1000000, b1000000[0], b1000000[1]) + " " + Br1000(broj % 1000000, p);
            else
                return Br1000(broj, p);
        }

        static string Padež(long broj, string hiljade, string hiljada)
        {
            long b;
            if ((broj >= 10) && (broj <= 20))
                b = 1;
            else
                b = broj % 10;
            switch (b)
            {
                case 2:
                case 3:
                case 4:
                    return hiljade;
                default:
                    return hiljada;
            }
        }

        static string Padež2(long broj, string milion, string miliona)
        {
            long b;
            if ((broj >= 10) && (broj <= 20))
                b = 2;
            else
                b = broj % 10;
            if (b == 1)

                return milion;
            else
                return miliona;

        }
    }
}
