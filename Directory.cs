using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnixFileSystem
{

    public class Directory : File
    {
        public List<File> contenu = new List<File>();
        

        public Directory(string nom, bool isSlash) : base(nom, isSlash) { }
        public Directory(string nom, Directory parent) : base(nom, parent) {

        }


        

        public bool mkdir(string nom) {

           
            if (base.canWrite() && verifyname(nom))
            {
                Directory newdirectory = new Directory(nom, this);
                contenu.Add(newdirectory);

                return true;
            }
            else
                return false;

            
        }

         public List<File> search(string name) 
        {
            List<File>  resultat = null;
            if (this.canRead())
            {
               resultat = new List<File>();

                foreach (File encours in contenu)
                {

                    if (encours.Nom == name)
                        resultat.Add(encours);

                    if (encours.isDirectory() && encours.canRead())
                    {
                        List<File> listofotherdirect = new List<File>();
                        Directory otherdirect = (Directory)encours;
                        listofotherdirect = otherdirect.search(name);
                        foreach (File courants in listofotherdirect)
                        {
                            resultat.Add(courants);
                        }
                    }

                }
            }

            return resultat;
        }

         public bool delete(string name) {

             if (this.canWrite() && !verifyname(name))
             {
                 foreach (File encours in contenu)
                 {

                     if (encours.Nom == name)
                     {
                         contenu.Remove(encours);
                         return true;
                     }
                     
                 }
                 return false;
             }
             else
                 return false;

         }


        public File cd (string name) {

            if (this.canRead())
            {
                foreach (File encours in contenu)
                {
                    if (encours.Nom == name && encours.canRead())
                        return encours;

                }

                return null;

            }
            else {
                return null;
            }
        }

        public bool rename(string nomfichier, string newname)
        {
            if (canWrite() && this.verifyname(newname))
            {
                foreach (File encours in contenu) {

                    if (encours.Nom == nomfichier && encours.canWrite()) { 
                        encours.Nom = newname;
                        return true;
                    }                    
                }
                return false;
            }
            else
            {
                return false;
            }


        }

        public bool createNewFile(string name) {

            if (this.canWrite() && verifyname(name)) {

                contenu.Add(new File(name, this));

                return true;
            }
            else
            return false;
        }


        public List<File> ls() {

            if (this.canRead())
            {
                return this.contenu;
            }
            else
                return null;
        }


        public bool verifyname(string name)
        {
            bool ok = true;

            int i = contenu.Count;

            if (name == "/") { ok = false; }

            while(0 < i  && ok){
                if (contenu[i - 1].Nom == name )
                { 
                    ok = false;
                }
                
                i--;
           }

            return ok;


        }

       
    }
}
