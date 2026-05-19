using System;

// JSONから読み込むステータス定義
[Serializable]
public class EntityStatus
{
    // 基礎ステータス
    public int str_base;
    public int vit_base;
    public int agi_base;

    // 成長率
    public int str_growth;
    public int vit_growth;
    public int agi_growth;
}
