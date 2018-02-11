using System;

public static partial class Events {
    public static class ResourceEvents {
        public static event Action<ResourceData, object> OnDeposit;

        public static void InvokeDeposit(ResourceData resourceData, object depositer) {
            var h = OnDeposit;
            h?.Invoke(resourceData, depositer);
        }
    }
}