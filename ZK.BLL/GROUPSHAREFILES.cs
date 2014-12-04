/**  智客知识管理平台。
* GROUPSHAREFILES.cs
*
* 功 能： N/A
* 类 名： GROUPSHAREFILES
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:35   N/A    初版
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
	/// GROUPSHAREFILES
	/// </summary>
	public partial class GROUPSHAREFILES
	{
		private readonly IGROUPSHAREFILES dal=DataAccess.CreateGROUPSHAREFILES();
		public GROUPSHAREFILES()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SID)
		{
			return dal.Exists(SID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.GROUPSHAREFILES model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.GROUPSHAREFILES model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string SID)
		{
			
			return dal.Delete(SID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string SIDlist )
		{
			return dal.DeleteList(SIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.GROUPSHAREFILES GetModel(string SID)
		{
			
			return dal.GetModel(SID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.GROUPSHAREFILES GetModelByCache(string SID)
		{
			
			string CacheKey = "GROUPSHAREFILESModel-" + SID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.GROUPSHAREFILES)objModel;
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
		public List<ZK.Model.GROUPSHAREFILES> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.GROUPSHAREFILES> DataTableToList(DataTable dt)
		{
			List<ZK.Model.GROUPSHAREFILES> modelList = new List<ZK.Model.GROUPSHAREFILES>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.GROUPSHAREFILES model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.GROUPSHAREFILES();
					if(dt.Rows[n]["SID"]!=null && dt.Rows[n]["SID"].ToString()!="")
					{
					model.SID=dt.Rows[n]["SID"].ToString();
					}
					if(dt.Rows[n]["MEMBERID"]!=null && dt.Rows[n]["MEMBERID"].ToString()!="")
					{
						model.MEMBERID=int.Parse(dt.Rows[n]["MEMBERID"].ToString());
					}
					if(dt.Rows[n]["GROUPID"]!=null && dt.Rows[n]["GROUPID"].ToString()!="")
					{
						model.GROUPID=int.Parse(dt.Rows[n]["GROUPID"].ToString());
					}
					if(dt.Rows[n]["FILENAMEORDIR"]!=null && dt.Rows[n]["FILENAMEORDIR"].ToString()!="")
					{
					model.FILENAMEORDIR=dt.Rows[n]["FILENAMEORDIR"].ToString();
					}
					if(dt.Rows[n]["ISDIR"]!=null && dt.Rows[n]["ISDIR"].ToString()!="")
					{
						model.ISDIR=int.Parse(dt.Rows[n]["ISDIR"].ToString());
					}
					if(dt.Rows[n]["ALLFILECOUNT"]!=null && dt.Rows[n]["ALLFILECOUNT"].ToString()!="")
					{
						model.ALLFILECOUNT=int.Parse(dt.Rows[n]["ALLFILECOUNT"].ToString());
					}
					if(dt.Rows[n]["ALLFILESIZE"]!=null && dt.Rows[n]["ALLFILESIZE"].ToString()!="")
					{
						model.ALLFILESIZE=long.Parse(dt.Rows[n]["ALLFILESIZE"].ToString());
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

