using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThirdpartySDK.Threading
{
    public class ThreadingAccess
    {
        public class MultiThreadAccessException : Exception
        {
            public MultiThreadAccessException() : this("can not access by multi-threads simultaneously")
            {

            }

            public MultiThreadAccessException(string message) : base(message)
            {

            }
        }

        private int _isAccessing;
        private void EnsureSingleThreadAccessing()
        {
            if (Interlocked.CompareExchange(ref _isAccessing, 1, 0) != 0)
                throw new MultiThreadAccessException();
        }

        private void ExitSingleThreadAccessing()
        {
            Interlocked.Exchange(ref _isAccessing, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="MultiThreadAccessException"></exception>
        /// <param name="c"></param>
        /// <param name="post"></param>
        public void DoWithSingleThreadEnsure(Action c, Action post = null)
        {
            EnsureSingleThreadAccessing();
            try
            {
                c?.Invoke();
            }
            finally
            {
                post?.Invoke();
                ExitSingleThreadAccessing();
            }
        }

        public T ReturnWithSingleThreadEnsure<T>(Func<T> c, Action post = null)
        {
            EnsureSingleThreadAccessing();
            try
            {
                return c();
            }
            finally
            {
                post?.Invoke();
                ExitSingleThreadAccessing();
            }
        }
    }
}
