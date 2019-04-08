using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
#pragma warning disable 0414

namespace UniDebugText
{
	/// <summary>
	/// デバッグテキストの UI を管理するクラス
	/// </summary>
	[DisallowMultipleComponent]
	public sealed partial class UniDebugTextUI : MonoBehaviour
	{
		//====================================================================================
		// 定数
		//====================================================================================
		private const string ENABLE_SYMBOL_NAME = "ENABLE_DEBUG_TEXT";

		//====================================================================================
		// 変数(SerializeField)
		//====================================================================================
		[SerializeField] private GameObject			m_closeBaseUI	= null			;
		[SerializeField] private GameObject			m_openBaseUI	= null			;
		[SerializeField] private Button				m_closeButtonUI	= null			;
		[SerializeField] private Button				m_openButtonUI	= null			;
		[SerializeField] private CanvasGroup		m_canvasGroup	= null			;
		[SerializeField] private GameObject			m_root			= null			;
		[SerializeField] private RectTransform		m_textBaseUI	= null			;
		[SerializeField] private Text				m_textUI		= null			;
		[SerializeField] private RectTransform		m_textRectUI	= null			;
		[SerializeField] private Vector2			m_sizeOffset	= Vector2.zero	;

		//====================================================================================
		// 変数
		//====================================================================================
		private bool			m_isOpen			;
		private string			m_currentText		;
		private Vector2			m_currentextSize	;
		private Func<string>	m_getText			;
		private float			m_interval			;
		private float			m_timer				;

		//====================================================================================
		// 関数
		//====================================================================================
		/// <summary>
		/// 初期化される時に呼び出されます
		/// </summary>
		private void Awake()
		{
#if ENABLE_DEBUG_TEXT

			m_closeButtonUI.onClick.AddListener( () => SetState( false ) );
			m_openButtonUI .onClick.AddListener( () => SetState( true  ) );
#else
			Destroy( gameObject );
#endif
		}

		/// <summary>
		/// 開始する時に呼び出されます
		/// </summary>
		private void Start()
		{
			m_root			.SetActive( true  );
			m_openBaseUI	.SetActive( false );
			m_closeBaseUI	.SetActive( true  );

			SetState( false );
		}

#if ENABLE_DEBUG_TEXT

		/// <summary>
		/// 更新される時に呼び出されます
		/// </summary>
		[Conditional( ENABLE_SYMBOL_NAME )]
		private void Update()
		{
			if ( !m_isOpen ) return;
			if ( m_interval <= 0 ) return;

			m_timer += Time.unscaledDeltaTime;

			if ( m_timer < m_interval ) return;

			m_timer -= m_interval;

			UpdateText();
			UpdateSize();
		}

#endif

		/// <summary>
		/// ステートを設定します
		/// </summary>
		[Conditional( ENABLE_SYMBOL_NAME )]
		private void SetState( bool isOpen )
		{
			m_isOpen = isOpen;

			m_openBaseUI	.SetActive(  isOpen );
			m_closeBaseUI	.SetActive( !isOpen );

			if ( !isOpen ) return;

			UpdateText();
			UpdateSize();
		}

		/// <summary>
		/// 表示するかどうかを設定します
		/// </summary>
		[Conditional( ENABLE_SYMBOL_NAME )]
		public void SetVisible( bool isVisible )
		{
			var alpha = isVisible ? 1 : 0;
			m_canvasGroup.alpha = alpha;
		}

		/// <summary>
		/// 表示を設定します
		/// </summary>
		[Conditional( ENABLE_SYMBOL_NAME )]
		public void SetDisp( string text )
		{
			SetDisp( 0, () => text );
		}

		/// <summary>
		/// 表示を設定します
		/// </summary>
		[Conditional( ENABLE_SYMBOL_NAME )]
		public void SetDisp( float interval, Func<string> getText )
		{
			m_interval	= interval	;
			m_getText	= getText	;

			UpdateText();
			UpdateSize();
		}

		/// <summary>
		/// テキストを更新します
		/// </summary>
		private void UpdateText()
		{
			if ( !m_isOpen ) return;

			var text = m_getText();

			if ( m_textUI.text == text ) return;

			m_textUI.text = text;
		}

		/// <summary>
		/// 描画範囲を更新します
		/// </summary>
		private void UpdateSize()
		{
			StartCoroutine( DoUpdateSize() );
		}

		/// <summary>
		/// 描画範囲を更新します
		/// </summary>
		private IEnumerator DoUpdateSize()
		{
			yield return null;

			if ( !m_isOpen ) yield break;

			var textSize = m_textRectUI.sizeDelta;

			if ( textSize == m_currentextSize ) yield break;

			var textBaseSize = m_textBaseUI.sizeDelta;

			textBaseSize = textSize + m_sizeOffset;
			m_textBaseUI.sizeDelta = textBaseSize;
			m_currentextSize = textSize;
		}
	}
}