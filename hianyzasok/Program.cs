namespace hianyzasok
{
    public class Hianyzas
    {
        public int Honap { get; set; }
        public int Nap { get; set; }
        public string Nev { get; set; }
        public string Hianyzasok { get; set; }
    }
    public class Program
    {
        static List<Hianyzas> hianyzas = new List<Hianyzas>();
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //1. feladat
            Beolvas();

            //2. feladat
            Feladat2();

            //3. feladat
            Feladat3();

            //4. feladat
            //hetnapja();

            //5. feladat
            Feladat5();

            //6. feladat
            Feladat6();

            //7. feladat
            Feladat7();

            Console.ReadLine();
        }
        private static void Beolvas()
        {
            StreamReader sr = new StreamReader(@"naplo.txt");

            int honap = 0, nap = 0;

            while (!sr.EndOfStream)
            {
                Hianyzas uj = new Hianyzas();

                string[] line = sr.ReadLine().Split(' ');

                if (line.Contains("#"))
                {
                    honap = Convert.ToInt32(line[1]);
                    nap = Convert.ToInt32(line[2]);
                }
                else
                {
                    uj.Honap = honap;
                    uj.Nap = nap;
                    uj.Nev = $"{line[0]} {line[1]}";
                    uj.Hianyzasok = line[2];

                    hianyzas.Add(uj);
                }
            }

            sr.Close();
        }
        private static void Feladat2()
        {
            Console.WriteLine("2. feladat");
            Console.WriteLine("A naplóban {0} bejegyzés van.", hianyzas.Count);
        }
        private static void Feladat3()
        {
            Console.WriteLine("3. feladat");

            int igazolt = 0, igazolatlan = 0;

            foreach (var item in hianyzas)
            {
                char[] line = item.Hianyzasok.ToCharArray();

                foreach (var x in line)
                {
                    if (x.Equals('X')) igazolt++;
                    if (x.Equals('I')) igazolatlan++;
                }
            }

            Console.WriteLine($"Az igazolt hiányzások száma {igazolt}, az igazolatlanoké {igazolatlan} óra.");
        }
        private static string hetnapja(int honap, int nap)
        {
            string[] napnev = { "vasarnap", "hetfo", "kedd", "szerda", "csutortok", "pentek", "szombat" };
            int[] napszam = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 335 };
            int napsorszam = (napszam[honap - 1] + nap) % 7;

            return napnev[napsorszam];
        }
        private static void Feladat5()
        {
            Console.WriteLine("5. feladat");

            Console.Write("A hónap sorszáma = ");
            int honap = Convert.ToInt32(Console.ReadLine());

            Console.Write("A nap sorszáma = ");
            int nap = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Azon a napon {0} volt.", hetnapja(honap, nap));
        }
        private static void Feladat6()
        {
            Console.WriteLine("6. feladat");

            Console.Write("A nap neve = ");
            string nap = Console.ReadLine();

            Console.Write("Az óra sorszáma = ");
            int ora = Convert.ToInt32(Console.ReadLine()) - 1;

            int osszHianyzas = 0;

            foreach (var item in hianyzas)
            {
                if (hetnapja(item.Honap, item.Nap).Equals(nap))
                {
                    char[] orak = item.Hianyzasok.ToCharArray();
                    if (!orak.ElementAt(ora).Equals('O') && !orak.ElementAt(ora).Equals('N')) osszHianyzas++;
                }
            }

            Console.WriteLine($"Ekkor összesen {osszHianyzas} óra hiányzás történt.");
        }
        private static void Feladat7()
        {
            Console.WriteLine("7. feladat");
            Console.Write("A legtöbbet hiányzó tanulók: ");

            Dictionary<string, int> hianyzasOrakba = new Dictionary<string, int>();
            var csoport = hianyzas.GroupBy(x => x.Nev).ToList();

            foreach (var group in csoport)
            {
                int ora = 0;

                foreach (var item in group)
                {
                    char[] line = item.Hianyzasok.ToCharArray();

                    foreach (char x in line)
                    {
                        if (x.Equals('X') || x.Equals('I')) ora++;
                    }
                }

                hianyzasOrakba.Add(group.Key, ora);
            }

            var legtobb = hianyzasOrakba.OrderByDescending(x => x.Value).ToList();
            int max = 0;

            for (int i = 0; i < legtobb.Count; i++)
            {
                if (legtobb[i].Value >= max)
                {
                    max = legtobb[0].Value;
                    Console.Write($"{legtobb[i].Key} ");
                }
            }
        }
    }
}