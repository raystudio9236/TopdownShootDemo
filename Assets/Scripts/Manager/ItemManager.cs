using System;
using System.Collections.Generic;
using Actions.Core;
using Components.Item;
using Item;
using Other;
using UnityEngine;
using Utils;

namespace Manager
{
    public class ItemManager : MonoBehaviour
    {
        private const string PATH_PREFIX = "Items";

        public static ItemManager Instance;

        private Dictionary<string, ItemData> _itemDic =
            new Dictionary<string, ItemData>();

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance.gameObject);

            Instance = this;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                var player = GameManager.Contexts.game.playerTagEntity;
                player.ChangeItem(new ChangeItemPair
                {
                    ItemName = "BulletFindClosetTarget",
                    Type = ChangeItemType.Add
                });
            }
        }

        public ItemData GetItem(string itemName)
        {
            if (!_itemDic.TryGetValue(itemName, out var itemData))
            {
                itemData = Resources.Load<ItemData>(
                    $"{PATH_PREFIX}/{itemName}");
                if (itemData == null)
                {
                    Debug.LogError(
                        $"找不到名为{itemData}的Item资源");
                    return null;
                }

                _itemDic.Add(itemName, itemData);
            }

            return itemData;
        }
    }
}