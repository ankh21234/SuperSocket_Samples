﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;

namespace CommonServerLib
{
    public class ServerPacketData
    {
        public string SessionID;

        public Int32 PacketID;
        public Int16 Value1;        
        public Int16 Value2;
        public byte[] BodyData;
        
        public void Assign(string sessionID, EFBinaryRequestInfo reqInfo)
        {
            SessionID = sessionID;

            PacketID = reqInfo.PacketID;
            Value1 = reqInfo.Value1;
            Value2 = reqInfo.Value2;
            
            if (reqInfo.Body.Length > 0)
            {
                BodyData = reqInfo.Body;
            }
        }

        public void Assign(DBResultQueue DBResult)
        {
            SessionID = DBResult.SessionID;

            PacketID = (short)DBResult.PacketID;
            Value1 = DBResult.LobbyID;
            Value2 = 0;
            BodyData = DBResult.Datas;
        }

        public static ServerPacketData MakeNTFInConnectOrDisConnectClientPacket(bool isConnect, string sessionID)
        {
            var packet = new ServerPacketData();
            
            if (isConnect)
            {
                packet.PacketID = (Int32)PACKETID.NTF_IN_CONNECT_CLIENT;
            }
            else
            {
                packet.PacketID = (Int32)PACKETID.NTF_IN_DISCONNECT_CLIENT;
            }

            packet.SessionID = sessionID;
            packet.Value1 = 0;
            packet.Value2 = 0;            
            return packet;
        }

        //public static ServerPacketData MakeNTFWrongUserPacket(WRONG_USER_TYPE type, string sessionID)
        //{
        //    var packet = new ServerPacketData();
        //    packet.PacketID = (Int32)PACKETID.SYSTEM_WRONG_USER;
        //    packet.SessionID = sessionID;
        //    packet.Value1 = 0;
        //    packet.Value2 = 0;
        //    packet.JsonFormatData = ((short)type).ToString();
        //    return packet;
        //}
    }

    
}
