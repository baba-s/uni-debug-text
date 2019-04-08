using System.Text;
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
			m_debugTextUI.SetDisp( 1, () =>
			{
				var sb = new StringBuilder();
				sb.AppendLine( $"Frame: {m_value}" );
				sb.AppendLine( $"Version: {Application.version}" );
				sb.AppendLine( $"Debug: {Debug.isDebugBuild}" );
				sb.Append( $"Unity Pro: {Application.HasProLicense()}" );
				return sb.ToString();
			} );
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
