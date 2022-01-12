using System;
using System.Collections.Generic;
using System.IO;
using AsciiArt;

namespace Jeu_du_pendu
{
    class Program
    {

        static void AfficherMot(string mot, List<char> lettres)
        {
            for(int i = 0; i < mot.Length; i++)
            {
                char lettre = mot[i];
                if (lettres.Contains(lettre))
                {
                    Console.Write(lettre + " ");
                }
                else
                {
                    Console.Write("_ ");
                }
            }
        }

        static bool ToutesLettresDevinees(string mot, List<char> lettres)
        {
            foreach(char lettre in lettres)
            {
                mot = mot.Replace(lettre.ToString(), ""); 
            }

            if (mot.Length == 0) { return true; }

            return false;
        }

        static char DemanderLettre()
        {
            while (true)
            {
                Console.WriteLine("");
                Console.Write("Rentrez une lettre : ");
                string input = Console.ReadLine();
                if (input.Length == 1)
                {
                    input = input.ToUpper();
                    return input[0];
                }
                else
                {
                    Console.WriteLine("Erreur merci de rentrer une lettre");
                }
            }
        }

        static void DevinerMot(string mot)
        {
            List<char> lettres = new List<char>();
            List<char> notInWord = new List<char>();
            const int NB_VIES = 6;
            int viesRestantes = NB_VIES;

            while (viesRestantes > 0)
            {
                // ici, on importe depuis un autre fichier un tableau de string, qui vont afficher un pendu diffrent à chaque vie perdu
                Console.WriteLine(Ascii.PENDU[NB_VIES - viesRestantes]);
                Console.WriteLine("");

                AfficherMot(mot, lettres);
                Console.WriteLine("");
                char lettre = DemanderLettre();
                Console.Clear();

                if(mot.Contains(lettre))
                {
                    Console.WriteLine("La lettre est dans le mot");
                    Console.WriteLine("");
                    lettres.Add(lettre);
                    if(ToutesLettresDevinees(mot, lettres))
                    {
                        break;
                    }
                }

                else if (notInWord.Contains(lettre))
                {
                    Console.WriteLine("Vous avez déjà entré la lettre : " + lettre + " entrez une autre !");
                }

                else
                {
                    notInWord.Add(lettre);
                    viesRestantes--;
                    Console.WriteLine("Vies restantes : " + viesRestantes);
                    Console.WriteLine("");
                }
                if(notInWord.Count > 0)
                {
                    Console.WriteLine("Le mot ne contient pas les lettres : " + String.Join(", ", notInWord));
                    Console.WriteLine("");
                }

            }
            if (viesRestantes == 0)
            {
                Console.WriteLine("PERDU ! Le mot était : " + mot);
            }
            else
            {
                AfficherMot(mot, lettres);
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("GAGNÉ !"); 
            }
        }

        static string[] ChargerListMot(string nomFichier)
        {
            // cette fonction va appeler un fichier et retourner une collection qui prendra par argument chaque ligne de notre fichier
            try
            {
                return File.ReadAllLines(nomFichier);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erreur : Erreur de lecture du fichier" + nomFichier + "( " + ex + " )");
            }

            return null;
        }

        static void Main(string[] args)
        {
            char replay = 'O';
            while (replay == 'O')
            {
                var mots = ChargerListMot("mots.txt");

            if(mots == null || mots.Length == 0)
            {
                Console.WriteLine("La liste des mots est vide");
            }
            else{
                Random rand = new Random();
                string mot = mots[rand.Next(0, mots.Length)].Trim().ToUpper();
                DevinerMot(mot);
                    while (true)
                    {
                        Console.WriteLine("Voulez-vous rejouer ? o/n");
                        replay = DemanderLettre();
                        if(replay == 'O' || replay == 'N')
                        {
                            Console.Clear();
                            break;
                        }
                        Console.WriteLine("Je n'ai pas compris");
                    }
                }  
            }
        }
    }
}
