﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnixFileSystem
{
class Program
{
        
// J'ai pris le parti de mettre tout l'affichage dans mon programme principale
// Je m'explique : J'ai souhaiter séparer algo pur et affichage graphique, pour les raisons évoqué dans ton cours (tests unitaires, interface ...) 
// Je ne sais pas si cela est une bonne pratique ... Je t'avais d'ailleurs fait la remarque par mail (Ajouter une classe pour l'affichage), mais tu m'avais dit que ce n'était pas la peine. 
// De plus, c'est ce que j'ai compris dans ta démarche de nous imposé les signatures de méthodes. 
// Bonne correction ! 
    
static void Main(string[] args)
{
    string command;
     File courants = new Directory("/", true);

    do
    {
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.Write("["+ courants.Nom +"] # ");
    Console.ResetColor();
    string saisie = Console.ReadLine();
    command = GetCommand(saisie);
    string arg = GetArg(saisie);
    string secondarg = GetSecondArg(saisie);

    if (courants.isDirectory())  
    {
        Directory courant = (Directory)courants;


        if (command.Equals("create") && arg != null)
        {
            bool ok = courant.createNewFile(arg);

            if (ok)
            {
                Console.WriteLine("Fichier correctement créer");
            }
            else
                Console.WriteLine("Echec dans la création du fichier");
             
        }

        else if (command.Equals("mkdir") && arg != null)
        {
            bool ok = courant.mkdir(arg);
            if (ok)
                Console.WriteLine("Dossier " + arg + " créer");
            else
                Console.WriteLine("Echec dans la création du dossier");
        }
        if (command.Equals("ls"))
        {
          
                List<File> list = courant.ls();

                if (list == null) {

                    Console.WriteLine("Vous n'avez pas le droit de faire cette action");
                
                }
                else if (list.Count == 0)
                {
                    Console.WriteLine("Le dossier spécifié est vide");
                }
                else
                {
                    foreach (File encours in list)
                    {
                        string type = "";

                        if (encours.isDirectory()) { type = "Directory"; }
                        else if (encours.isFile()) { type = "File"; }

                        Console.WriteLine(encours.GetPermission() + " " + encours.Nom + " " + type);

                    }
                }
          
                
        }
        else if (command.Equals("cd") && arg != null)
        {
            
             File next = courant.cd(arg);

            if (next == null)
             {
                 Console.WriteLine("Déplacement invalide");
             }
             else {
                 courants = next;
             }
        }

        else if (command.Equals("search") && arg != null)
        {

            List<File> resultat = courant.search(arg);
            if (resultat != null)
            {
                foreach (File encours in resultat)
                    Console.WriteLine(encours.getPath());
            }
            else {
                Console.WriteLine("Vous n'avez pas les droits de lire dans ce dossier");
            }
        }

        else if (command.Equals("delete") && arg != null)
        {
            
            bool ok = courant.delete(arg);

            if (ok)
            {
                Console.WriteLine("Fichier correctement supprimée");
            }
            else
                Console.WriteLine("Echec dans la suppression");

        }
        else if (command.Equals("rename") && arg != null && secondarg != null)
        {
            bool ok = courant.rename(arg, secondarg);

            if (ok)
            {
                Console.WriteLine("Le fichier/dossier à correctement été renommé");
            }
            else
            {
                Console.WriteLine("Vous ne pouvez pas renommé ce fichier/dossier");
            }
        }


        }
    else if(courants.isFile()) { // COMMANDE SI C'EST UNE FILE 

        if (command == "ls")
        {

                Console.WriteLine("Vous êtes dans une file");
        }    

    }

    // COMMANDE VALABLE POUR LES 2 TYPES
    if (command.Equals("")) { }

    if (command.Equals("parent")) {

        courants = courants.getParent();
    }

    if (command.Equals("path"))
    {
        string path  = courants.getPath();

        if (path != null)
        {
            Console.WriteLine(path);
        }
        else
            Console.WriteLine("Vous n'avez pas le droit de faire cette action");
    }

    if (command.Equals("file"))
    {
        if (courants.isFile())
        {
            Console.WriteLine("Cette chose est un fichier");
        }
        else
        {
            Console.WriteLine("Cette chose n'est pas un fichier");
        }
    }

    if (command.Equals("directory"))
    {

        if (courants.isDirectory())
        {
            Console.WriteLine("Cette chose est un dossier");
        }
        else
        {
            Console.WriteLine("Cette chose n'est pas un dossier");
        }
    }

   

    if (command.Equals("root"))
    {

        File root = courants.getRoot();

        if (root != null)
        {
        Console.WriteLine(root.Nom);
          }
        else
            Console.WriteLine("Vous n'avez pas le droit de faire cette action");
    }



    if (command.Equals("chmod") && saisie != null && arg != null)
        {
            try
            {
                int perm = int.Parse(arg);
                if(perm >= 0 && perm < 8 && !courants.isSlash)
                    courants.chmod(perm);
                else if (perm <= 0 || perm > 8)
                   Console.WriteLine("Le CHMOD est compris entre 0 et 7 !");
                else if (courants.isSlash)
                    Console.WriteLine("Le CHMOD ne peut être effectué sur / ");
            }
            catch {
                Console.WriteLine("CHMOD incorrect");
            }
        }

    if (command.Equals("exit") && saisie != null)
        {
            Console.WriteLine("System halted");
        }
              

    } while(!command.Equals("exit"));

    Console.ReadLine();


}

    static string GetCommand ( string tapuser){

        string[] mots = tapuser.Split(' ');

        if (mots.Length > 0)
            return mots[0];

        else
        {
            return null;
        }
    }

    static string GetArg(string tapuser)
    {
        string[] mots = tapuser.Split(' ');

        if (mots.Length > 1)
            return mots[1];

        else
        {
            return null;
        }
            
    }

    static string GetSecondArg(string tapuser)
    {
        string[] mots = tapuser.Split(' ');

        if (mots.Length > 2)
            return mots[2];

        else
        {
            return null;
        }

    }

    
}
}
