using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _2_feladat_szerver
{
    class Protocol
    {
        public static List<Targy> Targyak = new List<Targy>();
        public static List<User> Users = new List<User>() { new User("proba1", "jelszo"), new User("proba2", "jelszo"), new User("proba3", "jelszo") };
        public static List<User> ActiveUsers = new List<User>();
        public StreamReader reader;
        public StreamWriter writer;
        public string user = null;
        public Protocol(TcpClient c)
        {
            this.reader = new StreamReader(c.GetStream(), Encoding.UTF8);
            this.writer = new StreamWriter(c.GetStream(), Encoding.UTF8);
        }
        public void StartKomm()
        {
            writer.WriteLine("Sikeres csatlakozás a szerverre. Üdvözli önt a Vatera 2.0 alkalmazás!<" +
                             "  /----------------------------------------------------------------------<" +
                             "  | Info:<" +
                             "  |\t A parancsokat és a paramétereket '|' jellel kell elválasztanod<" +
                             "  |\t Pl.: PARANCS|param1|param2|...<" +
                             "  |\t A ()-ek közötti szöveg megjegyzés, magyarázat a parancshoz<" +
                             "  /----------------------------------------------------------------------<" +
                             "  | Parancsok:<" +
                             "  |<" +
                             "  |\t LOGIN|felhasználónév|jelszó (bejelentkezés).<" +
                             "  |<" +
                             "  |\t FELRAK|megnevezés|összeg|(Tárgy felrakása a Vatera 2.0-ba.).<" +
                             "  |<" +
                             "  |\t MEGVESZ|azonosító (Az adott azonosítóval rendelkező termék vásárlása)<" +
                             "  |<" +
                             "  |\t TÖRÖL|azonosító (Az adott azonosítóval rendelkező termék törlése) .<" +            
                             "  |<" +
                             "  |\t ELADASOK (Kilistázza az eladaott termékeket.) .<" +
                             "  |<" +
                             "  |\t BYE (kapcsolat bontása és kliens bezárása).<" +
                             "  |<" +
                             "  \\----------------------------------------------------------------------<");
            writer.Flush();
            Console.WriteLine(reader.ReadLine());
            bool ok = true;
            while (ok)
            {
                string command = null;
                try
                {
                    string message = reader.ReadLine();// add|1212|1212|1212
                    string[] param = message.Split('|');
                    command = param[0].ToUpper();
                    switch (command)
                    {
                        case "LOGIN": Login(param[1], param[2]); break;
                        case "FELRAK": Felrak(param[1], param[2]); break;
                        case "MEGVESZ": Megvesz(param[1]); break;
                        case "TÖRÖL": Torol(param[1]); break;
                        case "ELADASOK": EladasokKilistaz(); break;
                        case "KILEP": writer.WriteLine("Köszönjük, hogy a Vatera 2.0-t használta. További szép napot kívánok önnek!"); ok = false; break;
                        default: writer.WriteLine("ERR|Ismeretlen parancs."); break;
                    }
                }
                catch (Exception e)
                {
                    writer.WriteLine("ERR|{0}", e.Message);
                }
                writer.Flush();
            }
            Console.WriteLine("A kliens lecsatlakozott");
        }

        private void Login(string name, string password)
        {
            if (user != null) { writer.WriteLine("ERR|Már be vagy jelentkezve!"); }
            else
            {
                bool userAlreadyLoggedIn = false;
                for (int i = 0; i < ActiveUsers.Count; i++)
                {
                    if (ActiveUsers[i].Username == name)
                    {
                        userAlreadyLoggedIn = true;
                        writer.WriteLine("ERR|Ez a felhasználó már bent van!");
                        break;
                    }
                }
                bool notExists = false;
                if (!userAlreadyLoggedIn)
                {
                    lock (Users)
                    {
                        for (int i = 0; i < Users.Count; i++)
                        {
                            if (Users[i].Username == name && Users[i].Password == password)
                            {
                                user = Users[i].Username;
                                lock (ActiveUsers)
                                {
                                    ActiveUsers.Add(Users[i]);
                                }
                                writer.WriteLine("OK");
                            }
                            else if (!Users.Any(f => f.Username == name))
                            {
                                notExists = true;
                            }
                        }
                    }
                    if (notExists) writer.WriteLine("ERR|Hibás felhasználónév vagy jelszó!");
                }
            }
        }
        private bool Bejelentkezve() 
        {
            if (user == null)
            {
                writer.WriteLine("ERR|Jelentkezzen be előbb a funkció használatához!");
                return false;
            }
            return true;
        }
        private void Felrak(string megnevezes, string ar)
        {
            if (Bejelentkezve())
            {
                lock (Targyak)
                {
                    Targyak.Add(new Targy(megnevezes,int.Parse(ar),user,null));
                    writer.WriteLine("OK");
                }
            }
        }

        private void Megvesz(string id)
        {
            if (Bejelentkezve())
            {
                lock (Targyak)
                {
                    Targy targy = Targyak.Where(t1 => t1.Id == int.Parse(id)).First();
                    if (!targy.Equals(null))
                    {
                        if (!targy.Vasarlo.Equals(null))
                        {
                            writer.WriteLine("ERR|Ezt a tárgyat már megvásárolták. Kérem válasszon másikat.");
                        }
                        else
                        {
                            targy.Vasarlo = user;
                            writer.WriteLine("OK");
                        }
                    }
                }
            }
            else 
            {
                writer.WriteLine("ERR|Ez a tárgy azonosító nem szerepel a Vater 2.0 adatbázisában!");
            }
        }

        private void Torol(string id)
        {
            if (Bejelentkezve())
            {
                Targy targy = Targyak.Where(t1 => t1.Id == int.Parse(id)).First();
                if (!targy.Equals(null))
                {
                    if (!targy.Vasarlo.Equals(null))
                    {
                        Targyak.Remove(targy);
                        writer.WriteLine("OK");
                    }
                    else 
                    {
                        writer.WriteLine("ERR|Ezt a tárgyat már megvásárolták, ezért ez már törölni nem tudja. Kérem válasszon másikat.");
                    }
                }
                else 
                {
                    writer.WriteLine("ERR|Ez a tárgy azonosító nem szerepel a Vater 2.0 adatbázisában!");
                }
            }
        }

        private void EladasokKilistaz()
        {
                writer.WriteLine("Eladott tárgyak listázása");
                lock (Targyak)
                {
                    foreach (Targy targy in Targyak)
                        writer.WriteLine("ID: " + targy.Id + ", " + "Létrehozó: " + targy.Felrako + ", " + "Megnevezés: " + targy.Nev + ", " + "Összeg:" + targy.Ar.ToString() + ", " + "Vásárló:" + targy.Vasarlo == null? "Nincs meghatározva" : targy.Vasarlo);
                }
                writer.WriteLine("Listázás vége");
        }
    }

}
