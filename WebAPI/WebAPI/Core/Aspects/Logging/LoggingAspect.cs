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
        
        #region Complie Time
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            _fullMethodName = string.Format("{0}.{1}",method.DeclaringType.Name,method.Name);

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
                message = string.Format("[{0}] Entering with parameters: ({1})", _fullMethodName, MarshalObjectParameters(args));
                log.Debug(message);
            }
            catch (Exception e)
	        {
                message = string.Format("[{0}] LoggingAspect failed to log OnEntry.", _fullMethodName);
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
                message = string.Format("[{0}] Successfully executed!", _fullMethodName);
                log.Debug(message);
            }
            catch (Exception e)
            {
                message = string.Format("[{0}] LoggingAspect failed to log OnSuccess.", _fullMethodName);
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
                message = string.Format("[{0}] -- Exception occured -- \n\n {1}", _fullMethodName, args.Exception);
                log.Error(message, args.Exception);
            }
            catch (Exception e)
            {
                message = string.Format("[{0}] LoggingAspect failed to log OnException.", _fullMethodName);
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
                message = string.Format("[{0}] Exiting with value: ({1})", _fullMethodName, MarshalReturnValue(args));
                log.Debug(message);
            }
            catch (Exception e)
            {
                message = string.Format("[{0}] LoggingAspect failed to log OnExit. ", _fullMethodName);
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
