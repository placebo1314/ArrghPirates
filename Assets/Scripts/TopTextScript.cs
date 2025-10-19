using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TopTextScript : MonoBehaviour
{
    public Text TopText;

    private Coroutine messageRoutine;
    private string persistentText = "";

    public void ChangeText(string txt)
    {
        persistentText = txt;

        if (messageRoutine == null)
        {
            TopText.text = persistentText;
        }
    }

    public void ShowTemporaryMessage(string txt, float duration)
    {
        if (messageRoutine != null)
        {
            StopCoroutine(messageRoutine);
        }

        messageRoutine = StartCoroutine(ChangeTextWithTime(txt, duration));
    }

    private IEnumerator ChangeTextWithTime(string txt, float duration)
    {
        float time = 0;
        TopText.text = txt;
        while (time <= duration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        TopText.text = persistentText;
        messageRoutine = null;
    }
}
