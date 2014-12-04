/**  智客知识管理平台。
* GROUPMEMBERS.cs
*
* 功 能： N/A
* 类 名： GROUPMEMBERS
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
	/// GROUPMEMBERS
	/// </summary>
	public partial class GROUPMEMBERS
	{
		private readonly IGROUPMEMBERS dal=DataAccess.CreateGROUPMEMBERS();
		public GROUPMEMBERS()
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
		public bool Exists(int MEMBERID,int GROUPID)
		{
			return dal.Exists(MEMBERID,GROUPID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.GROUPMEMBERS model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.GROUPMEMBERS model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int MEMBERID,int GROUPID)
		{
			
			return dal.Delete(MEMBERID,GROUPID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.GROUPMEMBERS GetModel(int MEMBERID,int GROUPID)
		{
			
			return dal.GetModel(MEMBERID,GROUPID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.GROUPMEMBERS GetModelByCache(int MEMBERID,int GROUPID)
		{
			
			string CacheKey = "GROUPMEMBERSModel-" + MEMBERID+GROUPID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(MEMBERID,GROUPID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.GROUPMEMBERS)objModel;
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
		public List<ZK.Model.GROUPMEMBERS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.GROUPMEMBERS> DataTableToList(DataTable dt)
		{
			List<ZK.Model.GROUPMEMBERS> modelList = new List<ZK.Model.GROUPMEMBERS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.GROUPMEMBERS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.GROUPMEMBERS();
					if(dt.Rows[n]["MEMBERID"]!=null && dt.Rows[n]["MEMBERID"].ToString()!="")
					{
						model.MEMBERID=int.Parse(dt.Rows[n]["MEMBERID"].ToString());
					}
					if(dt.Rows[n]["GROUPID"]!=null && dt.Rows[n]["GROUPID"].ToString()!="")
					{
						model.GROUPID=int.Parse(dt.Rows[n]["GROUPID"].ToString());
					}
					if(dt.Rows[n]["ISMANAGER"]!=null && dt.Rows[n]["ISMANAGER"].ToString()!="")
					{
						model.ISMANAGER=int.Parse(dt.Rows[n]["ISMANAGER"].ToString());
					}
					if(dt.Rows[n]["LASTRECVMSGSID"]!=null && dt.Rows[n]["LASTRECVMSGSID"].ToString()!="")
					{
						model.LASTRECVMSGSID=long.Parse(dt.Rows[n]["LASTRECVMSGSID"].ToString());
					}
					if(dt.Rows[n]["MSGHINTSETTING"]!=null && dt.Rows[n]["MSGHINTSETTING"].ToString()!="")
					{
						model.MSGHINTSETTING=int.Parse(dt.Rows[n]["MSGHINTSETTING"].ToString());
					}
					if(dt.Rows[n]["MODIFYCARDBYMNG"]!=null && dt.Rows[n]["MODIFYCARDBYMNG"].ToString()!="")
					{
						model.MODIFYCARDBYMNG=int.Parse(dt.Rows[n]["MODIFYCARDBYMNG"].ToString());
					}
					if(dt.Rows[n]["CARD_NAME"]!=null && dt.Rows[n]["CARD_NAME"].ToString()!="")
					{
					model.CARD_NAME=dt.Rows[n]["CARD_NAME"].ToString();
					}
					if(dt.Rows[n]["CARD_SEX"]!=null && dt.Rows[n]["CARD_SEX"].ToString()!="")
					{
						model.CARD_SEX=int.Parse(dt.Rows[n]["CARD_SEX"].ToString());
					}
					if(dt.Rows[n]["CARD_NUMBER"]!=null && dt.Rows[n]["CARD_NUMBER"].ToString()!="")
					{
					model.CARD_NUMBER=dt.Rows[n]["CARD_NUMBER"].ToString();
					}
					if(dt.Rows[n]["CARD_EMAIL"]!=null && dt.Rows[n]["CARD_EMAIL"].ToString()!="")
					{
					model.CARD_EMAIL=dt.Rows[n]["CARD_EMAIL"].ToString();
					}
					if(dt.Rows[n]["CARD_REMARK"]!=null && dt.Rows[n]["CARD_REMARK"].ToString()!="")
					{
					model.CARD_REMARK=dt.Rows[n]["CARD_REMARK"].ToString();
					}
					if(dt.Rows[n]["JOINTIME"]!=null && dt.Rows[n]["JOINTIME"].ToString()!="")
					{
						model.JOINTIME=DateTime.Parse(dt.Rows[n]["JOINTIME"].ToString());
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

