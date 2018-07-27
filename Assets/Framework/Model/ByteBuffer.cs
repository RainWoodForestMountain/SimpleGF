﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
using LuaInterface;

namespace GameFramework
{
    public class ByteBuffer
    {
        MemoryStream stream = null;
        BinaryWriter writer = null;
        BinaryReader reader = null;
        bool needLength = true;

        public ByteBuffer() {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        public ByteBuffer(bool _nl)
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
            needLength = _nl;
        }

        public ByteBuffer(byte[] data) {
            if (data != null) {
                stream = new MemoryStream(data);
                reader = new BinaryReader(stream);
            } else {
                stream = new MemoryStream();
                writer = new BinaryWriter(stream);
            }
        }

        public void Close() {
            if (writer != null) writer.Close();
            if (reader != null) reader.Close();

            stream.Close();
            writer = null;
            reader = null;
            stream = null;
        }

        public void Write(byte[] v)
        {
            writer.Write(v);
        }
        public void WriteByte(byte v) {
            writer.Write(v);
        }

        public void WriteInt(int v) {
            writer.Write((int)v);
        }

        public void WriteShort(ushort v) {
            writer.Write((ushort)v);
        }

        public void WriteLong(long v) {
            writer.Write((long)v);
        }

        public void WriteFloat(float v) {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            writer.Write(BitConverter.ToSingle(temp, 0));
        }

        public void WriteDouble(double v) {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            writer.Write(BitConverter.ToDouble(temp, 0));
        }

        public void WriteString(string v) {
            byte[] bytes = Encoding.UTF8.GetBytes(v);
            if (needLength) writer.Write((int)bytes.Length);
            writer.Write(bytes);
        }

        public void WriteBytes(byte[] v) {
            if (needLength) writer.Write((int)v.Length);
            writer.Write(v);
        }

        public void WriteBuffer(LuaByteBuffer strBuffer) {
            WriteBytes(strBuffer.buffer);
        }
        
        public byte ReadByte() {
            return reader.ReadByte();
        }

        public int ReadInt() {
            return (int)reader.ReadInt32();
        }

        public ushort ReadShort() {
            return (ushort)reader.ReadInt16();
        }

        public long ReadLong() {
            return (long)reader.ReadInt64();
        }

        public float ReadFloat() {
            byte[] temp = BitConverter.GetBytes(reader.ReadSingle());
            Array.Reverse(temp);
            return BitConverter.ToSingle(temp, 0);
        }

        public double ReadDouble() {
            byte[] temp = BitConverter.GetBytes(reader.ReadDouble());
            Array.Reverse(temp);
            return BitConverter.ToDouble(temp, 0);
        }

        public string ReadString() {
            int len = ReadInt();
            byte[] buffer = new byte[len];
            buffer = reader.ReadBytes(len);
            return Encoding.UTF8.GetString(buffer);
        }
        public string ReadString(int _length)
        {
            byte[] buffer = new byte[_length];
            buffer = reader.ReadBytes(_length);
            return Encoding.UTF8.GetString(buffer);
        }

        public byte[] ReadBytes()
        {
            int len = ReadInt();
            return reader.ReadBytes(len);
        }
        public byte[] ReadBytes(int _length)
        {
            return reader.ReadBytes(_length);
        }

        public LuaByteBuffer ReadBuffer() {
            byte[] bytes = ReadBytes();
            return new LuaByteBuffer(bytes);
        }
        public LuaByteBuffer ReadBuffer(int _length)
        {
            byte[] bytes = ReadBytes(_length);
            return new LuaByteBuffer(bytes);
        }

        public byte[] ToBytes() {
            if (writer != null) writer.Flush();
            return stream.ToArray();
        }

        public void Flush() {
            if (writer != null) writer.Flush();
            if (reader == null) reader = new BinaryReader(stream);
        }
    }
}