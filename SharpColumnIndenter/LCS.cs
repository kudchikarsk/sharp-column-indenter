using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpColumnIndenter
{
    class LCS<T>
    {
        private List<T> _commonSequence;
        private readonly IEnumerable<T>[] _sequences;
        private IEqualityComparer<T> _comparer;

        public LCS(IEqualityComparer<T> comparer,params IEnumerable<T>[] sequences)
        {
            _commonSequence = new List<T>();
            _sequences = sequences;
            _comparer = comparer;
        }

        public IEnumerable<T> Result
        {
            get
            {
                return _commonSequence;
            }
        }            

        public int Execute()
        {
            var lcs = 0;
            var first = true;
            foreach (var sequence in _sequences)
            {
                if (first)
                {
                    _commonSequence = sequence.ToList();
                    first = false;
                }
                
                _commonSequence = Execute(sequence.ToArray(), _commonSequence.ToArray(), sequence.Count(), _commonSequence.Count(),new List<T>());
                _commonSequence.Reverse();
            }

            return lcs;
        }

        private List<T> Execute(T[] x, T[] y, int m, int n,List<T> commonList)
        {
            if (m == 0 || n == 0)
                return commonList;
            if (_comparer.Equals(x[m - 1], y[n - 1]))
            {
                commonList.Add(x[m - 1]);
                return Execute(x, y, m - 1, n - 1, commonList.ToList());
            }
            else
            {
                var list1 = Execute(x, y, m, n - 1, commonList.ToList());
                var list2 = Execute(x, y, m - 1, n, commonList.ToList());
                var max=Math.Max(list1.Count, list2.Count);
                if (max == list2.Count) return list2;
                return list1;                
            }
        }
    }
}
