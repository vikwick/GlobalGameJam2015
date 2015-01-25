using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Message : MonoBehaviour {

	public static Message instance;

	public float messageDuration = 1.0f;
	public float fadeOutDuration = 1.0f;
	public AnimationCurve fadeOutCurve;

	private CanvasRenderer canvasRenderer;
	private Text text;
	private Coroutine runningCoroutine;

	private string _message;
	public string message {
		get { return _message; }
		set {
			_message = value;
			if (this.runningCoroutine != null) {
				StopCoroutine (runningCoroutine);
			}
			this.runningCoroutine = StartCoroutine (ShowMessage());
		}
	}

	void Awake () {
		Message.instance = this;
		this.canvasRenderer = GetComponent<CanvasRenderer> ();
		this.text = GetComponent<Text> ();

		this.canvasRenderer.SetAlpha (0);
	}

	private IEnumerator ShowMessage () {
		text.text = this.message;
		this.canvasRenderer.SetAlpha (1);
		yield return new WaitForSeconds (messageDuration);

		float time = 0;
		while (time < this.fadeOutDuration) {
			float alpha = this.fadeOutCurve.Evaluate (time / this.fadeOutDuration);
			this.canvasRenderer.SetAlpha (alpha);
			yield return null;
			time += Time.deltaTime;
		}
	}

}
