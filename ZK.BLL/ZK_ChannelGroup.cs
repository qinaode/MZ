/**  智客知识管理平台。
* ZK_ChannelGroup.cs
*
* 功 能： N/A
* 类 名： ZK_ChannelGroup
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
	public partial class ZK_ChannelGroup
	{
		private readonly IZK_ChannelGroup dal=DataAccess.CreateZK_ChannelGroup();
		public ZK_ChannelGroup()
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
		public bool Exists(int channelGroupID)
		{
			return dal.Exists(channelGroupID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZK.Model.ZK_ChannelGroup model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.ZK_ChannelGroup model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int channelGroupID)
		{
			
			return dal.Delete(channelGroupID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string channelGroupIDlist )
		{
			return dal.DeleteList(channelGroupIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.ZK_ChannelGroup GetModel(int channelGroupID)
		{
			
			return dal.GetModel(channelGroupID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.ZK_ChannelGroup GetModelByCache(int channelGroupID)
		{
			
			string CacheKey = "ZK_ChannelGroupModel-" + channelGroupID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(channelGroupID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.ZK_ChannelGroup)objModel;
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
		public List<ZK.Model.ZK_ChannelGroup> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.ZK_ChannelGroup> DataTableToList(DataTable dt)
		{
			List<ZK.Model.ZK_ChannelGroup> modelList = new List<ZK.Model.ZK_ChannelGroup>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.ZK_ChannelGroup model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.ZK_ChannelGroup();
					if(dt.Rows[n]["channelGroupID"]!=null && dt.Rows[n]["channelGroupID"].ToString()!="")
					{
						model.channelGroupID=int.Parse(dt.Rows[n]["channelGroupID"].ToString());
					}
					if(dt.Rows[n]["channelID"]!=null && dt.Rows[n]["channelID"].ToString()!="")
					{
						model.channelID=int.Parse(dt.Rows[n]["channelID"].ToString());
					}
					if(dt.Rows[n]["channelGroupName"]!=null && dt.Rows[n]["channelGroupName"].ToString()!="")
					{
					model.channelGroupName=dt.Rows[n]["channelGroupName"].ToString();
					}
					if(dt.Rows[n]["channelGroupDesc"]!=null && dt.Rows[n]["channelGroupDesc"].ToString()!="")
					{
					model.channelGroupDesc=dt.Rows[n]["channelGroupDesc"].ToString();
					}
					if(dt.Rows[n]["channelGroupParent"]!=null && dt.Rows[n]["channelGroupParent"].ToString()!="")
					{
						model.channelGroupParent=int.Parse(dt.Rows[n]["channelGroupParent"].ToString());
					}
					if(dt.Rows[n]["channelGroupLevel"]!=null && dt.Rows[n]["channelGroupLevel"].ToString()!="")
					{
						model.channelGroupLevel=int.Parse(dt.Rows[n]["channelGroupLevel"].ToString());
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

