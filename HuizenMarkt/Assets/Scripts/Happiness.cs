using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Happiness : MonoBehaviour
{
    [SerializeField]  Student student;

    [SerializeField]  Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    [SerializeField]  SpriteRenderer sprite;

    void Start()
    {
        student = transform.parent.GetComponent<Student>();
        gradient = new Gradient();
        SetGradiant();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void SetGradiant()
    {
        colorKey = new GradientColorKey[3];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.yellow;
        colorKey[1].time = 0.5f;
        colorKey[2].color = Color.green;
        colorKey[2].time = 1.0f;

        alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 0.5f;
        alphaKey[2].alpha = 1.0f;
        alphaKey[2].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }

    void Update()
    {
        float i = Mathf.Clamp(student.happiness, 0, 100);
        Debug.Log(i + " " + student.happiness);
        sprite.color = ColorFromGradient(i/100);
    }

    Color ColorFromGradient(float value)
    {
        return gradient.Evaluate(value);
    }
}
