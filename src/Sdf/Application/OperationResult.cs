using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Application
{
    public class OperationResult
    {
        public virtual string Msg { get;  set; }
        public virtual bool Success { get;  set; }
        //public bool IsHasNextOperation { get; set; }
        public virtual object Result { get; set; }
        public virtual List<object> ErrorList { get;  set; }
        public OperationResult(string msg, bool success)
        {
            this.Msg = msg;
            this.Success = success;
        }
        public OperationResult(string msg, bool success, object result)
        {
            this.Msg = msg;
            this.Success = success;
            this.Result = result;
        }
  
        public OperationResult(string msg, bool success, object result, List<object> errorList)
        {
            this.Msg = msg;
            this.Success = success;
            this.Result = result;
            this.ErrorList = errorList;
        }
        public void ReplaceInstance(OperationResult newOperationResult)
        {
            this.ErrorList = newOperationResult.ErrorList;
            this.Msg = newOperationResult.Msg;
            this.Result = newOperationResult.Result;
            this.Success = newOperationResult.Success;
        }
        public OperationResult()
        {

        }
        public virtual void CreateDefaultSuccessResult(string msg)
        { 
            this.Msg=msg;
            this.Success = true;
        }
        public virtual void CreateDefaultFailedResult(string msg, List<object> errorList)
        {
            this.Msg = msg;
            this.Success = false;
            this.ErrorList=errorList;
        }
        /// <summary>
        /// 创建Success的OperationResult
        /// </summary>
        /// <param name="operationMsg"></param>
        /// <returns></returns>
        public static OperationResult CreateSuccessResult(string msg)
        {
            return new OperationResult(msg, true);
        }
        /// <summary>
        /// 创建Success的OperationResult
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static OperationResult CreateSuccessResult(object result)
        {
            return new OperationResult("ok", true, result);
        }
        /// <summary>
        /// 创建Success的OperationResult
        /// </summary>
        /// <param name="operationMsg"></param>
        /// <returns></returns>
        public static OperationResult CreateSuccessResult(string msg, object result)
        {
            return new OperationResult(msg, true, result);
        }
        /// <summary>
        /// 创建Failed的OperationResult
        /// </summary>
        /// <param name="operationMsg"></param>
        /// <returns></returns>
        public static OperationResult CreateFailedResult(string msg)
        {
            return new OperationResult(msg, false);
        }
        /// <summary>
        /// 创建Failed的OperationResult
        /// </summary>
        /// <param name="operationMsg"></param>
        /// <returns></returns>
        public static OperationResult CreateFailedResult(string msg, object result)
        {
            return new OperationResult(msg, false, result);
        }

        /// <summary>
        /// 默认的成功操作结果
        /// </summary>
        public static OperationResult DefaultSuccessResult = new OperationResult("操作成功", true);
        /// <summary>
        /// 默认的失败操作结果
        /// </summary>
        public static OperationResult DefaultFailedResult = new OperationResult("操作失败", false);
        public override string ToString()
        {
            return $"Success:{Success} msg:{Msg}";
        }
    }
    public class OperationResult<T>
    {
        public string Msg { get; set; }
        public bool Success { get; set; }

        public  T Result { get; set; }
        public List<object> ErrorList { get; set; }
        public OperationResult(string msg, bool success)
        {
            this.Msg = msg;
            this.Success = success;
        }
        public OperationResult(string msg, bool success, T result)
        {
            this.Msg = msg;
            this.Success = success;
            this.Result = result;
        }

        public OperationResult(string msg, bool success, T result, List<object> errorList)
        {
            this.Msg = msg;
            this.Success = success;
            this.Result = result;
            this.ErrorList = errorList;
        }
        public void ReplaceInstance(OperationResult<T> newOperationResult)
        {
            this.ErrorList = newOperationResult.ErrorList;
            this.Msg = newOperationResult.Msg;
            this.Result = newOperationResult.Result;
            this.Success = newOperationResult.Success;
        }
        public OperationResult()
        {

        }
        public OperationResult ToOperationResult()
        {
            return new OperationResult(Msg, Success, Result, ErrorList);
        }
        /// <summary>
        /// 创建Success的OperationResult
        /// </summary>
        /// <param name="operationMsg"></param>
        /// <returns></returns>
        public static OperationResult<T> CreateSuccessResult(string msg)
        {
            return new OperationResult<T>(msg, true);
        }
        /// <summary>
        /// 创建Success的OperationResult
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static OperationResult<T> CreateSuccessResult(T result)
        {
            return new OperationResult<T>("ok", true, result);
        }
        /// <summary>
        /// 创建Success的OperationResult
        /// </summary>
        /// <param name="operationMsg"></param>
        /// <returns></returns>
        public static OperationResult<T> CreateSuccessResult(string msg, T result)
        {
            return new OperationResult<T>(msg, true, result);
        }
        /// <summary>
        /// 创建Failed的OperationResult
        /// </summary>
        /// <param name="operationMsg"></param>
        /// <returns></returns>
        public static OperationResult<T> CreateFailedResult(string msg)
        {
            return new OperationResult<T>(msg, false);
        }
        /// <summary>
        /// 创建Failed的OperationResult
        /// </summary>
        /// <param name="operationMsg"></param>
        /// <returns></returns>
        public static OperationResult<T> CreateFailedResult(string msg, T result)
        {
            return new OperationResult<T>(msg, false, result);
        }

        /// <summary>
        /// 默认的成功操作结果
        /// </summary>
        public static OperationResult<T> DefaultSuccessResult = new OperationResult<T>("操作成功", true);
        /// <summary>
        /// 默认的失败操作结果
        /// </summary>
        public static OperationResult<T> DefaultFailedResult = new OperationResult<T>("操作失败", false);


    }
    
}
