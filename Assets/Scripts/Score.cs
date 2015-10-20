using UnityEngine;

public class Score : MonoBehaviour
{
	// スコアを表示するGUIText
	public GUIText scoreGUIText;
	
	// ハイスコアを表示するGUIText
	public GUIText highScoreGUIText;
	
	// スコア
	private int score;
	
	// ハイスコア
	// 以下の行を削除
	//private int highScore;

	// 代わりに以下の2行を追加
	private NCMB.HighScore highScore;
	private bool isNewRecord;
	
	// PlayerPrefsで保存するためのキー
	// もうローカルには保存しないので、以下の行は削除
	//private string highScoreKey = "highScore";
	
	void Start ()
	{
		Initialize ();

		// ハイスコアを取得する。保存されてなければ0点。
		string name = FindObjectOfType<UserAuth>().currentPlayer();
		highScore = new NCMB.HighScore( 0, name );
		highScore.fetch();
	}
	
	void Update ()
	{
		// スコアがハイスコアより大きければ
		//if (highScore < score) {
		if (highScore.score < score) {
			isNewRecord = true; // フラグを立てる
			highScore.score = score;
			//highScore = score;
		}
		
		// スコア・ハイスコアを表示する
		scoreGUIText.text = score.ToString ();
		highScoreGUIText.text = "HighScore : " + highScore.score.ToString ();
		//highScoreGUIText.text = "HighScore : " + highScore.ToString ();
	}
	
	// ゲーム開始前の状態に戻す
	private void Initialize ()
	{
		// スコアを0に戻す
		score = 0;

		// フラグを初期化する
		isNewRecord = false;
		
		// ハイスコアを取得する。保存されてなければ0を取得する。
		//highScore = PlayerPrefs.GetInt (highScoreKey, 0);
	}
	
	// ポイントの追加
	public void AddPoint (int point)
	{
		score = score + point;
	}
	
	// ハイスコアの保存
	public void Save ()
	{
		// ハイスコアを保存する
		if (isNewRecord) {
			highScore.save ();
		}
		//PlayerPrefs.SetInt (highScoreKey, highScore);
		//PlayerPrefs.Save ();
		
		// ゲーム開始前の状態に戻す
		Initialize ();
	}
}