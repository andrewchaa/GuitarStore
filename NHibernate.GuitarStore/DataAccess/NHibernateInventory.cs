using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using NHibernate.GuitarStore.Common;

namespace NHibernate.GuitarStore.DataAccess
{
    public class NHibernateInventory : NHibernateBase
    {
        public IList<Inventory> ExecuteICriteriaOrderBy(string orderBy)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                try
                {
                    IList<Inventory> result =
                        Session.CreateCriteria(typeof (Inventory)).AddOrder(Order.Asc(orderBy)).List<Inventory>();
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IList<Inventory>  ExecuteICriteria(Guid id)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                try
                {
                    var result = Session.CreateCriteria(typeof (Inventory))
                        .Add(Restrictions.Eq("TypeId", id))
                        .List<Inventory>();

                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
