using Components.Stat;
using Events;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIBattlePanel : MonoBehaviour
    {
        public Text HpText;
        public Text VelocityText;
        public Text AttackSpeedText;
        public Text BulletCountText;
        public Text DamageText;

        private void Start()
        {
            GameManager.AddHandler<GameEntity>(GlobalEvent.PlayerSpawn,
                OnPlayerSpawn);
        }

        private void OnDestroy()
        {
            GameManager.RemoveHandler<GameEntity>(GlobalEvent.PlayerSpawn,
                OnPlayerSpawn);
        }

        private void OnPlayerSpawn(short eventtype, GameEntity data)
        {
            data.AddStatHandler(OnStatChangeHandler);

            HpText.text =
                data.GetStat(StatFlag.Hp).ToString("0.0");
            VelocityText.text =
                data.GetStat(StatFlag.Velocity).ToString("0.0");
            AttackSpeedText.text =
                data.GetStat(StatFlag.AttackSpeed).ToString("0.0");
            BulletCountText.text =
                data.GetStat(StatFlag.BulletCount).ToString("0.0");
            DamageText.text =
                data.GetStat(StatFlag.Damage).ToString("0.0");
        }

        private void OnStatChangeHandler(short eventtype, StatChangeData data)
        {
            switch (data.StatFlag)
            {
                case StatFlag.Hp:
                    HpText.text =
                        data.Value.ToString("0.0");
                    break;

                case StatFlag.Velocity:
                    VelocityText.text = data.Value.ToString("0.0");
                    break;

                case StatFlag.AttackSpeed:
                    AttackSpeedText.text =
                        data.Value.ToString("0.0");
                    break;

                case StatFlag.BulletCount:
                    BulletCountText.text =
                        data.Value.ToString("0.0");
                    break;

                case StatFlag.Damage:
                    DamageText.text =
                        data.Value.ToString("0.0");
                    break;
            }
        }
    }
}