using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    class LPoint//Двунаправленный список 
    { 
        public int data;//Информация в текущем элементе
        public LPoint next, pred;//Ссылки на следующий и предыдущий элементы
        public LPoint(int d = -1)
        {
            data = d;
            next = null;
            pred = null;
        }
        public override string ToString()
        {
            string t="";
            var beg = this;
            t += beg.data+" ";
            while (beg.next!=null)
            {
                beg = beg.next;
                t += beg.data + " ";
            }
            return t;
        }
        public int Search(int d)//Поиск в двунаправленном списке по значению    
        {
            int i = 0;
            LPoint beg = this;
            //Если список пуст, то элемента нет
            if (beg == null) return -1;
            //Если следующего элемента нет, то если текущий элемент имеет нужное значение возвращается его индекс (0)
            //Иначе элемента с заданным значением нет
            if (beg.next == null)
                if (beg.data == d) return 0;
                else return -1;
            else
            {
                //Иначе поиск по двунаправленному списку либо до того как кончатся элементы в списке,
                // либо до того как найдется нужный элемент
                while (beg != null)
                {
                    if (beg.data == d) return i;
                    i++;
                    beg = beg.next;
                }
                return -1;

            }
        }
    }
    class Program
    {
        static LPoint DeleteByPosition(LPoint beg,int num)
            //Удаление из списка по индексу (num)
        {
            LPoint r = beg;
            //Если индекс не неотрицательное число, возвращается изначальный список
            if (num < 0)
                return beg;
            else
            {
                //Удаление первого элемента
                if (num==0)
                {
                    /*beg = r.next;
                    beg.pred = null;*/
                    return null;
                }
                else
                {
                    //Перенос указателя на элемент с заданной позицией
                    //Или если элемента с такой позицией нет, возвращается изначальный список
                    for (int i = 0; i < num&&r.next!=null; i++)
                    {
                        r = r.next;
                    }
                    if (r == null) return beg;
                    else
                    {
                        //Удаление элемента с индексом num
                        LPoint q = r.pred;
                        LPoint e = r.next;
                        q.next = e;
                        if (e != null)
                            e.pred = q;
                        return beg;
                    }
                }
            }
        }
        static LPoint DeleteByValue(LPoint beg,int d)
            //Удаление по значению (d)
        {
            //Поиск позиции данного элемента в списке
            int pos = beg.Search(d);
            //Если элемент есть в списке, то удаление его
            if (pos!=-1)
                beg=DeleteByPosition(beg, pos);
            else
            {
                Console.WriteLine("Элемента с таким значением не существует в списке");
            }
            return beg;
        }
        //static int i = 1;
        static LPoint MakePoint(int d)
        {
            LPoint p = new LPoint(d);
            return p;
        }
        static LPoint MakeList(int size)//Создание списка заданной длины
        {
            //Создание хвоста со значением "1"
            LPoint beg = MakePoint(1);
            LPoint r = beg;
            for (int j = 2; j <= size; j++)
            {
                //Добавление следующих элементов слева от текущего, так что элемент со значением size будет первым,
                //а элемент со значением "1" последним
                LPoint p = MakePoint(j);
                r.pred = p;
                p.next = r;
                r = p;
            }
            return r;
        }
        static void Main(string[] args)
        {
            int K;
            bool OK=false;
            Console.WriteLine("Введите количество элементов в двунаправленном списке: ");
            do
            {
                OK = Int32.TryParse(Console.ReadLine(), out K) && K > 0;
                if (!OK) Console.WriteLine("Ошибка ввода");
            } while (!OK);
            //Создание списка
            LPoint beg = MakeList(K);
            Console.WriteLine("Ваш список: "+beg.ToString());
            int info;
            Console.WriteLine("Введите значение элемента, которого вы хотите удалить: ");
            do
            {
                OK = Int32.TryParse(Console.ReadLine(), out info);
                if (!OK) Console.WriteLine("Ошибка ввода");
            } while (!OK);
            beg = DeleteByValue(beg, info);
            if (beg != null)
                Console.WriteLine("Ваш новый список: " + beg.ToString());
            else
                Console.WriteLine("Ваш новый список пуст!");
            Console.Read();
        }
    }
}
