﻿/**  智客知识管理平台。
* OFFGROUPMSGS.cs
*
* 功 能： N/A
* 类 名： OFFGROUPMSGS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:36   N/A    初版
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
	/// OFFGROUPMSGS
	/// </summary>
	public partial class OFFGROUPMSGS
	{
		private readonly IOFFGROUPMSGS dal=DataAccess.CreateOFFGROUPMSGS();
		public OFFGROUPMSGS()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long SID)
		{
			return dal.Exists(SID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(ZK.Model.OFFGROUPMSGS model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.OFFGROUPMSGS model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long SID)
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
		public ZK.Model.OFFGROUPMSGS GetModel(long SID)
		{
			
			return dal.GetModel(SID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.OFFGROUPMSGS GetModelByCache(long SID)
		{
			
			string CacheKey = "OFFGROUPMSGSModel-" + SID;
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
			return (ZK.Model.OFFGROUPMSGS)objModel;
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
		public List<ZK.Model.OFFGROUPMSGS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.OFFGROUPMSGS> DataTableToList(DataTable dt)
		{
			List<ZK.Model.OFFGROUPMSGS> modelList = new List<ZK.Model.OFFGROUPMSGS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.OFFGROUPMSGS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.OFFGROUPMSGS();
					if(dt.Rows[n]["SID"]!=null && dt.Rows[n]["SID"].ToString()!="")
					{
						model.SID=long.Parse(dt.Rows[n]["SID"].ToString());
					}
					if(dt.Rows[n]["GROUPID"]!=null && dt.Rows[n]["GROUPID"].ToString()!="")
					{
						model.GROUPID=int.Parse(dt.Rows[n]["GROUPID"].ToString());
					}
					if(dt.Rows[n]["MSGID"]!=null && dt.Rows[n]["MSGID"].ToString()!="")
					{
					model.MSGID=dt.Rows[n]["MSGID"].ToString();
					}
					if(dt.Rows[n]["MSGLEVEL"]!=null && dt.Rows[n]["MSGLEVEL"].ToString()!="")
					{
						model.MSGLEVEL=int.Parse(dt.Rows[n]["MSGLEVEL"].ToString());
					}
					if(dt.Rows[n]["CONTENT"]!=null && dt.Rows[n]["CONTENT"].ToString()!="")
					{
					model.CONTENT=dt.Rows[n]["CONTENT"].ToString();
					}
					if(dt.Rows[n]["FONT"]!=null && dt.Rows[n]["FONT"].ToString()!="")
					{
					model.FONT=dt.Rows[n]["FONT"].ToString();
					}
					if(dt.Rows[n]["SENDER_USERID"]!=null && dt.Rows[n]["SENDER_USERID"].ToString()!="")
					{
						model.SENDER_USERID=int.Parse(dt.Rows[n]["SENDER_USERID"].ToString());
					}
					if(dt.Rows[n]["SENDER_NICKNAME"]!=null && dt.Rows[n]["SENDER_NICKNAME"].ToString()!="")
					{
					model.SENDER_NICKNAME=dt.Rows[n]["SENDER_NICKNAME"].ToString();
					}
					if(dt.Rows[n]["SENDER_ACTUALNAME"]!=null && dt.Rows[n]["SENDER_ACTUALNAME"].ToString()!="")
					{
					model.SENDER_ACTUALNAME=dt.Rows[n]["SENDER_ACTUALNAME"].ToString();
					}
					if(dt.Rows[n]["SENDTIME"]!=null && dt.Rows[n]["SENDTIME"].ToString()!="")
					{
						model.SENDTIME=DateTime.Parse(dt.Rows[n]["SENDTIME"].ToString());
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

