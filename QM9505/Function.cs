using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QM9505
{
    public class Function
    {
        Motion motion = new Motion();
        Method method = new Method();
        private object threadLock;
        double pValue;     //返回值
        uint pClock;       //时钟信号
        public Function()
        {
            threadLock = new object();
        }

        public void ReadIO()
        {
            while (true)
            {
                InSenser();
                OutSenser();

                #region 读取IO刷新

                //读取板卡上输入点
                mc.GTN_GetDiEx(1, mc.MC_GPI, out Variable.XValue_0[0], 3);//主卡通用输入(核1)16,16,16
                mc.GTN_GetDi(2, mc.MC_GPI, out Variable.XValue_1[0]);      //主卡通用输入(核2)    16
                mc.GTN_GetExtDi(1, 1, out Variable.XValue[0]);          //通用输入(扩展卡1-32)    32
                mc.GTN_GetExtDi(1, 33, out Variable.XValue[1]);         //通用输入(扩展卡33-64)   32
                mc.GTN_GetExtDi(1, 65, out Variable.XValue[2]);         //通用输入(扩展卡65-96)   32
                mc.GTN_GetExtDi(1, 97, out Variable.XValue[3]);         //通用输入(扩展卡97-128)  32
                mc.GTN_GetExtDi(1, 129, out Variable.XValue[4]);         //通用输入(扩展卡129-160)  32
                mc.GTN_GetExtDi(1, 161, out Variable.XValue[5]);         //通用输入(扩展卡161-192)  32
                mc.GTN_GetExtDi(1, 193, out Variable.XValue[6]);         //通用输入(扩展卡193-224)  32
                mc.GTN_GetExtDi(1, 225, out Variable.XValue[7]);         //通用输入(扩展卡225-256)  32
                mc.GTN_GetExtDi(1, 257, out Variable.XValue[8]);         //通用输入(扩展卡257-288)  32
                mc.GTN_GetExtDi(1, 289, out Variable.XValue[9]);         //通用输入(扩展卡289-320)  32
                mc.GTN_GetExtDi(1, 321, out Variable.XValue[10]);         //通用输入(扩展卡321-352)  32
                mc.GTN_GetExtDi(1, 353, out Variable.XValue[11]);         //通用输入(扩展卡353-384)  32
                mc.GTN_GetExtDi(1, 385, out Variable.XValue[12]);         //通用输入(扩展卡386-416)  32
                mc.GTN_GetExtDi(1, 417, out Variable.XValue[13]);         //通用输入(扩展卡417-448)  32

                //读取板卡上输出点
                mc.GTN_GetDo(1, mc.MC_GPO, out Variable.YValue_0[0]);//主卡通用输出(核1)  10,10,10
                mc.GTN_GetDo(2, mc.MC_GPO, out Variable.YValue_1[0]);//主卡通用输出(核2)       10
                mc.GTN_GetExtDo(1, 1, out Variable.YValue[0]);       //通用输出(扩展卡1-32)    32
                mc.GTN_GetExtDo(1, 33, out Variable.YValue[1]);      //通用输出(扩展卡33-64)   32
                mc.GTN_GetExtDo(1, 65, out Variable.YValue[2]);      //通用输出(扩展卡65-96)   32
                mc.GTN_GetExtDo(1, 97, out Variable.YValue[3]);      //通用输出(扩展卡97-128)  32
                mc.GTN_GetExtDo(1, 129, out Variable.YValue[4]);      //通用输出(扩展卡129-160)  32
                mc.GTN_GetExtDo(1, 161, out Variable.YValue[5]);         //通用输出(扩展卡161-192)  32
                mc.GTN_GetExtDo(1, 193, out Variable.YValue[6]);         //通用输出(扩展卡193-224)  32
                mc.GTN_GetExtDo(1, 225, out Variable.YValue[7]);         //通用输出(扩展卡225-256)  32
                mc.GTN_GetExtDo(1, 257, out Variable.YValue[8]);         //通用输出(扩展卡257-288)  32
                mc.GTN_GetExtDo(1, 289, out Variable.YValue[9]);         //通用输出(扩展卡289-320)  32
                mc.GTN_GetExtDo(1, 321, out Variable.YValue[10]);         //通用输出(扩展卡321-352)  32
                mc.GTN_GetExtDo(1, 353, out Variable.YValue[11]);         //通用输出(扩展卡353-384)  32
                mc.GTN_GetExtDo(1, 385, out Variable.YValue[12]);         //通用输出(扩展卡385-416)  32
                mc.GTN_GetExtDo(1, 417, out Variable.YValue[13]);         //通用输出(扩展卡417-448)  32

                //轴原点
                mc.GTN_GetDi(1, mc.MC_HOME, out Variable.Home1);   //核1原点信号
                mc.GTN_GetDi(2, mc.MC_HOME, out Variable.Home2);   //核2原点信号

                //正极限
                mc.GTN_GetDi(1, mc.MC_LIMIT_POSITIVE, out Variable.Plimit1);   //核1正限位信号
                mc.GTN_GetDi(2, mc.MC_LIMIT_POSITIVE, out Variable.Plimit2);   //核2正限位信号

                //负极限
                mc.GTN_GetDi(1, mc.MC_LIMIT_NEGATIVE, out Variable.Nlimit1);   //核1负限位信号
                mc.GTN_GetDi(2, mc.MC_LIMIT_NEGATIVE, out Variable.Nlimit2);   //核2负限位信号

                //报警
                mc.GTN_GetDi(1, mc.MC_ALARM, out Variable.Alarm1);   //核1报警信号
                mc.GTN_GetDi(2, mc.MC_ALARM, out Variable.Alarm2);   //核2报警位信号              

                #endregion

                #region 读取轴规划位置

                for (int i = 1; i < 13; i++)
                {
                    mc.GTN_GetPrfPos(1, (short)i, out pValue, 1, out pClock);   //读取轴1实际位置
                    Variable.AIMpos[i] = Math.Round((pValue / Variable.AxisPulse[i]) * Variable.AxisPitch[i], 2);
                }
                for (int i = 1; i < 5; i++)
                {
                    mc.GTN_GetPrfPos(2, (short)i, out pValue, 1, out pClock);   //读取轴1实际位置
                    Variable.AIMpos[12 + i] = Math.Round((pValue / Variable.AxisPulse[12 + i]) * Variable.AxisPitch[12 + i], 2);
                }

                #endregion

                #region 读取轴实际位置
                for (int i = 1; i < 13; i++)
                {
                    mc.GTN_GetEncPos(1, (short)i, out pValue, 1, out pClock);   //读取轴1实际位置
                    Variable.REApos[i] = Math.Round((pValue / Variable.AxisPulse[i]) * Variable.AxisPitch[i], 2);
                }
                for (int i = 1; i < 5; i++)
                {
                    mc.GTN_GetEncPos(2, (short)i, out pValue, 1, out pClock);   //读取轴1实际位置
                    Variable.REApos[12 + i] = Math.Round((pValue / Variable.AxisPulse[12 + i]) * Variable.AxisPitch[12 + i], 2);
                }


                #endregion

                Thread.Sleep(1);
            }
        }

        #region 输入信号
        public void InSenser()
        {
            //主卡1          
            Variable.XStatus[0] = Variable.StartButton = GetBit0(IntToBin32(Variable.XValue_0[0], 0));
            Variable.XStatus[1] = Variable.PauseButton = GetBit0(IntToBin32(Variable.XValue_0[0], 1));
            Variable.XStatus[2] = Variable.AlarmClrButton = GetBit0(IntToBin32(Variable.XValue_0[0], 2));
            Variable.XStatus[3] = Variable.OneCycleButton = GetBit0(IntToBin32(Variable.XValue_0[0], 3));
            Variable.XStatus[4] = Variable.CleanOutButton = GetBit0(IntToBin32(Variable.XValue_0[0], 4));
            Variable.XStatus[5] = Variable.ZeroButton = GetBit0(IntToBin32(Variable.XValue_0[0], 5));
            Variable.XStatus[6] = Variable.XStatus[6] = GetBit0(IntToBin32(Variable.XValue_0[0], 6));
            Variable.XStatus[7] = GetBit0(IntToBin32(Variable.XValue_0[0], 7));
            Variable.XStatus[8] = GetBit0(IntToBin32(Variable.XValue_0[0], 8));
            Variable.XStatus[9] = GetBit0(IntToBin32(Variable.XValue_0[0], 9));
            Variable.XStatus[10] = Variable.EMG = !GetBit0(IntToBin32(Variable.XValue_0[0], 10));
            Variable.XStatus[11] = GetBit0(IntToBin32(Variable.XValue_0[0], 11));
            Variable.XStatus[12] = GetBit0(IntToBin32(Variable.XValue_0[0], 12));
            Variable.XStatus[13] = GetBit0(IntToBin32(Variable.XValue_0[0], 13));
            Variable.XStatus[14] = GetBit0(IntToBin32(Variable.XValue_0[0], 14));
            Variable.XStatus[15] = GetBit0(IntToBin32(Variable.XValue_0[0], 15));

            //主卡4         
            Variable.XStatus[16] = GetBit0(IntToBin32(Variable.XValue_1[0], 0));
            Variable.XStatus[17] = GetBit0(IntToBin32(Variable.XValue_1[0], 1));
            Variable.XStatus[18] = GetBit0(IntToBin32(Variable.XValue_1[0], 2));
            Variable.XStatus[19] = GetBit0(IntToBin32(Variable.XValue_1[0], 3));
            Variable.XStatus[20] = GetBit0(IntToBin32(Variable.XValue_1[0], 4));
            Variable.XStatus[21] = GetBit0(IntToBin32(Variable.XValue_1[0], 5));
            Variable.XStatus[22] = GetBit0(IntToBin32(Variable.XValue_1[0], 6));
            Variable.XStatus[23] = GetBit0(IntToBin32(Variable.XValue_1[0], 7));
            Variable.XStatus[24] = GetBit0(IntToBin32(Variable.XValue_1[0], 8));
            Variable.XStatus[25] = GetBit0(IntToBin32(Variable.XValue_1[0], 9));
            Variable.XStatus[26] = GetBit0(IntToBin32(Variable.XValue_1[0], 10));
            Variable.XStatus[27] = GetBit0(IntToBin32(Variable.XValue_1[0], 11));
            Variable.XStatus[28] = GetBit0(IntToBin32(Variable.XValue_1[0], 12));
            Variable.XStatus[29] = GetBit0(IntToBin32(Variable.XValue_1[0], 13));
            Variable.XStatus[30] = GetBit0(IntToBin32(Variable.XValue_1[0], 14));
            Variable.XStatus[31] = GetBit0(IntToBin32(Variable.XValue_1[0], 15));

            for (int b = 0; b < 13; b++)//模块输入信息
            {
                for (int a = 0; a < 32; a++)
                {
                    if (b == 0)//X032--X063
                    {
                        if (a == 1 || a == 6 || a == 12 || a == 15 || a == 20)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                    else if (b == 1)//X064--X095
                    {
                        if (a == 1 || a == 5 || a == 10 || a == 15 || a == 23 || a == 27)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                    else if (b == 2)//X096--X127
                    {
                        if (a == 25)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                    else if (b == 3)//X128--X159
                    {
                        if (a == 7 || a == 25)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                    else if (b == 4)//X160--X191
                    {
                        if (a == 7 || a == 25)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                    else if (b == 5)//X192--X223
                    {
                        if (a == 7 || a == 25)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                    else if (b == 6)//X224--X255
                    {
                        if (a == 7 || a == 25)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                    else if (b == 7)//X256--X287
                    {
                        if (a == 7 || a == 25)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                    else if (b == 8)//X288--X319
                    {
                        if (a == 7 || a == 25)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                    else if (b == 9)//X320--X351
                    {
                        if (a == 7 || a == 25)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                    else if (b == 10)//X352--X383
                    {
                        if (a == 7 || a == 25)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                    else if (b == 11)//X384--X415
                    {
                        if (a == 7 || a == 25)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                    else//X416--X447
                    {
                        if (a == 7)
                        {
                            Variable.XStatus[32 * (b + 1) + a] = !GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                        else
                        {
                            Variable.XStatus[32 * (b + 1) + a] = GetBit1(IntToBin32(Variable.XValue[b], a));
                        }
                    }
                }
            }
        }

        #endregion

        #region 输出信号
        public void OutSenser()
        {
            //主卡1          
            Variable.YStatus[0] = GetBit0(IntToBin32(Variable.YValue_0[0], 0));
            Variable.YStatus[1] = GetBit0(IntToBin32(Variable.YValue_0[0], 1));
            Variable.YStatus[2] = GetBit0(IntToBin32(Variable.YValue_0[0], 2));
            Variable.YStatus[3] = GetBit0(IntToBin32(Variable.YValue_0[0], 3));
            Variable.YStatus[4] = GetBit0(IntToBin32(Variable.YValue_0[0], 4));
            Variable.YStatus[5] = GetBit0(IntToBin32(Variable.YValue_0[0], 5));
            Variable.YStatus[6] = GetBit0(IntToBin32(Variable.YValue_0[0], 6));
            Variable.YStatus[7] = GetBit0(IntToBin32(Variable.YValue_0[0], 7));
            Variable.YStatus[8] = GetBit0(IntToBin32(Variable.YValue_0[0], 8));
            Variable.YStatus[9] = GetBit0(IntToBin32(Variable.YValue_0[0], 9));

            //主卡4  
            Variable.YStatus[10] = GetBit0(IntToBin32(Variable.YValue_1[0], 0));
            Variable.YStatus[11] = GetBit0(IntToBin32(Variable.YValue_1[0], 1));
            Variable.YStatus[12] = GetBit0(IntToBin32(Variable.YValue_1[0], 2));
            Variable.YStatus[13] = GetBit0(IntToBin32(Variable.YValue_1[0], 3));
            Variable.YStatus[14] = GetBit0(IntToBin32(Variable.YValue_1[0], 4));
            Variable.YStatus[15] = GetBit0(IntToBin32(Variable.YValue_1[0], 5));
            Variable.YStatus[16] = GetBit0(IntToBin32(Variable.YValue_1[0], 6));
            Variable.YStatus[17] = GetBit0(IntToBin32(Variable.YValue_1[0], 7));
            Variable.YStatus[18] = GetBit0(IntToBin32(Variable.YValue_1[0], 8));
            Variable.YStatus[19] = GetBit0(IntToBin32(Variable.YValue_1[0], 9));

            for (int b = 0; b < 13; b++)//模块输入信息
            {
                for (int a = 0; a < 32; a++)
                {
                    if (b == 0)//Y020--Y051
                    {
                        Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                    }
                    else if (b == 1)//Y052--Y083
                    {
                        Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                    }
                    else if (b == 2)//Y084--Y115
                    {
                        Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                    }
                    else if (b == 3)//Y116--Y147
                    {
                        Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                    }
                    else if (b == 4)//Y148--Y179
                    {
                        Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                    }
                    else if (b == 5)//Y180--Y211
                    {
                        Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                    }
                    else if (b == 6)//Y212--Y243
                    {
                        Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                    }
                    else if (b == 7)//Y244--Y275
                    {
                        Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                    }
                    else if (b == 8)//Y276--Y307
                    {
                        Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                    }
                    else if (b == 9)//Y308--Y340
                    {
                        Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                    }
                    else if (b == 10)//Y340--Y371
                    {
                        Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                    }
                    else if (b == 11)//Y372--Y403
                    {
                        Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                    }
                    else if (b == 12)//Y404--Y435
                    {
                        if (a < 16)
                        {
                            Variable.YStatus[32 * b + 20 + a] = GetBit1(IntToBin32(Variable.YValue[b], a));
                        }
                    }
                }
            }
        }

        #endregion

        #region 输出动作

        //EX1
        //三色灯
        public void SetRedLampOn()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 7, 0);
        }
        public void SetRedLampOff()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 7, 1);
        }
        public void SetYellowLampOn()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 8, 0);
        }
        public void SetYellowLampOff()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 8, 1);
        }
        public void SetGreenLampOn()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 9, 0);
        }
        public void SetGreenLampOff()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 9, 1);
        }
        public void SetBuzzerOn()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 10, 0);
        }
        public void SetBuzzerOff()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 10, 1);
        }

        //按键灯
        public void StartLampOn()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 1, 0);
        }

        public void StartLampOff()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 1, 1);
        }

        public void StopLampOn()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 2, 0);
        }

        public void StopLampOff()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 2, 1);
        }

        public void RestLampOn()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 3, 0);
        }

        public void RestLampOff()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 3, 1);
        }

        public void OneCycleOn()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 4, 0);
        }

        public void OneCycleOff()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 4, 1);
        }

        public void CleanOutOn()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 5, 0);
        }

        public void CleanOutOff()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 5, 1);
        }

        public void ReturnZeroOn()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 6, 0);
        }

        public void ReturnZeroOff()
        {
            mc.GTN_SetDoBit(1, mc.MC_GPO, 6, 1);
        }


        public void OutYON(int data)
        {
            if (data >= 10 && data < 20)//主卡
            {
                mc.GTN_SetDoBit(2, mc.MC_GPO, (short)(data - 10 + 1), 0);
            }
            if (data >= 20 && data < 420)//扩展模块
            {
                mc.GTN_SetExtDoBit(1, (short)(data - 19), 1);
            }

            Variable.OnEnable[data] = true;
            Variable.OffEnable[data] = false;
            Variable.OnTime[data] = 0;
        }

        public void OutYOFF(int data)
        {
            if (data >= 10 && data < 20)//主卡
            {
                mc.GTN_SetDoBit(2, mc.MC_GPO, (short)(data - 10 + 1), 1);
            }
            if (data >= 20 && data < 420)//扩展模块
            {
                mc.GTN_SetExtDoBit(1, (short)(data - 19), 0);
            }

            Variable.OffEnable[data] = true;
            Variable.OnEnable[data] = false;
            Variable.OffTime[data] = 0;
        }
        #endregion

        #region 读取IO值转Bool

        public static bool GetBit0(string data)
        {
            if (data == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool GetBit1(string data)
        {
            if (data == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 将整数转换二进制
        /// <summary>
        /// 将整数转换二进制
        /// </summary>
        /// <param name="data">整数值</param>
        /// <param name="a">截取二进制位置</param>
        /// <returns></returns>
        public string IntToBin32(int data, int a)
        {
            lock (threadLock)
            {
                string cnt;//记录转换过后二进制值
                cnt = Convert.ToString(data, 2);
                if (cnt.Length < 32)
                {
                    int c = cnt.Length;
                    for (int b = 0; b < (32 - c); b++)
                    {
                        cnt = "0" + cnt;
                    }
                }
                return cnt.Substring(31 - a, 1);
            }
        }

        public string IntToBin16(int data, int a)
        {
            lock (threadLock)
            {
                string cnt;//记录转换过后二进制值
                byte[] IO_input = new byte[16];

                for (int i = 0; i < 16; i++)
                {
                    IO_input[i] = (byte)(((data & 1) + 1) % 2);
                    data = data >> 1;//n变成n向右移一位的那个数
                }
                cnt = IO_input[a].ToString();
                return cnt;
            }
        }

        #endregion




    }
}
