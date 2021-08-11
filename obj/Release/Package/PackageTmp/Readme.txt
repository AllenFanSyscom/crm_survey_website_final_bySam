測試頁說明
1. NewTestingPage.html
	1.1 功能: 模擬點擊問卷平台頁籤
	1.2 參數
		a. userID (ex: C1C45678-C8D6-EA11-9731-00155D13601B)
		b. SurveyID (ex: 807b9379-0b1f-eb11-a49b-00155d13601b)

2. RejectSurveyTest,html
	2.1 功能: 模擬主管退回陳核動作
	2.2 參數
		a. URL: 網址(中華環境: https://192.168.200.31/applications/newsurvey/)
		b. userID (ex: C1C45678-C8D6-EA11-9731-00155D13601B)
		c. SurveyID (ex: 807b9379-0b1f-eb11-a49b-00155d13601b)
3. TestWebApi.aspx
	3.1 功能: 驗測CRMWeb站台使用到的API
	3.2 overHttp vs overHttps
		a. 呼叫api的方法不同
		b. 呼叫api的網站不同(參考web.config)
			b.1 overHttp: 使用 SurveyHostWithHTTP 定義的網址
			b.2 overHttps: 使用 SurveyHostWithHTTPS 定義的網址

4 TestWebApi.aspx按鍵使用說明
	4.1 GetToken: 取得token
		a. 參數
			a.1 UserID (ex: C1C45678-C8D6-EA11-9731-00155D13601B)
	4.2 TokenAuth: 驗證token
		a. 參數
			a.1 Token (可以透過4.1取得)
	4.3 取得網址: 取得問卷正式/測試網址
		a.參數
			a.1 userID (ex: C1C45678-C8D6-EA11-9731-00155D13601B)
			a.2 SurveyID (ex: 807b9379-0b1f-eb11-a49b-00155d13601b)
	4.4 下載檔案: 下載問卷資料
		a.參數
			a.1 userID (ex: C1C45678-C8D6-EA11-9731-00155D13601B)
			a.2 SurveyID (ex: 807b9379-0b1f-eb11-a49b-00155d13601b)
	4.5 產生問卷: 產生問卷至問卷平台
		a.參數
			a.1 userID (ex: C1C45678-C8D6-EA11-9731-00155D13601B)
			a.2 SurveyID (ex: 807b9379-0b1f-eb11-a49b-00155d13601b)
	4.6 行銷活動方式陳核: 模擬行銷活動方式送出陳核
		a.參數
			a.1 SurveyID (ex: 807b9379-0b1f-eb11-a49b-00155d13601b)
			a.2 Window Top: 距離螢幕上面的距離，單位是 pixels。
			a.3 Window left: 距離螢幕左邊的距離，單位是 pixels。
    4.7 Base64 Encode: Data欄位 > Base64 Encode > Base Data欄位
	4.8 開啟Chrome: cs後台呼叫前端javascirpt執行bat檔啟動Chrome