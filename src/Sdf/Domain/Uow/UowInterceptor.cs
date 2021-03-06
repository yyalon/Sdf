﻿using Castle.DynamicProxy;
using Sdf.Application;
using Sdf.Common;
using Sdf.Domain.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sdf.Domain.Uow
{
    public class UowInterceptor:IInterceptor
    {
        private IUowManager uowManager;
        private string id;
        public UowInterceptor(IUowManager uowManager)
        {
            this.uowManager = uowManager;
            id = Guid.NewGuid().ToString();
        }
       
        private bool IsOperationResult(Type objectType)
        {
            if (objectType == typeof(OperationResult))
            {
                return true;
            }
            if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(OperationResult<>))
            {
                return true;
            }
            return false;
        }
        private bool? ParseOperationResultState(object obj)
        {
            if (obj != null && IsOperationResult(obj.GetType()))
            {
                var pState = obj.GetType().GetProperties().Where(m => m.Name == nameof(OperationResult.State)).FirstOrDefault();
                var stateValue = (bool)pState.GetValue(obj);
                return stateValue;
            }
            return null;
        }
        //private void SetOperationResultMsg(object obj,string msg, List<object> errList)
        //{
        //    if (obj == null)
        //    {
        //        return;
        //    }
        //    var pstate = obj.GetType().GetProperties().Where(m => m.Name == nameof(OperationResult.State)).FirstOrDefault();
        //    var pmsg = obj.GetType().GetProperties().Where(m => m.Name == nameof(OperationResult.Msg)).FirstOrDefault();
        //    var perrorList = obj.GetType().GetProperties().Where(m => m.Name == nameof(OperationResult.ErrorList)).FirstOrDefault();
           
        //    pstate.SetValue(obj, false);
        //    pmsg.SetValue(obj, msg);
        //    perrorList.SetValue(obj, errList);
        //}
        public void Intercept(IInvocation invocation)
        {
            var uow = uowManager.Begin();
            invocation.Proceed();

            if (uow != null)
            {
                var returnValue = invocation.ReturnValue;
                
                bool? state = ParseOperationResultState(returnValue);
               
                if (state == null || state.Value)
                {
                    var dbRes = uow.Complete();
                    if (dbRes != null && !dbRes.State)
                    {
                        if (state.HasValue)
                        {
                            List<object> errList = new List<object>();
                            if (dbRes.ErrorList != null)
                            {
                                foreach (var item in dbRes.ErrorList)
                                {
                                    errList.Add(item.Message);
                                }
                            }
                            invocation.ReturnValue = OperationResult.CreateFailedResult("数据库异常", errList);
                            //SetOperationResultMsg(returnValue, "数据库异常", errList);
                        }
                    }
                    while (!uowManager.CompletedHandles.IsEmpty)
                    {
                        Action<DbChangeResult> handle = null;
                        if (uowManager.CompletedHandles.TryDequeue(out handle))
                        {
                            handle.Invoke(dbRes);
                        }
                    }
                }
                uow.Dispose();
            }
        }
    }
}
