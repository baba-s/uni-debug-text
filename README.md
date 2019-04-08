# UniDebugText

テキストのデバッグ表示を簡単に実装できる UI

[![](https://img.shields.io/github/release/baba-s/uni-debug-text.svg?label=latest%20version)](https://github.com/baba-s/uni-debug-text/releases)
[![](https://img.shields.io/github/release-date/baba-s/uni-debug-text.svg)](https://github.com/baba-s/uni-debug-text/releases)
![](https://img.shields.io/badge/Unity-2018.3%2B-red.svg)
![](https://img.shields.io/badge/.NET-4.x-orange.svg)
[![](https://img.shields.io/github/license/baba-s/uni-debug-text.svg)](https://github.com/baba-s/uni-debug-text/blob/master/LICENSE)

## バージョン

- Unity 2018.3.9f1

## 準備

![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20190408/20190408115836.png)

`ENABLE_DEBUG_TEXT` シンボルを定義することで使用できるようになります  
リリースビルド時はこのシンボルを消すことで、UniDebugText を無効化できます  

## 使用例

![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20190408/20190408115841.png)

「UniDebugTextUI」プレハブをシーンに配置します  

```cs
using System.Text;
using UnityEngine;

public sealed class DemoScene : MonoBehaviour
{
    public UniDebugTextUI m_debugTextUI = null;

    private int m_value;

    private void Start()
    {
        // 第 1 引数：描画を更新する間隔（秒）
        // 第 2 引数：描画するテキスト
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

    private void Update()
    {
        m_value++;
    }
}
```

そして、上記のようなコードを記述することで  

![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20190408/20190408115847.gif)

このように使用することができます