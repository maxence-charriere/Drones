using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Infrastructure
{
    public abstract class DisposableBase : IDisposable
    {
        // @Public
        public void Dispose()
        {
            Dispose(true);
        }


        // @Protected
        bool _disposed = false;
        protected bool Disposed
        {
            get
            {
                return _disposed;
            }
            private set
            {
                _disposed = value;
            }
        }

        protected abstract void DisposeOverride();


        // @Private
        void Dispose(bool disposing)
        {
            if (disposing && Disposed == false)
            {
                DisposeOverride();
            }
            GC.SuppressFinalize(this);
        }
    }
}
