這是 Carto 使用手冊的首頁，請參閱各章節獲得更多資訊。**目前手冊適用於版本 0.2.1**。

This is the homepage of Carto’s user manual; please visit [[English version|Home]] here. **The current manual version is compatible with version 0.2.1**.

### 安裝

你可以在 Paradox Mods，唯一的正式發佈管道上新增至遊玩集（Playset）或直接下載 Carto。當你啟動遊戲時，模組應該會被添加並載入至遊戲內。

### 章節

1. [更新日誌](#更新日誌)
2. [一般分頁](#一般分頁)
3. 自訂輸出分頁
4. 雜項分頁
5. 靈感

### 回饋及聯絡方式

你可以從 PDX 論壇的留言區、Cities: Skylines Modding Discord 伺服器與 Cities: Skylines Taiwan Assets Discord 伺服器聯繫我。若你想要開啟更深度的討論，或回報複雜的程式錯誤，我會建議你使用 GitHub 的議題區（Issue）或討論區（Discussion），這樣我能夠比較容易追蹤進度。

* 🌐 [Paradox 論壇](about:blank)
* 🛜 [Cities: Skylines Modding](https://discord.gg/HTav7ARPs2)－<u>僅限</u>英文
* 🛜 [Cities: Skylines Taiwan Assets](https://discord.gg/Gz4K66jT64)－中文為主
* 📧 [4alpelna4lve@gmail.com](mailto:4alpelna4lve@gmail.com)

## 更新日誌

### 0.2.1

* 現在 Carto 可以輸出可遊玩區域之外的世界高度圖（World Heightmap）。
  * 新增世界高程欄位（自訂輸出 > 進階 > 地形 > 世界高程）。
  * 新增世界深度欄位（自訂輸出 > 進階 > 水體 > 世界深度）。
* 新增 GeoTIFF 格式選項（雜項 > GeoTIFF 格式），使用者可以選擇要將 GeoTIFF 輸出為 Int16（16位元整數）、Norm16 *（16位元標準化整數）或 Float32（32位元浮點數）格式；預設為 Int16。
* 重寫 Shapefile 的輸出模式。現在 dBASE 檔案中的字串欄位長度不再是固定的254位元組，而是根據所有資料轉換之位元組最長者計算，平均可以減少 80% 的檔案大小。
* 重新整理選項頁面。一般分頁中增加了「版本」資訊，而原先在一般分頁的「啟用所有欄位」按鈕被移至雜項分頁。
* 添加警告視窗，能夠協助使用者排除遇到的問題。
* 添加簡體中文（zh-HANS）與繁體中文（zh-HANT）翻譯。

註記：Norm16 實際上是 UInt16 格式，此處為配合遊戲內程式碼稱呼而沿襲此用法。

### 0.2

* 現在 Carto 可以輸出建築圖徵，並且可以從自訂輸出分頁中調整這些欄位的狀態。
  * 建築圖徵適用的**空間**欄位包括：邊緣。
  * 建築圖徵適用的**非空間**欄位包括：<u>地址</u>、資產、<u>品牌</u>、分類、<u>員工</u>、<u>家庭</u>、<u>等級</u>、物體、<u>產品</u>、<u>居民</u>與<u>分區</u>。
* 分區的「分類」欄位現在被稱為「分區」。
* 部分通用類別被集中遷移至的 Carto.Domain 與 Carto.Utils.ExportUtils 命名空間。

註記：<u>底線</u>表示為新欄位。

### 0.1.2

* 現在 Carto 可以輸出分區的空間欄位：邊緣。
  * Carto.Systems.ZoningSystem.GetZoningProperties() 現在整合到了 Carto.Utils.ExportUtils 內。
  * 移除遊戲載入後會自動呼叫前述方法的行為。
* 修正當 Zone Color Changer 模組啟用時，無法輸出遊戲內原始分區顏色的錯誤。
* 新增雜項分頁，並且添加了兩個設定：
  * 新增輸出無分區單元選項（雜項 > 輸出無分區單元），使用者可以選擇不輸出沒有任何分區的單元以降低檔案大小；預設關閉。
  * 新增使用 Zone Color Changer 的顏色選項（雜項 > 使用 Zone Color Changer 的顏色），使用者可以選擇使用在 Zone Color Changer 中設定的顏色；預設開啟。

### 0.1.1

* 現在 Carto 可以輸出分區的非空間欄位，包括：分類、<u>顏色</u>、<u>密度</u>、物體與<u>主題</u>。
  * 目前當載入遊戲時便會自動呼叫 Carto.Systems.ZoningSystem.GetZoningProperties() 方法，會在日誌（Log）中留下分區的資訊。
  * 目前無法輸出分區的空間欄位。
* 使用 System.Reflection 以整合 Zone Color Changer 模組中使用者設定的分區顏色資訊。

### 0.1

* Carto 的第一個公開儲存庫版本。

註記：<u>底線</u>表示為新欄位。

## 一般分頁

![預設的一般分頁介面](src/Carto-General-Tab-Default-zh.png)

*0.2.1 版本預設的一般分頁介面。*
