/**  智客知识管理平台。
* ZK_RoleList.cs
*
* 功 能： N/A
* 类 名： ZK_RoleList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/31 10:47:22   N/A    初版
*
* Copyright (c) 2012 BeiJing HaoLian Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：北京浩联教育科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using ZK.Common;
using ZK.Model;
using ZK.DALFactory;
using ZK.IDAL;
namespace ZK.BLL
{
	/// <summary>
	/// ZK_RoleList
	/// </summary>
	public partial class ZK_RoleList
	{
		private readonly IZK_RoleList dal=DataAccess.CreateZK_RoleList();
		public ZK_RoleList()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int roleID)
		{
			return dal.Exists(roleID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZK.Model.ZK_RoleList model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.ZK_RoleList model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int roleID)
		{
			
			return dal.Delete(roleID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string roleIDlist )
		{
			return dal.DeleteList(roleIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.ZK_RoleList GetModel(int roleID)
		{
			
			return dal.GetModel(roleID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.ZK_RoleList GetModelByCache(int roleID)
		{
			
			string CacheKey = "ZK_RoleListModel-" + roleID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(roleID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.ZK_RoleList)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.ZK_RoleList> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.ZK_RoleList> DataTableToList(DataTable dt)
		{
			List<ZK.Model.ZK_RoleList> modelList = new List<ZK.Model.ZK_RoleList>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.ZK_RoleList model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.ZK_RoleList();
					if(dt.Rows[n]["roleID"]!=null && dt.Rows[n]["roleID"].ToString()!="")
					{
						model.roleID=int.Parse(dt.Rows[n]["roleID"].ToString());
					}
					if(dt.Rows[n]["roleName"]!=null && dt.Rows[n]["roleName"].ToString()!="")
					{
					model.roleName=dt.Rows[n]["roleName"].ToString();
					}
					if(dt.Rows[n]["roleDesc"]!=null && dt.Rows[n]["roleDesc"].ToString()!="")
					{
					model.roleDesc=dt.Rows[n]["roleDesc"].ToString();
					}
					if(dt.Rows[n]["roleASC"]!=null && dt.Rows[n]["roleASC"].ToString()!="")
					{
						model.roleASC=int.Parse(dt.Rows[n]["roleASC"].ToString());
					}
					if(dt.Rows[n]["roleType"]!=null && dt.Rows[n]["roleType"].ToString()!="")
					{
						model.roleType=int.Parse(dt.Rows[n]["roleType"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			return dal.GetList(PageSize,PageIndex,strWhere);
		}

		#endregion  Method
	}
}

