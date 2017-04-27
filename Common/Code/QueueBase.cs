using System;
using System.IO;
using System.Threading;
using System.Timers;
using Common.Write;
using Timer = System.Timers.Timer;

namespace Common.Code
{
    /// <summary>
    /// 队列轮循
    /// </summary>
    public abstract class QueueBase
    {
        /// <summary>
        /// 时间控件
        /// </summary>
        private readonly Timer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueBase"/> class.
        /// </summary>
        protected QueueBase()
        {
            // ReSharper disable once VirtualMemberCallInContructor
            this.timer = new Timer(this.Interval);
        }

        /// <summary>
        /// 时间间隔
        /// </summary>
        public abstract double Interval { get; }

        /// <summary>
        /// 轮循的线程函数
        /// </summary>
        public abstract void ThreadProc();

        /// <summary>
        /// 获取程序名称
        /// </summary>
        /// <returns>默认为空</returns>
        public virtual string GetProgramName()
        {
            return string.Empty;
        }

        /// <summary>
        /// 获取调度执行日志地址
        /// </summary>
        /// <returns>调度执行日志地址</returns>
        public virtual string GetTaskLogPath()
        {
            return "Logs/TaskLog";
        }

        /// <summary>
        /// 获取调度异常日志地址
        /// </summary>
        /// <returns>调度异常日志地址</returns>
        public virtual string GetExceptionLogPath()
        {
            return "Logs/ExceptionLog";
        }

        /// <summary>
        /// 开始函数
        /// </summary>
        public void Start()
        {
            this.timer.Elapsed += this.timer_Elapsed;
            this.timer.Enabled = true;
            this.timer.Start();
        }

        /// <summary>
        /// 停止函数
        /// </summary>
        public void Stop()
        {
            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer.Close();
            }
        }

        /// <summary>
        /// timer执行事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        public void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ((Timer)sender).Enabled = false;
            try
            {
                string programName = this.GetProgramName();
                LogService.WriteLog(programName + "调度程序正在执行.......", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.GetTaskLogPath()));
                this.ThreadProc();
            }
            catch (System.Exception ex)
            {
                LogService.WriteLog(ex.Message + Environment.NewLine + ex.StackTrace, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.GetExceptionLogPath()));
            }
            finally
            {
                ((Timer)sender).Enabled = true;
            }
        }
    }

    /// <summary>
    /// 队列轮循(while方式)
    /// </summary>
    public abstract class QueueBaseWhile
    {
        /// <summary>
        /// 时间间隔
        /// </summary>
        public abstract double Interval { get; }

        /// <summary>
        /// 轮循的线程函数
        /// </summary>
        public abstract void ThreadProc();

        /// <summary>
        /// 获取程序名称
        /// </summary>
        /// <returns>默认为空</returns>
        public virtual string GetProgramName()
        {
            return string.Empty;
        }

        /// <summary>
        /// 获取调度执行日志地址
        /// </summary>
        /// <returns>调度执行日志地址</returns>
        public virtual string GetTaskLogPath()
        {
            return "Logs/TaskLog";
        }

        /// <summary>
        /// 获取调度异常日志地址
        /// </summary>
        /// <returns>调度异常日志地址</returns>
        public virtual string GetExceptionLogPath()
        {
            return "Logs/ExceptionLog";
        }

        /// <summary>
        /// 是否停止
        /// </summary>
        private bool IsStop;

        /// <summary>
        /// 开始函数
        /// </summary>
        public void Start()
        {
            while (true)
            {
                if (this.IsStop)
                {
                    break;
                }
                try
                {
                    string programName = this.GetProgramName();
                    LogService.WriteLog(programName + "调度程序正在执行.......", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.GetTaskLogPath()));
                    this.ThreadProc();
                }
                catch (System.Exception ex)
                {
                    LogService.WriteLog(ex.Message + Environment.NewLine + ex.StackTrace, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.GetExceptionLogPath()));
                }
                int ti = (int)this.Interval;
                Thread.Sleep(ti);
            }
        }

        /// <summary>
        /// 停止函数
        /// </summary>
        public void Stop()
        {
            this.IsStop = true;
        }
    }
}
