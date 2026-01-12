using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AudioRopa.Logger;
using AudioRopa.Model;
using AudioRopa.Utility;

namespace AudioRopa.Bluetooth
{
    public class DeviceState
    {
        private static readonly Lazy<DeviceState> _instance = new Lazy<DeviceState>(() => new DeviceState());
        public static DeviceState Instance => _instance.Value;

        public Dictionary<string, LibControl.LocalDevice> device_map = new Dictionary<string, LibControl.LocalDevice>();
        public LibControl.InitParam init_param = new LibControl.InitParam(-1, -1, -1, -1, null, null);
        public LibControl.InitBleParam init_ble_param = new LibControl.InitBleParam(-1, -1, "", 0, (int)AirohaDeviceType.HEADSET, null, null);
        public LEConnectDeviceInfo connectedDevice;
        public int protocol = 0;
        public int handle = -1;
        public FileLogger gLogger = FileLogger.Instance;
        public static LibControl.SDK_ApiResultCallback callback = null;
        public LibControl.SDK_LogCallback printlog = null;
        private Object mLock = new Object();
        private Stack<MsgInfo> msgStack = new Stack<MsgInfo>();
        private Queue<string> _commandQueue = new Queue<string>();
        private long lastGetSettingsTime = 0;

        private DeviceState()
        {
            if (callback == null)
                callback = new LibControl.SDK_ApiResultCallback(UpdateResult);
            init_param.log_cb = printlog;
            init_param.result_cb = callback;
        }

        //Dispatch events
        public event Action TWSConnected;
        public event Action TWSNotConnected;
        public event Action GetSettingsStarted;
        public event Action AllSettingsLoaded;

        private bool isFetching = false;

        //Device info supported by base API
        private int batteryMaster = 0;
        private int batterySlave = 0;
        private string deviceAgentMac = "";
        private string devicePartnerMac = "";
        private string deviceAgentName = "";
        private string devicePartnerName = "";
        private int gameChatRatio = 0; //0-20;
        private int gameMicVolume = 0; //0-100;


        /**
         * Get settings calling sequence after TWS is connected:
         * LibControl.GetDeviceInfoEx(handle);
         * LibControl.GetBatteryInfoEx(handle);
         * Then after receiving GetBatteryInfoEx(MessageID.BATTERY_STATUS), call:
         *     LibControl.GetGameChatMixRatioEx(handle);
         *     LibControl.GetGameMicVolumeEx(handle);
         *     GetEarBudsSettings();
         */

        private void initialize()
        {
            LibControl.CheckRemoteStatusEx(handle);
            LibControl.GetAgentChannelEx(handle);
            LibControl.GetDeviceTypeEx(handle);
            LibControl.GetAudioFeatureCapability(handle);
            LibControl.GetAncSettingsEx(handle);
        }


        public void UpdateResult(LibControl.Result result)
        {
            lock (mLock)
            {
                MsgInfo info = new MsgInfo();
                info.statusCode = result.statusCode;
                info.msgID = result.msgID;
                info.extra1 = result.extra1;
                info.extra2 = result.extra2;
                info.extra3 = result.extra3;
                info.data3_raw = result.extra3;
                Debug.WriteLine("info:" + info.ToString());
                string message = System.Text.Encoding.Default.GetString(info.extra3).Trim('\0');
                Debug.WriteLine("message:" + message);
                if (message == "3")
                {
                    DispatchTwsConnectedAndGetDeviceInfo();
                }
                if (message == "1")
                {
                    TWSNotConnected?.Invoke();
                }
                msgStack.Push(info);
                ReceiveCallbackMsg(info);
            }
        }

        private void ReceiveCallbackMsg(MsgInfo info)
        {
            switch (info.statusCode)
            {
                case (int)StatusCode.STATUS_SUCCESS:
                    if (info.msgID == (int)MessageID.DEVICE_INFO)
                    {
                        //Parse device info
                        LibControl.DeviceInformation agentInfo = new LibControl.DeviceInformation();
                        LibControl.DeviceInformation partnerInfo = new LibControl.DeviceInformation();
                        LibControl.GetDeviceInfoResultEx(handle, out agentInfo, out partnerInfo);
                        deviceAgentMac = DataConverter.ByteArrToString(agentInfo.device_mac, 64);
                        devicePartnerMac = DataConverter.ByteArrToString(partnerInfo.device_mac, 64);
                        deviceAgentName = DataConverter.ByteArrToString(agentInfo.device_name, 64);
                        devicePartnerName = DataConverter.ByteArrToString(partnerInfo.device_name, 64);
                    }
                    else if (info.msgID == (int)MessageID.TWS_STATUS)
                    {
                        DispatchTwsConnectedAndGetDeviceInfo();
                    }
                    else if (info.msgID == (int)MessageID.BATTERY_STATUS)
                    {
                        //BatteryStatusUpdateUI(info);
                        batteryMaster = (int)info.extra1;
                        batterySlave = (int)info.extra2;
                        Debug.WriteLine("masterLevel: " + batteryMaster);
                        Debug.WriteLine("slaveLevel: " + batterySlave);

                        //Get GameChatMixRatio
                        LibControl.GetGameChatMixRatioEx(handle);
                        //Get GameMicVolume
                        LibControl.GetGameMicVolumeEx(handle);
                        //Get all custom settings
                        GetEarBudsSettings();
                    }
                    else if (info.msgID == (int)MessageID.GAME_CHAT_RATIO)
                    {
                        gameChatRatio = (int)info.extra1;
                        Debug.WriteLine("info.statusCode " + info.statusCode);
                        Debug.WriteLine("Game Chat Mix Ratio: " + info.extra1.ToString() + " (0~20)");
                    }
                    else if (info.msgID == (int)MessageID.GAME_MIC_VOLUME)
                    {
                        gameMicVolume = (int)info.extra1;
                        Debug.WriteLine("info.statusCode " + info.statusCode);
                        Debug.WriteLine("Game Mic Volume: " + info.extra1.ToString() + " (0~100)");
                    }
                    else if (info.msgID == (int)MessageID.DEVICE_TYPE)
                    {
                        //DeviceTypeUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.AGENT_CHANNEL)
                    {
                        //AgentChannelUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.ANC_STATUS)
                    {
                        //ParseAncSettings();
                    }
                    else if (info.msgID == (int)MessageID.AUTO_PAUSE)
                    {
                        //AutoPlayPauseStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SET_AUTO_PAUSE)
                    {
                        //SetAutoPlayPauseStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.AUTO_POWER_OFF)
                    {
                        //AutoPowerOffStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SET_AUTO_POWER_OFF)
                    {
                        //SetAutoPowerOffStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.MULTI_AI_STATUS)
                    {
                        //MultiAiStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SET_MULTI_AI_STATUS)
                    {
                        //SetMultiAiStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SET_FIND_ME_STATUS)
                    {
                        //SetFindMeStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SMART_SWITCH_STATUS)
                    {
                        //SmartSwitchStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SET_SMART_SWITCH_STATUS)
                    {
                        //SetSmartSwitchStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SEALING_STATUS)
                    {
                        //SealingStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.TOUCH_STATUS)
                    {
                        //TouchStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SET_TOUCH_STATUS)
                    {
                        //SetTouchStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SIDETONE_STATE)
                    {
                        //SideToneStateUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SET_SIDETONE_STATE)
                    {
                        //SetSideToneStateUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SET_FACTORY_RESET)
                    {
                        //SetFactoryResetUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.VOICE_PROMPTS_STATUS)
                    {
                        //VoicePromptsStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SET_VOICE_PROMPTS_STATUS)
                    {
                        //SetVoicePromptsStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.ADVANCED_PASSTHROUGH_STATUS)
                    {
                        //AdvancedPassthroughStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.SET_ADVANCED_PASSTHROUGH_STATUS)
                    {
                        //SetAdvancedPassthroughStatusUpdateUI(info);
                    }
                    else if (info.msgID == (int)MessageID.AUDIO_FEATURE_CAPABILITY)
                    {
                        Debug.WriteLine("-- AUDIO_FEATURE_CAPABILITY --");
                        //TODO: This is to simulate TWS is connected as the real API does not work.
                        DispatchTwsConnectedAndGetDeviceInfo();
                    }
                    else if (info.msgID == (int)MessageID.CUSTOM_READ_NV)
                    {
                        //UpdateReadNVUI(info);
                    }
                    else if (info.msgID == (int)MessageID.CUSTOM_WRITE_NV)
                    {
                        //UpdateWriteNVUI(info);
                    }
                    else if (info.msgID == (int)MessageID.CUSTOM_RACE)
                    {
                        //CustomCommandReceived?.Invoke(info);
                        ParseCutomCommandRepone(info);
                    }
                    break;
                case (int)StatusCode.STATUS_NOTIFY:
                    if (info.msgID.Equals((int)MessageID.NOTIFY_FROM_DEVICE))
                    {
                        //NotifyFromDeviceUpdateUI(info);
                    }
                    break;
                case (int)StatusCode.STATUS_TIMEOUT:
                    break;
            }
        }

        private void DispatchTwsConnectedAndGetDeviceInfo()
        {
            connectedDevice = new LEConnectDeviceInfo();
            connectedDevice.is_connect = true;
            TWSConnected?.Invoke();
            //Get device info after TWS is connected
            LibControl.GetDeviceInfoEx(handle);
            GetSettingsStarted?.Invoke();
        }

        private void ParseCutomCommandRepone(MsgInfo info)
        {
            LibControl.CustomCommandReceiverStopEx(handle);
            string cmdHex = "";
            byte[] result = new byte[info.data3_raw.Length];
            for (int i = 0; i < info.extra1; i++)
            {
                result[i] = Convert.ToByte(info.data3_raw[i]);
                cmdHex += result[i].ToString("X2");
            }
            Debug.WriteLine("\nCUSTOM_RACE RECEIVE: " + cmdHex);
            Debug.WriteLine("\nresult: " + string.Join(", ", result));
            Debug.WriteLine("\nresult (hex): " + string.Join(" ", result.Select(b => b.ToString("X2"))));

            if (cmdHex == Commands.SET_SURROUND_RESPONSE ||
                cmdHex == Commands.SET_ELEVATION_RESPONSE ||
                cmdHex == Commands.SET_CIRCLE_SIZE_RESPONSE)
            {
                
            }
            else
            {
                String prefix = cmdHex.Substring(0, 18);
                Debug.WriteLine("prefix: " + prefix);
            }
            FetchNextGetCommand();
        }

        public DongleConnectState ConnectDongle()
        {
            init_param.vid = 0x0E8D;
            init_param.pid = 0x080A; //Aero Max
            init_param.conn_dev_type = (byte)AirohaDeviceType.DONGLE;
            init_param.target_dev_type = (byte)AirohaDeviceControlEnum.LOCAL;

            int ret_handle = LibControl.InitializeAirohaSDKEx(init_param);
            protocol = (int)SDKConnProtocol.HID;
            if (ret_handle == -1)
            {
                Debug.WriteLine("Dongle connect failed!");
                LibControl.CloseAirohaSDKEx();
                return DongleConnectState.DISCONNECTED;
            }
            else
            {
                Debug.WriteLine("Dongle connect OK!");
                handle = ret_handle;
                initialize();
                return DongleConnectState.CONNECTED;
            }
        }

        public int GetBatteryMaster()
        {
            return batteryMaster;
        }

        public int GetBatterySlave()
        {
            return batterySlave;
        }

        public string GetDeviceName()
        {
            return deviceAgentName;
        }

        public string GetDeviceMac()
        {
            return deviceAgentMac;
        }

        public int GetGameChatRatio()
        {
            return gameChatRatio;
        }

        public int GetGameMicVolume()
        {
            return gameMicVolume;
        }

        private void GetEarBudsSettings()
        {
            _commandQueue.Enqueue(Commands.GET_MIC_MUTE);
            _commandQueue.Enqueue(Commands.GET_MIC_RECORDING_TONE);
            _commandQueue.Enqueue(Commands.GET_MIC_AUTO_GAIN);
            FetchNextGetCommand();
        }

        private void SendCustomCommand(String command)
        {
            LibControl.CmdSettings cmd_setting = new LibControl.CmdSettings();
            cmd_setting.target = 1;
            byte[] data = DataConverter.StringToByteArray(command);
            cmd_setting.cmd_length = (ushort)data.Length;
            cmd_setting.resp_type = Convert.ToByte("5B", 16);
            cmd_setting.cmd = new byte[1024];
            Array.Copy(data, 0, cmd_setting.cmd, 0, cmd_setting.cmd_length);
            LibControl.SendCustomCommandEx(handle, cmd_setting);
        }

        private void FetchNextGetCommand()
        {
            _ = Task.Run(async () => await ExecuteNextGetCommand());
        }

        private async Task ExecuteNextGetCommand()
        {
            await Task.Delay(100);
            if (_commandQueue.Count > 0)
            {
                string command = _commandQueue.Dequeue();
                SendCustomCommand(command);
            }
            else
            {
                long current = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                long diff = current - lastGetSettingsTime;
                if (diff < 1000) return;
                lastGetSettingsTime = current;
                AllSettingsLoaded?.Invoke();
            }
        }
    }
}
