using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using System.IO;

public class AddressablesAssetEditor : EditorWindow
{
    #region Declartion

    public const string Confirm = "确定";
    public const string PlayerDataGroupName = "Built In Data";
    public const string DefaultLocalGroupName = "Default Local Group";

    public const string TablePath = TableScriptsEditor.TablePath;
    public const string TableAssetsPath = "Assets/" + TablePath;
    public const string TableFilter = "t:TextAsset";
    public const string TableGroupName = "Table Group";

    public const string PrefabPath = "Prefabs";
    public const string PrefabAssetsPath = "Assets/" + PrefabPath;
    public const string PrefabFilter = "t:Prefab";
    public const string PrefabGroupName = "Prefab Group";

    public const string ScenePath = "Scenes";
    public const string SceneAssetsPath = "Assets/" + ScenePath;
    public const string SceneFilter = "t:Scene";
    public const string SceneGroupName = "Scene Group";

    public const string SpritePath = "Arts/Sprites";
    public const string SpriteAssetsPath = "Assets/" + SpritePath;
    public const string SpriteFilter = "t:Sprite";
    public const string SpriteGroupName = "Sprite Group";

    public const string SpriteAtlasPath = "Arts/SpriteAtlases";
    public const string SpriteAtlasAssetsPath = "Assets/" + SpriteAtlasPath;
    public const string SpriteAtlasFilter = "t:SpriteAtlas";
    public const string SpriteAtlasGroupName = "SpriteAtlas Group";

    public const string SpriteSheetPath = "Arts/SpriteSheets";
    public const string SpriteSheetAssetsPath = "Assets/" + SpriteSheetPath;
    public const string SpriteSheetFilter = "t:Sprite";
    public const string SpriteSheetGroupName = "SpriteSheet Group";

    public const string ShaderPath = "Arts/Shaders";
    public const string ShaderAssetsPath = "Assets/" + ShaderPath;
    public const string ShaderFilter = "t:Shader";
    public const string ShaderGroupName = "Shader Group";

    public const string VideoPath = "Arts/Videos";
    public const string VideoAssetsPath = "Assets/" + VideoPath;
    public const string VideoFilter = "t:VideoClip";
    public const string VideoGroupName = "Video Group";

    private static AddressableAssetSettings m_AddressableAssetSettings => AddressableAssetSettingsDefaultObject.GetSettings(true);

    #endregion

    [MenuItem("Tools/Addressables工具/一键部署所有资源")]
    public static void MoveAllEntriesToSpecificGroup()
    {
        try
        {
            EditorUtility.DisplayProgressBar("部署Addressables资源", "正在准备部署", 0.0f);
            RemoveAllGroups();

            EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署Table资源", 0.05f);
            MoveTableEntriesToTableGroup(false, false);

            EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署Prefab资源", 0.20f);
            MovePrefabEntriesToPrefabGroup(false, false);

            EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署Scene资源", 0.40f);
            MoveSceneEntriesToSceneGroup(false, false);

            EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署Sprite资源", 0.60f);
            MoveSpriteEntriesToSpriteGroup(false, false);

            EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署SpriteAtlas资源", 0.60f);
            MoveSpriteAtlasEntriesToSpriteAtlasGroup(false, false);

            EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署SpriteSheet资源", 0.60f);
            MoveSpriteSheetEntriesToSpriteSheetGroup(false, false);

            EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署Shader资源", 0.80f);
            MoveShaderEntriesToShaderGroup(false, false);

            EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署Video资源", 0.80f);
            MoveVideoEntriesToVideoGroup(false, false);

            EditorUtility.DisplayProgressBar("部署Addressables资源", "部署资源即将完成", 1.0f);
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署资源发生异常: " + ex, Confirm);
            throw;
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        EditorUtility.DisplayDialog("部署Addressables资源", "部署资源已全部完成", Confirm);
    }

    [MenuItem("Tools/Addressables工具/部署Table资源")]
    public static void MoveTableEntriesToTableGroup()
    {
        MoveTableEntriesToTableGroup(true, true);
    }

    public static void MoveTableEntriesToTableGroup(bool isShowDialog, bool isShowProgressBar)
    {
        try
        {
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署表格资源", 0.0f);
            }

            MoveEntriesToGroup(TablePath, TableGroupName, TableAssetsPath, TableFilter, BundledAssetGroupSchema.BundleCompressionMode.LZ4, true);
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "部署表格资源完成", 1.0f);
            }
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署表格资源发生异常: " + ex, Confirm);
            throw;
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        if (isShowDialog)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署表格资源完成", Confirm);
        }
    }

    [MenuItem("Tools/Addressables工具/部署Prefab资源")]
    public static void MovePrefabEntriesToPrefabGroup()
    {
        MovePrefabEntriesToPrefabGroup(true, true);
    }

    public static void MovePrefabEntriesToPrefabGroup(bool isShowDialog, bool isShowProgressBar)
    {
        try
        {
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署Prefab资源", 0.0f);
            }

            MoveEntriesToGroup(PrefabPath, PrefabGroupName, PrefabAssetsPath, PrefabFilter);
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "部署Prefab资源完成", 1.0f);
            }
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署Prefab资源发生异常: " + ex, Confirm);
            throw;
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        if (isShowDialog)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署Prefab资源完成", Confirm);
        }
    }

    [MenuItem("Tools/Addressables工具/部署Scene资源")]
    public static void MoveSceneEntriesToSceneGroup()
    {
        MoveSceneEntriesToSceneGroup(true, true);
    }

    public static void MoveSceneEntriesToSceneGroup(bool isShowDialog, bool isShowProgressBar)
    {
        try
        {
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署Scene资源", 0.0f);
            }

            MoveEntriesToGroup(ScenePath, SceneGroupName, SceneAssetsPath, SceneFilter);
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "部署Scene资源完成", 1.0f);
            }
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署Scene资源发生异常: " + ex, Confirm);
            throw;
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        if (isShowDialog)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署Scene资源完成", Confirm);
        }
    }

    [MenuItem("Tools/Addressables工具/部署Sprite资源")]
    public static void MoveSpriteEntriesToSpriteGroup()
    {
        MoveSpriteEntriesToSpriteGroup(true, true);
    }

    public static void MoveSpriteEntriesToSpriteGroup(bool isShowDialog, bool isShowProgressBar)
    {
        try
        {
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署Sprite资源", 0.0f);
            }

            MoveEntriesToGroup(SpritePath, SpriteGroupName, SpriteAssetsPath, SpriteFilter);
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "部署Sprite资源完成", 1.0f);
            }
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署Sprite资源发生异常: " + ex, Confirm);
            throw;
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        if (isShowDialog)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署Sprite资源完成", Confirm);
        }
    }

    [MenuItem("Tools/Addressables工具/部署SpriteAtlas资源")]
    public static void MoveSpriteAtlasEntriesToSpriteAtlasGroup()
    {
        MoveSpriteAtlasEntriesToSpriteAtlasGroup(true, true);
    }

    public static void MoveSpriteAtlasEntriesToSpriteAtlasGroup(bool isShowDialog, bool isShowProgressBar)
    {
        try
        {
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署SpriteAtlas资源", 0.0f);
            }

            MoveEntriesToGroup(SpriteAtlasPath, SpriteAtlasGroupName, SpriteAtlasAssetsPath, SpriteAtlasFilter);
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "部署SpriteAtlas资源完成", 1.0f);
            }
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署SpriteAtlas资源发生异常: " + ex, Confirm);
            throw;
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        if (isShowDialog)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署SpriteAtlas资源完成", Confirm);
        }
    }

    [MenuItem("Tools/Addressables工具/部署SpriteSheet资源")]
    public static void MoveSpriteSheetEntriesToSpriteSheetGroup()
    {
        MoveSpriteSheetEntriesToSpriteSheetGroup(true, true);
    }

    public static void MoveSpriteSheetEntriesToSpriteSheetGroup(bool isShowDialog, bool isShowProgressBar)
    {
        try
        {
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署SpriteSheet资源", 0.0f);
            }

            MoveEntriesToGroup(SpriteSheetPath, SpriteSheetGroupName, SpriteSheetAssetsPath, SpriteSheetFilter);
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "部署SpriteSheet资源完成", 1.0f);
            }
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署SpriteSheet资源发生异常: " + ex, Confirm);
            throw;
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        if (isShowDialog)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署SpriteSheet资源完成", Confirm);
        }
    }

    [MenuItem("Tools/Addressables工具/部署Shader资源")]
    public static void MoveShaderEntriesToShaderGroup()
    {
        MoveShaderEntriesToShaderGroup(true, true);
    }

    public static void MoveShaderEntriesToShaderGroup(bool isShowDialog = true, bool isShowProgressBar = true)
    {
        try
        {
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署Shader资源", 0.0f);
            }

            MoveEntriesToGroup(ShaderPath, ShaderGroupName, ShaderAssetsPath, ShaderFilter);
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "部署Shader资源完成", 1.0f);
            }
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署Shader资源发生异常: " + ex, Confirm);
            throw;
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        if (isShowDialog)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署Shader资源完成", Confirm);
        }
    }

    [MenuItem("Tools/Addressables工具/部署Video资源")]
    public static void MoveVideoEntriesToVideoGroup()
    {
        MoveVideoEntriesToVideoGroup(true, true);
    }

    public static void MoveVideoEntriesToVideoGroup(bool isShowDialog = true, bool isShowProgressBar = true)
    {
        try
        {
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "正在部署Video资源", 0.0f);
            }

            MoveEntriesToGroup(VideoPath, VideoGroupName, VideoAssetsPath, VideoFilter, BundledAssetGroupSchema.BundleCompressionMode.Uncompressed);
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "部署Video资源完成", 1.0f);
            }
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署Video资源发生异常: " + ex, Confirm);
            throw;
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        if (isShowDialog)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "部署Video资源完成", Confirm);
        }
    }

    [MenuItem("Tools/Addressables工具/移除所有资源部署")]
    public static void RemoveEntriesAndGroup()
    {
        RemoveEntriesAndGroup(true, true);
    }

    public static void RemoveEntriesAndGroup(bool isShowDialog = true, bool isShowProgressBar = true)
    {
        try
        {
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "正在移除资源部署", 0.0f);
            }

            RemoveAllGroups();
            if (isShowProgressBar)
            {
                EditorUtility.DisplayProgressBar("部署Addressables资源", "移除资源部署完成", 1.0f);
            }
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "移除资源部署发生异常: " + ex, Confirm);
            throw;
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        if (isShowDialog)
        {
            EditorUtility.DisplayDialog("部署Addressables资源", "移除资源部署完成", Confirm);
        }
    }

    /// <summary>
    /// 部署指定资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="groupName"></param>
    /// <param name="assetPath"></param>
    /// <param name="filter"></param>
    /// <param name="isSetDefaultGroup"></param>
    /// <param name="bundleCompressionMode"></param>
    private static void MoveEntriesToGroup(string path, string groupName, string assetPath, string filter,
        BundledAssetGroupSchema.BundleCompressionMode bundleCompressionMode = BundledAssetGroupSchema.BundleCompressionMode.LZ4, bool isSetDefaultGroup = false)
    {
        try
        {
            RemoveGroup(groupName);
            var group = CreateGroup(groupName, isSetDefaultGroup, bundleCompressionMode);
            if (!EditorFileHelper.IsFolderExists(path))
            {
                EditorFileHelper.CreateFolder(path);
                EditorUtility.DisplayDialog("部署Addressables资源", "现已生成文件夹，请将对应资源放至" + path + "目录下", Confirm);
            }
            else
            {
                var guides = EditorFileHelper.GetAllGuidUnderFolderWithFilter(filter, assetPath);
                var addressableAssetEntries = new List<AddressableAssetEntry>();
                guides.ForEach(guid =>
                {
                    var entry = m_AddressableAssetSettings.CreateOrMoveEntry(guid, group, true);
                    if (entry != null)
                    {
                        addressableAssetEntries.Add(entry);
                    }
                });
                SimplifyAddresses(addressableAssetEntries);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 创建分组
    /// </summary>
    /// <param name="groupName"></param>
    /// <param name="isSetDefaultGroup"></param>
    /// <param name="bundleCompressionMode"></param>
    /// <returns></returns>
    private static AddressableAssetGroup CreateGroup(string groupName, bool isSetDefaultGroup, BundledAssetGroupSchema.BundleCompressionMode bundleCompressionMode)
    {
        var group = m_AddressableAssetSettings.CreateGroup(groupName, isSetDefaultGroup, false, false, null, typeof(ContentUpdateGroupSchema), typeof(BundledAssetGroupSchema));
        var schema = group.GetSchema<BundledAssetGroupSchema>();
        schema.BuildPath.SetVariableByName(m_AddressableAssetSettings, AddressableAssetSettings.kLocalBuildPath);
        schema.LoadPath.SetVariableByName(m_AddressableAssetSettings, AddressableAssetSettings.kLocalLoadPath);
        schema.BundleMode = BundledAssetGroupSchema.BundlePackingMode.PackTogether;
        schema.Compression = bundleCompressionMode;
        m_AddressableAssetSettings.SetDirty(AddressableAssetSettings.ModificationEvent.GroupAdded, null, false, true);
        return group;
    }

    /// <summary>
    /// 删除分组
    /// </summary>
    /// <param name="groupName"></param>
    /// <returns></returns>
    private static void RemoveGroup(string groupName)
    {
        var group = m_AddressableAssetSettings.FindGroup(groupName);
        if (group != null)
        {
            m_AddressableAssetSettings.RemoveGroup(group);
        }
    }

    /// <summary>
    /// 移除所有分组
    /// </summary>
    /// <param name="isIgnoreDefault">是否忽略AddressablesAssetSystem的默认分组</param>
    private static void RemoveAllGroups(bool isIgnoreDefault = true)
    {
        var groups = new List<AddressableAssetGroup>(m_AddressableAssetSettings.groups);
        for (int i = 0; i < groups.Count; i++)
        {
            var group = groups[i];
            if (group != null)
            {
                if (isIgnoreDefault)
                {
                    if (group.Name == PlayerDataGroupName)
                    {
                        continue;
                    }
                }
            }

            m_AddressableAssetSettings.RemoveGroup(group);
        }
    }

    /// <summary>
    /// 简化可寻址资源名称
    /// </summary>
    /// <param name="entries"></param>
    private static void SimplifyAddresses(IReadOnlyCollection<AddressableAssetEntry> entries)
    {
        var modifiedGroups = new HashSet<AddressableAssetGroup>();
        foreach (var entry in entries)
        {
            entry.SetAddress(Path.GetFileNameWithoutExtension(entry.address), false);
            modifiedGroups.Add(entry.parentGroup);
        }

        foreach (var group in modifiedGroups)
        {
            group.SetDirty(AddressableAssetSettings.ModificationEvent.EntryModified, entries, false, true);
        }

        m_AddressableAssetSettings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryModified, entries, true, false);
    }
}