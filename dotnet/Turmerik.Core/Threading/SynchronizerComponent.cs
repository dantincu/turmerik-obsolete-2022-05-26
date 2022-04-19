using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Threading
{
    public interface ISynchronizerComponent
    {
        void Invoke(Action action);
        T Invoke<T>(Func<T> action);

        void Invoke(
            Action action,
            Func<bool> condition = null,
            Action elseAction = null);

        T Invoke<T>(
            Func<T> action,
            Func<bool> condition = null,
            Func<T> elseAction = null);
    }

    public class SynchronizerComponent : ISynchronizerComponent
    {
        public readonly object SyncRoot;

        public SynchronizerComponent(object syncRoot = null)
        {
            this.SyncRoot = syncRoot ?? new object();
        }

        public void Invoke(Action action)
        {
            lock (SyncRoot)
            {
                action();
            }
        }

        public T Invoke<T>(Func<T> action)
        {
            T result;

            lock (SyncRoot)
            {
                result = action();
            }

            return result;
        }

        public void Invoke(Action action, Func<bool> condition = null, Action elseAction = null)
        {
            if (condition?.Invoke() ?? true)
            {
                lock (SyncRoot)
                {
                    if (condition?.Invoke() ?? true)
                    {
                        action();
                    }
                    else
                    {
                        elseAction?.Invoke();
                    }
                }
            }
            else
            {
                elseAction?.Invoke();
            }
        }

        public T Invoke<T>(Func<T> action, Func<bool> condition = null, Func<T> elseAction = null)
        {
            T result;

            if (condition?.Invoke() ?? true)
            {
                lock (SyncRoot)
                {
                    if (condition?.Invoke() ?? true)
                    {
                        result = action();
                    }
                    else if (elseAction != null)
                    {
                        result = elseAction();
                    }
                    else
                    {
                        result = default(T);
                    }
                }
            }
            else if (elseAction != null)
            {
                result = elseAction();
            }
            else
            {
                result = default(T);
            }

            return result;
        }
    }
}
