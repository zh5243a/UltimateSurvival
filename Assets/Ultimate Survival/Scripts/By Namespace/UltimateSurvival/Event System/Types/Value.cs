using System;
using UnityEngine;

namespace UltimateSurvival
{
    /// <summary>
    /// This allows to have a callback when the value changes (An example would be updating the GUI when the player health changes).
    /// 这允许当值改变时回调（当玩家健康变化时，一个示例将更新GUI）。
    /// </summary>
    public class Value<T>
	{
		public delegate T Filter(T lastValue, T newValue);

		private Action m_Set;
		private Filter m_Filter;
		private T m_CurrentValue;//现在的值
		private T m_LastValue;//最后的值


		/// <summary>
		/// 将传入的值设为现在的值,原先的值设为最后的值
		/// </summary>
		public Value(T initialValue)
		{
			m_CurrentValue = initialValue;
			m_LastValue = m_CurrentValue;
		}

		public bool Is(T value)
		{
			return m_CurrentValue != null && m_CurrentValue.Equals(value);
		}

        /// <summary>
        /// When this value will change, the callback method will be called.
        /// 当这个值发生变化时，回调方法将被调用。
        /// </summary>
        public void AddChangeListener(Action callback)
		{
			m_Set += callback;
		}

		/// <summary>
		/// 
		/// </summary>
		public void RemoveChangeListener(Action callback)
		{
			m_Set -= callback;
		}

        /// <summary>
        /// A "filter" will be called before the regular callbacks, useful for clamping values (like the player health, etc).
        /// “过滤器”将在常规回调调用，用于夹持的价值观（如Player的健康，等）。
        /// </summary>
        public void SetFilter(Filter filter)
		{
			m_Filter = filter;
		}

		/// <summary>
		/// 获得现在的值
		/// </summary>
		public T Get()
		{
			return m_CurrentValue;
		}

		/// <summary>
		/// 获得最后的值
		/// </summary>
		public T GetLastValue()
		{
			return m_LastValue;
		}

		/// <summary>
		/// 
		/// </summary>
		public void Set(T value)
		{
			m_LastValue = m_CurrentValue;
			m_CurrentValue = value;

			if(m_Filter != null)
				m_CurrentValue = m_Filter(m_LastValue, m_CurrentValue);
			
			if(m_Set != null && (m_LastValue == null || !m_LastValue.Equals(m_CurrentValue)))
				m_Set();
		}

		/// <summary>
		/// 
		/// </summary>
		public void SetAndForceUpdate(T value)
		{
			m_LastValue = m_CurrentValue;
			m_CurrentValue = value;

			if(m_Filter != null)
				m_CurrentValue = m_Filter(m_LastValue, m_CurrentValue);

			if(m_Set != null)
				m_Set();
		}

		public void SetAndDontUpdate(T value)
		{
			m_LastValue = m_CurrentValue;
			m_CurrentValue = value;

			if(m_Filter != null)
				m_CurrentValue = m_Filter(m_LastValue, m_CurrentValue);
		}
	}
}