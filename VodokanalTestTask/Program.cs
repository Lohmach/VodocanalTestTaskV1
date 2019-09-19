using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodokanalTestTask
{
    class Clodes
    {
        public int Id;
    }

    class Outerwear : Clodes
    {
        public string name;
        public int size;
        public int height;
    }

    class Footwear : Clodes
    {
        public string name;
        public int size;
    }


    class Program
    {

        static string[] type_outerwear = { "куртка утеплённая", "куртка для руководителя", "жилет защитный" };
        static string[] type_footwear = { "ботинки кирзовые", "ботинки мужские утеплённые" };

        static int id_counter = 0;


        static void Main(string[] args)
        {
            List<object> BD = new List<object>();
            Random rnd = new Random();           
            for (int i = 0; i < 10; i++)         
            {                                    
                                                 
                BD.Add(init(rnd));               
            }
            Console.WriteLine("Вывод всех элементов");
            for (int i = 0; i < BD.Count; i++)
            {
                object a = BD[i];
                writer(a);
            }
            Console.WriteLine("___________________________");

            Console.WriteLine("Вывод элемента с id = 3");
            writer(found_for_id(3,BD)[0]);
            

            Console.WriteLine("Поиск по размеру > 40, сортировка по возрастанию размера:");
            List<object> sortSise = found_for_size(40, BD);
            foreach (var a in sortSise) writer(a);
            Console.WriteLine("___________________________________________");

            Console.WriteLine("Вывод элементов по имени \"жилет защитный\", сортировка по убыванию размера:");
            List<object> sort_name1 = found_for_name("жилет защитный", BD);
            for (int i = 0; i < sort_name1.Count; i++) { writer(sort_name1[i]); }
            Console.WriteLine("___________________________________________");

            Console.WriteLine("Вывод элементов по имени \"ботинки кирзовые\", сортировка по убыванию размера:");
            List<object> sort_name2 = found_for_name("ботинки кирзовые",BD);
            for (int i = 0; i < sort_name2.Count; i++) { writer(sort_name2[i]); }
            Console.WriteLine("___________________________________________");

            Console.WriteLine("Вывод элементов c height > 170, сортировка по убыванию ID");
            List<object> sortHeight = found_for_height(170, BD);
            for (int i = 0; i < sortHeight.Count; i++) { writer(sortHeight[i]); }
            Console.WriteLine("___________________________________________");
        }

        static object init(Random rnd)
        {

            if (rnd.Next() % 2 == 1)
            {
                Outerwear a = new Outerwear();
                a.Id = id_counter;
                id_counter++;
                a.name = type_outerwear[rnd.Next(0, 3)];
                a.size = rnd.Next(30, 50);
                a.height = rnd.Next(150, 200);
                return a;
            }

            else
            {
                Footwear b = new Footwear();
                b.Id = id_counter;
                id_counter++;
                b.name = type_footwear[rnd.Next(0, 2)];
                b.size = rnd.Next(30, 50);
                return b;
            }
        }
        static void writer(object a)
        {
            if (a is Outerwear)
            {
                Outerwear out_a = (Outerwear)a;
                Console.WriteLine("Id: {0}", out_a.Id);
                Console.WriteLine("Название: {0}", out_a.name);
                Console.WriteLine("Размер: {0}", out_a.size);
                Console.WriteLine("Рост: {0}", out_a.height);
                Console.WriteLine();
            }

            if (a is Footwear)
            {
                Footwear foot_a = (Footwear)a;
                Console.WriteLine("Id: {0}", foot_a.Id);
                Console.WriteLine("Название: {0}", foot_a.name);
                Console.WriteLine("Размер: {0}", foot_a.size);
                Console.WriteLine();
            }
        }

        static List<object> found_for_id (int id, List<Object> BD) //поиск по ID 
        {
            List<object> a = new List<object>();
            List<Clodes> b = new List<Clodes>();

            for (int i = 0; i < BD.Count; i++)
            {
                b.Add((Clodes)BD[i]);
            }

            var clodes = from i in b
                         where (i.Id == id)
                         select i;
            foreach (var k in clodes)  a.Add(k); 
            
            return a;
        }
        static List<object> found_for_size(int size, List<object> BD) //поиск по размеру
        {
            List<object> a = new List<object>();
            List<Outerwear> out1 = new List<Outerwear>();
            List<Footwear> foot1 = new List<Footwear>();

            for (int i = 0; i < BD.Count ; i++)
            {
                if (BD[i] is Outerwear) out1.Add((Outerwear)BD[i]);
                if (BD[i] is Footwear) foot1.Add((Footwear)BD[i]);
            }

            var clodes = from i in out1
                         where (i.size > size) // ставим нужное нам условие
                         orderby i.size //упорядочиваем по возрастанию, если нужно, то можно менять тип сортировки
                         select i;
            var clodes1 = from j in foot1
                          where (j.size > size)
                          orderby j.size
                          select j;
            foreach (var k in clodes) a.Add((object)k);
            foreach (var k in clodes1) a.Add((object)k);
            return a;
        }

        static List<object> found_for_name(string name, List<object> BD) //поиск по названию 
        {
            List<object> a = new List<object>();
            List<Outerwear> out1 = new List<Outerwear>();
            List<Footwear> foot1 = new List<Footwear>();

            for (int i = 0; i < BD.Count; i++)
            {
                if (BD[i] is Outerwear) out1.Add((Outerwear)BD[i]);
                if (BD[i] is Footwear) foot1.Add((Footwear)BD[i]);
            }

            var clodes = from i in out1
                         where i.name == name // ставим нужное нам условие
                         orderby i.size descending //сортировка по убыванию размера
                         select i;

            var clodes1 = from j in foot1
                          where (j.name == name)
                          orderby j.size descending //сортировка по убыванию размера
                          select j;
            foreach (var k in clodes) a.Add((object)k);
            foreach (var k in clodes1) a.Add((object)k);

            return a;
        }

        static List<object> found_for_height(int heigth, List<object> BD) //поиск по росту 
        {
            List<object> a = new List<object>();
            List<Outerwear> out1 = new List<Outerwear>();

            for (int i = 0; i < BD.Count; i++)
            {
                if (BD[i] is Outerwear) out1.Add((Outerwear)BD[i]);
            }

            var clodes = from i in out1
                         where i.height > heigth // ставим нужное нам условие
                         orderby i.Id descending //сортировка по убыванию ID
                         select i;

            foreach (var k in clodes) a.Add((object)k);

            return a;
        }
    }
}