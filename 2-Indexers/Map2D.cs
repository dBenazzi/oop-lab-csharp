namespace Indexers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <inheritdoc cref="IMap2D{TKey1,TKey2,TValue}" />
    public class Map2D<TKey1, TKey2, TValue> : IMap2D<TKey1, TKey2, TValue>
    {
        private readonly Dictionary<Tuple<TKey1, TKey2>, TValue> insideMap = new Dictionary<Tuple<TKey1, TKey2>, TValue>();

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.NumberOfElements" />
        public int NumberOfElements
        {
            get
            {
                return insideMap.Count;
            }
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.this" />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get
            {
                return this.insideMap[new Tuple<TKey1, TKey2>(key1, key2)];
            }
            set
            {
                this.insideMap.Add(new Tuple<TKey1, TKey2>(key1, key2), value);
            }
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetRow(TKey1)" />
        public IList<Tuple<TKey2, TValue>> GetRow(TKey1 key1)
        {
            IList<Tuple<TKey2, TValue>> list = new List<Tuple<TKey2, TValue>>();

            foreach (KeyValuePair<Tuple<TKey1, TKey2>, TValue> elem in insideMap)
            {
                if (elem.Key.Item1.Equals(key1))
                {
                    list.Add(new Tuple<TKey2, TValue>(elem.Key.Item2, elem.Value));
                }
            }
            return list;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetColumn(TKey2)" />
        public IList<Tuple<TKey1, TValue>> GetColumn(TKey2 key2)
        {
            IList<Tuple<TKey1, TValue>> list = new List<Tuple<TKey1, TValue>>();

            foreach (KeyValuePair<Tuple<TKey1, TKey2>, TValue> elem in insideMap)
            {
                if (elem.Key.Item2.Equals(key2))
                {
                    list.Add(new Tuple<TKey1, TValue>(elem.Key.Item1, elem.Value));
                }
            }
            return list;
        }


        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetElements" />
        public IList<Tuple<TKey1, TKey2, TValue>> GetElements()
        {
            IList<Tuple<TKey1, TKey2, TValue>> list = new List<Tuple<TKey1, TKey2, TValue>>();

            foreach (KeyValuePair<Tuple<TKey1, TKey2>, TValue> elem in insideMap)
            {
                list.Add(new Tuple<TKey1, TKey2, TValue>(elem.Key.Item1, elem.Key.Item2, elem.Value));
            }
            return list;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.Fill(IEnumerable{TKey1}, IEnumerable{TKey2}, Func{TKey1, TKey2, TValue})" />
        public void Fill(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, Func<TKey1, TKey2, TValue> generator)
        {
            foreach(TKey1 val1 in keys1)
            {
                foreach (TKey2 val2 in keys2)
                {
                    insideMap.Add(new Tuple<TKey1, TKey2>(val1, val2), generator.Invoke(val1, val2));
                }
            }
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IMap2D<TKey1, TKey2, TValue> other)
        {
            return this.NumberOfElements == other.NumberOfElements &&
                this.GetElements().Equals(other.GetElements());
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (obj is IMap2D<TKey1, TKey2, TValue> map2DObj)
            {
                this.Equals(map2DObj);
            }

            return false;
                   
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            // TODO: improve
            return base.GetHashCode();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.ToString"/>
        public override string ToString()
        {
            return this.insideMap.ToString();
        }
    }
}
