﻿/**  智客知识管理平台。
* ADMINLOGS.cs
*
* 功 能： N/A
* 类 名： ADMINLOGS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:33   N/A    初版
*
* Copyright (c) 2012 BeiJing HaoLian Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：北京浩联教育科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace ZK.IDAL
{
	/// <summary>
	/// 接口层ADMINLOGS
	/// </summary>
	public interface IADMINLOGS
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(long SID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		long Add(ZK.Model.ADMINLOGS model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(ZK.Model.ADMINLOGS model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(long SID);
		bool DeleteList(string SIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZK.Model.ADMINLOGS GetModel(long SID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		int GetRecordCount(string strWhere);
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
	} 
}
