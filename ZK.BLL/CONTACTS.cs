/**  智客知识管理平台。
* CONTACTS.cs
*
* 功 能： N/A
* 类 名： CONTACTS
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
	/// CONTACTS
	/// </summary>
	public partial class CONTACTS
	{
		private readonly ICONTACTS dal=DataAccess.CreateCONTACTS();
		public CONTACTS()
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
		public bool Exists(int CONTACTID,int USERID)
		{
			return dal.Exists(CONTACTID,USERID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.CONTACTS model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.CONTACTS model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int CONTACTID,int USERID)
		{
			
			return dal.Delete(CONTACTID,USERID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.CONTACTS GetModel(int CONTACTID,int USERID)
		{
			
			return dal.GetModel(CONTACTID,USERID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.CONTACTS GetModelByCache(int CONTACTID,int USERID)
		{
			
			string CacheKey = "CONTACTSModel-" + CONTACTID+USERID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CONTACTID,USERID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.CONTACTS)objModel;
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
		public List<ZK.Model.CONTACTS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.CONTACTS> DataTableToList(DataTable dt)
		{
			List<ZK.Model.CONTACTS> modelList = new List<ZK.Model.CONTACTS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.CONTACTS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.CONTACTS();
					if(dt.Rows[n]["CONTACTID"]!=null && dt.Rows[n]["CONTACTID"].ToString()!="")
					{
						model.CONTACTID=int.Parse(dt.Rows[n]["CONTACTID"].ToString());
					}
					if(dt.Rows[n]["USERID"]!=null && dt.Rows[n]["USERID"].ToString()!="")
					{
						model.USERID=int.Parse(dt.Rows[n]["USERID"].ToString());
					}
					if(dt.Rows[n]["CONTACTGROUPID"]!=null && dt.Rows[n]["CONTACTGROUPID"].ToString()!="")
					{
						model.CONTACTGROUPID=int.Parse(dt.Rows[n]["CONTACTGROUPID"].ToString());
					}
					if(dt.Rows[n]["REMARKNAME"]!=null && dt.Rows[n]["REMARKNAME"].ToString()!="")
					{
					model.REMARKNAME=dt.Rows[n]["REMARKNAME"].ToString();
					}
					if(dt.Rows[n]["HIDEWHENONLINE"]!=null && dt.Rows[n]["HIDEWHENONLINE"].ToString()!="")
					{
						model.HIDEWHENONLINE=int.Parse(dt.Rows[n]["HIDEWHENONLINE"].ToString());
					}
					if(dt.Rows[n]["ONLINEWHENHIDE"]!=null && dt.Rows[n]["ONLINEWHENHIDE"].ToString()!="")
					{
						model.ONLINEWHENHIDE=int.Parse(dt.Rows[n]["ONLINEWHENHIDE"].ToString());
					}
					if(dt.Rows[n]["REMINDMEWHENLOGIN"]!=null && dt.Rows[n]["REMINDMEWHENLOGIN"].ToString()!="")
					{
						model.REMINDMEWHENLOGIN=int.Parse(dt.Rows[n]["REMINDMEWHENLOGIN"].ToString());
					}
					if(dt.Rows[n]["NOMESSAGE"]!=null && dt.Rows[n]["NOMESSAGE"].ToString()!="")
					{
						model.NOMESSAGE=int.Parse(dt.Rows[n]["NOMESSAGE"].ToString());
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

