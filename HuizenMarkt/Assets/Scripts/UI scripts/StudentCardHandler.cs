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

}
