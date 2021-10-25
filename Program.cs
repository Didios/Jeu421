using System;
using System.Collections;
using System.Collections.Generic;

namespace DE_YAM
{
    public class De
    {
        public int NbFaces = 6;
        protected Random random = new Random();
        private int face;

        public int Face
        {
            get { return face; }
            set { face = value; }
        }

        public De()
        {
            Face = Lancer();
        }
        public De(int nbrFace)
        {
            NbFaces = nbrFace;
            Face = Lancer();
        }

        public override string ToString()
        {
            return Face.ToString();
        }

        public virtual int Lancer()
        {
            Face = random.Next(1, NbFaces +1);
            return Face;
        }
    }

    public class DeTruque : De
    {
        public DeTruque() : base()
        {
        }

        public override int Lancer()
        {
            int proba = this.random.Next(1, 13);

            if (proba < 7)
                this.Face = 6;
            else if (proba < 9)
                this.Face = 5;
            else
                this.Face = proba - 8;

            return new int();
        }
    }


    public class Jeu
    {
        public readonly int NbManches;
        public readonly int NbDes;
        private List<De> Des = new List<De>();

        public Jeu()
        {
            NbManches = 5;
            NbDes = 5;

            for (int i = 0; i < NbDes; ++i)
            {
                Des.Add(new De());
            }
        }
        public Jeu(int nbrM, int nbrD)
        {
            NbManches = nbrM;
            NbDes = nbrD;

            for(int i = 0; i < NbDes; ++i)
            {
                Des.Add(new De());
            }
        }
        public Jeu(int nbrM, int nbrD, int nbrDT)
        {
            NbManches = nbrM;
            NbDes = nbrD + nbrDT;

            for (int i = 0; i < nbrD; ++i)
            {
                Des.Add(new De());
            }
            for (int i = 0; i < nbrDT; ++i)
            {
                Des.Add(new DeTruque());
            }
        }

        public int Relancer(int i)
        {
            if (i >= Des.Count)
                i = Des.Count - 1;

            return Des[i].Lancer();
        }

        public int Score()
        {
            int score = 0;

            foreach (De d in Des)
                score += d.Face;

            return score;
        }

        public int Run()
        {
            Console.WriteLine("Jeu du 421");

            for(int i = 0; i < NbManches; ++i) // on répète autant de fois que de manches
            {
                // on affiche les dés
                Console.WriteLine(ToString());

                // on demande qu'elles dés relancés
                Console.Write("Quels sont les dés que vous souhaitez relancer ? : ");
                string choice = Console.ReadLine();
                string[] DeARelance = choice.Split(','); // on sépare les numeros

                // on relance les dés choisis
                foreach (string j in DeARelance)
                    Des[int.Parse(j) -1].Lancer();
            }
            // on affiche les dés
            Console.WriteLine(ToString());

            return Score();
        }

        public override string ToString()
        {
            string trait_ = "+---+ ";
            string trait = "";
            for (int i = 0; i < NbDes; ++i)
                trait += trait_;

            string tostring = trait + "\n";
            for (int i = 0; i < NbDes; ++i)
                tostring += $"| {Des[i].ToString()} | ";
            tostring += "\n" + trait;

            return tostring;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Jeu j = new Jeu();
            int score = j.Run();

            Console.WriteLine($"Score Finale : {score}");
        }
    }
}
