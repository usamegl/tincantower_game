using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI[] texts;
    private int[] numbers = new int[] { 488, 228, 106, 73, 65, 57, 29, 28, 21, 17, 20 };
    //1.1 den 6.2 ye kadar sýralý
    public InputField[] inputs;
    private int[] answers = new int[]{260,138,122,49,37,36,16,13,15,6};


    public Color startColor;
    public Color endColor;
    public float duration = 1.0f;
    private float startTime;
    public Image image;

    public GameObject succesPanel;
    public GameObject gameResumePanel;

    public bool isAllCorrect = true;
    
    public AudioSource SaudioEffect;
    public AudioSource WaudioEffect;

    public Sprite RedEgg;
    public Sprite GreenEgg;
    public Animator anim;
    void Start()
    {
        succesPanel.SetActive(false);
        gameResumePanel.SetActive(false);

        startTime = Time.time;
        
        for (int i = 0; i < numbers.Length; i++)
        {
            texts[i].text = numbers[i].ToString();
        }
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].text ="0";
        }

        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].GetComponent<Image>().sprite=RedEgg;
        }
    }

    void Update()
    {

        #region renk
        float t = (Time.time - startTime) / duration;
        image.color = Color.Lerp(startColor, endColor, t);

        if (t >= 2.0f)
        {
            startTime = Time.time;
            Color tempColor = startColor;
            startColor = endColor;
            endColor = tempColor;
        }
        #endregion

        for (int i = 0; i < answers.Length; i++)
        {
            if (answers[i] != int.Parse(inputs[i].text))
            {
                gameResumePanel.SetActive(true);
                isAllCorrect = false;
            }
            else
            {
                isAllCorrect=true;
            }
        }
        if (isAllCorrect)
        {
            Debug.Log("1");
            WaudioEffect.Play();
            Invoke("ShowSucces", 2f);
        }
    }

    public void EndEdit()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            if (int.Parse(inputs[i].text) == answers[i])
            {
                inputs[i].GetComponent<Image>().sprite = GreenEgg;
                anim = inputs[i].transform.parent.GetComponent<Animator>();


                TriggerEffects(true);
            }
            else
            {
                inputs[i].GetComponent<Image>().sprite= RedEgg;
            }

        }
    }
    void TriggerEffects(bool index)
    {
        if (index == true)
        {
            //basarýlý durumu
            GameObject particleEffect = GameObject.Find("ParticleEffect_1"); // GameObject'un adýný belirleyin
            particleEffect.GetComponent<ParticleSystem>().Play();
            SaudioEffect.Play();
            anim.enabled = true;

        }
        if (index==false)
        {
            //basarýsýz durumu
            
        }

    }
    void ShowSucces()
    {
        Debug.Log("2");
        gameResumePanel.SetActive(false);
        succesPanel.SetActive(true);
        Invoke("Quit", 4f);
    }
    void Quit()
    {
        Debug.Log("3");

        Application.Quit();
    }


}
