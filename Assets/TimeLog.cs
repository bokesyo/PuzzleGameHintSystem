using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Data;


public class TimeLog : MonoBehaviour
{
    private DataTable clicklog;

    void Start()
    {
        //创建表 设置表名
        clicklog = new DataTable("Sheet1");
        //创建列
        clicklog.Columns.Add("Click Time");
    }

    //private void Update()
    //{
    //    if (DateTime.Now.Second % 2 == 0)
    //    {
    //        StruggleTimeLog(DateTime.Now.ToString());
    //        Debug.Log(DateTime.Now.ToString());
    //    }
    //}

    public void StruggleTimeLog(string clicktime)
    {
        //MakeFile(header, clicklog, "ClickLog");
        DataRow dr = clicklog.NewRow();
        dr["Click Time"] = clicktime;
        clicklog.Rows.Add(dr);
        string filePath = Application.streamingAssetsPath + "\\ClickTime.csv";

        SaveCSV(filePath, clicklog);
    }

    public void SaveCSV(string CSVPath, DataTable mSheet)
    {
        //判断数据表内是否存在数据
        if (mSheet.Rows.Count < 1)
            return;

        //读取数据表行数和列数
        int rowCount = mSheet.Rows.Count;
        int colCount = mSheet.Columns.Count;

        //创建一个StringBuilder存储数据
        StringBuilder stringBuilder = new StringBuilder();

        //读取数据
        for (int i = 0; i < mSheet.Columns.Count; i++)
        {
            stringBuilder.Append(mSheet.Columns[i].ColumnName + ",");
        }
        stringBuilder.Append("\r\n");
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                //使用","分割每一个数值
                stringBuilder.Append(mSheet.Rows[i][j] + ",");
            }
            //使用换行符分割每一行
            stringBuilder.Append("\r\n");
        }

        //写入文件
        using (FileStream fileStream = new FileStream(CSVPath, FileMode.Create, FileAccess.Write))
        {
            using (TextWriter textWriter = new StreamWriter(fileStream, Encoding.UTF8))
            {
                textWriter.Write(stringBuilder.ToString());
                Debug.Log("写入");
            }
        }
    }


}
