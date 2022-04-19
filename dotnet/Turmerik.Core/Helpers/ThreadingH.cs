using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Threading;

namespace Turmerik.Core.Helpers
{
    public static class ThreadingH
    {
        public static TResult WithSync<T, TResult>(
            this ICollection<T> colctn,
            Func<ICollection<T>, TResult> action,
            SynchronizerComponent synchronizer = null)
        {
            synchronizer = synchronizer ?? new SynchronizerComponent(
                ((ICollection)colctn).SyncRoot);

            TResult result = synchronizer.Invoke(
                () => action(colctn));

            return result;
        }

        public static TResult WithSync<TResult>(
            this ICollection colctn,
            Func<ICollection, TResult> action,
            SynchronizerComponent synchronizer = null)
        {
            synchronizer = synchronizer ?? new SynchronizerComponent(
                colctn.SyncRoot);

            TResult result = synchronizer.Invoke(
                () => action(colctn));

            return result;
        }
        public static void WithSync<T>(
            this ICollection<T> colctn,
            Action<ICollection<T>> action,
            SynchronizerComponent synchronizer = null)
        {
            synchronizer = synchronizer ?? new SynchronizerComponent(
                ((ICollection)colctn).SyncRoot);

            synchronizer.Invoke(
                () => action(colctn));
        }

        public static void WithSync(
            this ICollection colctn,
            Action<ICollection> action,
            SynchronizerComponent synchronizer = null)
        {
            synchronizer = synchronizer ?? new SynchronizerComponent(
                colctn.SyncRoot);

            synchronizer.Invoke(
                () => action(colctn));
        }
    }
}
