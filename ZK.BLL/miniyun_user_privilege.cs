﻿/**  智客知识管理平台。
* miniyun_user_privilege.cs
*
* 功 能： N/A
* 类 名： miniyun_user_privilege
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/24 14:54:57   N/A    初版
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
	/// miniyun_user_privilege
	/// </summary>
	public partial class miniyun_user_privilege
	{
		private readonly Iminiyun_user_privilege dal=DataAccess.Createminiyun_user_privilege();
		public miniyun_user_privilege()
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
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.miniyun_user_privilege model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.miniyun_user_privilege model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.miniyun_user_privilege GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.miniyun_user_privilege GetModelByCache(int id)
		{
			
			string CacheKey = "miniyun_user_privilegeModel-" + id;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.miniyun_user_privilege)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.miniyun_user_privilege> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.miniyun_user_privilege> DataTableToList(DataTable dt)
		{
			List<ZK.Model.miniyun_user_privilege> modelList = new List<ZK.Model.miniyun_user_privilege>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.miniyun_user_privilege model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.miniyun_user_privilege();
					if(dt.Rows[n]["id"]!=null && dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					if(dt.Rows[n]["user_id"]!=null && dt.Rows[n]["user_id"].ToString()!="")
					{
						model.user_id=int.Parse(dt.Rows[n]["user_id"].ToString());
					}
					if(dt.Rows[n]["file_path"]!=null && dt.Rows[n]["file_path"].ToString()!="")
					{
					model.file_path=dt.Rows[n]["file_path"].ToString();
					}
					if(dt.Rows[n]["permission"]!=null && dt.Rows[n]["permission"].ToString()!="")
					{
					model.permission=dt.Rows[n]["permission"].ToString();
					}
					if(dt.Rows[n]["created_at"]!=null && dt.Rows[n]["created_at"].ToString()!="")
					{
						model.created_at=DateTime.Parse(dt.Rows[n]["created_at"].ToString());
					}
					if(dt.Rows[n]["updated_at"]!=null && dt.Rows[n]["updated_at"].ToString()!="")
					{
						model.updated_at=DateTime.Parse(dt.Rows[n]["updated_at"].ToString());
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


        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    return dal.GetRecordCount(strWhere);
        //}
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        //{
        //    return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
        //}
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

