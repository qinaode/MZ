/**  智客知识管理平台。
* ACCOUNTSECTIONS.cs
*
* 功 能： N/A
* 类 名： ACCOUNTSECTIONS
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
	/// 接口层ACCOUNTSECTIONS
	/// </summary>
	public interface IACCOUNTSECTIONS
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int BEGINUSERID,int ENDUSERID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(ZK.Model.ACCOUNTSECTIONS model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(ZK.Model.ACCOUNTSECTIONS model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int BEGINUSERID,int ENDUSERID);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZK.Model.ACCOUNTSECTIONS GetModel(int BEGINUSERID,int ENDUSERID);
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
