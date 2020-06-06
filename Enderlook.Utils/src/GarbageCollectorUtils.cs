using System;
using System.Threading.Tasks;

namespace Enderlook.Utils
{
    /// <summary>
    /// A helper class to raise events when a garbage collection happens.
    /// </summary>
    public class GarbageCollectorUtils
    {
        /// <summary>
        /// Event raised when a full garbage collection is about to happen.
        /// </summary>
        public static event Action OnFullGarbageCollectionApproach = TriggerOnceFullGarbageCollectionApproach;

        /// <summary>
        /// Event raised after a full garbage collection.
        /// </summary>
        public static event Action OnFullGarbageCollectionComplete = TriggerOnceFullGarbageCollectionComplete;

        /// <summary>
        /// Event raised when a generation zero garbage collection is completed.
        /// </summary>
        public static event Action OnZeroGarbageCollectionComplete = TriggerOnceZeroGarbageCollectionComplete;

        /// <summary>
        /// Event raised when a full garbage collection is about to happen.<br/>
        /// Subcribers to this event are automatically removed after one trigger.
        /// </summary>
        public static event Action OnNextFullGarbageCollectionApproach;

        /// <summary>
        /// Event raised after a full garbage collection.<br/>
        /// Subcribers to this event are automatically removed after one trigger.
        /// </summary>
        public static event Action OnNextFullGarbageCollectionComplete;

        /// <summary>
        /// Event raised when a generation zero garbage collection is completed.<br/>
        /// Subcribers to this event are automatically removed after one trigger.
        /// </summary>
        public static event Action OnNextZeroGarbageCollectionComplete;

        static GarbageCollectorUtils()
        {
            GC.RegisterForFullGCNotification(10, 10);
            Task.Run(WaitForFullGarbageCollectionApproach);
            Task.Run(WaitForFullGarbageCollectionComplete);

            _ = new GarbageCollectorUtils();
        }

        /// <summary>
        /// Raises <see cref="OnZeroGarbageCollectionComplete"/> and <see cref="OnNextZeroGarbageCollectionComplete"/>.
        /// </summary>
        ~GarbageCollectorUtils()
        {
            OnZeroGarbageCollectionComplete();
            _ = new GarbageCollectorUtils();
        }

        private static void TriggerOnceFullGarbageCollectionApproach()
        {
            OnNextFullGarbageCollectionApproach();
            OnNextFullGarbageCollectionApproach = null;
        }

        private static void TriggerOnceFullGarbageCollectionComplete()
        {
            OnNextFullGarbageCollectionComplete();
            OnNextFullGarbageCollectionComplete = null;
        }

        private static void TriggerOnceZeroGarbageCollectionComplete()
        {
            OnNextZeroGarbageCollectionComplete();
            OnNextZeroGarbageCollectionComplete = null;
        }

        private static void WaitForFullGarbageCollectionApproach()
        {
            while (true)
            {
                GCNotificationStatus notification = GC.WaitForFullGCApproach();
                if (notification == GCNotificationStatus.Succeeded)
                    OnFullGarbageCollectionApproach();
            }
        }

        private static void WaitForFullGarbageCollectionComplete()
        {
            while (true)
            {
                GCNotificationStatus notification = GC.WaitForFullGCComplete();
                if (notification == GCNotificationStatus.Succeeded)
                    OnFullGarbageCollectionComplete();
            }
        }
    }
}
