/**  智客知识管理平台。
* View_TeachFileList.cs
*
* 功 能： N/A
* 类 名： View_TeachFileList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/3/19 13:37:34   N/A    初版
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
	/// View_TeachFileList
	/// </summary>
	public partial class View_TeachFileList
	{
		private readonly IView_TeachFileList dal=DataAccess.CreateView_TeachFileList();
		public View_TeachFileList()
		{}
		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.View_TeachFileList model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.View_TeachFileList model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.Delete();
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.View_TeachFileList GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.View_TeachFileList GetModelByCache()
		{
			//该表无主键信息，请自定义主键/条件字段
			string CacheKey = "View_TeachFileListModel-" ;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel();
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.View_TeachFileList)objModel;
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
		public List<ZK.Model.View_TeachFileList> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.View_TeachFileList> DataTableToList(DataTable dt)
		{
			List<ZK.Model.View_TeachFileList> modelList = new List<ZK.Model.View_TeachFileList>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.View_TeachFileList model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.View_TeachFileList();
					if(dt.Rows[n]["id"]!=null && dt.Rows[n]["id"].ToString()!="")
					{
						model.id=long.Parse(dt.Rows[n]["id"].ToString());
					}
					if(dt.Rows[n]["fileID"]!=null && dt.Rows[n]["fileID"].ToString()!="")
					{
						model.fileID=int.Parse(dt.Rows[n]["fileID"].ToString());
					}
					if(dt.Rows[n]["fileName"]!=null && dt.Rows[n]["fileName"].ToString()!="")
					{
					model.fileName=dt.Rows[n]["fileName"].ToString();
					}
					if(dt.Rows[n]["cateID"]!=null && dt.Rows[n]["cateID"].ToString()!="")
					{
						model.cateID=int.Parse(dt.Rows[n]["cateID"].ToString());
					}
					if(dt.Rows[n]["catename"]!=null && dt.Rows[n]["catename"].ToString()!="")
					{
					model.catename=dt.Rows[n]["catename"].ToString();
					}
					if(dt.Rows[n]["fileTypeID"]!=null && dt.Rows[n]["fileTypeID"].ToString()!="")
					{
						model.fileTypeID=int.Parse(dt.Rows[n]["fileTypeID"].ToString());
					}
					if(dt.Rows[n]["fileTypeName"]!=null && dt.Rows[n]["fileTypeName"].ToString()!="")
					{
					model.fileTypeName=dt.Rows[n]["fileTypeName"].ToString();
					}
					if(dt.Rows[n]["channelID"]!=null && dt.Rows[n]["channelID"].ToString()!="")
					{
						model.channelID=int.Parse(dt.Rows[n]["channelID"].ToString());
					}
					if(dt.Rows[n]["channelName"]!=null && dt.Rows[n]["channelName"].ToString()!="")
					{
					model.channelName=dt.Rows[n]["channelName"].ToString();
					}
					if(dt.Rows[n]["createTime"]!=null && dt.Rows[n]["createTime"].ToString()!="")
					{
						model.createTime=DateTime.Parse(dt.Rows[n]["createTime"].ToString());
					}
					if(dt.Rows[n]["imageURL"]!=null && dt.Rows[n]["imageURL"].ToString()!="")
					{
					model.imageURL=dt.Rows[n]["imageURL"].ToString();
					}
					if(dt.Rows[n]["clickNum"]!=null && dt.Rows[n]["clickNum"].ToString()!="")
					{
						model.clickNum=int.Parse(dt.Rows[n]["clickNum"].ToString());
					}
					if(dt.Rows[n]["isDir"]!=null && dt.Rows[n]["isDir"].ToString()!="")
					{
						model.isDir=int.Parse(dt.Rows[n]["isDir"].ToString());
					}
					if(dt.Rows[n]["filePath"]!=null && dt.Rows[n]["filePath"].ToString()!="")
					{
					model.filePath=dt.Rows[n]["filePath"].ToString();
					}
					if(dt.Rows[n]["fileDesc"]!=null && dt.Rows[n]["fileDesc"].ToString()!="")
					{
					model.fileDesc=dt.Rows[n]["fileDesc"].ToString();
					}
					if(dt.Rows[n]["USERID"]!=null && dt.Rows[n]["USERID"].ToString()!="")
					{
						model.USERID=int.Parse(dt.Rows[n]["USERID"].ToString());
					}
					if(dt.Rows[n]["USERNAME"]!=null && dt.Rows[n]["USERNAME"].ToString()!="")
					{
					model.USERNAME=dt.Rows[n]["USERNAME"].ToString();
					}
					if(dt.Rows[n]["CategoryId"]!=null && dt.Rows[n]["CategoryId"].ToString()!="")
					{
						model.CategoryId=int.Parse(dt.Rows[n]["CategoryId"].ToString());
					}
					if(dt.Rows[n]["CategoryName"]!=null && dt.Rows[n]["CategoryName"].ToString()!="")
					{
					model.CategoryName=dt.Rows[n]["CategoryName"].ToString();
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

