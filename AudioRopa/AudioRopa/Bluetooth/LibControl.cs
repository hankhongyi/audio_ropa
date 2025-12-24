/* Copyright Statement:
 *
 * (C) 2020  Airoha Technology Corp. All rights reserved.
 *
 * This software/firmware and related documentation ("Airoha Software") are
 * protected under relevant copyright laws. The information contained herein
 * is confidential and proprietary to Airoha Technology Corp. ("Airoha") and/or its licensors.
 * Without the prior written permission of Airoha and/or its licensors,
 * any reproduction, modification, use or disclosure of Airoha Software,
 * and information contained herein, in whole or in part, shall be strictly prohibited.
 * You may only use, reproduce, modify, or distribute (as applicable) Airoha Software
 * if you have agreed to and been bound by the applicable license agreement with
 * Airoha ("License Agreement") and been granted explicit permission to do so within
 * the License Agreement ("Permitted User").  If you are not a Permitted User,
 * please cease any access or use of Airoha Software immediately.
 * BY OPENING THIS FILE, RECEIVER HEREBY UNEQUIVOCALLY ACKNOWLEDGES AND AGREES
 * THAT AIROHA SOFTWARE RECEIVED FROM AIROHA AND/OR ITS REPRESENTATIVES
 * ARE PROVIDED TO RECEIVER ON AN "AS-IS" BASIS ONLY. AIROHA EXPRESSLY DISCLAIMS ANY AND ALL
 * WARRANTIES, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE OR NONINFRINGEMENT.
 * NEITHER DOES AIROHA PROVIDE ANY WARRANTY WHATSOEVER WITH RESPECT TO THE
 * SOFTWARE OF ANY THIRD PARTY WHICH MAY BE USED BY, INCORPORATED IN, OR
 * SUPPLIED WITH AIROHA SOFTWARE, AND RECEIVER AGREES TO LOOK ONLY TO SUCH
 * THIRD PARTY FOR ANY WARRANTY CLAIM RELATING THERETO. RECEIVER EXPRESSLY ACKNOWLEDGES
 * THAT IT IS RECEIVER'S SOLE RESPONSIBILITY TO OBTAIN FROM ANY THIRD PARTY ALL PROPER LICENSES
 * CONTAINED IN AIROHA SOFTWARE. AIROHA SHALL ALSO NOT BE RESPONSIBLE FOR ANY AIROHA
 * SOFTWARE RELEASES MADE TO RECEIVER'S SPECIFICATION OR TO CONFORM TO A PARTICULAR
 * STANDARD OR OPEN FORUM. RECEIVER'S SOLE AND EXCLUSIVE REMEDY AND AIROHA'S ENTIRE AND
 * CUMULATIVE LIABILITY WITH RESPECT TO AIROHA SOFTWARE RELEASED HEREUNDER WILL BE,
 * AT AIROHA'S OPTION, TO REVISE OR REPLACE AIROHA SOFTWARE AT ISSUE,
 * OR REFUND ANY SOFTWARE LICENSE FEES OR SERVICE CHARGE PAID BY RECEIVER TO
 * AIROHA FOR SUCH AIROHA SOFTWARE AT ISSUE.
 */
/* Airoha restricted information */

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AudioRopa
{
    public class LibControl
    {
        private const string dll_path = "/Bluetooth/AirohaHidCoreDll.dll";

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public delegate void SDK_LogCallback(int log_level, string log);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public delegate void SDK_ResultCallback(int statusCode, int msgID, string msg);

        [DllImport(dll_path, CallingConvention = CallingConvention.StdCall)]
        public static extern void registerSetLogEnqueueCallback(SDK_LogCallback logger);

        [DllImport(dll_path, CallingConvention = CallingConvention.StdCall)]
        public static extern void registerUpdateResultCallback(SDK_ResultCallback cb);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int initializeAirohaSDK(ushort vid, ushort pid);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void requestDFUInfo();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void startDataTransfer();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setDfuMode(int mode);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setBatteryLevel(int level);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setDfuAgentFilepath(byte[] path);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setDfuPartnerFilepath(byte[] path);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void closeAirohaSDK();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void applyNewFirmware(int batteryLevel);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setTargetDevice(byte target);
		
		[DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void clearResource();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void programCustomPartition(byte[] filepath, int partitionAddr, byte storageType, byte deviceRole);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void StopDataTransfer();


        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public delegate void SDK_ApiResultCallback(Result result);

        [DllImport(dll_path, CallingConvention = CallingConvention.StdCall)]
        public static extern void registerUpdateApiResultCallback(SDK_ApiResultCallback cb);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getProductCategory();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getDfuBinPackageInfo(byte[] filename, out FotaPackageInformation head);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setConnectedDeviceType(byte deviceType);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getConnectedDeviceType();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDeviceType();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTwsConnectStatus();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAgentChannel();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetBatteryInfo();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetSidetoneLevel();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSidetoneLevel(int level);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDeviceInfo();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDeviceInfoResult(out DeviceInformation agentInfo, out DeviceInformation partnerInfo);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAncSettings();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAncSettings(AncSettings settings, int save_or_not);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAncSettingsResult(out AncSettings settings);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEqSettingsBySeqNum(int seqNum, out EqSettings eq_settings);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEqSettingsByCategoryId(int categoryId, out EqSettings eq_settings);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAllEQSettings();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetRunningEQSetting();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEQSetting(int categoryId, EqPayload eqPayload, byte saveOrNot);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetGameChatMixRatio();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetGameChatMixRatio(int ratio);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetGameMicVolume();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetGameMicVolume(int volume);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetGestureStatus();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetGestureStatusResult(out GestureSettings gesture_settings);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetGestureStatus(GestureSettings gesture_settings);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetMmiState();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void HandlePhone(int type);

        // LEA
        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ScanRemoteDevice(int handle, ScanParam param);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void StopScanRemoteDevice(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ConnectRemoteDevice(int handle, ConnParam param);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DisconnectRemoteDevice(int handle, ConnParam param);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QueryRemoteDeviceStatus(int handle, ConnParam param);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetRemoteDeviceStatus(int handle, ConnParam param, out RemoteDeviceStatus result);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QueryRemoteDeviceList(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetRemoteDeviceListResult(int handle, int seq_num, out RemoteDeviceStatus result);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QueryRemoteAllDeviceInfoList(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetConnectiontMode(int handle, byte mode);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte GetConnectiontMode(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAudioActiveDevice(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAudioActiveDevice(int handle, ConnParam param);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDevLinkType(int handle, int link_type);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetLEABroadcastDevice(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLEABroadcastDevice(int handle, LeaBroadcastParam param);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RemovePairedRecord(int handle, ConnParam param);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDongleMode(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDongleMode(int handle, byte mode);

        // For multi-dev API*************************************
        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetDeviceHandleEx(InitParam param);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitializeAirohaSDKEx(InitParam param);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitializeAirohaBleSDKEx(InitBleParam param);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitializeAirohaPairedBleSDKEx(InitBleParam param);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CloseAirohaSDKEx();

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ClearResourceEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RequestDFUInfoEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetBatteryLevelEx(int handle, int level);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDfuAgentFilepathEx(int handle, byte[] path);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDfuPartnerFilepathEx(int handle, byte[] path);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetTargetDeviceEx(int handle, byte target);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetConnectedDeviceTypeEx(int handle, byte deviceType);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void StartDataTransferEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void StopDataTransferEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.StdCall)]
        public static extern void RegisterUpdateResultCallbackEx(int handle, SDK_ResultCallback cb);

        [DllImport(dll_path, CallingConvention = CallingConvention.StdCall)]
        public static extern void RegisterSetLogEnqueueCallbackEx(int handle, SDK_LogCallback logger);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ApplyNewFirmwareEx(int handle, int batteryLevel);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetDfuBinPackageInfoEx(int handle, byte[] filename, out FotaPackageInformation head);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetProductCategoryEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEqSettingsBySeqNumEx(int handle, int seqNum, out EqSettings eq_settings);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEqSettingsByCategoryIdEx(int handle, int categoryId, out EqSettings eq_settings);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAllEQSettingsEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetRunningEQSettingEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEQSettingEx(int handle, int categoryId, EqPayload eqPayload, byte saveOrNot);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void GetEQExtraParamEx(int handle, string name, [In, Out]StringBuilder value);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEQExtraParamEx(int handle, string name, string value);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CheckRemoteStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDeviceInfoEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetDeviceInfoResultEx(int handle, out DeviceInformation agentInfo, out DeviceInformation partnerInfo);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetDeviceNameEx(int handle, string deviceName);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTwsConnectStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetBatteryInfoEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDeviceTypeEx(int handle);
        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAgentChannelEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAncSettingsEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAncSettingsResultEx(int handle, out AncSettings settings);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAncSettingsEx(int handle, AncSettings settings, int save_or_not);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAutoPlayPauseStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAutoPlayPauseStatusEx(int handle, byte onoff);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAutoPowerOffStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAutoPowerOffStatusEx(int handle, int interval);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetMultiAIStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMultiAIStatusEx(int handle, int ai);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetFindMyBudsEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetFindMyBudsEx(int handle, int targetChannel, int behavior);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetGestureStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetGestureStatusResultEx(int handle, out GestureSettings gesture_settings);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetGestureStatusEx(int handle, GestureSettings gesture_settings);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDynamicLRGestureStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDynamicLRGestureStatusEx(int handle, GestureSettings gesture_settings);


        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetSealingStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetSmartSwitchStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSmartSwitchStatusEx(int handle, int musicSetting);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTouchStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetTouchStatusEx(int handle, byte onoff);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetSidetoneStateEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSidetoneStateEx(int handle, SidetoneInfo info);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetMmiStateEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void HandlePhoneEx(int handle, int type);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetFactoryResetEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetVoicePromptsStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetVoicePromptsStatusEx(int handle, byte language);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetSoundMaxVolumeEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSoundMaxVolumeEx(int handle, int vol);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetGameChatBalanceSettingsEx(int handle);

        //[DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void GetGameChatBalanceSettingsResultEx(int handle, out UiGameChatBalanceSettings settings);

        //[DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void SetGameChatBalanceSettingsEx(int handle, UiGameChatBalanceSettings settings);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetGameChatMixRatioEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetGameMicVolumeEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetGameChatMixRatioEx(int handle, int ratio);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetGameMicVolumeEx(int handle, int volume);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAdvancedPassthroughStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAdvancedPassthroughStatusEx(int handle, byte onoff);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetEnvironmentDetectionInfoEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetEnvironmentDetectionInfoResultEx(int handle, out EnvironmentDetectionInfo info);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetEnvironmentDetectionStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEnvironmentDetectionStatusEx(int handle, byte onoff);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAdaptiveAncStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAdaptiveAncStatusEx(int handle, byte onoff);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAdaptiveEqStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAdaptiveEqStatusEx(int handle, byte onoff);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetVoiceNoiseCancelingStatusEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetVoiceNoiseCancelingStatusEx(int handle, byte onoff);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetLeaDongleStreamModeEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLeaDongleStreamModeEx(int handle, byte mode);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDongleRssiEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDongleRssiResultEx(int handle, out DongleRssiInfo info);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CustomReadNvEx(int handle, NvSettings setting);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CustomWriteNvEx(int handle, NvSettings setting);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendCustomCommandEx(int handle, CmdSettings setting);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CustomCommandReceiverStopEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void StartLogDumpEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TriggerLogDumpEx(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetWearDetection(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetWearDetection(int handle, byte onoff);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetBusyLightStatus(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetBusyLightStatus(int handle, byte onoff);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetUcProfile(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetUcProfile(int handle, byte uc_app);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetUcAppStatus(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetNotificationActivation(int handle, byte sub_id, ushort map);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetNotificationActivation(int handle, byte sub_id);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetNotificationMap(int handle, byte device_target);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTelemetryData(int handle, byte telemetry_id);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLoopbackData(int handle, LoopbackSettings setting);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetVoiceGuidance(int handle, byte onoff);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetVoiceGuidance(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDisInformation(int handle, byte data_id);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAinrStatus(int handle, byte onoff);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetMultiHostName(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDongleBisName(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDongleBisName(int handle, string bisName);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDongleBisQos(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDongleBisQos(int handle, string qosValue);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDongleBisCode(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDongleBisCode(int handle, string bisCode);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetAudioFeatureCapability(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsHybridPassthruSupported(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsAdaptivePassthruSupported(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsHwVividPassthruSupported(int handle);

        [DllImport(dll_path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDynamicLRStatusEx(int handle);

        [StructLayout(LayoutKind.Sequential)]
        public struct InitParam
        {
            public short vid;
            public short pid;
            public int conn_dev_type;
            public int target_dev_type;
            public SDK_LogCallback log_cb;
            public SDK_ApiResultCallback result_cb;
            public InitParam(short vid, short pid, int conn_dev_type, int target_dev_type,
                SDK_LogCallback log_cb = null, SDK_ApiResultCallback result_cb = null)
            {
                this.vid = vid;
                this.pid = pid;
                this.conn_dev_type = conn_dev_type;
                this.target_dev_type = target_dev_type;
                this.log_cb = log_cb;
                this.result_cb = result_cb;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct InitBleParam
        {
            public int conn_dev_type;
            public int target_dev_type;
            public SDK_LogCallback log_cb;
            public SDK_ApiResultCallback result_cb;
            public string address;
            public int scan_timeout;
            public int device_type;
            public InitBleParam(int conn_dev_type, int target_dev_type, string address, int scan_timeout, int device_type,
                SDK_LogCallback log_cb = null, SDK_ApiResultCallback result_cb = null)
            {
                this.conn_dev_type = conn_dev_type;
                this.target_dev_type = target_dev_type;
                this.log_cb = log_cb;
                this.result_cb = result_cb;
                this.address = address;
                this.scan_timeout = scan_timeout;
                this.device_type = device_type;
            }
        }

        public struct ScanParam
        {
            public UInt16 timeout;
        }

        public struct ConnParam
        {
            public byte type;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] le_address;
            public byte link_type;
        }

        public struct RemoteDeviceStatus
        {
            public byte type;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] le_address;
            public UInt16 device_handle_id;
            public byte group_id;
            public byte role;
            public byte connect;
            public byte link_type;
            public byte channel;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] device_name;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct LocalDevice
        {
            public short handle;
            public int remote_device_count;
            public AirohaProductCategory product_category;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public RemoteDeviceStatus[] remote_device_list;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Result
        {
            public int msgID;
            public int statusCode;
            public double extra1;
            public double extra2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] extra3;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FotaPackageInformation
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] product_category;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] fw_name;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
            public byte[] version;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DeviceInformation
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] device_pid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] device_vid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] device_mid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] firmware_version;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] device_mac;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] device_name;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] device_uid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] target_addr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] role;

            public int battery_info;
            public int connectable;
            public int preferred_protocol;
            public int channel;
            public long scanned_timestamp;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct EqSettings
        {
            public int categoryId;
            public int status;
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            //public ca[] payload;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public EqPayload[] payload;


            //public EQ_SETTINGS(int _categoryId, int _status, EQ_PAYLOAD[] _payload)
            //{
            //    categoryId = _categoryId;
            //    status = _status;
            //    payload = _payload;
            //}
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct EqPayload
        {
            public int index;
            public int bandCount;
            public int sampleRate;
            public double calibration;
            public double leftGain;
            public double rightGain;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public EqParameter[] parameters;

            public EqPayload(int _index, int _bandCount, int _sampleRate, double _calibration, double _leftGain, double _rightGain, EqParameter[] _parameters)
            {
                index = _index;
                bandCount = _bandCount;
                sampleRate = _sampleRate;
                calibration = _calibration;
                leftGain = _leftGain;
                rightGain = _rightGain;
                parameters = _parameters;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct EqParameter
        {
            public int bandType;
            public double gainValue;
            public double frequency;
            public double qValue;

            public EqParameter(int _bandType, double _gainValue, double _frequency, double _qValue)
            {
                bandType = _bandType;
                gainValue = _gainValue;
                frequency = _frequency;
                qValue = _qValue;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct GameChatBalanceSettings
        {
            public int enable;
            public int effective_threshold;
            public int failure_threshold;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AncSettings
        {
            public int filter_index;
            public int anc_mode;
            public double anc_gain;
            public double passthrough_gain;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MultiLangVoicePrompt
        {
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SidetoneInfo
        {
            public byte onoff;
            public int level;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct EnvironmentDetectionInfo
        {
            public int level;
            public int left_stationary_noise;
            public int right_stationary_noise;
            public double ff_gain;
            public double fb_gain;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct GestureSettings
        {
            public int side_id;
            public int single_tap_left_action;
            public int double_tap_left_action;
            public int long_press_left_action;
            public int triple_tap_left_action;
            public int dlong_left_action;
            public int single_tap_right_action;
            public int double_tap_right_action;
            public int long_press_right_action;
            public int triple_tap_right_action;
            public int dlong_right_action;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NvSettings
        {
            public byte target;
            public ushort nv_id;
            public ushort nv_length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] data;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CmdSettings
        {
            public byte target;
            public ushort cmd_length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] cmd;
            public byte resp_type;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LeaBroadcastParam
        {
            public byte count;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] group;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LoopbackSettings
        {
            public byte map_id;
            public ushort map_data;
            public byte checksum;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DongleRssiInfo
        {
            public byte is_bt_valid;
            public double bt_rssi;
            public double bt_per;
            public byte is_ble_valid;
            public double ble_rssi;
            public double ble_per;
        }
    }
}
