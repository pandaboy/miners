using System;
using System.Collections;

namespace Pathfinding
{
    public class Heap : IList, ICloneable
    {
        private ArrayList list;
        private IComparer comparer = null;
        private bool useObjectsComparison;

        public Heap()
        {
            Init(null, 0);
        }

        public Heap(int capacity)
        {
            Init(null, capacity);
        }

        public Heap(IComparer comparer)
        {
            Init(comparer, 0);
        }

        public Heap(IComparer comparer, int capacity)
        {
            Init(comparer, capacity);
        }

        private bool addDuplicates;
        public bool AddDuplicates
        {
            set { addDuplicates = value; }
            get { return addDuplicates; }
        }

        public int Capacity
        {
            set { list.Capacity = value; }
            get { return list.Capacity; }
        }

        public object this[int i]
        {
            get
            {
                if (i >= list.Count || i < 0)
                    throw new ArgumentOutOfRangeException("Attempting to access outside of list range");
                return list[i];
            }
            set
            {
                throw new InvalidOperationException("[] can not be used to set values in a Heap");
            }
        }

        public int Add(object O)
        {
            int return_i = -1;
            if(ObjectIsCompliant(O)) {
                int i = IndexOf(O);
                int new_i = i >= 0 ? i : -i - 1;
                if (new_i >= Count) {
                    list.Add(O);
                }
                else {
                    list.Insert(new_i, O);
                }
                return_i = new_i;
            }

            return return_i;
        }

        public bool Contains(object O)
        {
            return list.BinarySearch(O, comparer) >= 0;
        }

        public int IndexOf(object O)
        {
            int result = -1;
            result = list.BinarySearch(O, comparer);
            while (result > 0 && list[result - 1].Equals(O))
            {
                result--;
            }
            return result;
        }

        public bool IsFixedSize
        {
            get { return list.IsFixedSize; }
        }

        public bool IsReadOnly
        {
            get { return list.IsReadOnly; }
        }

        public void Clear()
        {
            list.Clear();
        }

        public void Insert(int i, object O)
        {
            throw new InvalidOperationException("Insert method unavailable on a heap");
        }

        public void Remove(object O)
        {
            list.Remove(O);
        }

        public void RemoveAt(int i)
        {
            list.RemoveAt(i);
        }


        public void CopyTo(Array a, int i)
        {
            list.CopyTo(a, i);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsSynchronized
        {
            get { return list.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return list.SyncRoot; }
        }


        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }


        public object Clone()
        {
            Heap Clone = new Heap(comparer, list.Capacity);
            Clone.list = (ArrayList)list.Clone();
            Clone.addDuplicates = addDuplicates;
            return Clone;
        }


        public delegate bool Equality(object O1, object O2);


        public override string ToString()
        {
            String outString = "{";
            for (int i = 0; i < list.Count; i++)
            {
                outString += list[i].ToString() + (i != list.Count - 1 ? "; " : "}");
            }
            return outString;
        }

        public override bool Equals(object O)
        {
            Heap sl = (Heap)O;
            if (sl.Count != Count)
            {
                return false;
            }
            for (int i = 0; i < Count; i++)
            {
                if (!sl[i].Equals(this[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return list.GetHashCode();
        }


        public int IndexOf(object O, int start)
        {
            int result = -1;
            result = list.BinarySearch(start, list.Count - start, O, comparer);
            while (result > start && list[result - 1].Equals(O))
            {
                result--;
            }
            return result;
        }

        public int IndexOf(object O, Equality areEqual)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (areEqual(list[i], O))
                {
                    return i;
                }
            }

            return -1;
        }

        public int IndexOf(object O, int start, Equality areEqual)
        {
            if (start < 0 || start >= list.Count)
            {
                throw new ArgumentException("Start index must belong to [0, Count-1]");
            }

            for (int i = start; i < list.Count; i++)
            {
                if (areEqual(list[i], O))
                {
                    return i;
                }
            }

            return -1;
        }

        public void AddRange(ICollection C)
        {
            foreach (object Object in C)
            {
                Add(Object);
            }
        }

        public void InsertRange(int i, ICollection C)
        {
            throw new InvalidOperationException("Insert cannot be called on a Heap.");
        }

        public void LimitOccurrences(object O, int numberToKeep)
        {
            if (O == null)
            {
                throw new ArgumentNullException("Value");
            }

            int i = 0;
            while ((i = IndexOf(O, i)) >= 0)
            {
                if (numberToKeep <= 0){
                    list.RemoveAt(i);
                }
                else
                {
                    i++;
                    numberToKeep--;
                }
                if (comparer.Compare(list[i], O) > 0)
                    break;
            }
        }

        public void RemoveDuplicates()
        {
            int i = 0;
            while (i < Count - 1)
            {
                if (comparer.Compare(this[i], this[i + 1]) == 0) {
                    RemoveAt(i);
                }
                else {
                    i++;
                }
            }
        }

        public int IndexOfMin()
        {
            int i = -1;
            if (list.Count > 0)
            {
                i = 0;
                object RetObj = list[0];
            }

            return i;
        }

        public int IndexOfMax()
        {
            int i = -1;
            if (list.Count > 0)
            {
                i = list.Count - 1;
                object RetObj = list[list.Count - 1];
            }
            return i;
        }

        public object Pop()
        {
            if (list.Count == 0) {
                throw new InvalidOperationException("The heap is empty.");
            }
            object O = list[Count - 1];
            list.RemoveAt(Count - 1);
            return (O);
        }

        public int Push(object O)
        {
            return (Add(O));
        }


        private bool ObjectIsCompliant(object O)
        {
            if (useObjectsComparison && !(O is IComparable))
            {
                throw new ArgumentException("The Heap is set to use the IComparable interface of objects, and the object to add does not implement the IComparable interface.");
            }
            
            if (!addDuplicates && Contains(O)) {
                return false;
            }

            return true;
        }

        private class Comparison : IComparer
        {
            public int Compare(object O1, object O2)
            {
                IComparable C = O1 as IComparable;
                return C.CompareTo(O2);
            }
        }

        private void Init(IComparer Comparer, int capacity)
        {
            if (Comparer != null)
            {
                comparer = Comparer;
                useObjectsComparison = false;
            }
            else
            {
                comparer = new Comparison();
                useObjectsComparison = true;
            }

            list = capacity > 0 ? new ArrayList(capacity) : new ArrayList();
            addDuplicates = true;
        }
    }
}
