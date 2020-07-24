using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Application
{
    public class OperationResult
    {
        public string Msg { get;  set; }
        public bool State { get;  set; }
        //public bool IsHasNextOperation { get; set; }
        public object Result { get; set; }
        public List<object> ErrorList { get;  set; }
        public OperationResult(string msg, bool state)
        {
            this.Msg = msg;
            this.State = state;
        }
        public OperationResult(string msg, bool state, object result)
        {
            this.Msg = msg;
            this.State = state;
            this.Result = result;
        }
        public OperationResult(string msg, bool state, object result, List<object> errorList)
        {
            this.Msg = msg;
            this.State = state;
            this.Result = result;
            this.ErrorList = errorList;
        }
        public void ReplaceInstance(OperationResult newOperationResult)
        {
            this.ErrorList = newOperationResult.ErrorList;
            this.Msg = newOperationResult.Msg;
            this.Result = newOperationResult.Result;
            this.State = newOperationResult.State;
        }
        public OperationResult()
        {

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
    }
    public class OperationResult<T>
    {
        public string Msg { get;  set; }
        public bool State { get;  set; }
        public bool IsHasNextOperation { get; set; }
        public T Result { get; set; }
        public List<object> ErrorList { get;  set; }
        public OperationResult(string msg, bool state)
        {
            this.Msg = msg;
            this.State = state;
        }
        public OperationResult(string msg, bool state, T result)
        {
            this.Msg = msg;
            this.State = state;
            this.Result = result;
        }

        public OperationResult(string msg, bool state, T result, List<object> errorList)
        {
            this.Msg = msg;
            this.State = state;
            this.Result = result;
            this.ErrorList = errorList;
        }
        public void ReplaceInstance(OperationResult<T> newOperationResult)
        {
            this.ErrorList = newOperationResult.ErrorList;
            this.Msg = newOperationResult.Msg;
            this.Result = newOperationResult.Result;
            this.State = newOperationResult.State;
        }
        public OperationResult()
        {

        }
        public OperationResult ToOperationResult()
        {
            return new OperationResult(Msg, State, Result, ErrorList);
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
    /*public class OperationResult<T>: OperationResult
    {
        //public string Msg { get; private set; }
        //public bool State { get; private set; }
        //public bool IsHasNextOperation { get; set; }
        public new T Result { get; set; }
        //public List<object> ErrorList { get; private set; }
        public OperationResult(string msg, bool state)
        {
            this.Msg = msg;
            this.State = state;
        }
        public OperationResult(string msg, bool state, T result)
        {
            this.Msg = msg;
            this.State = state;
            this.Result = result;
        }

        public OperationResult(string msg, bool state, T result, List<object> errorList)
        {
            this.Msg = msg;
            this.State = state;
            this.Result = result;
            this.ErrorList = errorList;
        }
        public void ReplaceInstance(OperationResult<T> newOperationResult)
        {
            this.ErrorList = newOperationResult.ErrorList;
            this.Msg = newOperationResult.Msg;
            this.Result = newOperationResult.Result;
            this.State = newOperationResult.State;
        }
        public OperationResult()
        {

        }
        /// <summary>
        /// 创建Success的OperationResult
        /// </summary>
        /// <param name="operationMsg"></param>
        /// <returns></returns>
        public new static OperationResult<T> CreateSuccessResult(string msg)
        {
            return new OperationResult<T>(msg, true);
        }
        /// <summary>
        /// 创建Success的OperationResult
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public  static OperationResult<T> CreateSuccessResult(T result)
        {
            return new OperationResult<T>("ok", true, result);
        }
        /// <summary>
        /// 创建Success的OperationResult
        /// </summary>
        /// <param name="operationMsg"></param>
        /// <returns></returns>
        public  static OperationResult<T> CreateSuccessResult(string msg, T result)
        {
            return new OperationResult<T>(msg, true, result);
        }
        /// <summary>
        /// 创建Failed的OperationResult
        /// </summary>
        /// <param name="operationMsg"></param>
        /// <returns></returns>
        public new static OperationResult<T> CreateFailedResult(string msg)
        {
            return new OperationResult<T>(msg, false);
        }
        /// <summary>
        /// 创建Failed的OperationResult
        /// </summary>
        /// <param name="operationMsg"></param>
        /// <returns></returns>
        public  static OperationResult<T> CreateFailedResult(string msg, T result)
        {
            return new OperationResult<T>(msg, false, result);
        }

        /// <summary>
        /// 默认的成功操作结果
        /// </summary>
        public new static OperationResult<T> DefaultSuccessResult = new OperationResult<T>("操作成功", true);
        /// <summary>
        /// 默认的失败操作结果
        /// </summary>
        public new static OperationResult<T> DefaultFailedResult = new OperationResult<T>("操作失败", false);


    }*/
}
