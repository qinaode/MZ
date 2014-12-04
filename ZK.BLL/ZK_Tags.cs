/**  智客知识管理平台。
* ZK_Tags.cs
*
* 功 能： N/A
* 类 名： ZK_Tags
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:49   N/A    初版
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
	public partial class ZK_Tags
	{
		private readonly IZK_Tags dal=DataAccess.CreateZK_Tags();
		public ZK_Tags()
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
		public bool Exists(int tagID)
		{
			return dal.Exists(tagID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZK.Model.ZK_Tags model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.ZK_Tags model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int tagID)
		{
			
			return dal.Delete(tagID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string tagIDlist )
		{
			return dal.DeleteList(tagIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.ZK_Tags GetModel(int tagID)
		{
			
			return dal.GetModel(tagID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.ZK_Tags GetModelByCache(int tagID)
		{
			
			string CacheKey = "ZK_TagsModel-" + tagID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(tagID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.ZK_Tags)objModel;
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
		public List<ZK.Model.ZK_Tags> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.ZK_Tags> DataTableToList(DataTable dt)
		{
			List<ZK.Model.ZK_Tags> modelList = new List<ZK.Model.ZK_Tags>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.ZK_Tags model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.ZK_Tags();
					if(dt.Rows[n]["tagID"]!=null && dt.Rows[n]["tagID"].ToString()!="")
					{
						model.tagID=int.Parse(dt.Rows[n]["tagID"].ToString());
					}
					if(dt.Rows[n]["tagName"]!=null && dt.Rows[n]["tagName"].ToString()!="")
					{
					model.tagName=dt.Rows[n]["tagName"].ToString();
					}
					if(dt.Rows[n]["ownerID"]!=null && dt.Rows[n]["ownerID"].ToString()!="")
					{
						model.ownerID=int.Parse(dt.Rows[n]["ownerID"].ToString());
					}
					if(dt.Rows[n]["relevantNum"]!=null && dt.Rows[n]["relevantNum"].ToString()!="")
					{
						model.relevantNum=int.Parse(dt.Rows[n]["relevantNum"].ToString());
					}
					if(dt.Rows[n]["relevantNumdy"]!=null && dt.Rows[n]["relevantNumdy"].ToString()!="")
					{
						model.relevantNumdy=int.Parse(dt.Rows[n]["relevantNumdy"].ToString());
					}
					if(dt.Rows[n]["relevantNumjx"]!=null && dt.Rows[n]["relevantNumjx"].ToString()!="")
					{
						model.relevantNumjx=int.Parse(dt.Rows[n]["relevantNumjx"].ToString());
					}
					if(dt.Rows[n]["relevantNumxz"]!=null && dt.Rows[n]["relevantNumxz"].ToString()!="")
					{
						model.relevantNumxz=int.Parse(dt.Rows[n]["relevantNumxz"].ToString());
					}
					if(dt.Rows[n]["createTime"]!=null && dt.Rows[n]["createTime"].ToString()!="")
					{
						model.createTime=DateTime.Parse(dt.Rows[n]["createTime"].ToString());
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

