using System.Collections;
using TMPro;
using UnityEngine;

public class ObjectiveView : MonoBehaviour
{
    [SerializeField] GameObject[] textboxes;

    int fieldsInView = 0;
    
    void Awake()
    {
        foreach(GameObject textbox in textboxes) textbox.SetActive(false);
    }

    void Start()
    {
        if (SceneController.instance.levelStep == 2) textboxes[0].SetActive(true);
    }

    void Update()
    {
        if (SceneController.instance.levelStep > fieldsInView)
        {
            if (fieldsInView > 0 && textboxes[fieldsInView - 1].GetComponent<TextMeshPro>())
                textboxes[fieldsInView - 1].GetComponent<TextMeshPro>().color = Color.black;
            StartCoroutine(EnableText(textboxes[fieldsInView]));
            fieldsInView++;
        }
    }

    IEnumerator EnableText(GameObject textbox)
    {
        if (textbox.activeSelf) yield break;

        yield return new WaitForSeconds(0.2f);

        textbox.SetActive(true);

        // Textbox flashing into view
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.12f);
            textbox.SetActive(false);
            yield return new WaitForSeconds(0.12f);
            textbox.SetActive(true);
        }
    }
}
