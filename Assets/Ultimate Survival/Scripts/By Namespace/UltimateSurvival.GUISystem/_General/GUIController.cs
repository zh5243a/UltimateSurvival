using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	public class GUIController : MonoSingleton<GUIController>
	{
		public PlayerEventHandler Player { get; private set; }

		/// <summary>The main Canvas that's used for the GUI elements.</summary>
        /// UI界面的主画布
		public Canvas Canvas { get { return m_Canvas; } }

		/// <summary>All the item collections that are part of the GUI.</summary>
        /// 所有GUI的集合
		public ItemContainer[] Containers { get; private set; }

		public Font Font { get { return m_Font; } }

		[Header("Setup")]

		[SerializeField]
		private Canvas m_Canvas;//画布

		[SerializeField]
		private Camera m_GUICamera;//画布相机

		[SerializeField]
		private Font m_Font;//字体

		[SerializeField]
		[Reorderable]
		[Tooltip("If the player clicks while on those rects, the current selection will not be lost.")]
        //如果玩家点击在这些矩形，当前的选择不会丢失,包含3个窗口,物品信息栏,物品制作所需材料栏,以及制作界面的竖直滑动条
        private ReorderableRectTransformList m_SelectionBlockers;

		[Header("Audio")]

		[SerializeField]//背包打开的声音
		private AudioClip m_InventoryOpenClip;

		[SerializeField]//背包关闭的声音
		private AudioClip m_InventoryCloseClip;

        //获得指定物体名称上的ItemContainer组件
        public ItemContainer GetContainer(string name)
		{
			for(int i = 0;i < Containers.Length;i ++)
				if(Containers[i].Name == name)
					return Containers[i];

			Debug.LogWarning("No container with the name " + name + " found!");

			return null;
		}
        //当前鼠标点是否在当前相机画布下的点
		public bool MouseOverSelectionKeeper()
		{
			for (int i = 0; i < m_SelectionBlockers.Count; i++) 
			{
				if(!m_SelectionBlockers[i].gameObject.activeSelf)
					continue;
				
				bool containsPoint = RectTransformUtility.RectangleContainsScreenPoint(m_SelectionBlockers[i], Input.mousePosition, m_GUICamera);
				if(containsPoint)
					return true;
			}

			return false;
		}
        /// <summary>
        /// 应用所有的ItemContainer
        /// </summary>
		public void ApplyForAllCollections()
		{
			foreach(var collection in GetComponentsInChildren<ItemContainer>(true))
				collection.ApplyAll();
		}

		private void Awake()
		{
			Containers = GetComponentsInChildren<ItemContainer>(true);//获得子物体上所有包含ItemContainer的物体
            Player = GameController.LocalPlayer;//获得Player

			DontDestroyOnLoad(gameObject);//不要摧毁GUIController
        }

		private void Start()
		{
			InventoryController.Instance.State.AddChangeListener(OnChanged_InventoryState);//声音事件监听
		}

		private void OnChanged_InventoryState()
		{
			if(!InventoryController.Instance.IsClosed)
				GameController.Audio.Play2D(m_InventoryOpenClip, 0.6f);
			else
				GameController.Audio.Play2D(m_InventoryCloseClip, 0.6f);
		}
	}
}
