using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drones.Infrastructure
{
    public abstract class WorkerBase : DisposableBase
    {
        // @Properties
        public bool IsAlive
        {
            get
            {
                return _cancellationTokenSource != null;
            }
        }


        // @Events
        public event Action<object, Exception> UnhandledException;


        // @Public
        public void Start()
        {
            if (_cancellationTokenSource != null)
            {
                return;
            }

            lock (_locker)
            {
                if (_cancellationTokenSource != null)
                {
                    return;
                }

                _cancellationTokenSource = new CancellationTokenSource();
                var thread = new Thread(RunLoop)
                {
                    Name = GetType().Name
                };
                thread.Start();
            }
        }

        public void Stop()
        {
            if (_cancellationTokenSource == null)
            {
                return;
            }

            lock (_locker)
            {
                if (_cancellationTokenSource == null)
                {
                    return;
                }
                _cancellationTokenSource.Cancel();
            }
        }

        public void Join()
        {
            while (IsAlive)
            {
                Thread.Sleep(1);
            }
        }


        // @Protected
        protected override void DisposeOverride()
        {
            Stop();
        }

        protected abstract void Loop(CancellationToken token);

        protected virtual void OnException(Exception e)
        {
            Trace.TraceError("{0} - Exception: {1}", GetType(), e.Message);
            Trace.TraceError(e.StackTrace);

            OnUnhandledException(e);
        }

        protected virtual void OnUnhandledException(Exception e)
        {
           if (UnhandledException != null)
           {
                UnhandledException(this, e);
           }
        }


        // @Private
        object _locker = new object();
        CancellationTokenSource _cancellationTokenSource;

        void RunLoop(object obj)
        {
            try
            {
                Loop(_cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                OnException(e);
            }
            finally
            {
                lock (_locker)
                {
                    var cancellationTokenSource = _cancellationTokenSource;
                    _cancellationTokenSource = null;
                    cancellationTokenSource.Dispose();
                }
            }
        }
    }
}
