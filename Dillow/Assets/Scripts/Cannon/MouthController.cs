using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthController : MonoBehaviour {

    public enum MouthState {
        neutral,
        open,
        close,
        blow,
        smile,
        frown,
		eat,
    }

    MouthState mouthState;

    public float open;
    public float close;
    public float blow;
    public float smile;
    public float frown;

    public int index;

    // Use this for initialization
    void Start () {
        open = 0f;
        close = 0f;
        blow = 0f;
        smile = 0f;
        frown = 0f;

        mouthState = MouthState.neutral;

        index = 0;
        //InvokeRepeating ("Express", 2f, 2f);
	}

    void Express () {
        SetExpression ((MouthState)index, 1f);

        index++;
        index %= 6;
    }

    public void SetExpression (MouthState expression, float seconds = 1f) {
        StopCoroutine ("SetExpressionCR");
        StartCoroutine (SetExpressionCR (expression, seconds));
    }

    IEnumerator SetExpressionCR (MouthState expression, float seconds) {
        bool complete = false;
        float sOpen = open;
        float sClose = close;
        float sBlow = blow;
        float sSmile = smile;
        float sFrown = frown;
        float t = 0f;

        SkinnedMeshRenderer r = GetComponent<SkinnedMeshRenderer> ();

        while (!complete) {
            switch (expression) {
                default:
                    open = Mathf.Lerp (sOpen, 0f, t * seconds);
                    close = Mathf.Lerp (sClose, 0f, t * seconds);
                    blow = Mathf.Lerp (sBlow, 0f, t * seconds);
                    smile = Mathf.Lerp (sSmile, 0f, t * seconds);
                    frown = Mathf.Lerp (sFrown, 0f, t * seconds);

                    if (open + close + blow + smile + frown < float.Epsilon)
                        complete = true;

                    break;
                case MouthState.open:
                    open = Mathf.Lerp (sOpen, 100f, t * seconds);
                    close = Mathf.Lerp (sClose, 0f, t * seconds);
                    blow = Mathf.Lerp (sBlow, 0f, t * seconds);
                    smile = Mathf.Lerp (sSmile, 0f, t * seconds);
                    frown = Mathf.Lerp (sFrown, 0f, t * seconds);

                    if (100f - open < float.Epsilon)
                        complete = true;
                    break;
                case MouthState.close:
                    open = Mathf.Lerp (sOpen, 0f, t * seconds);
                    close = Mathf.Lerp (sClose, 100f, t * seconds);
                    blow = Mathf.Lerp (sBlow, 0f, t * seconds);
                    smile = Mathf.Lerp (sSmile, 0f, t * seconds);
                    frown = Mathf.Lerp (sFrown, 0f, t * seconds);

                    if (100f - close < float.Epsilon)
                        complete = true;
                    break;
                case MouthState.blow:
                    open = Mathf.Lerp (sOpen, 0f, t * seconds);
                    close = Mathf.Lerp (sClose, 0f, t * seconds);
                    blow = Mathf.Lerp (sBlow, 100f, t * seconds);
                    smile = Mathf.Lerp (sSmile, 0f, t * seconds);
                    frown = Mathf.Lerp (sFrown, 0f, t * seconds);

                    if (100f - blow < float.Epsilon)
                        complete = true;
                    break;
                case MouthState.smile:
                    open = Mathf.Lerp (sOpen, 0f, t * seconds);
                    close = Mathf.Lerp (sClose, 0f, t * seconds);
                    blow = Mathf.Lerp (sBlow, 0f, t * seconds);
                    smile = Mathf.Lerp (sSmile, 100f, t * seconds);
                    frown = Mathf.Lerp (sFrown, 0f, t * seconds);

                    if (100f - smile < float.Epsilon)
                        complete = true;
                    break;
                case MouthState.frown:
                    open = Mathf.Lerp (sOpen, 0f, t * seconds);
                    close = Mathf.Lerp (sClose, 0f, t * seconds);
                    blow = Mathf.Lerp (sBlow, 0f, t * seconds);
                    smile = Mathf.Lerp (sSmile, 0f, t * seconds);
                    frown = Mathf.Lerp (sFrown, 100f, t * seconds);

                    if (100f - frown < float.Epsilon)
                        complete = true;
                    break;
				case MouthState.eat:
					//print("Open: " + open + "\tClose: " + close);
					if (t > 1f) {
						open = Mathf.Max(0f, 100 * Mathf.Cos(2 * 2 * Mathf.PI * (t - 1f)));
						close = Mathf.Max(0f, -100 * Mathf.Cos(2 * 2 * Mathf.PI * (t - 1f)));
						blow = Mathf.Lerp(sBlow, 0f, t * seconds);
						smile = Mathf.Lerp(sSmile, 0f, t * seconds);
						frown = Mathf.Lerp(sFrown, 0f, t * seconds);
					}

					if (t > 2.75f) {
						complete = true;
					}
					break;
			}

            r.SetBlendShapeWeight (0, open);
            r.SetBlendShapeWeight (1, close);
            r.SetBlendShapeWeight (2, blow);
            r.SetBlendShapeWeight (3, smile);
            r.SetBlendShapeWeight (4, frown);

            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

	// Update is called once per frame
	void Update () {

    }
}
