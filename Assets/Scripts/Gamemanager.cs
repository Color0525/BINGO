using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] GameObject m_cellPrefab = default;
    [SerializeField] Transform m_cardPanel = default;
    [SerializeField] Transform m_numberPanel = default;
    [SerializeField] Text m_BINGOCountText = default;
    [SerializeField] Text m_RouletteText = default;

    int m_BINGOCount = default;
    int m_RouletteCount = default;
    Cell[,] m_cardCells = new Cell[5, 5];
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

        //カード生成
        for (int i = 0; i < 5; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                GameObject go = Instantiate(m_cellPrefab, m_cardPanel);
                Cell newCell = go.GetComponent<Cell>();
                m_cardCells[i, k] = newCell;
                if (i == 2 && k == 2)
                {
                    newCell.Number = 0;
                    go.GetComponentInChildren<Text>().text = "F";
                    newCell.Checked = true;
                    go.GetComponent<Image>().color = Color.red;
                    continue;
                }
                int num = default;
                if (i == 0) num = GetRandomNamber(m_columnB);
                else if (i == 1) num = GetRandomNamber(m_columnI);
                else if (i == 2) num = GetRandomNamber(m_columnN);
                else if (i == 3) num = GetRandomNamber(m_columnG);
                else if (i == 4) num = GetRandomNamber(m_columnO);
                newCell.Number = num;
                go.GetComponentInChildren<Text>().text = num.ToString();
            }
        }
    }

    public void StartRoulette()
    {
        m_RouletteCount++;
        m_RouletteText.text = m_RouletteCount.ToString();
        
        //生成
        GameObject go = Instantiate(m_cellPrefab, m_numberPanel);
        int num = GetRandomNamber(m_allnumbers);
        go.GetComponentInChildren<Text>().text = num.ToString();
        //数字チェック
        for (int i = 0; i < 5; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                if (m_cardCells[i, k].Number == num)
                {
                    m_cardCells[i, k].Checked = true;
                    m_cardCells[i, k].gameObject.GetComponent<Image>().color = Color.red;
                    //BINGOチェック
                    int[,] dirCounts = GetCheckNumber(i, k);
                    if (dirCounts[0, 0] + dirCounts[2, 2] == 4) m_BINGOCount++;
                    if (dirCounts[0, 1] + dirCounts[2, 1] == 4) m_BINGOCount++;
                    if (dirCounts[0, 2] + dirCounts[2, 0] == 4) m_BINGOCount++;
                    if (dirCounts[1, 0] + dirCounts[1, 2] == 4) m_BINGOCount++;
                    m_BINGOCountText.text = $"{m_BINGOCount} B I N G O";
                }
            }
        }
    }

    /// <summary>
    /// ランダムに取り出し、それをListから削除
    /// </summary>
    /// <param name="nambers"></param>
    /// <returns></returns>
    int GetRandomNamber(List<int> nambers)
    {
        int ran = Random.Range(0, nambers.Count);
        int num = nambers[ran];
        nambers.RemoveAt(ran);
        return num;
    }

    /// <summary>
    /// 8方向のチェック済み数を返す
    /// </summary>
    /// <param name="indexC"></param>
    /// <param name="indexR"></param>
    /// <returns></returns>
    int[,] GetCheckNumber(int indexC, int indexR)
    {
        int[,] dirCounts = new int[3, 3];
        for (int i = -1; i <= 1; i++)
        {
            for (int k = -1; k <= 1; k++)
            {
                int count = 0;
                int dirC = indexC;
                int dirR = indexR;
                for (int m = 0; m < 4; m++)
                {
                    dirC += i;
                    dirR += k;
                    if (0 > dirC || dirC >= 5 || 0 > dirR || dirR >= 5) continue;
                    if (m_cardCells[dirC, dirR] == m_cardCells[indexC, indexR]) break;
                    if (m_cardCells[dirC, dirR].Checked)
                    {
                        count++;
                    }
                }
                dirCounts[i + 1, k + 1] = count;
            }
        }
        return dirCounts;
    }
}