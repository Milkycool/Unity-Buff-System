using NoSLoofah.BuffSystem.Dependence;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace NoSLoofah.BuffSystem
{
    /// <summary>
    /// 位Tag管理器，用于判断Tag之间的互斥关系
    /// </summary>
    public class BitBuffTagManager : BuffTagManager
    {
        private static string dataPath => Manager.BuffManager.SO_PATH + "/BuffTagData.asset";

        private BitBuffTagData tagData;
        private void Awake()
        {
            tagData = AssetDatabase.LoadAssetAtPath<BitBuffTagData>(dataPath);
            if (tagData == null) Debug.LogError(dataPath + "：Tag数据丢失");
        }
        public override bool IsTagRemoveOther(BuffTag tag, BuffTag other)
        {
            if (tag == 0) return false;
            if (tag < 0) throw new System.Exception("使用了负标签");
            int index = BitBuffTagData.GetIndex(tag);
            return ((int)tagData.RemovedTags[index] & (int)other) > 0;
        }

        public override bool IsTagCanAddWhenHaveOther(BuffTag tag, BuffTag other)
        {            
            if (tag == 0) return false;
            if (tag < 0) throw new System.Exception("使用了负标签");
            int index = BitBuffTagData.GetIndex(tag);
            return ((int)tagData.BlockTags[index] & (int)other) > 0;
        }
    }
}