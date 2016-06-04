using log4net;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace WebAPI.Core.Aspects.Logging
{
    [Serializable]
    [MulticastAttributeUsage(
        MulticastTargets.Method,
        TargetMemberAttributes = MulticastAttributes.NonAbstract, 
        Inheritance = MulticastInheritance.Multicast
        )]
    [AttributeUsage(AttributeTargets.Assembly)]
    public class LoggingAspect : OnMethodBoundaryAspect
    {     
        private static readonly ILog log = LogManager.GetLogger("TRACE");
        private string _fullMethodName;
        private int indent = 0;
        
        #region Complie Time
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            _fullMethodName = string.Format("{0}.{1}",
                method.DeclaringType.FullName,
                method.Name);

            base.CompileTimeInitialize(method, aspectInfo);
        }

        public override bool CompileTimeValidate(MethodBase method)
        {
            return base.CompileTimeValidate(method);
        }
        #endregion

        #region Run time
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!log.IsDebugEnabled) { return; }

            string message = string.Empty;
            try
            {
                message = string.Format("{2}Entering [{0}] with parameters ( [{1}] )", _fullMethodName, MarshalObjectParameters(args), new string(' ',indent));
                log.Debug(message);
                ++indent;
            }
            catch (Exception e)
	        {
                message = string.Format("LoggingAspect failed to log OnEntry in [{0}]", _fullMethodName);
                log.Error(message, e);
            }

            base.OnEntry(args);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (!log.IsDebugEnabled) { return; }

            string message = string.Empty;
            try
            {
                message = string.Format("{1}Successfully executed [{0}]", _fullMethodName, new string(' ', indent));
                log.Debug(message);
            }
            catch (Exception e)
            {
                message = string.Format("LoggingAspect failed to log OnSuccess in [{0}]", _fullMethodName);
                log.Error(message, e);
            }
            base.OnSuccess(args);
        }

        public override void OnException(MethodExecutionArgs args)
        {
            if (!log.IsErrorEnabled) { return; }

            string message = string.Empty;
            try
            {
                --indent;
                message = string.Format("{2}Exception occured in [{0}] | [{1}]", _fullMethodName, args.Exception, new string(' ', indent));
                log.Error(message, args.Exception);
            }
            catch (Exception e)
            {
                message = string.Format("LoggingAspect failed to log OnException in [{0}]", _fullMethodName);
                log.Error(message, e);
            }
            base.OnException(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            if (!log.IsDebugEnabled) { return; }

            string message = string.Empty;
            try
            {
                --indent;
                message = string.Format("{2}Exiting [{0}] with value: ( [{1}] )", _fullMethodName, MarshalReturnValue(args), new string(' ', indent));
                log.Debug(message);
            }
            catch (Exception e)
            {
                message = string.Format("LoggingAspect failed to log OnExit in [{0}]", _fullMethodName);
                log.Error(message, e);
            }

            base.OnExit(args);
        }
        #endregion

        private string MarshalObjectParameters(MethodExecutionArgs args)
        {
            string result = string.Empty;
            if(null != args.Arguments && args.Arguments.Any())
            {
                result = string.Join("; ", args.Arguments.ToList()
                                            .ConvertAll(a => (a ?? "null").ToString()).ToArray());
            }
            return result;

        }

        private object MarshalReturnValue(MethodExecutionArgs args)
        {
            object result = args.ReturnValue ?? "No Return Value";
            return result;
        }
    }
}
