/**  智客知识管理平台。
* GROUPS.cs
*
* 功 能： N/A
* 类 名： GROUPS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:34   N/A    初版
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
	/// GROUPS
	/// </summary>
	public partial class GROUPS
	{
		private readonly IGROUPS dal=DataAccess.CreateGROUPS();
		public GROUPS()
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
		public bool Exists(int GROUPID)
		{
			return dal.Exists(GROUPID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.GROUPS model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.GROUPS model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int GROUPID)
		{
			
			return dal.Delete(GROUPID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string GROUPIDlist )
		{
			return dal.DeleteList(GROUPIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.GROUPS GetModel(int GROUPID)
		{
			
			return dal.GetModel(GROUPID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.GROUPS GetModelByCache(int GROUPID)
		{
			
			string CacheKey = "GROUPSModel-" + GROUPID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(GROUPID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.GROUPS)objModel;
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
		public List<ZK.Model.GROUPS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.GROUPS> DataTableToList(DataTable dt)
		{
			List<ZK.Model.GROUPS> modelList = new List<ZK.Model.GROUPS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.GROUPS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.GROUPS();
					if(dt.Rows[n]["GROUPID"]!=null && dt.Rows[n]["GROUPID"].ToString()!="")
					{
						model.GROUPID=int.Parse(dt.Rows[n]["GROUPID"].ToString());
					}
					if(dt.Rows[n]["GROUPNAME"]!=null && dt.Rows[n]["GROUPNAME"].ToString()!="")
					{
					model.GROUPNAME=dt.Rows[n]["GROUPNAME"].ToString();
					}
					if(dt.Rows[n]["INTRODUCTION"]!=null && dt.Rows[n]["INTRODUCTION"].ToString()!="")
					{
					model.INTRODUCTION=dt.Rows[n]["INTRODUCTION"].ToString();
					}
					if(dt.Rows[n]["NOTICE"]!=null && dt.Rows[n]["NOTICE"].ToString()!="")
					{
					model.NOTICE=dt.Rows[n]["NOTICE"].ToString();
					}
					if(dt.Rows[n]["JOINSETTING"]!=null && dt.Rows[n]["JOINSETTING"].ToString()!="")
					{
						model.JOINSETTING=int.Parse(dt.Rows[n]["JOINSETTING"].ToString());
					}
					if(dt.Rows[n]["CREATORID"]!=null && dt.Rows[n]["CREATORID"].ToString()!="")
					{
						model.CREATORID=int.Parse(dt.Rows[n]["CREATORID"].ToString());
					}
					if(dt.Rows[n]["OWNERID"]!=null && dt.Rows[n]["OWNERID"].ToString()!="")
					{
						model.OWNERID=int.Parse(dt.Rows[n]["OWNERID"].ToString());
					}
					if(dt.Rows[n]["OWNERUSERTYPE"]!=null && dt.Rows[n]["OWNERUSERTYPE"].ToString()!="")
					{
						model.OWNERUSERTYPE=int.Parse(dt.Rows[n]["OWNERUSERTYPE"].ToString());
					}
					if(dt.Rows[n]["CREATETIME"]!=null && dt.Rows[n]["CREATETIME"].ToString()!="")
					{
						model.CREATETIME=DateTime.Parse(dt.Rows[n]["CREATETIME"].ToString());
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

