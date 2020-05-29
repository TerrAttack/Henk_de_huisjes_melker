using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentCardHandler : MonoBehaviour
{
    [SerializeField] GameObject studentCardPrefab = null;

    public void AddNewStudentCard(Student _student)
    { 
        GameObject studentCard = Instantiate(studentCardPrefab, this.transform);
        studentCard.GetComponent<DisplayStudentUICard>().student = _student;
    }

    public void clearContent()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void evictStudent(DisplayStudentUICard card)
    {
        Destroy(card.gameObject);
        Destroy(card.student.gameObject);
    }

    public void Update()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject t = transform.GetChild(i).gameObject;
            t.transform.position = new Vector3(0,this.transform.position.y - i, 0);
        }
    }
}
