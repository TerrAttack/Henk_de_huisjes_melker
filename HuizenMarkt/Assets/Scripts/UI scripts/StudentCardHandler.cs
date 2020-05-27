using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentCardHandler : MonoBehaviour
{
    [SerializeField] GameObject studentCardPrefab;

    private void Start()
    {

    }

    public void AddNewStudentCard(Student _student)
    {
        StudentUICard studentUICard = new StudentUICard()
        {
            studentName = _student.studentName,
            money = _student.currentMoney,
            income = _student.income,
        };
        GameObject studentCard = Instantiate(studentCardPrefab, this.transform);
        studentCard.GetComponent<DisplayStudentUICard>().studentUICard = studentUICard;
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
