using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour {
    public int status;
    public Color stdColor = Color.gray;
    public Color selectableColor;
    public Color flagColor = Color.blue;
    public Color openedColor = Color.white;
    public GameObject flag;

    private bool isOpened;
    private bool isMine;
    private bool isFlag;
    private GameObject currFlag;
    private Color color;
    private TextMesh text;
    private new Renderer renderer;
    public int x; ///Плохо называть переменную таким именем 
    public int y;
    void Start()
    { 
        isOpened = false;
        isMine = false;
        isFlag = false;
        renderer = this.GetComponent<Renderer>();
        renderer.material.color = stdColor;
        color = stdColor;
        text = transform.GetChild(0).GetComponent<TextMesh>();
    }
    /// <summary>
    /// True, если была открыта бомба
    /// </summary>
    /// <returns></returns>
    private bool OpenCellsAround()
    {
        bool result = false;
        int counter = 0;
        CellBehaviour[,] field = Generator.Instance.field;
        //Проверка на то, что нужно ли открывать вокруг клетки
        for (int i = -1; i < 2; i++)
        {
            if (x + i < 0 || x + i > field.GetLength(0) - 1)
                continue;
            for (int j = -1; j < 2; j++)    
            {
                if (y + j < 0 || y + j > field.GetLength(1) - 1)
                    continue;
                if (field[x + i, y + j].isFlag)
                    counter++;
            }
        }
        if (counter >= status)
        {
            for (int i = -1; i < 2; i++)
            {
                if (x + i < 0 || x + i > field.GetLength(0) - 1)
                    continue;
                for (int j = -1; j < 2; j++)
                {
                    if (y + j < 0 || y + j > field.GetLength(1) - 1)
                        continue;
                    if (field[x + i, y + j].OpenCell() && !field[x + i, y + j].isFlag)
                        return true;
                }
            }
        }
        return false;
    }
    public void OnTouch()
    {
        if (CheckMines())
            Detonate();
            
    }
    private void Detonate()
    {

    }
    private bool CheckMines()
    { 
        if (isOpened)
        {
            return OpenCellsAround();
        }
        else
        {
            return OpenCell();
        }
    }
    private bool OpenCell()
    {
        UpdateColor();
        if(!isMine)
        {
            if(status == 0)
            {
                OpenWhiteCells(x, y, Generator.Instance.field);
            }
            return false;
        }
        return true;
    }
    public void SetFlag()
    {
        if (isOpened || Generator.FlagsLeft() == 0)
            return;
        if(!isFlag)
        {
            renderer.material.color = Color.blue;
            isFlag = true;
            color = flagColor;
            PutFlag();
            Generator.IncreaseFlags();
        }
        else
        {
            renderer.material.color = stdColor;
            isFlag = false;
            RemoveFlag();
            color = stdColor;
            Generator.DecreaseFlags();
        }
    }

    /// <summary>
    /// Открывает клетки. Возвращает true, если была бомба.
    /// </summary>
    /// <param name="x">Строка клетки</param>
    /// <param name="y">Столбец клетки</param>
    /// <param name="cells">Массив клеток</param>
    /// <returns></returns>
    private static void OpenWhiteCells(int x, int y, CellBehaviour[,] cells)
    {
        CellBehaviour current = cells[x, y];
        current.UpdateColor();
        for (int i = -1; i < 2; i++)
        {
            if (x + i < 0 || x + i > cells.GetLength(0) - 1)
                continue;
            for (int j = -1; j < 2; j++)
            {
                if (y + j < 0 || y + j > cells.GetLength(1) - 1)
                    continue;
                current = cells[x + i, y + j];
                if (!current.isOpened)
                {
                    if (current.status == 0)
                    {
                        OpenWhiteCells(x + i, y + j, cells);
                    }
                    else
                    {
                        current.OpenCell(); 
                    }
                }
            }
        }
    }
    public void Select()
    {
        renderer.material.color = selectableColor; 
    }
    public void Unselect()
    {
        renderer.material.color = color;
    }
    public void UpdateColor() //Нужно разделить
    {
        if (!isFlag)
        {
            switch (status)
            {
                case 0:
                    color = Color.white;
                    break;
                case 1:
                    color = Color.cyan;
                    break;
                case 2:
                    color = Color.green;
                    break;
                case 3:
                    color = Color.yellow;
                    break;
                case 4:
                    color = new Color(1F, 0.5F, 0F, 1);
                    break;
                case 5:
                    color = Color.red;
                    break;
                case 6:
                    color = new Color(1F, (float)(36.0 / 255), (float)(175.0 / 255), 1);
                    break;
                case 7:
                    color = new Color((float)(138.0 / 255), 0, (float)(87.0 / 255));
                    break;
                case 8:
                    color = new Color((float)(153.0 / 255), (float)(89.0 / 255), (float)(46.0 / 255));
                    break;
                case -1:
                    color = Color.black;
                    break;
            }
            //renderer.material.color = color;
            this.GetComponent<Renderer>().material.color = color;
            //text.color = color;
            if (status!= -1 && status != 0)
                text.text = status.ToString();
            isOpened = true;
        }
    }
    public void SetIndex(int i, int j)
    {
        x = i;
        y = j;
    }
    void PutFlag()
    {
        currFlag = Instantiate(this.flag, transform, false) as GameObject;
    }
    void RemoveFlag()
    {
        Destroy(currFlag);
    }
    public bool IsMine()
    {
        return isMine;
    }
    public void SetMine()
    {
        this.status = -1;
        isMine = true;
    }
}
