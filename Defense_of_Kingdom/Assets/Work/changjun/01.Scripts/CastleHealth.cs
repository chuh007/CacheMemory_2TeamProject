using System.Collections;
using Chuh007Lib.Dependencies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Provide]
public class CastleHealth : MonoBehaviour, IDependencyProvider
{
    // 추후에 힐이나 맞은 피격을 넣게 될 수 있으니 주석으로 임시 처리 했습니다.
        [SerializeField] private float maxHp = 100;
        [SerializeField] float currentHp;
        [Header("HP UI")]
        [SerializeField]private Image hpGauge;
        [SerializeField]private TextMeshProUGUI hpText;
        private SpriteRenderer spriteRenderer;
        // private Color originalColor;
    
        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            currentHp = maxHp;
            UpdateHpUI();
            //originalColor = spriteRenderer.color;
        }
    
        public void TakeDamage(float Damage)
        {
            currentHp -= Damage;
            currentHp = Mathf.Clamp(currentHp, 0, maxHp);
            UpdateHpUI();
            //StartCoroutine(HitFlash());
            if (currentHp <= 0)
                Die();
        }
    
        // private IEnumerator HitFlash()
        // {
        //     spriteRenderer.color = new Color(1f, 0.3f, 0.3f, 0.7f);
        //     yield return new WaitForSeconds(0.2f);
        //     spriteRenderer.color = originalColor;
        // }
    
    
        private void Update()
        {
            if (currentHp <= 0)
            {
                currentHp = 0;
            }
            UpdateHpUI();
            UpdateHpText();
            if (currentHp <= 0)
                Die();
        }
    
        // public void Heal(float amount)
        // {
        //     currentHp += amount;
        //     currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        //     UpdateHpUI();
        //     UpdateHpText();
        // }
    
        private void UpdateHpText()
        {
            hpText.text = $"{currentHp}/{maxHp}";
        }
    
        private void UpdateHpUI()
        {
            if (hpGauge != null)
                hpGauge.fillAmount = (float)currentHp / maxHp;
        }
    
        private void Die()
        {
            UpdateHpText();
            gameObject.SetActive(false);
        }
        
}
