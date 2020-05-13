using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
   interface IPencil
   {
        //string Color { get; set; }
        void Write(string text);
   }
   //class RedPencil : IPencil
   //{
   //    public void Write(string text)
   //    {
   //        Console.WriteLine("Red Pencil : " + text);
   //    }
   //}
   //
   //class WhitePencil
   //{
   //    public void Write(string text)
   //    {
   //        Console.WriteLine("White Pencil : " + text);
   //    }
   //}
   class Pencil : IPencil
    {
        private string Color { get; set; }
        public Pencil(string color)
        {
            Color = color;
        }
        public void Write(string text)
        {
            Console.WriteLine(Color+"Pencil"+text);
        }
    }


    class Note
    {
        //private RedPencil redPencil==강결합
        private Pencil redPencil;//==약결합
        private Pencil whitePencil;

        public Note(Pencil red,Pencil white)
        {
            this.redPencil = red;
            this.whitePencil = white;
        }
        public void Write(string text)
        {
            redPencil.Write(text);
            whitePencil.Write(text);
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            var redPencil = new Pencil("Red");
            var whitePencil = new Pencil("White");
            var note = new Note(redPencil,whitePencil);
            note.Write("Writing");
        }
    }
}
