using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRopa.Model
{
    public class AptCommunicator
    {
        private static readonly Lazy<AptCommunicator> _instance = new Lazy<AptCommunicator>(() => new AptCommunicator());
        public static AptCommunicator Instance => _instance.Value;

        public event Action OnConnectClicked;
        public event Action OnGenerateQrCodeClicked;
        public event Action<AuracastInfo> OnAuracastInfoUpdated;
        public event Action OnSettingClicked;
        public event Action OnAptInfoSettingCancelled;
        public event Action<bool> OnAgcOnOffChanged;
        public event Action<AuracastInfo> OnAuracastInfoRead;
        public event Action<AprInfo> OnAprSettingTransferClicked;
        public event Action<AprInfo> OnAprChannelNameChanged;
        public event Action<AprInfo> OnAprPassowrdChanged;
        public event Action OnAprTransferClicked;
        public event Action OnAprTransferCancelled;
        public event Action OnReturnHomeClicked;

        private AptCommunicator()
        {
        }

        public void InvokeConnect()
        {
            OnConnectClicked.Invoke();
        }

        public void InvokeGenerateQrCode()
        {
            OnGenerateQrCodeClicked.Invoke();
        }

        public void InvokeAuracastInfoUpdate(AuracastInfo auracast)
        {
            OnAuracastInfoUpdated.Invoke(auracast);
        }

        public void InvokeSetting()
        {
            OnSettingClicked.Invoke();
        }

        public void InvokeAgcOnOff(bool isOn)
        {
            OnAgcOnOffChanged.Invoke(isOn);
        }

        public void InvokeAptInfoSettingCancelled()
        {
            OnAptInfoSettingCancelled.Invoke();
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
