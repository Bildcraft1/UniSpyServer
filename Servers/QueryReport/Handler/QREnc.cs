﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QueryReport.Handler
{
    public class QREnc
    {
        public static byte GSValFunc(int reg)
        {

            if (reg < 26)
                return (byte)(reg + 'A');
            if (reg < 52)
                return (byte)(reg + 'G');
            if (reg < 62)
                return (byte)(reg - 4);
            if (reg == 62)
                return (byte)('+');
            if (reg == 63)
                return (byte)('/');

            return (0);
        }


        public static byte[] GSSecKey(byte[] dst, byte[] src, byte[] key, int enctype)
        {
            int i, size, keysz;
            byte[] enctmp = new byte[256];
            byte[] tmp = new byte[66];
            byte x, y, z, a, b;
            //byte[] p;
            byte[] enctype1_data =
        {
                0x01,0xba,0xfa,0xb2,0x51,0x00,0x54,0x80,0x75,0x16,0x8e,0x8e,0x02,0x08,0x36,
                0xa5,0x2d,0x05,0x0d,0x16,0x52,0x07,0xb4,0x22,0x8c,0xe9,0x09,0xd6,0xb9,0x26,
                0x00,0x04,0x06,0x05,0x00,0x13,0x18,0xc4,0x1e,0x5b,0x1d,0x76,0x74,0xfc,0x50,
                0x51,0x06,0x16,0x00,0x51,0x28,0x00,0x04,0x0a,0x29,0x78,0x51,0x00,0x01,0x11,
                0x52,0x16,0x06,0x4a,0x20,0x84,0x01,0xa2,0x1e,0x16,0x47,0x16,0x32,0x51,0x9a,
                0xc4,0x03,0x2a,0x73,0xe1,0x2d,0x4f,0x18,0x4b,0x93,0x4c,0x0f,0x39,0x0a,0x00,
                0x04,0xc0,0x12,0x0c,0x9a,0x5e,0x02,0xb3,0x18,0xb8,0x07,0x0c,0xcd,0x21,0x05,
                0xc0,0xa9,0x41,0x43,0x04,0x3c,0x52,0x75,0xec,0x98,0x80,0x1d,0x08,0x02,0x1d,
                0x58,0x84,0x01,0x4e,0x3b,0x6a,0x53,0x7a,0x55,0x56,0x57,0x1e,0x7f,0xec,0xb8,
                0xad,0x00,0x70,0x1f,0x82,0xd8,0xfc,0x97,0x8b,0xf0,0x83,0xfe,0x0e,0x76,0x03,
                0xbe,0x39,0x29,0x77,0x30,0xe0,0x2b,0xff,0xb7,0x9e,0x01,0x04,0xf8,0x01,0x0e,
                0xe8,0x53,0xff,0x94,0x0c,0xb2,0x45,0x9e,0x0a,0xc7,0x06,0x18,0x01,0x64,0xb0,
                0x03,0x98,0x01,0xeb,0x02,0xb0,0x01,0xb4,0x12,0x49,0x07,0x1f,0x5f,0x5e,0x5d,
                0xa0,0x4f,0x5b,0xa0,0x5a,0x59,0x58,0xcf,0x52,0x54,0xd0,0xb8,0x34,0x02,0xfc,
                0x0e,0x42,0x29,0xb8,0xda,0x00,0xba,0xb1,0xf0,0x12,0xfd,0x23,0xae,0xb6,0x45,
                0xa9,0xbb,0x06,0xb8,0x88,0x14,0x24,0xa9,0x00,0x14,0xcb,0x24,0x12,0xae,0xcc,
                0x57,0x56,0xee,0xfd,0x08,0x30,0xd9,0xfd,0x8b,0x3e,0x0a,0x84,0x46,0xfa,0x77,0xb8
            };
            size = src.Length;
            if (size < 1 || size > 65)
            {
                dst[0] = 0;
                return dst;
            }
            keysz = key.Length;
            for (i = 0; i < 256; i++)
            {
                enctmp[i] = (byte)i;
            }
            a = 0; b = 0;
            for (i = 0;src[i]!=0; i++)
            {
                a += (byte)(src[i] +1);
                x = enctmp[a];
                b += x;
                y = enctmp[b];
                enctmp[b] = x;
                enctmp[a] = y;
                tmp[i] = (byte)(src[i] ^ enctmp[(x + y) & 0xff]);

            }
            for (size = i; size % 3!=0; size++)
            {
                tmp[size] = 0;
            }
            if (enctype == 1)
            {
                for (i = 0; i < size; i++)
                {
                    tmp[i] = enctype1_data[tmp[i]];
                }
            }
            else if (enctype == 2)
            {
                for (i = 0; i < size; i++)
                {
                    tmp[i] ^= key[i % keysz];
                }
            }

            
            for (i = 0; i < size; i += 3)
            {
                x = tmp[i];
                y = tmp[i + 1];
                z = tmp[i + 2];
                dst[i++] = GSValFunc(x >> 2);
                dst[i++] = GSValFunc(((x & 3) << 4) | (y >> 4));
                dst[i++] = GSValFunc(((y & 15) << 2) | (z >> 6));
                dst[i++] = GSValFunc(z & 63);
            }
            return dst;
        }
    }
}