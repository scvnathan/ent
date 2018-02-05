using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using UnityEngine;

public class Intro : MonoBehaviour {
	public List<CanvasGroup> textIntro = new List<CanvasGroup>();

	async void Start() {
		await new WaitForSeconds(3f);
		
		var seq = LeanTween.sequence();
		for (int i = 0; i < textIntro.Count; i++) {
			seq.append(LeanTween.alphaCanvas(textIntro[i], 1f, 2f).setDelay(2.5f).setEaseInQuad());
		}

		seq.append(() => {
			for (int i = 0; i < textIntro.Count; i++) {
				LeanTween.moveY(textIntro[i].GetComponent<RectTransform>(), 1000f, 5f).setEaseInQuad();
				LeanTween.alphaCanvas(textIntro[i], 0f, 3f);
			}
		});
	}
}