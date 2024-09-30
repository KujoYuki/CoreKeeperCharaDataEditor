# CKFoodMaker

[CoreKeepr](https://store.steampowered.com/app/1621690/Core_Keeper/)のインベントリ内容を操作する外部拡張エディターです。  
通常の手段では作れない料理の作成などを目的として制作しています。  

## できること
- 任意の食材の組み合わせによる料理の作成します。  
  - 料理のレア度や個数を調整することが出来ます。  
- ポーションやシーズン料理を食材とした料理を作成し、料理補正の乗った効果量や効果時間を得ることが出来ます。  
- ただし一部の作成したアイテムが有効かどうかはゲーム側に判定されます。
- 料理が未作成の食材の組み合わせをリストップします。
- インベントリ内の全てのアイテム個数を変更できます。
- インベントリ内の全ての装備の耐久度を変更できます。
- インベントリ内の全てのペットスキルを変更できます。
- 未取得のアイテムや装備をインベントリに作成できます。
- ペットスキルの編集ができます。
  - スキルの変更と有効/無効の切り替えができます。
  - 未実装のペットスキルを対象に含みます。
- 通常手段で入手不可の未実装アイテムの取得ができます。
- 負数個の料理の作成
  - 負数個のアイテムは回路のバグ挙動を使ったアイテム増殖に利用出来ます。  
- ゲームやmodのバージョンに殆ど関係なく使えます。

![CKFoodMaker Overview](Document/images/imageSample.png)  
![](Document/images/imageEditPet.png)
## 免責事項
キャラクターデータのインベントリを直接編集します。
また、上級者向けの機能として料理に限らない全てのアイテムの編集機能を持ちます。
データの破損が紛失が起きても責任はおいかねます。    
必ずバックアップをとってください。  

## 制限事項
以下の方は本ツールの機能に制限をかけています。  
- エンディングに到達していない方  
- 特定のパラメータが異常域に達している方  

要はクリア後の正常なやりこみ勢向けツールになります。

## 動作環境
.NET 8.0以降のランタイムパッケージがインストールされていること。

## Explain
CKFoodMaker is an external extension editor designed to modify the inventory contents of [Core Keeper](https://store.steampowered.com/app/1621690/Core_Keeper/).  
This tool is primarily intended for creating dishes that cannot be made through normal means.  
It allows you to create dishes from rare golden ingredients and adjust the rarity and quantity of the dishes.  
By using potions and seasonal dishes as ingredients, you can achieve effects and durations that exceed normal levels thanks to the enhanced effects and skill boosts.  
No mods are required to use or bring the created dishes and items into the game, but whether the created items are valid is determined by the game itself.  

## Disclaimer
This tool directly edits the character data's inventory. It also includes advanced features that allow you to edit any item, not just dishes. Use at your own risk, as we are not responsible for any data corruption or loss.  

## Features
- Create dishes with any specified ingredients
- Create dishes using potions and seasonal items that normally cannot be used as ingredients in cooking
- Modify the quantity, rarity, and category of the dishes
- Create dishes in quantities exceeding 999
- Create dishes with a negative quantity
  - Negative quantity items can be used for item duplication via circuits  

## Advanced Features
- Modify the item level of equipment
- Increase the durability of equipment beyond its normal limits
- Possess items that cannot normally be obtained

## 仕組みと解説
Explanation is in Japanese only  
[セーブデータ編集について](Document/analysis.md)  
[パラメータについて](Document/parameter.md)  
