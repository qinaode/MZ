/**  智客知识管理平台。
* ZK_Lesson.cs
*
* 功 能： N/A
* 类 名： ZK_Lesson
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:48   N/A    初版
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
	public partial class ZK_Lesson
	{
		private readonly IZK_Lesson dal=DataAccess.CreateZK_Lesson();
		public ZK_Lesson()
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
		public bool Exists(int lessonID)
		{
			return dal.Exists(lessonID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZK.Model.ZK_Lesson model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.ZK_Lesson model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int lessonID)
		{
			
			return dal.Delete(lessonID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string lessonIDlist )
		{
			return dal.DeleteList(lessonIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.ZK_Lesson GetModel(int lessonID)
		{
			
			return dal.GetModel(lessonID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.ZK_Lesson GetModelByCache(int lessonID)
		{
			
			string CacheKey = "ZK_LessonModel-" + lessonID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(lessonID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.ZK_Lesson)objModel;
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
		public List<ZK.Model.ZK_Lesson> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.ZK_Lesson> DataTableToList(DataTable dt)
		{
			List<ZK.Model.ZK_Lesson> modelList = new List<ZK.Model.ZK_Lesson>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.ZK_Lesson model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.ZK_Lesson();
					if(dt.Rows[n]["lessonID"]!=null && dt.Rows[n]["lessonID"].ToString()!="")
					{
						model.lessonID=int.Parse(dt.Rows[n]["lessonID"].ToString());
					}
					if(dt.Rows[n]["lessonName"]!=null && dt.Rows[n]["lessonName"].ToString()!="")
					{
					model.lessonName=dt.Rows[n]["lessonName"].ToString();
					}
					if(dt.Rows[n]["lessonDesc"]!=null && dt.Rows[n]["lessonDesc"].ToString()!="")
					{
					model.lessonDesc=dt.Rows[n]["lessonDesc"].ToString();
					}
					if(dt.Rows[n]["lessonParent"]!=null && dt.Rows[n]["lessonParent"].ToString()!="")
					{
						model.lessonParent=int.Parse(dt.Rows[n]["lessonParent"].ToString());
					}
					if(dt.Rows[n]["lessonLevel"]!=null && dt.Rows[n]["lessonLevel"].ToString()!="")
					{
						model.lessonLevel=int.Parse(dt.Rows[n]["lessonLevel"].ToString());
					}
					if(dt.Rows[n]["classID"]!=null && dt.Rows[n]["classID"].ToString()!="")
					{
						model.classID=int.Parse(dt.Rows[n]["classID"].ToString());
					}
					if(dt.Rows[n]["teachMB"]!=null && dt.Rows[n]["teachMB"].ToString()!="")
					{
					model.teachMB=dt.Rows[n]["teachMB"].ToString();
					}
					if(dt.Rows[n]["teachND"]!=null && dt.Rows[n]["teachND"].ToString()!="")
					{
					model.teachND=dt.Rows[n]["teachND"].ToString();
					}
					if(dt.Rows[n]["teachZD"]!=null && dt.Rows[n]["teachZD"].ToString()!="")
					{
					model.teachZD=dt.Rows[n]["teachZD"].ToString();
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

