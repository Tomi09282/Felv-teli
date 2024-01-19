using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace felveteli
{
    public class Diak : IFelvetelizo
    {

        string om_azonosito;
        string nev;
        string email;
        DateTime szuletesi_datum;
        string lakcim;
        int matek, magyar;

        public Diak(string om_azonosito, string nev, string email, DateTime szuletesi_datum, string lakcim, int matek, int magyar)
        {
            this.om_azonosito = om_azonosito;
            this.nev = nev;
            this.email = email;
            this.szuletesi_datum = szuletesi_datum;
            this.lakcim = lakcim;
            this.matek = matek;
            this.magyar = magyar;
        }

        public string OM_Azonosito { get => om_azonosito; set => om_azonosito = value; }
        public string Neve { get => nev; set => nev = value; }
        public string Email { get => email; set => email = value; }
        public DateTime SzuletesiDatum { get => szuletesi_datum; set => szuletesi_datum = value; }
        public string ErtesitesiCime { get => lakcim; set => lakcim = value; }
        public int Matematika { get => matek; set => matek = value; }
        public int Magyar { get => magyar; set => magyar = value; }

        public string CSVSortAdVissza()
        {
            throw new NotImplementedException();
        }

        public void ModositCSVSorral(string csvString)
        {
            throw new NotImplementedException();
        }
    }
}
