using System.Collections.Generic;
using UnityEngine;
using UltimateSurvival.Building;

namespace UltimateSurvival
{
	/// <summary>
	/// 
	/// </summary>
	public class PlayerEventHandler : EntityEventHandler
	{
        /// <summary>Used for respawning when dying, if the position is (0, 0, 0), the player will be respawned through other methods.</summary>
        /// 用于重生，临死的时候，如果位置（0, 0, 0），玩家将通过其他方法重生。
        public Value<Vector3> LastSleepPosition = new Value<Vector3>(Vector3.zero);

		/// <summary>耐力</summary>
		public Value<float> Stamina = new Value<float>(100f);

		/// <summary>口渴</summary>
		public Value<float> Thirst = new Value<float>(100f);

		/// <summary>饥饿</summary>
		public Value<float> Hunger = new Value<float>(100f);

		/// <summary>防御</summary>
		public Value<int> Defense = new Value<int>(0);

		/// <summary>运动</summary>
		public Value<Vector2> MovementInput = new Value<Vector2>(Vector2.zero);

		/// <summary>看</summary>
		public Value<Vector2> LookInput	= new Value<Vector2>(Vector2.zero);

		/// <summary>看的方向</summary>
		public Value<Vector3> LookDirection = new Value<Vector3>(Vector3.zero);

		/// <summary>查看锁定</summary>
		public Value<bool> ViewLocked = new Value<bool>(false);

		/// <summary>运动速度</summary>
		public Value<float> MovementSpeedFactor = new Value<float>(1f);

		/// <summary>附近的途径</summary>
		public Queue<Transform> NearLadders = new Queue<Transform>();

		/// <summary>光线数据</summary>
		public Value<RaycastData> RaycastData = new Value<RaycastData>(null);

        /// <summary>互动一次</summary>
        public Attempt InteractOnce = new Attempt();

        /// <summary>持续相互作用</summary>
        public Value<bool> InteractContinuously = new Value<bool>(false);
        //玩家角色离墙/物体太近了吗？
		/// <summary>Is the player character too close to a wall / object? (and it's facing it)</summary>
		public Value<bool> IsCloseToAnObject = new Value<bool>(false);

        /// <summary>
        /// <para>SavableItem - item to equip</para>
        /// <para>bool - do it instantly?</para>
        /// 改变装备的物品
        /// </summary>
        public Attempt<SavableItem, bool> ChangeEquippedItem = new Attempt<SavableItem, bool>();

		/// <summary>装备物品</summary>
		public Value<SavableItem> EquippedItem = new Value<SavableItem>(null);

        /// <summary>Mainly used when the durability of an item hits 0, and the equipped item should be destroyed.
        /// 主要用于物品耐久性达到0时，并应销毁所装备的物品。
        /// </summary>
        public Attempt DestroyEquippedItem = new Attempt();

		/// <summary>开始睡觉</summary>
		public Attempt<SleepingBag> StartSleeping = new Attempt<SleepingBag>();

		/// <summary>睡觉</summary>
		public Activity Sleep = new Activity();

		/// <summary>走动</summary>
		public Activity	Walk = new Activity();

        /// <summary>一旦攻击</summary>
        public Attempt AttackOnce = new Attempt();

        /// <summary>连续进攻</summary>
        public Attempt AttackContinuously = new Attempt();

        /// <summary>可以显示对象预览</summary>
        public Value<bool> CanShowObjectPreview = new Value<bool>(false);

		/// <summary>放置物品</summary>
		public Attempt PlaceObject = new Attempt();

		/// <summary>滚动的值</summary>
		public Value<float> ScrollValue = new Value<float>(0f);

        /// <summary>选择可信赖的</summary>
        public Value<BuildingPiece> SelectedBuildable = new Value<BuildingPiece>(null);

        /// <summary>选择可信赖的</summary>
        public Activity SelectBuildable = new Activity();

		/// <summary>旋转物体</summary>
		public Attempt<float> RotateObject = new Attempt<float>();

		/// <summary>跑</summary>
		public Activity Run = new Activity();

		/// <summary>蹲伏</summary>
		public Activity Crouch = new Activity();

		/// <summary>跳</summary>
		public Activity Jump = new Activity();

		/// <summary>目标</summary>
		public Activity Aim = new Activity();
	}
}
