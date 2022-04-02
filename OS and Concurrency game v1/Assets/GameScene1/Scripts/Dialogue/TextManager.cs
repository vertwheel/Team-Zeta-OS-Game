using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animatior;

    [SerializeField] private Queue<string> sentences = new Queue<string>();


    // Start is called before the first frame update
    void Start()
    {
        //sentences = new Queue<string>();
    }

    public void StartText(TextClass text) {
        animatior.SetBool("IsOpen", true);

        nameText.text = text.name;

        sentences.Clear();
        foreach (string sentence in text.sentences) {

            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {

        if (sentences.Count == 0) {
            EndText();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

    }

    void EndText() {
        animatior.SetBool("IsOpen", false);
        GameObject.Find("GameObject").GetComponent<GameScript>().startLevel();
    }


}
