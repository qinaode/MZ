/**  智客知识管理平台。
* ZK_FileList.cs
*
* 功 能： N/A
* 类 名： ZK_FileList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/31 13:58:01   N/A    初版
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
	/// ZK_FileList
	/// </summary>
	public partial class ZK_FileList
	{
		private readonly IZK_FileList dal=DataAccess.CreateZK_FileList();
		public ZK_FileList()
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
		public bool Exists(int fileID)
		{
			return dal.Exists(fileID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZK.Model.ZK_FileList model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.ZK_FileList model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int fileID)
		{
			
			return dal.Delete(fileID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string fileIDlist )
		{
			return dal.DeleteList(fileIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.ZK_FileList GetModel(int fileID)
		{
			
			return dal.GetModel(fileID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.ZK_FileList GetModelByCache(int fileID)
		{
			
			string CacheKey = "ZK_FileListModel-" + fileID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(fileID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.ZK_FileList)objModel;
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
		public List<ZK.Model.ZK_FileList> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.ZK_FileList> DataTableToList(DataTable dt)
		{
			List<ZK.Model.ZK_FileList> modelList = new List<ZK.Model.ZK_FileList>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.ZK_FileList model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.ZK_FileList();
					if(dt.Rows[n]["fileID"]!=null && dt.Rows[n]["fileID"].ToString()!="")
					{
						model.fileID=int.Parse(dt.Rows[n]["fileID"].ToString());
					}
					if(dt.Rows[n]["fileName"]!=null && dt.Rows[n]["fileName"].ToString()!="")
					{
					model.fileName=dt.Rows[n]["fileName"].ToString();
					}
					if(dt.Rows[n]["filePath"]!=null && dt.Rows[n]["filePath"].ToString()!="")
					{
					model.filePath=dt.Rows[n]["filePath"].ToString();
					}
					if(dt.Rows[n]["parentID"]!=null && dt.Rows[n]["parentID"].ToString()!="")
					{
						model.parentID=int.Parse(dt.Rows[n]["parentID"].ToString());
					}
					if(dt.Rows[n]["isDir"]!=null && dt.Rows[n]["isDir"].ToString()!="")
					{
						model.isDir=int.Parse(dt.Rows[n]["isDir"].ToString());
					}
					if(dt.Rows[n]["ownerID"]!=null && dt.Rows[n]["ownerID"].ToString()!="")
					{
						model.ownerID=int.Parse(dt.Rows[n]["ownerID"].ToString());
					}
					if(dt.Rows[n]["fileTypeID"]!=null && dt.Rows[n]["fileTypeID"].ToString()!="")
					{
						model.fileTypeID=int.Parse(dt.Rows[n]["fileTypeID"].ToString());
					}
					if(dt.Rows[n]["clickNum"]!=null && dt.Rows[n]["clickNum"].ToString()!="")
					{
						model.clickNum=int.Parse(dt.Rows[n]["clickNum"].ToString());
					}
					if(dt.Rows[n]["createTime"]!=null && dt.Rows[n]["createTime"].ToString()!="")
					{
						model.createTime=DateTime.Parse(dt.Rows[n]["createTime"].ToString());
					}
					if(dt.Rows[n]["imageURL"]!=null && dt.Rows[n]["imageURL"].ToString()!="")
					{
					model.imageURL=dt.Rows[n]["imageURL"].ToString();
					}
					if(dt.Rows[n]["fileDesc"]!=null && dt.Rows[n]["fileDesc"].ToString()!="")
					{
					model.fileDesc=dt.Rows[n]["fileDesc"].ToString();
					}
					if(dt.Rows[n]["trastatus"]!=null && dt.Rows[n]["trastatus"].ToString()!="")
					{
						model.trastatus=int.Parse(dt.Rows[n]["trastatus"].ToString());
					}
					if(dt.Rows[n]["isTraf"]!=null && dt.Rows[n]["isTraf"].ToString()!="")
					{
						model.isTraf=int.Parse(dt.Rows[n]["isTraf"].ToString());
					}
					if(dt.Rows[n]["fileOldID"]!=null && dt.Rows[n]["fileOldID"].ToString()!="")
					{
						model.fileOldID=int.Parse(dt.Rows[n]["fileOldID"].ToString());
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

