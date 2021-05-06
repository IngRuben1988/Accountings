using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTACore.Collections
{
    public class CollectionsUtils
    {

        public class IEnumerableUtils
        {
            ///*****************************************///
            public static IEnumerable<T> AddSingle<T>(T item)
            {
                var list = new List<T>(1)
            {
                item
            }; return list;
            }
            
            public static IEnumerable<T> AddSingle<T>(IEnumerable<T> source, T item)
            {
                return source.Concat(new[] { item });
            }

            public static IEnumerable<T> AddList<T>(IList<T> source)
            {
                return source.AsEnumerable<T>();
            }

            public static IEnumerable<T> AddList<T>(IEnumerable<T> source, IList<T> items)
            {
                var x = source.ToList();
                foreach (T model in items)
                { x.Add(model); }
                return x;
            }
            
            public static IEnumerable<T> AddList<T>(IEnumerable<T> source, IEnumerable<T> items)
            {
                var x = source.ToList();
                foreach (T model in items)
                { x.Add(model); }
                return x;
            }

            public static IEnumerable<T> AddToList<T>(IEnumerable<T> source, T item)
            {
                var x = source.ToList();

                x.Add(item);
                // source = x;
                return x.AsEnumerable();
            }
        }

        public class IListUtils
        {
            ///**********************************************///
            
            public static IList<T> AddSingle<T>(T item)
            {
                var list = new List<T>(1); list.Add(item); return list;
            }
            
            public static IList<T> AddSingle<T>(IList<T> source, T item)
            {
                source.Add(item); return source;
            }
            
            public static IList<T> AddList<T>(IList<T> source)
            {
                return source.ToList();
            }

            public static IList<T> AddList<T>(IList<T> source, IList<T> items)
            {
                var x = source.ToList();
                foreach (T model in items)
                { x.Add(model); }
                return x;
            }

            public static IList<T> AddListtoList<T>(IList<T> source, IEnumerable<T> items)
            {
                foreach (T model in items)
                { source.Add(model); }
                return source;
            }

            public static IList<T> AddListtoList<T>(IList<T> source, IList<T> items)
            {
                foreach (T model in items)
                { source.Add(model); }
                return source;
            }

            
            public static IList<T> AddToList<T>(IList<T> source, T item)
            {
                source.Add(item);
                return source;
            }
        }

        public class ListUtils
        {
            public static void AddListtoList<T>(List<T> source, List<T> items)
            {
                foreach (T model in items)
                { source.Add(model); }
                //return source;
            }

            public static void AddListtoList<T>(List<T> source, IEnumerable<T> items)
            {
                foreach (T model in items)
                { source.Add(model); }
                // return source;
            }
        }
    }
}