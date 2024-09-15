# CKFoodMaker

![CKFoodMaker Overview](Document/images/imageSample.png)

[CoreKeepr](https://store.steampowered.com/app/1621690/Core_Keeper/)のインベントリ内容を操作する外部拡張エディターです。  
通常の手段では作れない料理の作成を目的として制作しています。  
手に入りにくい金色食材同士の料理や、作成する料理のレア度や個数を調整することが出来ます。  
ポーションやシーズン料理を食材として扱うことによって、料理の補正効果やスキル効果が乗ることにより通常以上の効果量や効果時間を得ることが出来ます。  
作成した料理やアイテムの使用や持ち込みにmod等は一切必要ありませんが、作成したアイテムが有効かどうかはゲーム側に判定されます。

CKFoodMaker is an external extension editor designed to modify the inventory contents of [Core Keeper](https://store.steampowered.com/app/1621690/Core_Keeper/).  
This tool is primarily intended for creating dishes that cannot be made through normal means.  
It allows you to create dishes from rare golden ingredients and adjust the rarity and quantity of the dishes.  
By using potions and seasonal dishes as ingredients, you can achieve effects and durations that exceed normal levels thanks to the enhanced effects and skill boosts.  
No mods are required to use or bring the created dishes and items into the game, but whether the created items are valid is determined by the game itself.

## 免責事項
キャラクターデータのインベントリを直接編集します。
また、上級者向けの機能として料理に限らない全てのアイテムの編集機能を持ちます。
データの破損が紛失が起きても自己責任でお願いします。

## Disclaimer
This tool directly edits the character data's inventory. It also includes advanced features that allow you to edit any item, not just dishes. Use at your own risk, as we are not responsible for any data corruption or loss.

## 機能
- 任意の食材を指定した料理の作成
- ポーションやシーズン限定アイテムなどの、通常は食材として鍋に入れられないアイテムを食材指定した料理の作成
- 作成する料理の個数、レア度、カテゴリの変更
- 9999個を超えた料理の作成。
- 負数個の料理の作成
  - 負数個のアイテムは回路のバグ挙動を使ったアイテム増殖に利用出来ます。

## Features
- Create dishes with any specified ingredients
- Create dishes using potions and seasonal items that normally cannot be used as ingredients in cooking
- Modify the quantity, rarity, and category of the dishes
- Create dishes in quantities exceeding 999
- Create dishes with a negative quantity
  - Negative quantity items can be used for item duplication via circuits

## 上級者向け機能で可能なこと
- 装備のアイテムLv変更
- 装備の耐久度の上限を超えた変更
- 通常入手が不可能な設置物の所持

## Advanced Features
- Modify the item level of equipment
- Increase the durability of equipment beyond its normal limits
- Possess items that cannot normally be obtained

## 出来ないこと
- インベントリ以外のキャラデータ内容の変更
- ペットなど、補助データを持つアイテムの変更

## Limitations
- Cannot modify character data outside of the inventory
- Cannot modify items with auxiliary data, such as pets

## 仕組みと解説
[セーブデータ編集について](Document/analysis.md)  
[パラメータについて](Document/parameter.md)  
Japanese only.