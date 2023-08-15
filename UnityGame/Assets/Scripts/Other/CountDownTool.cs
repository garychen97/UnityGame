using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CountDownTool : MonoBehaviour
{
    public Text countDownText;

    private long countDownFinishTick;//倒计时结束时间戳

    [Header("年")]
    public int countDownFinishYear;
    [Header("月")]
    public int countDownFinishMonth;
    [Header("日")]
    public int countDownFinishDay;
    [Header("时")]
    public int countDownFinishHour;
    [Header("分")]
    public int countDownFinishMinute;
    [Header("秒")]
    public int countDownFinishSecond;
    
    // Start is called before the first frame update
    void Start()
    {
        DateTime countDownFinishDate = new DateTime(countDownFinishYear, countDownFinishMonth, countDownFinishDay, countDownFinishHour, countDownFinishMinute, countDownFinishSecond);
        countDownFinishTick = countDownFinishDate.Ticks;
        StartCoroutine(UpdateCountDown());
    }
    
    IEnumerator UpdateCountDown()
    {
        long tick = countDownFinishTick - DateTime.Now.Ticks;
        if (tick > 0)
        {
            UpdateTime(tick);
            yield return new WaitForSeconds(1);
            StartCoroutine(UpdateCountDown());
        }
    }

    public void UpdateTime(long timestamp)
    {
        countDownText.text = "离职倒计时: " + PraseTimeStamp(timestamp);
    }

    public string PraseTimeStamp(long timestamp)
    {
        int diff = (int)(timestamp / 10000000);
        int day = diff / (24 * 60 * 60);
        diff -= day * (24 * 60 * 60);
        int hour = diff / (60 * 60);
        diff -= hour * (60 * 60);
        int minute = diff / 60;
        diff -= minute * 60;
        int second = diff;
        return day == 0? "":(day.ToString() + "天") + ((day == 0 && hour == 0) ? "" : (hour.ToString() + "时")) + ((day == 0 && hour == 0 && minute == 0)?"":(minute.ToString() + "分")) +
              second.ToString() + "秒"; 
    }
    
}
