using System;
using System.Collections;
using java.util;
using net.sf.mpxj;
using net.sf.mpxj.mpp;
using net.sf.mpxj.MpxjUtilities;
using net.sf.mpxj.reader;

namespace Mpxj
{
    class Program
    {
        static void Main(string[] args)
        {
            ProjectReader reader = new MPPReader(); 
            var projectObj = reader.read(@"D:\Produquimica\gantt\477.mpp");

            foreach (Task task in ToEnumerable(projectObj.getTasks()))
            {
                var inicio = "";
                try{inicio = task.getStart().ToDateTime().ToString("dd/MM/yyyy");}catch{inicio = "não Programado";}

                var fim = "";
                try{fim = task.getFinish().ToDateTime().ToString("dd/MM/yyyy");}catch{fim = "não Programado";}


                var inicioBase = "";
                try{inicioBase = task.getBaselineStart().ToDateTime().ToString("dd/MM/yyyy");}catch{inicioBase = "não Programado";}

                var fimBase = "";
                try{fimBase = task.getBaselineFinish().ToDateTime().ToString("dd/MM/yyyy");}catch{fimBase = "não Programado";}
                
                Console.WriteLine($"Task: {task.getName()}  ID= {task.getID()}  Unique ID= {task.getUniqueID()} status={task.getPercentageComplete()} Custo=  {task.getCost()} Inicio= {inicio} Fim= {fim} InicioBase= {inicioBase} Fimbase= {fimBase}" );
            }

            Console.ReadKey();
        }

        private static EnumerableCollection ToEnumerable(Collection javaCollection)
        {
            return new EnumerableCollection(javaCollection);
        }

        private class EnumerableCollection
        {
            public EnumerableCollection(Collection collection)
            {
                _mCollection = collection;
            }

            public IEnumerator GetEnumerator()
            {
                return new Enumerator(_mCollection);
            }

            private readonly Collection _mCollection;
        }

        private class Enumerator : IEnumerator
        {
            public Enumerator(Collection collection)
            {
                _mCollection = collection;
                _mIterator = _mCollection.iterator();
            }

            public object Current => _mIterator.next();

            public bool MoveNext()
            {
                return _mIterator.hasNext();
            }

            public void Reset()
            {
                _mIterator = _mCollection.iterator();
            }

            private readonly Collection _mCollection;
            private Iterator _mIterator;
        }
    }
}
