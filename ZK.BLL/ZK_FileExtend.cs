/**  智客知识管理平台。
* ZK_FileExtend.cs
*
* 功 能： N/A
* 类 名： ZK_FileExtend
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:45   N/A    初版
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
	public partial class ZK_FileExtend
	{
		private readonly IZK_FileExtend dal=DataAccess.CreateZK_FileExtend();
		public ZK_FileExtend()
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
		public bool Exists(int fileExtendID)
		{
			return dal.Exists(fileExtendID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZK.Model.ZK_FileExtend model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.ZK_FileExtend model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int fileExtendID)
		{
			
			return dal.Delete(fileExtendID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string fileExtendIDlist )
		{
			return dal.DeleteList(fileExtendIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.ZK_FileExtend GetModel(int fileExtendID)
		{
			
			return dal.GetModel(fileExtendID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.ZK_FileExtend GetModelByCache(int fileExtendID)
		{
			
			string CacheKey = "ZK_FileExtendModel-" + fileExtendID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(fileExtendID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.ZK_FileExtend)objModel;
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
		public List<ZK.Model.ZK_FileExtend> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.ZK_FileExtend> DataTableToList(DataTable dt)
		{
			List<ZK.Model.ZK_FileExtend> modelList = new List<ZK.Model.ZK_FileExtend>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.ZK_FileExtend model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.ZK_FileExtend();
					if(dt.Rows[n]["fileExtendID"]!=null && dt.Rows[n]["fileExtendID"].ToString()!="")
					{
						model.fileExtendID=int.Parse(dt.Rows[n]["fileExtendID"].ToString());
					}
					if(dt.Rows[n]["fileID"]!=null && dt.Rows[n]["fileID"].ToString()!="")
					{
						model.fileID=int.Parse(dt.Rows[n]["fileID"].ToString());
					}
					if(dt.Rows[n]["fileExtendName"]!=null && dt.Rows[n]["fileExtendName"].ToString()!="")
					{
					model.fileExtendName=dt.Rows[n]["fileExtendName"].ToString();
					}
					if(dt.Rows[n]["fileExtendValue"]!=null && dt.Rows[n]["fileExtendValue"].ToString()!="")
					{
					model.fileExtendValue=dt.Rows[n]["fileExtendValue"].ToString();
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

