/*
 * Assignment 2 
 * Involves creating an ADO.NET data access layer to manage CRUD (Create, Read, Update, Delete) operations for the JOB_PORTAL_DB. 
 * The task requires developing repositories for handling data, 
 * such as applicant education, and implementing methods to create, read, update, and delete records. 
 * The focus is on integrating ADO.NET with the database and ensuring smooth data interactions.
 * 
 * Author: Earl Lamier
 * Student Account: N01710966
 * Email: earlblamier@gmail.com
 * Created: 2024-11-14
 * Course: Full Stack .NET Cloud Developer
 * School: Humber Polytechnic
 * Instructor: Amandeep Patti
 * Submission Date: 2024-11-19
 */


using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CareerCloud.DataAccessLayer
{
    public interface IDataRepository<T>
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        void Add(params T[] items);
        void Update(params T[] items);
        void Remove(params T[] items);
        void CallStoredProc(string name, params Tuple<string, string>[] parameters);
       
    }
}