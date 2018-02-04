using System;
using UnityEngine;

[Serializable]
public class IntReference {
    
    [SerializeField] bool _useConstant = true;
    [SerializeField] int _constantValue;
    [SerializeField] IntVariable _variable;

    public IntReference() {}

    public IntReference(int value) {

        _useConstant = true;
        _constantValue = value;
    }

    public int Value {

        get { return _useConstant ? _constantValue : _variable.value; }
    }

    public static implicit operator int(IntReference reference) {

        return reference.Value;
    }
}
