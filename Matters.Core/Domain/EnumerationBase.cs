using System;
using System.Collections.Generic;
using System.Linq;

namespace Matters.Core.Domain
{
    public abstract class EnumerationBase<T, TId, TValue> where T : EnumerationBase<T, TId, TValue>, new()
    {
        public TId Code { get; private set; }
        public TValue Value { get; private set; }
        public static T Default { get; private set; }

        private static readonly Dictionary<TId, T> Main = new Dictionary<TId, T>();

        static EnumerationBase()
        {
            new T();
        }

        protected static T Register(TId id, TValue value)
        {
            var t = new T { Code = id, Value = value };
            Main.Add(id, t);
            return t;
        }

        public static IEnumerable<T> GetValues()
        {
            return Main.Values.ToList();
        }

        protected static void DefaultTo(T defaultvalue)
        {
            Default = defaultvalue;
        }

        public static bool operator ==(EnumerationBase<T, TId, TValue> a, T b)
        {
            if ((object)a == null && (object)b == null) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Code.Equals(b.Code);
        }

        public static bool operator !=(EnumerationBase<T, TId, TValue> a, T b)
        {
            return !(a == b);
        }

        public bool Equals(EnumerationBase<T, TId, TValue> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Code, Code);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!obj.GetType().IsSubclassOf(typeof(EnumerationBase<T, TId, TValue>))) return false;
            return Equals((EnumerationBase<T, TId, TValue>)obj);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}




