using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomCollection
{
    public class CustomVector<T> : IList<T>, IEnumerable<T>
    {
        private T[] _vector;

        public int Count { get; protected set; } = 0;
        public bool IsReadOnly => false;
        public int Capacity { get => _vector.Length; }

        public CustomVector()
        {
            _vector = new T[2];
        }

        public CustomVector(int capacity)
        {
            if (capacity < 1) capacity = 1;
            _vector = new T[capacity];
        }

        public CustomVector(IEnumerable<T> collection) : this()
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }
                return _vector[index];
            }
            set
            {
                if (IsReadOnly == false)
                {
                    if (index < 0 || index >= Count)
                    {
                        throw new IndexOutOfRangeException(nameof(index));
                    }
                    _vector[index] = value;
                }
                throw new InvalidOperationException("This array is ReadOnly.");
            }
        }

        public void Add(T item)
        {
            if (!HasFreeCells()) IncreaseSize();
            _vector[Count] = item;
            Count++;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public void Clear()
        {
            _vector = new T[1];
            Count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) > -1;
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index > -1)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            for (int i = index; i < Count - 1; i++)
            {
                _vector[i] = _vector[i + 1];
            }
            Count--;
            _vector[Count] = default(T);
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            T[] newArray;

            if (Capacity == Count)
            {
                newArray = new T[2 * Capacity];
            }
            else
            {
                newArray = new T[Capacity];
            }

            Array.Copy(_vector, 0, newArray, 0, index);
            newArray[index] = item;
            Array.Copy(_vector, index, newArray, index + 1, Count - index);

            _vector = newArray;
            Count++;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(_vector[i], item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (arrayIndex < 0)
            {
                throw new IndexOutOfRangeException(nameof(arrayIndex));
            }
            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException(nameof(arrayIndex));
            }

            for (int i = 0; i < Count; i++)
            {
                array[arrayIndex] = _vector[i];
                arrayIndex++;
            }
        }

        public T[] ToArray()
        {
            T[] array = new T[Count];

            Array.Copy(_vector, 0, array, 0, Count);

            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _vector[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool HasFreeCells()
        {
            return Count < Capacity;
        }

        private void IncreaseSize()
        {
            var newVector = new T[Capacity * 2];
            for (int i = 0; i < Capacity; i++)
            {
                newVector[i] = _vector[i];
            }
            _vector = newVector;
        }
    }
}
