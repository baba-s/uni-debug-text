using UnityEngine;

namespace UniDebugText.Demo
{
	/// <summary>
	/// デモを管理するクラス
	/// </summary>
	[DisallowMultipleComponent]
	public sealed class DemoScene : MonoBehaviour
	{
		//====================================================================================
		// 変数(SerializeField)
		//====================================================================================
		[SerializeField] private UniDebugTextUI	m_debugTextUI = null;

		//====================================================================================
		// 変数
		//====================================================================================
		private int m_value;

		//====================================================================================
		// 関数
		//====================================================================================
		/// <summary>
		/// 開始する時に呼び出されます
		/// </summary>
		private void Start()
		{
			m_debugTextUI.SetDisp( 1, () => m_value.ToString() );
		}

		/// <summary>
		/// 更新される時に呼び出されます
		/// </summary>
		private void Update()
		{
			m_value++;
		}
	}
}
