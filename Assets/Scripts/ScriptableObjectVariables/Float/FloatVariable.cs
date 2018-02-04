using UnityEngine;


[CreateAssetMenu(fileName = "FloatVariable", menuName = "Ent/FloatVariable")]
public class FloatVariable : ScriptableObject {

    [SerializeField] float _defaultValue;
    public float value;

    private void OnEnable() {

        value = _defaultValue;
    }
}
