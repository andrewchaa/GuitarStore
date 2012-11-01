using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;

namespace NHibernate.GuitarStore.DataAccess
{
    public class NHibernateBase
    {
        protected static ISessionFactory SessionFactory { get; set; }

        private static Configuration Configuration { get; set; }
        private static ISession _session = null;
        private static IStatelessSession _statelessSession = null;

        public static Configuration ConfigureNHibernate(string assembly)
        {
            Configuration = new Configuration();
            Configuration.AddAssembly(assembly);

            return Configuration;
        }

        public void Initialize(string assembly)
        {
            Configuration = ConfigureNHibernate(assembly);
            SessionFactory = Configuration.BuildSessionFactory();
        }

        public static ISession Session
        {
            get
            {
                if (_session == null)
                {
                    _session = SessionFactory.OpenSession();
                }
                
                return _session;
            }
        }

        public static IStatelessSession StatelessSession
        {
            get
            {
                if (_statelessSession == null)
                {
                    _statelessSession = SessionFactory.OpenStatelessSession();
                }

                return _statelessSession;
            }
        }

        public IList<T>  ExecuteCriteria<T>()
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                try
                {
                    IList<T> result = Session.CreateCriteria(typeof(T)).List<T>();
                    transaction.Commit();

                    return result;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
