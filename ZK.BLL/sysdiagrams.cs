/**  智客知识管理平台。
* sysdiagrams.cs
*
* 功 能： N/A
* 类 名： sysdiagrams
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:38   N/A    初版
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
	/// 1
	/// </summary>
	public partial class sysdiagrams
	{
		private readonly Isysdiagrams dal=DataAccess.Createsysdiagrams();
		public sysdiagrams()
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
		public bool Exists(string name,int principal_id,int diagram_id)
		{
			return dal.Exists(name,principal_id,diagram_id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZK.Model.sysdiagrams model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.sysdiagrams model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int diagram_id)
		{
			
			return dal.Delete(diagram_id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string name,int principal_id,int diagram_id)
		{
			
			return dal.Delete(name,principal_id,diagram_id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string diagram_idlist )
		{
			return dal.DeleteList(diagram_idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.sysdiagrams GetModel(int diagram_id)
		{
			
			return dal.GetModel(diagram_id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.sysdiagrams GetModelByCache(int diagram_id)
		{
			
			string CacheKey = "sysdiagramsModel-" + diagram_id;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(diagram_id);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.sysdiagrams)objModel;
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
		public List<ZK.Model.sysdiagrams> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.sysdiagrams> DataTableToList(DataTable dt)
		{
			List<ZK.Model.sysdiagrams> modelList = new List<ZK.Model.sysdiagrams>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.sysdiagrams model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.sysdiagrams();
					if(dt.Rows[n]["name"]!=null && dt.Rows[n]["name"].ToString()!="")
					{
					model.name=dt.Rows[n]["name"].ToString();
					}
					if(dt.Rows[n]["principal_id"]!=null && dt.Rows[n]["principal_id"].ToString()!="")
					{
						model.principal_id=int.Parse(dt.Rows[n]["principal_id"].ToString());
					}
					if(dt.Rows[n]["diagram_id"]!=null && dt.Rows[n]["diagram_id"].ToString()!="")
					{
						model.diagram_id=int.Parse(dt.Rows[n]["diagram_id"].ToString());
					}
					if(dt.Rows[n]["version"]!=null && dt.Rows[n]["version"].ToString()!="")
					{
						model.version=int.Parse(dt.Rows[n]["version"].ToString());
					}
					if(dt.Rows[n]["definition"]!=null && dt.Rows[n]["definition"].ToString()!="")
					{
						model.definition=(byte[])dt.Rows[n]["definition"];
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

