﻿using System;
using Evol.Common.Logging;
using NLog;

namespace Evol.Logging.AdapteNLog
{
    public class NLogger : Common.Logging.ILogger
    {
        private NLog.Logger _nlog;
        public NLogger(NLog.Logger logger)
        {
            _nlog = logger; 
        }

        #region LogCritical

        [Obsolete("预留暂不实现")]
        public void LogCritical(Exception exception, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        [Obsolete("预留暂不实现")]
        public void LogCritical(EventId eventId, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        [Obsolete("预留暂不实现")]
        public void LogCritical(EventId eventId, Exception exception, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        [Obsolete("预留暂不实现")]
        public void LogCritical(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void LogData<T>(T model)
        {
            var propValues = DictionaryObjectConvert.DictionarySimpleContractConvert(model);
            var logEventInfo = LogEventInfo.Create(LogLevel.Info, _nlog.Name, $"{typeof(T)}_数据库日志");
            foreach (var prop in propValues)
            {
                logEventInfo.Properties[prop.Key] = prop.Value;
            }
            _nlog.Log(logEventInfo);
        }



        public void LogDebug(EventId eventId, Exception exception, string message, params object[] args)
        {
            _nlog.Debug(exception, $"eventId:{eventId} | message:{message}", args);
        }

        public void LogDebug(EventId eventId, string message, params object[] args)
        {
            _nlog.Debug($"eventId:{eventId} | message:{message}", args);
        }

        public void LogDebug(Exception exception, string message, params object[] args)
        {
            _nlog.Debug(exception, message, args);
        }

        public void LogDebug(string message, params object[] args)
        {
            _nlog.Debug(message, args);
        }

        public void LogError(EventId eventId, Exception exception, string message, params object[] args)
        {
            _nlog.Error(exception, $"eventId:{eventId} | message:{message}", args);
        }

        public void LogError(EventId eventId, string message, params object[] args)
        {
            _nlog.Error($"eventId:{eventId} | message:{message}", args);
        }

        public void LogError(Exception exception, string message, params object[] args)
        {
            _nlog.Error(exception, message, args);
        }

        public void LogError(string message, params object[] args)
        {
            _nlog.Debug(message, args);
        }

        public void LogInformation(EventId eventId, Exception exception, string message, params object[] args)
        {
            _nlog.Info(exception, $"eventId:{eventId} | message:{message}", args);
        }

        public void LogInformation(EventId eventId, string message, params object[] args)
        {
            _nlog.Info($"eventId:{eventId} | message:{message}", args);
        }

        public void LogInformation(Exception exception, string message, params object[] args)
        {
            _nlog.Info(exception, message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _nlog.Info(message, args);
        }

        public void LogTrace(EventId eventId, Exception exception, string message, params object[] args)
        {
            _nlog.Trace(exception, $"eventId:{eventId} | message:{message}", args);
        }

        public void LogTrace(Exception exception, string message, params object[] args)
        {
            _nlog.Trace(exception, message, args);
        }

        public void LogTrace(string message, params object[] args)
        {
            _nlog.Trace(message, args);
        }

        public void LogTrace(EventId eventId, string message, params object[] args)
        {
            _nlog.Trace($"eventId:{eventId} | message:{message}", args);
        }

        public void LogWarning(EventId eventId, Exception exception, string message, params object[] args)
        {
            _nlog.Warn(exception, $"eventId:{eventId} | message:{message}", args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _nlog.Trace(message, args);
        }

        public void LogWarning(EventId eventId, string message, params object[] args)
        {
            _nlog.Warn($"eventId:{eventId} | message:{message}", args);
        }

        public void LogWarning(Exception exception, string message, params object[] args)
        {
            if (exception != null)
                message += $"\r\n  exception:{exception.Message}, \r\n InnerException:{exception?.InnerException.Message} \r\n StackTrace:{exception.StackTrace}";
            _nlog.Warn(exception, message, args);
        }

        internal void Log(LogEventInfo logEvent)
        {
            _nlog.Log(logEvent);
        }

        /// <summary>
        /// 记录基本操作日志
        /// </summary>
        /// <param name="operateType">操作类型 <see cref="BasicOperateLogType"/></param>
        /// <param name="ip">操作人IP</param>
        /// <param name="original">原始值</param>
        /// <param name="current">当前值</param>
        /// <param name="remark">备注</param>
        /// <param name="userId">操作人编号</param>
        /// <param name="username">操作人名称</param>
        public void LogBasicOperate(BasicOperateLogType operateType, string ip, string original, string current, string remark, string userId, string username, object logLevel = null)
        {
            LogEventInfo logEvent = new LogEventInfo(logLevel as LogLevel ?? LogLevel.Info, "", operateType.ToString());
            logEvent.Properties["id"] = Guid.NewGuid().ToString();
            logEvent.Properties["operateType"] = operateType;
            logEvent.Properties["ip"] = ip;
            logEvent.Properties["original"] = original;
            logEvent.Properties["current"] = current;
            logEvent.Properties["remark"] = remark;
            Log(logEvent);
        }


    }
}
