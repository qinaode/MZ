/**  智客知识管理平台。
* DEPARTMENTS.cs
*
* 功 能： N/A
* 类 名： DEPARTMENTS
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
	/// DEPARTMENTS
	/// </summary>
	public partial class DEPARTMENTS
	{
		private readonly IDEPARTMENTS dal=DataAccess.CreateDEPARTMENTS();
		public DEPARTMENTS()
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
		public bool Exists(int DEPARTID)
		{
			return dal.Exists(DEPARTID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.DEPARTMENTS model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.DEPARTMENTS model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int DEPARTID)
		{
			
			return dal.Delete(DEPARTID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string DEPARTIDlist )
		{
			return dal.DeleteList(DEPARTIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.DEPARTMENTS GetModel(int DEPARTID)
		{
			
			return dal.GetModel(DEPARTID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.DEPARTMENTS GetModelByCache(int DEPARTID)
		{
			
			string CacheKey = "DEPARTMENTSModel-" + DEPARTID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DEPARTID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.DEPARTMENTS)objModel;
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
		public List<ZK.Model.DEPARTMENTS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.DEPARTMENTS> DataTableToList(DataTable dt)
		{
			List<ZK.Model.DEPARTMENTS> modelList = new List<ZK.Model.DEPARTMENTS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.DEPARTMENTS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.DEPARTMENTS();
					if(dt.Rows[n]["DEPARTID"]!=null && dt.Rows[n]["DEPARTID"].ToString()!="")
					{
						model.DEPARTID=int.Parse(dt.Rows[n]["DEPARTID"].ToString());
					}
					if(dt.Rows[n]["DEPARTNAME"]!=null && dt.Rows[n]["DEPARTNAME"].ToString()!="")
					{
					model.DEPARTNAME=dt.Rows[n]["DEPARTNAME"].ToString();
					}
					if(dt.Rows[n]["ORDERVALUE"]!=null && dt.Rows[n]["ORDERVALUE"].ToString()!="")
					{
						model.ORDERVALUE=int.Parse(dt.Rows[n]["ORDERVALUE"].ToString());
					}
					if(dt.Rows[n]["PARENTDEPARTID"]!=null && dt.Rows[n]["PARENTDEPARTID"].ToString()!="")
					{
						model.PARENTDEPARTID=int.Parse(dt.Rows[n]["PARENTDEPARTID"].ToString());
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

