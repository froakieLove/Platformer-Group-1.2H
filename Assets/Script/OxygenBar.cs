using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public Image oxygenBarImage; // 引用UI Image组件
    public float maxOxygen = 100f; // 最大氧气量
    public float currentOxygen; // 当前氧气量
    public float consumptionRate = 1f; // 每秒消耗的氧气量

    private void Start()
    {
        currentOxygen = maxOxygen; // 初始时氧气为满
        oxygenBarImage.fillAmount = 1f; // 设置填充量为满
    }

    private void Update()
    {
        ConsumeOxygen(); // 消耗氧气
        UpdateOxygenBar(); // 更新氧气条UI
    }

    void ConsumeOxygen()
    {
        // 每帧消耗一定量的氧气
        currentOxygen -= consumptionRate * Time.deltaTime;
        currentOxygen = Mathf.Max(currentOxygen, 0); // 确保氧气量不低于0
    }

    void UpdateOxygenBar()
    {
        // 更新氧气条的填充量
        oxygenBarImage.fillAmount = currentOxygen / maxOxygen;
    }

    public void AddOxygen(float amount)
    {
        currentOxygen += amount;
        currentOxygen = Mathf.Min(currentOxygen, maxOxygen);
        UpdateOxygenBar();
    }

    public void TakeDamage(float amount)
    {
        // 受到伤害减少氧气
        currentOxygen -= amount;
        currentOxygen = Mathf.Max(currentOxygen, 0); // 防止氧气量成负
    }
}