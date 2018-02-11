using System;

public static class PlayerEvents {
    public static event Action<CouchPlayer> OnJump;

    public static void InvokeJump(CouchPlayer couchPlayer) {
        var h = OnJump;
        h?.Invoke(couchPlayer);
    }

    public static event Action<CouchPlayer> OnTransmission;

    public static void InvokeTransmission(CouchPlayer couchPlayer) {
        var h = OnTransmission;
        h?.Invoke(couchPlayer);
    }
}