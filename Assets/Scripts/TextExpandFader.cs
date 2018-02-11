using Asyncoroutine;
using TMPro;
using UnityEngine;

public class TextExpandFader : MonoBehaviour {
	private TextMeshProUGUI txt;

	// Use this for initialization
	void Start () {
		txt = GetComponent<TextMeshProUGUI>();
	}

	private void ExpandWidthToo() {
		var rt = txt.gameObject.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector3(rt.sizeDelta.x + txt.characterSpacing * 10f, rt.sizeDelta.y);
	}

	public LTDescr Expand(float duration = 2f, float maxSpread = 100f) {	
		return LeanTween.value(gameObject, (float f1, float f2) => {
				txt.characterSpacing = f1;
				txt.alpha = 1 - (f2 * 2f);
				ExpandWidthToo();
			},
			txt.characterSpacing, 
			(txt.characterSpacing + 1f) * 100f,
			duration
		).setEaseOutCirc();		
	}
}
