/**  智客知识管理平台。
* DEPARTUSERS.cs
*
* 功 能： N/A
* 类 名： DEPARTUSERS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/1 10:24:50   N/A    初版
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
	/// DEPARTUSERS
	/// </summary>
	public partial class DEPARTUSERS
	{
		private readonly IDEPARTUSERS dal=DataAccess.CreateDEPARTUSERS();
		public DEPARTUSERS()
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
		public bool Exists(int USERID,int DEPARTID)
		{
			return dal.Exists(USERID,DEPARTID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.DEPARTUSERS model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.DEPARTUSERS model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int USERID,int DEPARTID)
		{
			
			return dal.Delete(USERID,DEPARTID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.DEPARTUSERS GetModel(int USERID,int DEPARTID)
		{
			
			return dal.GetModel(USERID,DEPARTID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.DEPARTUSERS GetModelByCache(int USERID,int DEPARTID)
		{
			
			string CacheKey = "DEPARTUSERSModel-" + USERID+DEPARTID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(USERID,DEPARTID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.DEPARTUSERS)objModel;
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
		public List<ZK.Model.DEPARTUSERS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.DEPARTUSERS> DataTableToList(DataTable dt)
		{
			List<ZK.Model.DEPARTUSERS> modelList = new List<ZK.Model.DEPARTUSERS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.DEPARTUSERS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.DEPARTUSERS();
					if(dt.Rows[n]["USERID"]!=null && dt.Rows[n]["USERID"].ToString()!="")
					{
						model.USERID=int.Parse(dt.Rows[n]["USERID"].ToString());
					}
					if(dt.Rows[n]["DEPARTID"]!=null && dt.Rows[n]["DEPARTID"].ToString()!="")
					{
						model.DEPARTID=int.Parse(dt.Rows[n]["DEPARTID"].ToString());
					}
					if(dt.Rows[n]["DEPARTNAME"]!=null && dt.Rows[n]["DEPARTNAME"].ToString()!="")
					{
					model.DEPARTNAME=dt.Rows[n]["DEPARTNAME"].ToString();
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

