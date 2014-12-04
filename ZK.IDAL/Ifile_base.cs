/**  智客知识管理平台。
* file_base.cs
*
* 功 能： N/A
* 类 名： file_base
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/28 14:00:05   N/A    初版
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
	/// 接口层file_base
	/// </summary>
	public interface Ifile_base
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int file_id);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(ZK.Model.file_base model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(ZK.Model.file_base model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int file_id);
		bool DeleteList(string file_idlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZK.Model.file_base GetModel(int file_id);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
//		DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
	} 
}
