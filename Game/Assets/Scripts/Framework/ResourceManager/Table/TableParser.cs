using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loxodon.Framework.Execution;

public class TxtParser
{
    private const char m_CharComma = ',';
    private const string m_StringComma = ",";
    private const char m_CharQuotation = '"';

    /// <summary>
    /// 读取单个表格
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="tableContent"></param>
    /// <returns></returns>
    public static TableResult Read(string tableName, string tableContent)
    {
        try
        {
            tableContent = tableContent.Replace("\r", string.Empty);
            List<string> listContent = tableContent.Split('\n').ToList();
            listContent.RemoveAll(string.IsNullOrWhiteSpace);
            if (listContent.Count <= 0)
            {
                return null;
            }
            var dicCharContent = new Dictionary<int, List<string>>();
            var dic2 = new Dictionary<int, Dictionary<int, string>>();
            int lineLength = 0;
            for (int i = 0; i < listContent.Count; i++)
            {
                var tempContent = listContent[i];
                if (tempContent.Last() != m_CharComma)
                {
                    tempContent = tempContent.Append(m_StringComma);
                }
                var listTarget = new List<string>();
                var dicTarget = new Dictionary<int, string>();
                switch (i)
                {
                    case 0:
                    case 1:
                    case 2:
                        {
                            listTarget = tempContent.Split(m_CharComma).ToList();
                            listTarget.RemoveAll(string.IsNullOrWhiteSpace);
                            if (lineLength == 0)
                            {
                                lineLength = listTarget.Count;
                            }
                        }
                        break;
                    default:
                        {
                            var charArray = tempContent.ToList();
                            int yinHaoCount = 0;
                            var listLineChar = new List<char>();

                            for (int j = 0; j < charArray.Count; j++)
                            {
                                if (listTarget.Count >= lineLength)
                                {
                                    break;
                                }
                                var tempChar = charArray[j];
                                switch (tempChar)
                                {
                                    case m_CharComma:
                                        {
                                            switch (yinHaoCount)
                                            {
                                                case 0:
                                                    {
                                                        listTarget.Add(new StringBuilder().Append(listLineChar.ToArray()).ToString());
                                                        listLineChar.Clear();
                                                    }
                                                    break;
                                                case 1:
                                                case 2:
                                                    {
                                                        listLineChar.Add(tempChar);
                                                    }
                                                    break;
                                                default:
                                                    {
                                                        yinHaoCount = 0;
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                    case m_CharQuotation:
                                        {
                                            yinHaoCount++;
                                            if (yinHaoCount == 2)
                                            {
                                                listTarget.Add(new StringBuilder().Append(listLineChar.ToArray()).ToString());
                                                listLineChar.Clear();
                                                yinHaoCount = 3;
                                            }
                                        }
                                        break;
                                    default:
                                        {
                                            listLineChar.Add(tempChar);
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                }

                for (int k = 0; k < listTarget.Count; k++)
                {
                    dicTarget.Add(k, listTarget[k]);
                }
                dicCharContent[i] = listTarget;
                dic2[i] = dicTarget;
            }
            return new TableResult(tableName, dicCharContent, dic2);
        }
        catch (Exception e)
        {
            if (Executors.IsMainThread)
            {
                throw;
            }

            Executors.RunOnMainThread(() => throw e);
            return null;
        }
    }
}

/// <summary>
/// 读单个表的结果类
/// </summary>
public class TableResult
{
    public string TableName { get; }
    public Dictionary<int, List<string>> DicTableContent { get; }
    public Dictionary<int, Dictionary<int, string>> DicTable { get; }

    public TableResult(string tableName, Dictionary<int, List<string>> tableDic, Dictionary<int, Dictionary<int, string>> dic)
    {
        TableName = tableName;
        DicTableContent = tableDic;
        DicTable = dic;
    }
}