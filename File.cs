using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnixFileSystem
{
    public class File
    {

        public string Nom { get; set; }
        public Directory Parent = null;
        public int Permissions = 4 ;
        public bool isSlash = false;
        public string ParentPath = "";
       
        // STARTING

        public File(string nom, Directory parent){

            this.Nom = nom;
            this.Parent = parent;

            
        }

        public File(string nom, bool isSlash)
        {

            this.Nom = nom;
            this.isSlash = isSlash;
            this.Permissions = 7;

            Console.WriteLine("##############################");
            Console.WriteLine("   File System Initialized");
            Console.WriteLine("##############################");
            Console.WriteLine("");
        }

        public void chmod(int permission) {
            
            if(permission >= 0 && permission < 8 && !isSlash)
                this.Permissions = permission;
        }

        public string getPath()
        {
            if (canRead())
            {
                File inturn = this;
                string path = "";

                while (inturn.Nom != "/")
                {

                    path = inturn.Nom + "/" + path;
                    inturn = inturn.Parent;

                }

                return path;
            }
            else
            {
                return null;
            }
        }

        public File getRoot()
        {
            if (this.canRead()) { 
            File inturn = this;

            if (this.isSlash) { } else { 
            while (inturn.Parent.Nom != "/")
            {
                inturn = inturn.Parent;

            }
            }
            return inturn;
            }
            else
                return null;
        }


        
        public File getParent() {

            if (!this.isSlash) 
                return this.Parent;
            else{
                Console.WriteLine("Vous êtes déja au bout du monde");
                return this;
            }
        }

   


        public bool isFile() { 
            
            if(typeof(Directory) == this.GetType()){
                return false;
            }
            else
                return true;

        }


        public bool isDirectory() {

            if (typeof(File) == this.GetType())
            {
                return false;
            }
            else
                return true;
        }
    
        
        public bool canWrite() { return (this.Permissions & 2) > 0; }
        public bool canExecute() { return (this.Permissions & 1) > 0; }
        public bool canRead() { return (this.Permissions & 4) > 0; }
        public string GetPermission() {

            string perm;

            if (this.canRead())
            {
                perm = "r";
            }
            else {
                perm = "-";
            }

            if (this.canWrite())
            {
                perm = perm + "w";
            }
            else
            {
                perm = perm + "-";
            }
            if (this.canExecute())
            {
                perm = perm + "x";
            }
            else
            {
                perm = perm + "-";
            }

            return perm;
        
        }

    }
}
