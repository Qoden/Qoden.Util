using System;
using System.Threading.Tasks;
using Qoden.Validation;

namespace Qoden.Util
{
    /// <summary>
    /// <see cref="T:Qoden.Util.SingletonOperation`1"/> is async operation which can
    /// be run one at a time, like login operation.
    /// </summary>
    /// <remarks>
    /// <see cref="T:Qoden.Util.SingletonOperation`1"/> can be started from multiple threads.
    /// Only first caller starts real operation while others get already started operation as a result. 
    /// 
    /// Once operation finished it can be repeated.
    /// </remarks>
    public class SingletonOperation<T>
    {
        private Func<Task<T>> operation;
        private object mutex = new object();
        private volatile Task<T> task;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Qoden.Auth.SingletonOperation`1"/> class.
        /// </summary>
        /// <param name="operation">Operation to perform</param>
        public SingletonOperation(Func<Task<T>> operation)
        {
            Assert.Argument(operation, nameof(operation)).NotNull();
            this.operation = operation;
        }

        /// <summary>
        /// Start single operation
        /// </summary>
        /// <returns>Async operation result</returns>
        public Task<T> Start()
        {
            if (task == null)
            {
                lock (mutex)
                {
                    if (task == null)
                    {
                        task = DoStart();
                    }
                }
            }

            return task;
        }

        private async Task<T> DoStart()
        {
            try
            {
                return await operation();
            }
            finally 
            {
                task = null;   
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Qoden.Auth.SingletonOperation`1"/> is started.
        /// </summary>
        /// <value><c>true</c> if started; otherwise, <c>false</c>.</value>
        public bool Started => task != null;

        /// <summary>
        /// Gets running task.
        /// </summary>
        public Task<T> Task => task;
    }
}
