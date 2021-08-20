using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] GameObject m_cellPrefab = default;
    [SerializeField] Transform m_cardPanel = default;
    [SerializeField] Transform m_numberPanel = default;

    List<Cell> m_cardCells = new List<Cell>();
    List<int> m_allnumbers = new List<int>();
    List<int> m_columnB = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
    List<int> m_columnI = new List<int>() { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
    List<int> m_columnN = new List<int>() { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45 };
    List<int> m_columnG = new List<int>() { 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 };
    List<int> m_columnO = new List<int>() { 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75 };

    private void Start()
    {
        for (int i = 0; i < 75; i++)
        {
            m_allnumbers.Add(i + 1);
        }

        for (int i = 0; i < 25; i++)
        {
            GameObject go = Instantiate(m_cellPrefab, m_cardPanel);
            Cell newCell = go.GetComponent<Cell>();
            m_cardCells.Add(newCell);
            int num = default;
            if (i < 5)
            {
                num = GetRandomNamber(m_columnB);
            }
            else if (i < 10)
            {
                num = GetRandomNamber(m_columnI);
            }
            else if (i < 15)
            {
                num = GetRandomNamber(m_columnN);
            }
            else if (i < 20)
            {
                num = GetRandomNamber(m_columnG);
            }
            else if (i < 25)
            {
                num = GetRandomNamber(m_columnO);
            }
            newCell.Number = num;
            go.GetComponentInChildren<Text>().text = num.ToString();
        }
    }

    public void StartRoulette()
    {
        GameObject go = Instantiate(m_cellPrefab, m_numberPanel);
        int num = GetRandomNamber(m_allnumbers);
        go.GetComponentInChildren<Text>().text = num.ToString();
        //チェック
        foreach (var cell in m_cardCells)
        {
            if (cell.Number == num)
            {
                cell.Checked = true;
                cell.gameObject.GetComponent<Image>().color = Color.red;
            }
        }
    }

    int GetRandomNamber(List<int> nambers)
    {
        int ran = Random.Range(0, nambers.Count);
        int num = nambers[ran];
        nambers.RemoveAt(ran);
        return num;
    }
}
