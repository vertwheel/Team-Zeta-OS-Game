using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;

    public TextMeshProUGUI contentField;

    public TextMeshProUGUI layoutElement;

    public int characterWrapLimit;


    // Update is called once per frame
    private void Update()
    {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enableAutoSizing = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;

    }

}
