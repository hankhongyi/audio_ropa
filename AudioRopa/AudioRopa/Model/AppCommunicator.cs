using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRopa.Model
{
    public class AppCommunicator
    {
        private static readonly Lazy<AppCommunicator> _instance = new Lazy<AppCommunicator>(() => new AppCommunicator());
        public static AppCommunicator Instance => _instance.Value;

        public event Action<AprInfo> OnAptConnectClicked;
        public event Action OnAptGenerateQrCodeClicked;
        public event Action<AuracastInfo> OnAptSettingUpdateClicked;
        public event Action OnAptSettingClicked;
        public event Action OnAptSettingCancelled;
        public event Action<bool> OnAgcOnOffChanged;
        public event Action<AuracastInfo> OnAuracastInfoRead;
        public event Action<AprInfo> OnAprSettingTransferClicked;
        public event Action<AprInfo> OnAprChannelNameChanged;
        public event Action<AprInfo> OnAprPassowrdChanged;
        public event Action OnAprTransferClicked;
        public event Action OnAprTransferCancelled;
        public event Action OnReturnHomeClicked;

        private AppCommunicator()
        {
        }

        public void InvokeAptConnect(AprInfo aprInfo)
        {
            OnAptConnectClicked.Invoke(aprInfo);
        }

        public void InvokeGenerateQrCode()
        {
            OnAptGenerateQrCodeClicked.Invoke();
        }

        public void InvokeAptSettingUpdate(AuracastInfo auracast)
        {
            OnAptSettingUpdateClicked.Invoke(auracast);
        }

        public void InvokeSetting()
        {
            OnAptSettingClicked.Invoke();
        }

        public void InvokeAgcOnOff(bool isOn)
        {
            OnAgcOnOffChanged.Invoke(isOn);
        }

        public void InvokeAptInfoSettingCancelled()
        {
            OnAptSettingCancelled.Invoke();
        }

        public void InvokeAuracastInfoRead(AuracastInfo auracast)
        {
            OnAuracastInfoRead.Invoke(auracast);
        }

        public void InvokeAprSettingTransfer(AprInfo aprInfo)
        {
            OnAprSettingTransferClicked.Invoke(aprInfo);
        }

        public void InvokeAprTranfer()
        {
            OnAprTransferClicked.Invoke();
        }

        public void InvokeAprTransferCancelled()
        {
            OnAprTransferCancelled.Invoke();
        }

        public void InvokeReturnHome()
        {
            OnReturnHomeClicked.Invoke();
        }

        public void InvokeAprChannelNameChanged(AprInfo aprInfo)
        {
            OnAprChannelNameChanged?.Invoke(aprInfo);
        }

        public void InvokeAprPasswordChanged(AprInfo aprInfo)
        {
            OnAprPassowrdChanged?.Invoke(aprInfo);
        }
    }
}
