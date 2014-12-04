/**  智客知识管理平台。
* ZK_Channel.cs
*
* 功 能： N/A
* 类 名： ZK_Channel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:42   N/A    初版
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
	public partial class ZK_Channel
	{
		private readonly IZK_Channel dal=DataAccess.CreateZK_Channel();
		public ZK_Channel()
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
		public bool Exists(int channelID)
		{
			return dal.Exists(channelID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZK.Model.ZK_Channel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.ZK_Channel model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int channelID)
		{
			
			return dal.Delete(channelID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string channelIDlist )
		{
			return dal.DeleteList(channelIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.ZK_Channel GetModel(int channelID)
		{
			
			return dal.GetModel(channelID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.ZK_Channel GetModelByCache(int channelID)
		{
			
			string CacheKey = "ZK_ChannelModel-" + channelID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(channelID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.ZK_Channel)objModel;
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
		public List<ZK.Model.ZK_Channel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.ZK_Channel> DataTableToList(DataTable dt)
		{
			List<ZK.Model.ZK_Channel> modelList = new List<ZK.Model.ZK_Channel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.ZK_Channel model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.ZK_Channel();
					if(dt.Rows[n]["channelID"]!=null && dt.Rows[n]["channelID"].ToString()!="")
					{
						model.channelID=int.Parse(dt.Rows[n]["channelID"].ToString());
					}
					if(dt.Rows[n]["channelName"]!=null && dt.Rows[n]["channelName"].ToString()!="")
					{
					model.channelName=dt.Rows[n]["channelName"].ToString();
					}
					if(dt.Rows[n]["channelDesc"]!=null && dt.Rows[n]["channelDesc"].ToString()!="")
					{
					model.channelDesc=dt.Rows[n]["channelDesc"].ToString();
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

