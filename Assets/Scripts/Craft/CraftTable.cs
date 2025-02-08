using Assets.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour, IItemHolder
{
    [LabelText("加工类型")] public CraftType SupportCraftType;

    public GameObject ProgressBarPrefab; // 环形进度条预制体（UI Canvas下的预制体）
    public Transform ItemAttachPoint;

    private Item currentItem;
    private ProgressBar _progressBar;
    private bool isProcessing;
    private float timer;

    void Start()
    {
        // 初始化进度条
        GameObject progressUI = Instantiate(ProgressBarPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        progressUI.transform.SetParent(transform); // 绑定到工作台
        progressUI.SetActive(false);
    }

    void Update()
    {
        if (isProcessing)
        {
            currentItem.ProgressTimer += Time.deltaTime;
            var normalizedProgress = currentItem.ProgressTimer / currentItem.ProgressTime;
            if (normalizedProgress < 1)
            {
                _progressBar.UpdateProgress(normalizedProgress);
            }
            else
            {
                FinishProcessing();
            }

        }
    }

    public void TryStartProcessing()
    {
        if (ValidateCanProcess())
        {
            StartProcessing();
        }
    }

    void StartProcessing()
    {
        if (currentItem != null || isProcessing) return;

        // 启动进度条
        _progressBar.Show(true);
        isProcessing = true;
    }

    void FinishProcessing()
    {
        Destroy(currentItem);
        GameObject processedItem = Instantiate(currentItem.CraftTarget, ItemAttachPoint.position, Quaternion.identity);

        isProcessing = false;
        _progressBar.Show(false);
    }

    bool ValidateCanProcess()
    {
        return (SupportCraftType == currentItem.CraftType);
    }


    public Item OnTookItem()
    {
        throw new System.NotImplementedException();
    }

    public void OnPutItem(Item item)
    {
        throw new System.NotImplementedException();
    }
}
