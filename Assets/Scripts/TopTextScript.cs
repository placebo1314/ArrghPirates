using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopTextScript : MonoBehaviour
{
    public Text TopText;
    public IEnumerator ChangeTextWithTime(string txt, int duration)
    {
        float time = 0;
        TopText.text = txt;
        while (time <= duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        TopText.text = "";
    }

    public void ChangeText(string txt)
    {
        TopText.text = txt;
    }
}
