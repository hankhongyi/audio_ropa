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
using AudioRopa.Model;

namespace AudioRopa.Utility
{
    static public class DataConverter
    {
        static public String byteArrayToHexString(byte[] Data, string Sep)
        {
            if (Data == null)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < Data.Length; ++i)
            {
                sb.Append(Data[i].ToString("X2"));
                sb.Append(Sep);
            }

            string rtn = sb.ToString();
            return rtn.Substring(0, rtn.Length - Sep.Length);
        }

        static public byte[] hexStringToByteArray(string Hex)
        {
            // 奇數個數char的字串  原本是discard最後一位 e.g. 123 -> dicard 3
            // 改補 padding 0 e.g. 123 -> 0123

            if ((Hex.Length % 2) == 1)
            {
                Hex = "0" + Hex;
            }

            int len = Hex.Length / 2;
            Byte[] aryb = new byte[len];
            for (int i = 0; i < len; ++i)
            {
                aryb[i] = Convert.ToByte(Hex.Substring(i * 2, 2), 16);
            }
            return aryb;
        }

        static public UInt16 bytesToUShort(byte High, byte Low)
        {
            int high = High << 8;

            return Convert.ToUInt16(high + Low);
        }

        static public string ByteArrToString(byte[] arr, int size)
        {
            string retStr = "";
            int str_end = Array.IndexOf(arr, (byte)'\0');
            if (str_end == -1 || str_end >= size)
            {
                retStr = Encoding.UTF8.GetString(arr, 0, size);
            }
            else if (0 < str_end && str_end < size)
            {
                retStr = Encoding.UTF8.GetString(arr, 0, str_end);
            }
            return retStr;
        }

        static public byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
