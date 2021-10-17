using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O_ComTool_Pro
{
    public partial class Check : Form
    {
        public Check()
        {
            InitializeComponent();
            cmbWaySelect.SelectedIndex = 0;
        }

        byte CheckSum(byte[] buffer, uint length)
        {
            byte CS = 0;
            for (uint i = 0; i < length; i++)
                CS += buffer[i];
            return CS;
        }

        byte XOR(byte[] buffer, uint length)
        {
            byte xor = 0;
            for (uint i = 0; i < length; i++)
                xor ^= buffer[i];
            return xor;
        }

        public uint crc16_modbus(byte[] modbusdata, uint length)
        {
            uint i, j;
            uint crc16 = 0xffff;
            for (i = 0; i < length; i++)
            {
                crc16 ^= modbusdata[i];
                for (j = 0; j < 8; j++)
                {
                    if ((crc16 & 0x01) == 1)
                    {
                        crc16 = (crc16 >> 1) ^ 0xA001;
                    }
                    else
                    {
                        crc16 = crc16 >> 1;
                    }
                }
            }
            return crc16;
        }

        ulong[] Crc32Table = new ulong[256];
        public void GetCRC32Table()
        {
            ulong Crc;
            int i, j;
            for (i = 0; i < 256; i++)
            {
                Crc = (ulong)i;
                for (j = 8; j > 0; j--)
                {
                    if ((Crc & 1) == 1)
                        Crc = (Crc >> 1) ^ 0xEDB88320;
                    else
                        Crc >>= 1;
                }
                Crc32Table[i] = Crc;
            }
        }

        ulong crc32_calc(byte[] data, uint len)
        {
            ulong value = 0xffffffff;
            GetCRC32Table();
            for (int i = 0; i < len; i++)
            {
                value = (value >> 8) ^ Crc32Table[(value & 0xFF) ^ data[i]];
            }
            return value ^ 0xffffffff;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            uint i = 0;
            MatchCollection mc = Regex.Matches(txtData.Text, @"(?i)[\da-f]{2}");
            byte[] bytesToCheck = new byte[mc.Count];
            foreach (Match m in mc)//遍历所有mc，并将其转换成十六进制
            {
                bytesToCheck[i++] = byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber);//赋值并累加
            }
            switch (cmbWaySelect.SelectedIndex)
            {
                case 0:
                    txtResault.Text = "0x" + CheckSum(bytesToCheck, i).ToString("X2");
                    break;
                case 1:
                    txtResault.Text = "0x" + XOR(bytesToCheck, i).ToString("X2");
                    break;
                case 2:
                    txtResault.Text = "0x" + crc16_modbus(bytesToCheck, i).ToString("X4");
                    break;
                case 3:
                    txtResault.Text = "0x" + crc32_calc(bytesToCheck, i).ToString("X8");
                    break;

            }
        }

    }
}
