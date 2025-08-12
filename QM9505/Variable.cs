using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM9505
{
    class Variable
    {
        #region 各IO口信号读取存储

        public static double[] AIMpos = new double[30];//目标坐标值数组
        public static double[] REApos = new double[30];//实际坐标值数组
        public static int Home1, Plimit1, Nlimit1, Alarm1;
        public static int Home2, Plimit2, Nlimit2, Alarm2;

        //输入
        public static int[] XValue_0 = new int[4];
        public static int[] XValue_1 = new int[4];
        public static int[] XValue = new int[14];

        //输出
        public static int[] YValue_0 = new int[4];
        public static int[] YValue_1 = new int[4];
        public static int[] YValue = new int[14];

        #endregion

        #region 输入输出信号定义

        //按钮
        public static bool EMG;
        public static bool StartButton;
        public static bool PauseButton;
        public static bool AlarmClrButton;
        public static bool OneCycleButton;
        public static bool CleanOutButton;
        public static bool ZeroButton;

        //输入信号
        public static bool[] XStatus = new bool[600];
        //输出信号
        public static bool[] YStatus = new bool[600];
        public static bool[] OnEnable = new bool[600];
        public static bool[] OffEnable = new bool[600];

        #endregion

        #region 轴变量

        //轴导程
        public static double[] AxisPitch = new double[17] { 0, 47.4768, 34.9796, 34.9796, 2, 4, 4, 4, 0, 47.4768, 34.9796, 34.9796, 34.9796, 2, 4, 47.4768, 19.9892 };
        //轴脉冲
        public static double[] AxisPulse = new double[17] { 0, 10000, 10000, 10000, 10000, 10000, 1600, 1600, 0, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000 };
        //轴启动速度
        public static double[] AxisStartVel = new double[17];
        //轴运行速度
        public static double[] AxisRunVel = new double[17];
        //轴运行速度
        public static double[] AxisRealRunVel = new double[17];
        //轴加速度
        public static double[] AxisTacc = new double[17];
        //轴减速度
        public static double[] AxisTdec = new double[17];
        //平滑系数
        public static short[] AxisSmoothCoefficient = new short[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //平滑时间
        public static short[] AxisSmoothTime = new short[17] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1 };
        //Home搜索高速度
        public static double[] AxisHmoeVelHight = new double[17];
        //Home搜索低速度
        public static double[] AxisHmoeVelLow = new double[17];

        //轴坐标值数组
        public static double[,] AxisPos = new double[17, 20];

        //轴回原点完成标志
        public static bool[] AxisHomeFlag = new bool[17];

        #endregion

        #region 各流程步骤

        /// <summary>
        /// 复位流程
        /// </summary>
        public static int RestStep = 0;

        /// <summary>
        /// 上料待测复位流程
        /// </summary>
        public static int INAutoReadyRestStep = 0;

        /// <summary>
        /// 上料空盘复位流程
        /// </summary>
        public static int INAutoEmptyRestStep = 0;

        /// <summary>
        /// 下料良品复位流程
        /// </summary>
        public static int OutAutoOKRestStep = 0;

        /// <summary>
        /// 下料补料复位流程
        /// </summary>
        public static int OutAutoFillRestStep = 0;

        /// <summary>
        /// 下料NG复位流程
        /// </summary>
        public static int OutAutoNGRestStep = 0;

        /// <summary>
        /// 机械手复位流程
        /// </summary>
        public static int RobotAutoRestStep = 0;

        /// <summary>
        /// 单机复位流程
        /// </summary>
        public static int ModelAutoRestStep = 0;

        /// <summary>
        /// 自动流程步
        /// </summary>
        public static int AutoStep = 0;

        /// <summary>
        /// 上料分料自动流程
        /// </summary>
        public static int INAutoReady1Step = 0;

        /// <summary>
        /// 上料待测自动流程
        /// </summary>
        public static int INAutoReadyStep = 0;

        /// <summary>
        /// 上料空盘自动流程
        /// </summary>
        public static int INAutoEmptyStartStep = 0;

        /// <summary>
        /// 下料良品自动流程
        /// </summary>
        public static int OutAutoOKStartStep = 0;

        /// <summary>
        /// 下料补料自动流程
        /// </summary>
        public static int OutAutoFillStartStep = 0;

        /// <summary>
        /// 下料NG自动流程

        /// </summary>
        public static int OutAutoNGStartStep = 0;

        /// <summary>
        /// Robot复位流程
        /// </summary>
        public static int RobotHomeStartStep = 0;

        /// <summary>
        /// Robot自动流程
        /// </summary>
        public static int RobotAutoStartStep = 0;

        /// <summary>
        /// RobotTCP流程
        /// </summary>
        public static int RobotTCPAutoStep = 0;

        /// <summary>
        /// PhotoTCP流程
        /// </summary>
        public static int PhotoTCPAutoStep = 0;

        /// <summary>
        /// QRTCP流程
        /// </summary>
        public static int QRTCPAutoStep = 0;


        /// <summary>
        /// Robot放料自动流程
        /// </summary>
        public static int RobotSetStep = 0;


        /// <summary>
        /// Robot放料自动流程
        /// </summary>
        public static int RobotSet = 0;

        /// <summary>
        /// Robot取料自动流程
        /// </summary>
        public static int RobotGet = 0;

        /// <summary>
        /// Robot取料自动流程
        /// </summary>
        public static int RobotGetStep = 0;

        /// <summary>
        ///模块放料步
        /// </summary>
        public static int ModelSetStep = 0;

        /// <summary>
        ///模块取料步
        /// </summary>
        public static int ModelGetStep = 0;

        /// <summary>
        /// TCP通讯流程
        /// </summary>
        public static int TcpServerStartStep = 0;

        /// <summary>
        /// 自动流程记录
        /// </summary>
        public static string[] AutoStepMsg = new string[10];

        /// <summary>
        /// 自动流程记录
        /// </summary>
        public static string[] StepMsg = new string[14];

        #endregion

        #region 料盘参数

        /// <summary>
        /// LoadTray盘行数
        /// </summary>
        public static double RowNum;

        /// <summary>
        /// LoadTray盘行间距
        /// </summary>
        public static double RowSpacing;

        /// <summary>
        /// LoadTray盘列数
        /// </summary>
        public static double ListNum;

        /// <summary>
        /// LoadTray盘列间距
        /// </summary>
        public static double ListSpacing;

        /// <summary>
        /// 上料AB吸嘴间距
        /// </summary>
        public static double UpABSpacing;

        /// <summary>
        /// 下料AB吸嘴间距
        /// </summary>
        public static double DownABSpacing;

        /// <summary>
        /// 补偿值
        /// </summary>
        public static double[] offset = new double[200];


        #endregion    

        #region 阵列坐标集合

        /// <summary>
        /// 上料空Tray盘X轴坐标集合
        /// </summary>
        public static List<double> UpXNullTrayPositionA = new List<double>();
        public static List<double> UpXNullTrayPositionB = new List<double>();

        /// <summary>
        /// 上料空Tray盘Y轴坐标集合
        /// </summary>
        public static List<double> UpYNullTrayPositionA = new List<double>();
        public static List<double> UpYNullTrayPositionB = new List<double>();

        /// <summary>
        /// 上料待测Tray盘X轴坐标集合
        /// </summary>
        public static List<double> UpXReadyTrayPositionA = new List<double>();
        public static List<double> UpXReadyTrayPositionB = new List<double>();

        /// <summary>
        /// 上料待测Tray盘Y轴坐标集合
        /// </summary>
        public static List<double> UpYReadyTrayPositionA = new List<double>();
        public static List<double> UpYReadyTrayPositionB = new List<double>();

        /// <summary>
        /// 下料良品Tray盘X轴坐标集合
        /// </summary>
        public static List<double> DownXOKTrayPositionA = new List<double>();
        public static List<double> DownXOKTrayPositionB = new List<double>();

        /// <summary>
        /// 下料良品Tray盘Y轴坐标集合
        /// </summary>
        public static List<double> DownYOKTrayPositionA = new List<double>();
        public static List<double> DownYOKTrayPositionB = new List<double>();

        /// <summary>
        /// 下料OK备品Tray盘X轴坐标集合
        /// </summary>
        public static List<double> DownXReadyPositionA = new List<double>();
        public static List<double> DownXReadyPositionB = new List<double>();

        /// <summary>
        /// 下料OK备品Tray盘Y轴坐标集合
        /// </summary>
        public static List<double> DownYReadyPositionA = new List<double>();
        public static List<double> DownYReadyPositionB = new List<double>();

        /// <summary>
        /// 下料NGTray盘X轴坐标集合
        /// </summary>
        public static List<double> DownXNGTrayPositionA = new List<double>();
        public static List<double> DownXNGTrayPositionB = new List<double>();

        /// <summary>
        /// 下料NGTray盘Y轴坐标集合
        /// </summary>
        public static List<double> DownYNGTrayPositionA = new List<double>();
        public static List<double> DownYNGTrayPositionB = new List<double>();


        #endregion

        #region 各工位Tray盘状态标志

        /// <summary>
        /// 上料分料1自动标志
        /// </summary>
        public static bool INAutoReady1flag = false;

        /// <summary>
        /// 上料分料2自动标志
        /// </summary>
        public static bool INAutoReady2flag = false;

        /// <summary>
        /// 上料空Tray盘是否放满标志
        /// </summary>
        public static int UpNullTray;


        /// <summary>
        /// 上料空Tray盘是否放满标志
        /// </summary>
        public static bool UpNullTrayFull = false;

        /// <summary>
        /// 上料空Tray盘准备OK标志
        /// </summary>
        public static bool UpNullTrayOK = false;



        /// <summary>
        /// 上料待测1Tray盘准备OK标志
        /// </summary>
        public static bool UpReady1TrayOK = false;

        /// <summary>
        /// 上料待测1Tray盘准备OK标志
        /// </summary>
        public static bool UpReady1TrayFlag = false;

        /// <summary>
        /// 上料待测2Tray盘准备OK标志
        /// </summary>
        public static bool UpReady2TrayOK = false;

        /// <summary>
        /// 上料待测1Tray盘准备OK标志
        /// </summary>
        public static bool UpReady2TrayFlag = false;

        /// <summary>
        /// 上料待测Tray盘是否有空位标志
        /// </summary>
        public static int UpReadyTrayEmpty;

        /// <summary>
        /// 判断OK盘NG数量标志
        /// </summary>
        public static bool OKTrayNGCountFlag;


        /// <summary>
        /// 上料待测Tray盘OK标志位
        /// </summary>
        public static bool UpReadyTrayOK = false;



        /// <summary>
        /// 下料良品Tray盘是否放满标志
        /// </summary>
        public static int DownOKTrayFull;

        /// <summary>
        /// 下料良品Tray盘是否放满标志
        /// </summary>
        public static int DownOKTrayWait;

        /// <summary>
        /// 下料良品Tray盘产品都为OK标志
        /// </summary>
        public static bool DownOKTrayFullOK = false;

        /// <summary>
        /// 下料良品Tray盘没有NG标志
        /// </summary>
        public static int DownOKTrayNG;

        /// <summary>
        /// 下料良品Tray盘OK标志位
        /// </summary>
        public static bool DownGetTray = false;


        /// <summary>
        /// 下料OK备品Tray盘产品都为OK标志
        /// </summary>
        public static bool DownReadyTrayFullOK = false;

        /// <summary>
        /// 下料OK备品Tray盘产品都为OK标志
        /// </summary>
        public static int DownReadyTrayOK;

        /// <summary>
        /// 下料OK备品Tray盘工位2有无料盘标志位
        /// </summary>
        public static bool OutFillTrayFlag = false;

        /// <summary>
        /// 下料OK备品Tray盘工位料盘取空标志位
        /// </summary>
        public static bool DownReadyEmpty = false;

        /// <summary>
        /// 下料OK备品Tray盘工位料盘取数量
        /// </summary>
        public static int DownReadyOK;


        /// <summary>
        /// 下料NGTray盘是否有空位
        /// </summary>
        public static int DownNGTray;

        /// <summary>
        /// 下料NGTray盘是否放满标志
        /// </summary>
        public static bool DownNGTrayFull = false;

        /// <summary>
        /// 下料NGTray盘准备OK标志
        /// </summary>
        public static bool DownNGTrayOK = false;

        /// <summary>
        /// Robot取上料盘OK标志位
        /// </summary>
        public static bool RobotUpGetTray = false;

        /// <summary>
        /// Robot传文件上料机OK标志位
        /// </summary>
        public static bool RobotToFileUp = false;

        /// <summary>
        /// Robot上料机标志位
        /// </summary>
        public static bool RobotSetFlag = false;

        /// <summary>
        /// Robot下料机标志位
        /// </summary>
        public static bool RobotGetFlag = false;

        /// <summary>
        /// Robot取下料盘OK标志位
        /// </summary>
        public static bool RobotDownGetTray = false;


        /// <summary>
        /// Robot放料盘OK标志位
        /// </summary>
        public static bool RobotSetTray = false;

        /// <summary>
        /// Robot放料盘OK标志位
        /// </summary>
        public static bool RobotSetTrayOK = false;

        /// <summary>
        /// Robot安全点位
        /// </summary>
        public static bool RobotSafePoint = false;

        /// <summary>
        /// Robot取料中标志位
        /// </summary>
        public static bool RobotGeting = false;

        /// <summary>
        /// Robot放料中标志位
        /// </summary>
        public static bool RobotSeting = false;

        /// <summary>
        /// 料盘缺料标志
        /// </summary>
        public static string TrayNum;

        #endregion

        #region 用户密码
        public enum UserEnter
        {
            NoUser = 0,
            User = 1,
            Engineer = 2,
            Manufacturer = 3,
            Administrator = 4,
        }

        public static UserEnter userEnter = Variable.UserEnter.NoUser;

        #endregion

        #region 功能选择

        /// <summary>
        /// 速度
        /// </summary>
        public static double SpeedCom;       

        /// <summary>
        /// 上电选择
        /// </summary>
        public static bool FistPower = false;

        /// <summary>
        /// 上料门禁选择
        /// </summary>
        public static bool UpDoorCheck = false;

        /// <summary>
        /// 模组门禁选择
        /// </summary>
        public static bool ModelDoorCheck = false;

        /// <summary>
        /// 下料门禁选择
        /// </summary>
        public static bool DownDoorCheck = false;

        /// <summary>
        /// 高温选择
        /// </summary>
        public static bool HotModel = false;

        /// <summary>
        /// 相机选择
        /// </summary>
        public static bool PhotoCheck = false;

        /// <summary>
        /// 端口屏蔽选择
        /// </summary>
        public static bool siteShieldCheck = false;

        /// <summary>
        /// MES选择
        /// </summary>
        public static bool MEScheck = false;

        /// <summary>
        /// Tray盘扫码
        /// </summary>
        public static bool TrayQRCheck = false;

        /// <summary>
        /// 下料良品OK盘
        /// </summary>
        public static bool CheckDownOK = false;

        /// <summary>
        /// 蜂鸣选择
        /// </summary>
        public static bool BuzzerCheck = true;

        #endregion

        #region 报表

        /// <summary>
        /// OP
        /// </summary>
        public static string OP;

        /// <summary>
        /// 工单号
        /// </summary>
        public static string OrderNum;

        /// <summary>
        /// PO号
        /// </summary>
        public static string PONum;

        /// <summary>
        /// 批号
        /// </summary>
        public static string BatchNum;

        /// <summary>
        /// 档案名称
        /// </summary>
        public static string FileName;

        /// <summary>
        /// 档案名称标志
        /// </summary>
        public static bool modeNameFlag;

        /// <summary>
        /// 机种名
        /// </summary>
        public static string ModelName = "";

        /// <summary>
        /// 开始时间
        /// </summary>
        public static string startTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        public static string endTime;

        /// <summary>
        /// 运行时间
        /// </summary>
        public static string runTime;

        /// <summary>
        /// 停止时间
        /// </summary>
        public static string stopTime;

        /// <summary>
        /// 报警时间
        /// </summary>
        public static string alarmTime;

        /// <summary>
        /// 测试时间
        /// </summary>
        public static string testTime;

        /// <summary>
        /// 报警率
        /// </summary>
        public static string jamRate;

        /// <summary>
        /// 运行模式
        /// </summary>
        public static string runModel = "正常模式";

        /// <summary>
        /// 报警次数
        /// </summary>
        public static long alarmCount;

        /// <summary>
        /// UPH
        /// </summary>
        public static double UPH;

        /// <summary>
        /// 投入Tray数量
        /// </summary>
        public static double inTrayNum;

        /// <summary>
        /// 投入Tray数量
        /// </summary>
        public static double inTrayNumT;

        /// <summary>
        /// 产出Tray数量
        /// </summary>
        public static double outTrayNum;

        /// <summary>
        /// 投入芯片数量
        /// </summary>
        public static double inChipNum;

        /// <summary>
        /// 产出芯片数量
        /// </summary>
        public static double outChipNum;

        /// <summary>
        /// OK芯片数量
        /// </summary>
        public static double OKChipNum;

        /// <summary>
        /// NG芯片数量
        /// </summary>
        public static double NGChipNum;

        /// <summary>
        /// 良率
        /// </summary>
        public static double Yield;

        /// <summary>
        /// UPH计数
        /// </summary>
        public static double UPHNum = 0;

        /// <summary>
        /// 下料不良品NG计数
        /// </summary>
        public static double DownNGCount;

        /// <summary>
        /// QR NG次数
        /// </summary>
        public static double QRTime;

        /// <summary>
        /// 数量
        /// </summary>
        public static string Num;

        /// <summary>
        /// 总数量
        /// </summary>
        public static double TotalNum = 0;

        /// <summary>
        /// OK数量
        /// </summary>
        public static double OKNum = 0;

        #endregion

        #region 系统变量

        /// <summary>
        /// 机器状态
        /// </summary>
        /// 
        public enum MachineStatus
        {
            Emg = 0,// 急停
            Stop = 1,//停止
            Alarm = 5,//报警
            StandBy = 2,//准备就绪            
            Running = 3,//运行
            Pause = 4,//暂停
            zero = 6,//归零
            Reset = 7,//复位
        }
        public static MachineStatus MachineState;

        /// <summary>
        /// 轴错误
        /// </summary>
        public static bool ServerAlarm = false;

        /// <summary>
        /// 轴正限位错误
        /// </summary>
        public static bool PLimitAlarm = false;

        /// <summary>
        /// 轴负限位错误
        /// </summary>
        public static bool NLimitAlarm = false;

        /// <summary>
        /// 程序报警
        /// </summary>
        public static bool CommonAlarm1 = false;

        /// <summary>
        /// 程序报警
        /// </summary>
        public static bool CommonAlarm2 = false;

        /// <summary>
        /// 程序报警
        /// </summary>
        public static bool CommonAlarm3 = false;

        /// <summary>
        /// 运行使能
        /// </summary>
        public static bool RunEnable = false;

        /// <summary>
        /// 复位使能
        /// </summary>
        public static bool RestEnable = false;

        /// <summary>
        /// EXE
        /// </summary>
        public static bool ExeOpen;

        /// <summary>
        /// 保存标志
        /// </summary>
        public static bool saveFlag;

        /// <summary>
        /// 结批完标志
        /// </summary>
        public static bool CleanOutFlag;

        /// <summary>
        /// 调试界面标志
        /// </summary>
        public static bool ManualViewFlag;

        /// <summary>
        /// IO界面标志
        /// </summary>
        public static bool IOViewFlag;

        /// <summary>
        /// 参数保存标志
        /// </summary>
        public static bool parameterSaveFlag;

        /// <summary>
        /// 加载参数
        /// </summary>
        public static bool LoadparameterFlag;

        /// <summary>
        /// 参数窗体加载
        /// </summary>
        public static bool formOpenFlag = false;

        public static bool RobotRecOK = false;
        public static bool RobotResetNG = false;

        #endregion

        #region 按钮变量

        /// <summary>
        /// 开始按钮;
        /// </summary>
        public static bool btnStart = false;

        /// <summary>
        /// 停止按钮;
        /// </summary>
        public static bool btnStop = false;

        /// <summary>
        /// 复位按钮;
        /// </summary>
        public static bool btnReset = false;

        /// <summary>
        /// 清料按钮;
        /// </summary>
        public static bool btnCleanOne = false;

        /// <summary>
        /// 结批按钮;
        /// </summary>
        public static bool btnCleanOut = false;

        /// <summary>
        /// 回零按钮;
        /// </summary>
        public static bool btnZero = false;

        /// <summary>
        /// 清空机台
        /// </summary>
        public static bool CleanOne = false;

        /// <summary>
        /// 清料
        /// </summary>
        public static bool CleanOut = false;

        #endregion

        #region 报警变量

        public static bool[] ServoAlarmHappen = new bool[27];
        public static bool[] PlimitAlarmHappen = new bool[27];
        public static bool[] NlimitAlarmHappen = new bool[27];
        public static bool[] AxisAlarmHappen = new bool[27];
        public static bool[] AlarmHappen = new bool[600];
        public static string[] AlarmContent = new string[600];
        public static bool[] AireAlarm = new bool[40];
        public static bool QRAlarm;
        public static bool UpAxisAlarm;
        public static bool DownAxisAlarm;
        public static bool PhotoAlarm;
        public static bool RobotAlarm;
        public static bool ModelAlarm;
        public static bool ProbeAlarm;
        public static bool AlarmFlag;

        /// <summary>
        /// 报警弹窗步
        /// </summary>
        public static string step;

        /// <summary>
        /// 取消
        /// </summary>
        public static int cancelStep;

        /// <summary>
        /// 确定
        /// </summary>
        public static int sureStep;

        /// <summary>
        /// 弹窗标志
        /// </summary>
        public static bool POPFlag;

        /// <summary>
        /// OP确定
        /// </summary>
        public static bool OPsure;

        /// <summary>
        /// OP账号
        /// </summary>
        public static string OPNum;

        /// <summary>
        /// OP密码
        /// </summary>
        public static string OPPass;

        /// <summary>
        /// 报警内容
        /// </summary>
        public static string content;
        

        #endregion

        #region 输出动作时间

        public static int[] OnTime = new int[600];
        public static int[] OffTime = new int[600];
        public static int[] AxisPTime = new int[27];
        public static int[] AxisNTime = new int[27];
        public static int[] AxisAlarmTime = new int[27];
        public static int[] XAlarmTime = new int[27];

        #endregion

        #region 串口数据

        public static string portName;
        public static int baudRate;
        public static int dataBit;
        public static string parity;
        public static int stop;
        public static string strQR;
        public static string strQR1;
        #endregion

        #region 延时

        /// <summary>
        /// 上料A吸嘴吸真空延时
        /// </summary>
        public static double UpAabsorb;

        /// <summary>
        /// 上料B吸嘴吸真空延时
        /// </summary>
        public static double UpBabsorb;

        /// <summary>
        /// 上料A吸嘴破真空延时
        /// </summary>
        public static double UpAbroken;

        /// <summary>
        /// 上料B吸嘴破真空延时
        /// </summary>
        public static double UpBbroken;

        /// <summary>
        /// 下料A吸嘴吸真空延时
        /// </summary>
        public static double DownAabsorb;

        /// <summary>
        /// 下料B吸嘴吸真空延时
        /// </summary>
        public static double DownBabsorb;

        /// <summary>
        /// 下料A吸嘴破真空延时
        /// </summary>
        public static double DownAbroken;

        /// <summary>
        /// 下料B吸嘴破真空延时
        /// </summary>
        public static double DownBbroken;

        /// <summary>
        /// 气缸动作延时
        /// </summary>
        public static int airSetDelay;

        /// <summary>
        /// 拍照延时
        /// </summary>
        public static double photoDelay;// = 8000;

        /// <summary>
        /// QR延时
        /// </summary>
        public static double QRDelay = 1000;

        /// <summary>
        /// 轴报警延时
        /// </summary>
        public static int axisAlarmDelay = 4000;
        /// <summary>
        /// 报警延时
        /// </summary>
        public static int XAlarmDelay = 4000;

        #endregion

        #region 温度变量

        /// <summary>
        /// 温度到达延时设定;
        /// </summary>
        public static double tempSetDelay;

        /// <summary>
        /// 温度到达延时;
        /// </summary>
        public static long[] tempDelay = new long[40];

        /// <summary>
        /// 加热时间
        /// </summary>
        public static string HotTime;

        /// <summary>
        /// 温控器温度
        /// </summary>
        public static string Temp;

        /// <summary>
        /// 温控器温度上限
        /// </summary>
        public static double TempUpLimit;

        /// <summary>
        /// 温控器温度下限
        /// </summary>
        public static double TempDownLimit;

        /// <summary>
        /// 温度写入标志;
        /// </summary>
        public static bool temWriteFlag = false;

        /// <summary>
        /// 温度读取标志;
        /// </summary>
        public static bool temReadFlag = false;

        /// <summary>
        /// 温度
        /// </summary>
        public static double[] TemperData = new double[40];

        /// <summary>
        /// 温度到达
        /// </summary>
        public static bool[] tempOK = new bool[40];

        /// <summary>
        /// 10个模组温度达标
        /// </summary>
        public static bool[] TempOKFlag = new bool[40];

        #endregion

        #region 测试参数

        /// <summary>
        /// 测试时间
        /// </summary>
        public static double TestTime = 0;

        /// <summary>
        /// 测试等待时间
        /// </summary>
        public static double TestWaitTime = 0;

        /// <summary>
        /// 测试超时时间
        /// </summary>
        public static double TestOutTime = 0;

        /// <summary>
        /// VCC测试电压
        /// </summary>
        public static double VCCTestVol = 0;

        /// <summary>
        /// VCQ测试电压
        /// </summary>
        public static double VCQTestVol = 0;

        /// <summary>
        /// 测试良率
        /// </summary>
        public static double testYield = 0;

        /// <summary>
        /// 温度设置
        /// </summary>
        public static double temper;

        /// <summary>
        /// 最高温度设置
        /// </summary>
        public static double upTemper;

        /// <summary>
        /// 在线模式
        /// </summary>
        public static bool OnlineModcheck;

        /// <summary>
        /// VCQ选择
        /// </summary>
        public static bool VCQcheck;

        /// <summary>
        /// PGM选择
        /// </summary>
        public static bool PGMcheck;

        /// <summary>
        /// 测试状态显示
        /// </summary>
        public static bool[] PicBox = new bool[10];

        /// <summary>
        /// 屏蔽与选择机台
        /// </summary>
        public static bool[] modelCheck = new bool[40];

        /// <summary>
        /// 10个模组状态=>0:空，1:已放料，2:测试中，3:测试OK，10:屏蔽
        /// </summary>
        public static int[] ModelState = new int[40];

        /// <summary>
        /// 10个模组良率
        /// </summary>
        public static string[] YieldMode = new string[40];

        #endregion

        #region 端口NG设定

        /// <summary>
        /// 端口NG计数
        /// </summary>
        public static int[,] siteNGCount = new int[40, 152];

        /// <summary>
        /// 端口NG设定
        /// </summary>
        public static double siteNGSet = 0;
        #endregion

        #region 探针

        /// <summary>
        /// 探针次数
        /// </summary>
        public static double[] ProbeNum = new double[40];

        /// <summary>
        /// 探针寿命
        /// </summary>
        public static double[] ProbeSet = new double[40];

        #endregion

        #region 通信

        public static bool Server1Connect;
        public static bool Server2Connect;
        public static bool Server3Connect;
        public static string serverIP1 = "192.168.3.10";
        public static string serverport1 = "8000";
        public static string serverIP2 = "192.168.3.10";
        public static string serverport2 = "8001";
        public static string serverIP3 = "192.168.3.100";
        public static string serverport3 = "8010";

        public static string clientIP1;
        public static string clientport1;
        public static string clientIP2;
        public static string clientport2;
        public static string clientIP3;
        public static string clientport3;

        /// <summary>
        /// QR保存
        /// </summary>
        public static List<string> qrRecord = new List<string>();

        /// <summary>
        /// OP账号
        /// </summary>
        public static List<string> OPNumber = new List<string>();

        /// <summary>
        /// OP密码
        /// </summary>
        public static List<string> OPPassword = new List<string>();

        /// <summary>
        /// 扫码枪接受数据
        /// </summary>
        public static string QRRecMessage = "";

        /// <summary>
        /// 相机接受数据
        /// </summary>
        public static string PhotoRecMessage = "";

        /// <summary>
        /// 机械手接受数据
        /// </summary>
        public static string RobotRecMessage = "";

        /// <summary>
        /// 定义拍照触发字符串
        /// </summary>
        public static string TakePhoto1 = "T1";//触发

        /// <summary>
        /// 定义拍照触发字符串
        /// </summary>
        public static string TakePhoto2 = "T2";//触发

        /// <summary>
        /// 定义QR拍照触发字符串
        /// </summary>
        public static string TakeQR = "T1";//触发

        /// <summary>
        /// 相机接受数据1
        /// </summary>
        public static string[] PhotoData1 = new string[80];

        /// <summary>
        /// 相机接受数据2
        /// </summary>
        public static string[] PhotoData2 = new string[72];

        /// <summary>
        /// 相机接受数据3
        /// </summary>
        public static string[] PhotoData = new string[152];

        /// <summary>
        /// 相机测试结果;
        /// </summary>
        public static bool TestResult;

        /// <summary>
        /// Robot发送字符
        /// </summary>
        public static string RobotSendStr;//001

        #endregion

        #region 白夜班参数

        /// <summary>
        /// 白天
        /// </summary>
        public static string[] DayTime = new string[720];

        /// <summary>
        /// 夜晚
        /// </summary>
        public static string[] NightTime = new string[720];
        #endregion

        #region 备用变量

        public static bool[] flagVar = new bool[20];
        public static int[] intVar = new int[20];
        public static string[] strVar = new string[20];
        public static string[] messageRecord = new string[20];

        public static string inTrayNumSet;
        public static double inTrayNumRecord;
        public static string trayCombo;

        public static bool info;
        #endregion




















    }
}
