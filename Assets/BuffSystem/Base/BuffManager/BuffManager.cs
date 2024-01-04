using NoSLoofah.BuffSystem.Dependence;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace NoSLoofah.BuffSystem.Manager
{
    /// <summary>
    /// Buff������������ģʽ��
    /// ����ͨ����Ż�ȡBuff����
    /// </summary>
    public class BuffManager : MonoSingleton<BuffManager>, IBuffManager
    {
        public static readonly string SO_PATH = "Assets/Data/BuffData";    //����Data��·��
        private BuffCollection collection;
        private IBuffTagManager tagManager;
        public bool IsWorking => collection != null;

        public IBuffTagManager TagManager => tagManager;

        protected override void Awake()
        {
            base.Awake();
            collection = null;
            string[] assetPaths = AssetDatabase.FindAssets("t:BuffCollection", new string[] { SO_PATH });
            if (assetPaths.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetPaths[0]);
                collection = AssetDatabase.LoadAssetAtPath<BuffCollection>(assetPath);
            }
        }
        public IBuff GetBuff(int id)
        {
            if (id < 0 || id >= collection.Size)
            {
                throw new System.Exception("ʹ�÷Ƿ���Buff id��" + id + " (��ǰBuff����Ϊ" + collection.Size + ")");
            }
            if (collection.buffList[id]==null) throw new System.Exception("���õ�BuffΪnull��id��"+id);
            return collection.buffList[id].Clone();
        }

        public void RegisterBuffTagManager(IBuffTagManager mgr)
        {
            tagManager = mgr;
        }
    }
}