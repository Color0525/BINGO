using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] GameObject m_cellPrefab = default;
    [SerializeField] Transform m_cellPanel = default;

    List<int> m_columnB = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
    List<int> m_columnI = new List<int>() { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
    List<int> m_columnN = new List<int>() { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45 };
    List<int> m_columnG = new List<int>() { 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 };
    List<int> m_columnO = new List<int>() { 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75 };

    private void Start()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject go = Instantiate(m_cellPrefab, m_cellPanel);
            int num = default;
            if (i < 5)
            {
                int ran = Random.Range(0, m_columnB.Count);
                num = m_columnB[ran];
                m_columnB.RemoveAt(ran);
            }
            else if (i < 10)
            {
                int ran = Random.Range(0, m_columnI.Count);
                num = m_columnI[ran];
                m_columnI.RemoveAt(ran);
            }
            else if (i < 15)
            {
                int ran = Random.Range(0, m_columnN.Count);
                num = m_columnN[ran];
                m_columnN.RemoveAt(ran);
            }
            else if (i < 20)
            {
                int ran = Random.Range(0, m_columnG.Count);
                num = m_columnG[ran];
                m_columnG.RemoveAt(ran);
            }
            else if (i < 25)
            {
                int ran = Random.Range(0, m_columnO.Count);
                num = m_columnO[ran];
                m_columnO.RemoveAt(ran);
            }
            go.GetComponentInChildren<Text>().text = num.ToString();
        }
    }
}
