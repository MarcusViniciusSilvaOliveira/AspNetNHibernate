using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace System
{
    public class ServiceBase : IDisposable
    {
        protected ISessionFactory SessionFactory { get; set; }
        protected ISession Session { get; private set; }
        public ServiceBase(ISessionFactory factory)
        {
            SessionFactory = factory;
            Session = factory.OpenSession();
        }

        public T Get<T>(long? id) where T : class
        {
            if (id.HasValue)
                return Session.Get<T>(id);
            return null;
        }

        public IQueryOver<T, T> QueryOver<T>() where T : class
        {
            return Session.QueryOver<T>();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return Session.Query<T>();
        }

        public List<T> QueryToList<T>() where T : class
        {
            return Session.Query<T>().ToList();
        }

        public void DeleteInTransaction(Object o)
        {
            Session.Delete(o);
        }


        public void Delete(Object o)
        {
            ITransaction tx = Session.BeginTransaction();
            try
            {
                Session.Delete(o);
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public ITransaction BeginTransaction()
        {
            return Session.BeginTransaction();
        }

        public void SaveInTransaction(object o)
        {
            Session.Save(o);
        }

        public void UpdateInTransaction(object o)
        {
            Session.Update(o);
        }

        public void Save(Object o)
        {
            ITransaction tx = Session.BeginTransaction();
            try
            {
                Session.SaveOrUpdate(o);
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public void SaveRange(IEnumerable<Object> range)
        {
            ITransaction tx = Session.BeginTransaction();
            try
            {
                foreach (var i in range)
                    Session.Save(i);
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public void UpdateRange(IEnumerable<Object> range)
        {
            ITransaction tx = Session.BeginTransaction();
            try
            {
                foreach (var i in range)
                    Session.Update(i);
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public void Update(Object o)
        {
            ITransaction tx = Session.BeginTransaction();
            try
            {
                Session.Update(o);
                tx.Commit();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw new Exception("Ocorreu um erro!", ex);
            }
        }

        public void SaveOrUpdate(Object o)
        {
            ITransaction tx = Session.BeginTransaction();
            try
            {
                Session.SaveOrUpdate(o);
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public void MergeInTransaction(Object obj)
        {
            Session.Merge(obj);
        }

        public void SaveOrUpdateInTransaction(object obj)
        {
            Session.SaveOrUpdate(obj);
        }

        

        public void Merge(Object obj)
        {
            ITransaction tx = Session.BeginTransaction();

            try
            {
                Session.Merge(obj);
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        //public T Map<T>(object source)
        //{
        //    Type sourceType = source.GetType();
        //    if (sourceType.GetGenericArguments().Length > 0)
        //        sourceType = sourceType.GetGenericArguments()[0];
        //    Type destinationType = typeof(T);
        //    if (destinationType.GetGenericArguments().Length > 0)
        //        destinationType = destinationType.GetGenericArguments()[0];
            
        //    return AutoMapper.Mapper.Map<T>(source);
        //}

        //public void Map<Ts, Td>(Ts source, Td destination)
        //{
        //    Type sourceType = typeof(Ts);
        //    if (sourceType.GetGenericArguments().Length > 0)
        //        sourceType = sourceType.GetGenericArguments()[0];
        //    Type destinationType = typeof(Td);
        //    if (destinationType.GetGenericArguments().Length > 0)
        //        destinationType = destinationType.GetGenericArguments()[0];
           
        //    AutoMapper.Mapper.Map<Ts, Td>(source, destination);
        //}

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}
