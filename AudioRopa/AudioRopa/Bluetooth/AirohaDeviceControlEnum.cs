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

namespace AudioRopa
{
    public enum AirohaDeviceControlEnum
    {
        LOCAL,
        REMOTE,
        LOCAL_RELAY_TO_PARTNER,
        REMOTE_RELAY_TO_PARTNER,
        DUAL,
        TWS,
        REMOTE_DUAL,
        BTHID,
        BTHID_TWS,
        BLE,
        BLE_TWS,
    }

    public enum AirohaDeviceType
    {
        UNKNOWN = -1,
        HEADSET = 0,
        EARBUDS = 1,
        DONGLE = 2,
    }
    public enum AirohaAudioChannel
    {
        UNKNOWN = -1,
        LEFT = 0,
        RIGHT = 1,
    }
    public enum AirohaConnectionStatus
    {
        UNKNOWN = -1,
        DISCONNECTED = 0,
        CONNECTED = 1,
    }

    public enum AirohaFotaPackageType
    {
        UNKNOWN = -1,
        HEADSET = 0,
        EARBUDS = 1,
        DONGLE = 2,
        SPEAKER = 3,
        GAMEPAD = 4,
    }

    public enum AirohaProductCategory
    {
        AB1565_ULL1_DONGLE,  // ULL dongle
        HEADSET,             // headset
        AB1565_LEA_DONGLE,  // LEA dongle
        AB1565_DUAL_DONGLE, // Dual mode dongle
        AB1568_dual_chip,   // dual chip 68
        AB1565_dual_chip,   // dual chip 65
        AB158x_HEADSET,     // headset 85 88
        AB158x_EARBUDS,     // earbuds 85 88
        AB158x_LEA_DONGLE,  // LEA dongle 65
        AB158x_ULL2_DONGLE, // ULL 2.0 dongle
        AB1565_ULL2_DONGLE, // ULL 2.0 dongle
        AB1585_dual_chip_BT,
        AB1585_dual_chip_ULL2_USB,
        AB1565_Earbuds,     // earbuds 65
        AB1568_Earbuds,     // earbuds 68
        AB1568_ULL2_DONGLE, // ULL 2.0 dongle
        AB157x_ULL_DONGLE,
        AB157x_LEA_DONGLE,
        AB157x_Earbuds,
        AB157x_Headset,
        AB1565_dual_chip_BT,
        AB1577am_dual_chip_master,
        AB1571_dual_chip_slave,
        AB1565_8m_dual_chip_master,
        AB157x_Dongle,
        AB158x_Dongle,
        AB158x_Dual_Chip_Master,
        AB159x_Dongle,
        AB159x_Headset,
        AB159x_Earbuds,
        AB157x_Speaker,
        AB157x_Gamepad,
        AB162x_Headset,
        AB162x_Earbuds,
        AB162x_Dongle,
        AB162x_Gamepad,
        UNKNOWN,
    }
    public enum AncFilter
    {
        OFF,
        ANC1,
        ANC2,
        ANC3,
        PassThrough1,
        PassThrough2,
        PassThrough3,
        ANC4,
    }

    public enum AudioChannel
    {
        UNKNOWN = -1,
        NONE_CHANNEL = 0,
        STEREO_LEFT = 1,
        STEREO_RIGHT = 2,
    }

    public enum ConnectionProtocol
    {
        PROTOCOL_UNKNOWN = 0,
        PROTOCOL_BLE = (int)0x00000001,
        PROTOCOL_SPP = (int)0x00000010,
        PROTOCOL_WIFI = (int)0x00000100,
        PROTOCOL_CABLE = (int)0x00001000,
        PROTOCOL_USB = (int)0x00010000,
    }
    public enum HandlePhoneType
    {
        ACCEPT,
        REJECT,
        END,
    }
    
    public enum AirohaMmiState
    {
        //0
        BT_OFF,
        DISCONNECTED,
        CONNECTABLE,
        CONNECTED,
        HFP_INCOMING,
        //5
        HFP_OUTGOING,
        HFP_CALLACTIVE,
        HFP_CALLACTIVE_WITHOUT_SCO,
        HFP_TWC_INCOMING,
        HFP_TWC_OUTGOING,
        //10
        HFP_MULTITPART_CALL,
        WIRED_MUSIC_PLAY = 16,
        A2DP_PLAYING = 24,
        //25
        STATE_HELD_ACTIVE,
        STATE_FIND_ME,
        STATE_VA,
        ULTRA_LOW_LATENCY_PLAYING,
        STATE_RESERVED5,
        STATE_RESERVED6,
        //30
        STATE_RESERVED7,
        TOTAL_STATE_NO
    }

    public enum AirohaNotifyModuleId
    {
        NOTIFY_EQ_INDEX = 0,
        NOTIFY_ANC_STATUS = 5,
        NOTIFY_CUSTOMER_BASE = 100,
        NOTIFY_AGENT_BATTERY = 101,
        NOTIFY_PARTNER_BATTERY = 102,
        NOTIFY_BOX_BATTERY = 103,
        NOTIFY_AWS_STATE = 104,
        NOTIFY_TOUCH_STATE = 200,
        NOTIFY_SIDETONE_LEVEL = 206,
        NOTIFY_SIDETONE_ONOFF = 207,
        NOTIFY_MMI_STATE = 301,
        NOTIFY_RHO = 401,
    }

    public enum AirohaDeviceRole
    {
        UNKNOWN = 0xFF,
        NONE = 0x00,
        CLIENT = 0x10,
        PARTNER = 0x20,
        AGENT = 0x40,
    }

    public enum AirohaConnectionMode
    {
        NONE,
        MULTI_DEVICES,
        COEXIST,
        SINGLE,
        COEXIST_MULTI_DEVICES,
    }

    public enum AirohaLinkType
    {
        NONE,
        LEA,
        ULL,
        BT,
        GAMEPAD = 8,
    }

    public enum AirohaDongleMode
    {
        DUAL_MODE = 0x0C,
        BT_MODE = 0x09,
        LEA_MODE = 0x02,
    }

    public enum SDKConnProtocol
    {
        SPP,
        GATT,
        UART,
        HID,
        PC_BLE,
        UNKNOWN
    }

    public enum DongleConnectState
    {
        CONNECTED,
        DISCONNECTED
    }
}
