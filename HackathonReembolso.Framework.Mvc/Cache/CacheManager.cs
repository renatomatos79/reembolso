using System;
using System.Web;

namespace HackathonReembolso.Framework.Mvc.Cache
{
    public class CacheManager<T> where T : class
    {
        public delegate T CacheObjectHandler();
        
        private static readonly CacheManager<T> _Instance = new CacheManager<T>();

        public static CacheManager<T> Instance
        {
            get
            {
                return _Instance;
            }
        }

        protected virtual void Add(string key, T value, int minutes)
        {
            HttpContext.Current.Cache.Remove(key);
            HttpContext.Current.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(minutes), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public virtual T GetFromCache(string key, CacheObjectHandler defaultValue, int minutes)
        {
            var cache = HttpContext.Current.Cache.Get(key);
            if ((cache == null) && (defaultValue != null))
            {
                cache = defaultValue.Invoke();
                if (cache != null)
                {
                    Add(key, (T)cache, minutes);
                }
            }
            return (T)cache;
        }

        public virtual T GetFromCache(string key)
        {
            return GetFromCache(key, () => { return default(T); }, 20);
        }

        public virtual void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        public virtual void Update(string key, T value, int minutes)
        {
            Add(key, value, minutes);
        }
    }
}
