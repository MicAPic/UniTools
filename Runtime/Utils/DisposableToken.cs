using System;

namespace UniTools.Utils
{
    public class DisposableToken : IDisposable
    {
        private readonly Action _onDispose;
        private bool _disposed = false;

        public bool IsDisposed => _disposed;
        
        public DisposableToken(Action onDispose)
        {
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            if (_disposed) return;

            _onDispose?.Invoke();
            _disposed = true;
        }
    }
}