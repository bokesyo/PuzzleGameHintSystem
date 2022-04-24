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
        //������ ���ñ���
        clicklog = new DataTable("Sheet1");
        //������
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
        //�ж����ݱ����Ƿ��������
        if (mSheet.Rows.Count < 1)
            return;

        //��ȡ���ݱ�����������
        int rowCount = mSheet.Rows.Count;
        int colCount = mSheet.Columns.Count;

        //����һ��StringBuilder�洢����
        StringBuilder stringBuilder = new StringBuilder();

        //��ȡ����
        for (int i = 0; i < mSheet.Columns.Count; i++)
        {
            stringBuilder.Append(mSheet.Columns[i].ColumnName + ",");
        }
        stringBuilder.Append("\r\n");
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                //ʹ��","�ָ�ÿһ����ֵ
                stringBuilder.Append(mSheet.Rows[i][j] + ",");
            }
            //ʹ�û��з��ָ�ÿһ��
            stringBuilder.Append("\r\n");
        }

        //д���ļ�
        using (FileStream fileStream = new FileStream(CSVPath, FileMode.Create, FileAccess.Write))
        {
            using (TextWriter textWriter = new StreamWriter(fileStream, Encoding.UTF8))
            {
                textWriter.Write(stringBuilder.ToString());
                Debug.Log("д��");
            }
        }
    }


}
