using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEditor;

public class TableScriptsEditor : EditorWindow
{
    private static readonly StringBuilder m_StringBuilder = new StringBuilder();
    public const string TablePath = "Data/TableFiles";
    public const string TableDataPath = "Scripts/Framework/ResourceManager/Table/TableData";
    public const string TableRepositoryPath = "Scripts/Framework/ResourceManager/Table/TableRepository";
    public const string TableHelperPath = "Scripts/Framework/ResourceManager/Table";
    public const string TableServiceBundlePath = "Scripts/ServicesBundle";
    public const string TableHelperName = "TableHelper";
    public static Dictionary<string, string> DicTableAsset = new Dictionary<string, string>();
    public static readonly Dictionary<string, Dictionary<int, List<string>>> DicAllTables = new Dictionary<string, Dictionary<int, List<string>>>();

    [MenuItem("Tools/Table工具/一键生成Table脚本")]
    public static void CreateTableScript()
    {
        try
        {
            DicTableAsset.Clear();
            EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成Table脚本", 0.0f);

            if (!EditorFileHelper.IsFolderExists(TablePath))
            {
                EditorFileHelper.CreateFolder(TablePath);
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在创建" + TablePath + "目录", 1.0f);
                UnityEngine.Debug.LogError("现已生成TableFile文件夹，请将表格文件放至" + TablePath + "目录下");
            }
            else
            {
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成Table脚本", 0.2f);
                var listTables = EditorFileHelper.GetAssetListWithSuffixByPath(TablePath);
                listTables.ForEach(tableAsset =>
                {
                    DicTableAsset[tableAsset] = EditorFileHelper.Read(TablePath + "/" + tableAsset);
                });
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成Table脚本", 0.3f);
                ReadTable();
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在重新生成" + TableDataPath + "目录", 0.4f);
                EditorFileHelper.DeleteFolder(TableDataPath);
                EditorFileHelper.CreateFolder(TableDataPath);
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在重新生成" + TableRepositoryPath + "目录", 0.4f);
                EditorFileHelper.DeleteFolder(TableRepositoryPath);
                EditorFileHelper.CreateFolder(TableRepositoryPath);
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成Table脚本", 0.4f);
                int tableCount = 0;
                float progress = 0.0f;
                foreach (var tableContentPair in DicAllTables)
                {
                    progress = 0.4f + (float)tableCount / DicAllTables.Count;
                    string scriptName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetTableNameNoSuffix(tableContentPair.Key).ToLower());
                    string tableDataName = GetTableDataName(scriptName);
                    EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成" + tableContentPair.Key + "的TableData脚本: " + scriptName, progress);
                    var tableDataPath = TableDataPath + "/" + scriptName + "TableData.cs";
                    EditorFileHelper.DeleteFile(tableDataPath);
                    EditorFileHelper.CreateFile(tableDataPath);
                    EditorFileHelper.Write(tableDataPath, WriteTableDataScript(tableContentPair.Key, tableDataName, tableContentPair.Value));

                    string tableRepositoryName = GetTableRepositoryName(scriptName);
                    EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成" + tableContentPair.Key + "的TableRepository脚本: " + scriptName, progress);
                    var tableRepositoryPath = TableRepositoryPath + "/" + scriptName + "TableRepository.cs";
                    EditorFileHelper.DeleteFile(tableRepositoryPath);
                    EditorFileHelper.CreateFile(tableRepositoryPath);
                    EditorFileHelper.Write(tableRepositoryPath, WriteTableRepositoryScript(tableContentPair.Key, tableRepositoryName, tableDataName, tableContentPair.Value));

                    tableCount++;
                }

                progress += 0.1f;
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableHelper脚本: ", progress);
                string tableHelperPath = TableHelperPath + "/" + TableHelperName + ".cs";
                EditorFileHelper.DeleteFile(tableHelperPath);
                EditorFileHelper.CreateFile(tableHelperPath);
                EditorFileHelper.Write(tableHelperPath, WriteTableHelperScript(DicTableAsset.Keys));

                EditorUtility.DisplayProgressBar("自动生成Table脚本", "即将完成", 0.9f);
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            EditorUtility.DisplayProgressBar("自动生成Table脚本", "完成", 1.0f);
            EditorUtility.ClearProgressBar();
            m_StringBuilder.Clear();
        }
    }

    [MenuItem("Tools/Table工具/生成TableData脚本")]
    public static void CreateTableData()
    {
        try
        {
            DicTableAsset.Clear();
            EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableData脚本", 0.0f);

            if (!EditorFileHelper.IsFolderExists(TablePath))
            {
                EditorFileHelper.CreateFolder(TablePath);
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在创建" + TablePath + "目录", 1.0f);
                UnityEngine.Debug.LogError("现已生成TableFile文件夹，请将表格文件放至" + TablePath + "目录下");
            }
            else
            {
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableData脚本", 0.2f);
                var listTables = EditorFileHelper.GetAssetListWithSuffixByPath(TablePath);
                listTables.ForEach(tableAsset =>
                {
                    DicTableAsset[tableAsset] = EditorFileHelper.Read(TablePath + "/" + tableAsset);
                });
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableData脚本", 0.3f);
                ReadTable();
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在重新生成" + TableDataPath + "目录", 0.4f);
                EditorFileHelper.DeleteFolder(TableDataPath);
                EditorFileHelper.CreateFolder(TableDataPath);
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableData脚本", 0.4f);
                int tableCount = 0;
                foreach (var tableContentPair in DicAllTables)
                {
                    string scriptName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetTableNameNoSuffix(tableContentPair.Key).ToLower());
                    string className = GetTableDataName(scriptName);
                    EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成" + tableContentPair.Key + "的TableData脚本: " + scriptName,
                        0.4f + (float)tableCount / DicAllTables.Count);
                    var scriptPath = TableDataPath + "/" + scriptName + "TableData.cs";
                    EditorFileHelper.DeleteFile(scriptPath);
                    EditorFileHelper.CreateFile(scriptPath);
                    EditorFileHelper.Write(scriptPath, WriteTableDataScript(tableContentPair.Key, className, tableContentPair.Value));
                    tableCount++;
                }
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "即将完成", 0.9f);
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            EditorUtility.DisplayProgressBar("自动生成Table脚本", "完成", 1.0f);
            EditorUtility.ClearProgressBar();
            m_StringBuilder.Clear();
        }
    }

    [MenuItem("Tools/Table工具/生成TableRepository脚本")]
    public static void CreateTableRepository()
    {
        try
        {
            DicTableAsset.Clear();
            EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableRepository脚本", 0.0f);

            if (!EditorFileHelper.IsFolderExists(TablePath))
            {
                EditorFileHelper.CreateFolder(TablePath);
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在创建" + TablePath + "目录", 1.0f);
                UnityEngine.Debug.LogError("现已生成TableFile文件夹，请将表格文件放至" + TablePath + "目录下");
            }
            else
            {
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableRepository脚本", 0.2f);
                var listTables = EditorFileHelper.GetAssetListWithSuffixByPath(TablePath);
                listTables.ForEach(tableAsset =>
                {
                    DicTableAsset[tableAsset] = EditorFileHelper.Read(TablePath + "/" + tableAsset);
                });
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableRepository脚本", 0.3f);
                ReadTable();
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在重新生成" + TableRepositoryPath + "目录", 0.4f);
                EditorFileHelper.DeleteFolder(TableRepositoryPath);
                EditorFileHelper.CreateFolder(TableRepositoryPath);
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableRepository脚本", 0.4f);
                int tableCount = 0;
                foreach (var tableContentPair in DicAllTables)
                {
                    string scriptName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetTableNameNoSuffix(tableContentPair.Key).ToLower());
                    string tableDataName = GetTableDataName(scriptName);
                    string className = GetTableRepositoryName(scriptName);
                    EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成" + tableContentPair.Key + "的TableRepository脚本: " + scriptName,
                        0.4f + (float)tableCount / DicAllTables.Count);
                    var scriptPath = TableRepositoryPath + "/" + scriptName + "TableRepository.cs";
                    EditorFileHelper.DeleteFile(scriptPath);
                    EditorFileHelper.CreateFile(scriptPath);
                    EditorFileHelper.Write(scriptPath, WriteTableRepositoryScript(tableContentPair.Key, className, tableDataName, tableContentPair.Value));
                    tableCount++;
                }
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "即将完成", 0.9f);
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            EditorUtility.DisplayProgressBar("自动生成Table脚本", "完成", 1.0f);
            EditorUtility.ClearProgressBar();
            m_StringBuilder.Clear();
        }
    }

    [MenuItem("Tools/Table工具/生成TableHelper脚本")]
    public static void CreateTableHelper()
    {
        try
        {
            DicTableAsset.Clear();
            EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableHelper脚本", 0.0f);

            if (!EditorFileHelper.IsFolderExists(TablePath))
            {
                EditorFileHelper.CreateFolder(TablePath);
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在创建" + TablePath + "目录", 1.0f);
                UnityEngine.Debug.LogError("现已生成TableFile文件夹，请将表格文件放至" + TablePath + "目录下");
            }
            else
            {
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableHelper脚本", 0.2f);
                var listTables = EditorFileHelper.GetAssetListWithSuffixByPath(TablePath);
                listTables.ForEach(tableAsset =>
                {
                    DicTableAsset[tableAsset] = EditorFileHelper.Read(TablePath + "/" + tableAsset);
                });
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableHelper脚本", 0.3f);
                ReadTable();
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "正在生成TableHelper脚本", 0.7f);
                string tableHelperPath = TableHelperPath + "/" + TableHelperName + ".cs";
                EditorFileHelper.DeleteFile(tableHelperPath);
                EditorFileHelper.CreateFile(tableHelperPath);
                EditorFileHelper.Write(tableHelperPath, WriteTableHelperScript(DicTableAsset.Keys));
                EditorUtility.DisplayProgressBar("自动生成Table脚本", "即将完成", 0.9f);
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            EditorUtility.DisplayProgressBar("自动生成Table脚本", "完成", 1.0f);
            EditorUtility.ClearProgressBar();
            m_StringBuilder.Clear();
        }
    }

    /// <summary>
    /// 开始读表 (同步),只在Editor中调用
    /// </summary>
    /// <returns></returns>
    private static void ReadTable()
    {
        if (DicTableAsset == null || DicTableAsset.Count <= 0)
        {
            return;
        }

        foreach (var tableAssetPair in DicTableAsset)
        {
            var result = TxtParser.Read(tableAssetPair.Key, tableAssetPair.Value);
            DicAllTables[tableAssetPair.Key] = result.DicTableContent;
        }
    }

    /// <summary>
    /// 写入TableData代码
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="className"></param>
    /// <param name="dic"></param>
    /// <returns></returns>
    private static string WriteTableDataScript(string tableName, string className, Dictionary<int, List<string>> dic)
    {
        m_StringBuilder.Clear();
        m_StringBuilder.AppendLine("using UnityEngine;");
        m_StringBuilder.AppendLine("using System.Collections.Generic;");
        m_StringBuilder.AppendLine();
        m_StringBuilder.AppendLine("public class " + className);
        m_StringBuilder.AppendLine("{");
        List<string> annotationList = new List<string>();
        List<string> variatyNameList = new List<string>();
        List<string> keywordList = new List<string>();
        foreach (var pair in dic)
        {
            switch (pair.Key)
            {
                case 0:
                    {
                        annotationList.AddRange(pair.Value);
                    }
                    continue;
                case 1:
                    {
                        variatyNameList.AddRange(pair.Value);
                    }
                    continue;
                case 2:
                    {
                        keywordList.AddRange(pair.Value);
                    }
                    continue;
            }

            break;
        }

        if (annotationList.Count != keywordList.Count)
        {
            throw new Exception(tableName + " 注释和类型长度不匹配!");
        }

        if (annotationList.Count == 0)
        {
            throw new Exception(tableName + " 是否是空表?");
        }

        for (int i = 0; i < keywordList.Count; i++)
        {
            var annotation = annotationList[i];
            var variatyName = variatyNameList[i];
            var keyword = keywordList[i];
            m_StringBuilder.AppendLine("    /// <summary>");
            m_StringBuilder.AppendLine("    /// " + annotation);
            m_StringBuilder.AppendLine("    /// <summary>");
            switch (keyword)
            {
                case "string":
                    {
                        m_StringBuilder.AppendLine("    public " + keyword + " " + variatyName + " { get; }  = string.Empty;");
                    }
                    break;
                case "sbyte":
                case "byte":
                case "short":
                case "ushort":
                case "int":
                case "uint":
                case "long":
                case "ulong":
                    {
                        m_StringBuilder.AppendLine("    public " + keyword + " " + variatyName + " { get; } = 0;");
                    }
                    break;
                case "float":
                case "double":
                    {
                        m_StringBuilder.AppendLine("    public " + keyword + " " + variatyName + " { get; } = 0.0f;");
                    }
                    break;
                case "List<string>":
                case "List<sbyte>":
                case "List<byte>":
                case "List<short>":
                case "List<ushort>":
                case "List<int>":
                case "List<uint>":
                case "List<long>":
                case "List<ulong>":
                case "List<float>":
                case "List<double>":
                    {
                        m_StringBuilder.AppendLine("    public " + keyword + " " + variatyName + " { get; } = new " + keyword + "();");
                    }
                    break;
                case "Vector2":
                case "Vector3":
                case "Vector4":
                    {
                        m_StringBuilder.AppendLine("    public " + keyword + " " + variatyName + " { get; } = " + keyword + ".zero;");
                    }
                    break;
                default:
                    {
                        throw new Exception("不支持此类型" + keyword);
                    }
            }
            m_StringBuilder.AppendLine();
        }

        m_StringBuilder.Append("}");

        return m_StringBuilder.ToString();
    }

    /// <summary>
    /// 写入TableRepository代码
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="className"></param>
    /// <param name="tableDataName"></param>
    /// <param name="dic"></param>
    /// <returns></returns>
    private static string WriteTableRepositoryScript(string tableName, string className, string tableDataName, Dictionary<int, List<string>> dic)
    {
        m_StringBuilder.Clear();
        m_StringBuilder.AppendLine("using UnityEngine;");
        m_StringBuilder.AppendLine("using UnityEngine.AddressableAssets;");
        m_StringBuilder.AppendLine("using UnityEngine.ResourceManagement.AsyncOperations;");
        m_StringBuilder.AppendLine();
        string firstKeyword = "int";
        foreach (var pair in dic)
        {
            if (pair.Key == 2 && pair.Value != null && pair.Value.Count > 0)
            {
                firstKeyword = pair.Value[0];
                break;
            }
        }
        m_StringBuilder.AppendLine("public class " + className + " : TableRepositoryBase<" + firstKeyword + " ," + tableDataName + ">");
        m_StringBuilder.AppendLine("{");
        m_StringBuilder.AppendLine("    public override AsyncOperationHandle<TextAsset> LoadTable(string tableKey)");
        m_StringBuilder.AppendLine("    {");
        m_StringBuilder.AppendLine("            return Addressables.LoadAssetAsync<TextAsset>(tableKey);");
        m_StringBuilder.AppendLine("    }");
        m_StringBuilder.Append("}");

        return m_StringBuilder.ToString();
    }

    /// <summary>
    /// 写入TableHelper代码
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    private static string WriteTableHelperScript(Dictionary<string, string>.KeyCollection keys)
    {
        m_StringBuilder.Clear();
        m_StringBuilder.AppendLine("using System.Collections.Generic;");
        m_StringBuilder.AppendLine("using System.Threading.Tasks;");
        m_StringBuilder.AppendLine("using Loxodon.Framework.Execution;");
        m_StringBuilder.AppendLine("using UnityEngine;");
        m_StringBuilder.AppendLine("using UnityEngine.AddressableAssets;");
        m_StringBuilder.AppendLine("using UnityEngine.ResourceManagement.AsyncOperations;");
        m_StringBuilder.AppendLine();
        m_StringBuilder.AppendLine("public class " + TableHelperName);
        m_StringBuilder.AppendLine("{");
        m_StringBuilder.AppendLine("    private static Dictionary<string, AsyncOperationHandle<TextAsset>> m_DicAsyncOperationHandles = new Dictionary<string, AsyncOperationHandle<TextAsset>>();");
        m_StringBuilder.AppendLine("    private static int m_ParserCount;");
        m_StringBuilder.AppendLine("    public static readonly Dictionary<string, Dictionary<int, List<string>>> DicAllTables = new Dictionary<string, Dictionary<int, List<string>>>();");
        m_StringBuilder.AppendLine("    public static readonly Dictionary<string, Dictionary<int, Dictionary<int, string>>> DicAll = new Dictionary<string, Dictionary<int, Dictionary<int, string>>>();");
        m_StringBuilder.AppendLine();
        m_StringBuilder.AppendLine("    /// <summary>");
        m_StringBuilder.AppendLine("    /// 开始读表 (异步) 开发及上线时调用");
        m_StringBuilder.AppendLine("    /// </summary>");
        m_StringBuilder.AppendLine("    /// <returns></returns>");
        m_StringBuilder.AppendLine("    public static async Task ReadTable()");
        m_StringBuilder.AppendLine("    {");
        m_StringBuilder.AppendLine("        m_ParserCount = 0;");
        m_StringBuilder.AppendLine("        if (m_DicAsyncOperationHandles == null)");
        m_StringBuilder.AppendLine("        {");
        m_StringBuilder.AppendLine("            m_DicAsyncOperationHandles = new Dictionary<string, AsyncOperationHandle<TextAsset>>();");
        m_StringBuilder.AppendLine("        }");
        foreach (var primaryKey in keys)
        {
            var tableKey = "\"" + GetTableNameNoSuffix(primaryKey) + "\"";
            var content = "        m_DicAsyncOperationHandles.Add(" + tableKey + ", Addressables.LoadAssetAsync<TextAsset>(" + tableKey + "));";
            m_StringBuilder.AppendLine(content);
        }
        m_StringBuilder.AppendLine("        await WaitTask();");
        m_StringBuilder.AppendLine("    }");
        m_StringBuilder.AppendLine();
        m_StringBuilder.AppendLine("    /// <summary>");
        m_StringBuilder.AppendLine("    /// 等待读表结果");
        m_StringBuilder.AppendLine("    /// </summary>");
        m_StringBuilder.AppendLine("    /// <returns></returns>");
        m_StringBuilder.AppendLine("    private static async Task WaitTask()");
        m_StringBuilder.AppendLine("    {");
        m_StringBuilder.AppendLine("        foreach (var pair in m_DicAsyncOperationHandles)");
        m_StringBuilder.AppendLine("        {");
        m_StringBuilder.AppendLine("            await pair.Value.Task;");
        m_StringBuilder.AppendLine("            if (pair.Value.Status == AsyncOperationStatus.Succeeded)");
        m_StringBuilder.AppendLine("            {");
        m_StringBuilder.AppendLine("                string content = pair.Value.Result.text;");
        m_StringBuilder.AppendLine("                Executors.RunAsync(() => TxtParser.Read(pair.Key, content)).Callbackable().OnCallback(tableResult =>");
        m_StringBuilder.AppendLine("                {");
        m_StringBuilder.AppendLine("                    if (tableResult.Exception == null)");
        m_StringBuilder.AppendLine("                    {");
        m_StringBuilder.AppendLine("                        DicAllTables[tableResult.Result.TableName] = tableResult.Result.DicTableContent;");
        m_StringBuilder.AppendLine("                        DicAll[tableResult.Result.TableName] = tableResult.Result.DicTable;");
        m_StringBuilder.AppendLine("                    }");
        m_StringBuilder.AppendLine("                    m_ParserCount++;");
        m_StringBuilder.AppendLine("                    if (m_ParserCount == m_DicAsyncOperationHandles.Count)");
        m_StringBuilder.AppendLine("                    {");
        m_StringBuilder.AppendLine("                        foreach (var handle in m_DicAsyncOperationHandles.Values)");
        m_StringBuilder.AppendLine("                        {");
        m_StringBuilder.AppendLine("                            Addressables.Release(handle);");
        m_StringBuilder.AppendLine("                        }");
        m_StringBuilder.AppendLine("                        m_DicAsyncOperationHandles.Clear();");
        m_StringBuilder.AppendLine("                        m_DicAsyncOperationHandles = null;");
        m_StringBuilder.AppendLine("                    }");
        m_StringBuilder.AppendLine("                });");
        m_StringBuilder.AppendLine("            }");
        m_StringBuilder.AppendLine("        }");
        m_StringBuilder.AppendLine("    }");
        m_StringBuilder.Append("}");
        return m_StringBuilder.ToString();
    }

    /// <summary>
    /// 获取去除后缀名的表名
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    private static string GetTableNameNoSuffix(string tableName)
    {
        return tableName.Remove(tableName.LastIndexOf('.'));
    }

    /// <summary>
    /// 获取TableData名
    /// </summary>
    /// <param name="scriptName"></param>
    /// <returns></returns>
    private static string GetTableDataName(string scriptName)
    {
        return scriptName + "TableData";
    }

    /// <summary>
    /// 获取TableRepository名
    /// </summary>
    /// <param name="scriptName"></param>
    /// <returns></returns>
    private static string GetTableRepositoryName(string scriptName)
    {
        return scriptName + "TableRepository";
    }
}