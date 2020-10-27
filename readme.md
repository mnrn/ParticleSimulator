# Particle Simulator

コンピュートシェーダーを使ったパーティクルのシミュレーションを行います。

## デモ

![Demo](https://github.com/mnrn/ParticleSimulator/blob/main/Recordings/movie.gif)

## 制作経緯

職務経歴書のスキル欄を見ただけでは本人の実力が伝わらないと思ったので実力がわかりやすいものを制作しました。  
業務では普段はDirectXやOpenGL環境が多かったのですが、Unityの講師をやっているということもあり、Unityで今回は作ってみました。  

Unityは低レイヤーの部分を隠してかんたんにゲームが作れるという強みがありますが、低レイヤーの知識は高速化などが必要になった際になければならないです。  
ここでは、Unityを使いつつも、DirectXやOpenGL/Vulkanで使うようなグラフィックスパイプラインの知識がないと組めないような内容になっています。

Unity環境がMacOSにしかなかったため、MacOSでの開発となりましたが、そのためにShaderが一部Metal(MacOS)依存になっている部分があります。

### 製作環境

- OS
  - MacOSX Catalina 10.15
- ゲームエンジン
  - Unity 2019
- ツール
  - Visual Studio for Mac
  - VSCode
  - Git / GitHub
