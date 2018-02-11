using System;

public static partial class Events {
    public static class CreepEvents {
        public static event Action OnFinalCreep;

        public static void InvokeFinalCreep() {
            var h = OnFinalCreep;
            h?.Invoke();
        }
    }
}