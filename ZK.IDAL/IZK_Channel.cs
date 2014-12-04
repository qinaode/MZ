﻿/**  智客知识管理平台。
* ZK_Channel.cs
*
* 功 能： N/A
* 类 名： ZK_Channel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:42   N/A    初版
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
	/// 接口层1
	/// </summary>
	public interface IZK_Channel
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int channelID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(ZK.Model.ZK_Channel model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(ZK.Model.ZK_Channel model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int channelID);
		bool DeleteList(string channelIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZK.Model.ZK_Channel GetModel(int channelID);
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
