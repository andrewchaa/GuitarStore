using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.GuitarStore.Common;
using NHibernate.GuitarStore.DataAccess;
using NHibernate.Linq;

namespace NHibernate.GuitarStore.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var nHibernateBase = new NHibernateBase();
                nHibernateBase.Initialize("NHibernate.GuitarStore");
                
                System.Console.WriteLine("NHibernte.GuitarStore assembly initialised.");

                var list1 = NHibernateBase.StatelessSession.CreateCriteria("Inventory").List<Inventory>();
                var list2 = NHibernateBase.Session.CreateCriteria(typeof (Inventory)).List<Inventory>();
                IQueryable<Inventory> linq = NHibernateBase.Session.Query<Inventory>().Select(i => i);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += " - InnerException: " + ex.InnerException.Message;
                }

                System.Console.WriteLine();
                System.Console.WriteLine("***** ERROR *****");
                System.Console.WriteLine(message);
                System.Console.WriteLine();
                
            }
        }
    }
}
